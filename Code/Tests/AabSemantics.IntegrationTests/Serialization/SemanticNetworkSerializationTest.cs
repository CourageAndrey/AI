﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using NUnit.Framework;

using AabSemantics.Localization;
using AabSemantics.Serialization.Json;
using AabSemantics.Serialization.Xml;

namespace AabSemantics.IntegrationTests.Serialization
{
	[TestFixture]
	public class SemanticNetworkSerializationTest
	{
		[Test]
		public void GivenXml_WhenSerializeAndDeserialize_ThenSucceed()
		{
			CheckSerialization(
				(semanticNetwork, fileName) => semanticNetwork.SaveToXml(fileName),
				(fileName, language) => fileName.LoadSemanticNetworkFromXml(language));
		}

		[Test]
		public void GivenJson_WhenSerializeAndDeserialize_ThenSucceed()
		{
			CheckSerialization(
				(semanticNetwork, fileName) => semanticNetwork.SaveToJson(fileName),
				(fileName, language) => fileName.LoadSemanticNetworkFromJson(language));
		}

		private void CheckSerialization(
			Action<ISemanticNetwork, string> saveToFile,
			Func<string, ILanguage, SemanticNetwork> loadFromFile)
		{
			// arrange
			var language = Language.Default;
			var semanticNetwork = new SemanticNetwork(language);
			semanticNetwork.CreateCombinedTestData();
			var name = (LocalizedStringVariable) semanticNetwork.Name;
			var locales = name.Locales;
			string testFileName = Path.GetTempFileName();

			// act
			SemanticNetwork restored;
			try
			{
				saveToFile(semanticNetwork, testFileName);
				restored = loadFromFile(testFileName, language);
			}
			finally
			{
				if (File.Exists(testFileName))
				{
					File.Delete(testFileName);
				}
			}

			// assert
			var restoredName = (LocalizedStringVariable) restored.Name;
			Assert.IsTrue(locales.SequenceEqual(restoredName.Locales));
			foreach (string locale in locales)
			{
				Assert.AreEqual(name.GetValue(locale), restoredName.GetValue(locale));
			}

			var conceptMapping = new Dictionary<IConcept, IConcept>();
			Assert.AreEqual(semanticNetwork.Concepts.Count, restored.Concepts.Count);
			foreach (var concept in semanticNetwork.Concepts/*.Except(systemConcepts)*/)
			{
				var restoredConcept = restored.Concepts.Single(c =>
					c.Name.GetValue(language) == concept.Name.GetValue(language) &&
					c.Hint.GetValue(language) == concept.Hint.GetValue(language));
				conceptMapping[concept] = restoredConcept;
			}

			var systemConcepts = new HashSet<IConcept>(SystemConcepts.GetAll());
			foreach (var mapping in conceptMapping)
			{
				if (systemConcepts.Contains(mapping.Key))
				{
					Assert.AreSame(mapping.Key, mapping.Value);
				}
				else
				{
					Assert.AreNotSame(mapping.Key, mapping.Value);
				}
			}

			Assert.AreEqual(semanticNetwork.Statements.Count, restored.Statements.Count);
			foreach (var statement in semanticNetwork.Statements)
			{
				var statementType = statement.GetType();
				var childConcepts = statement.GetChildConcepts().Select(c => conceptMapping[c]).ToList();

				// Note: check method Single() below means, that test semantic network can not contain statement duplicates.
				restored.Statements.Single(s => statementType == s.GetType() && childConcepts.SequenceEqual(s.GetChildConcepts()));
			}
		}
	}
}
