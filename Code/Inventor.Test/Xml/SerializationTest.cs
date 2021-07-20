﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

using NUnit.Framework;

using Inventor.Core;
using Inventor.Core.Base;
using Inventor.Core.Localization;

namespace Inventor.Test.Xml
{
	public class SerializationTest
	{
		[Test]
		public void CheckLargeKnowledgeBaseSerialization()
		{
			// arrange
			var language = Language.Default;
			var knowledgeBase = new TestKnowledgeBase(language).KnowledgeBase;
			var name = (LocalizedStringVariable)knowledgeBase.Name;
			var locales = name.Locales;
			string testFileName = Path.GetTempFileName();

			// act
			KnowledgeBase restored;
			try
			{
				knowledgeBase.Save(testFileName);
				restored = KnowledgeBase.Load(testFileName, language);
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
			Assert.AreEqual(knowledgeBase.Concepts.Count, restored.Concepts.Count);
			foreach (var concept in knowledgeBase.Concepts/*.Except(systemConcepts)*/)
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

			Assert.AreEqual(knowledgeBase.Statements.Count, restored.Statements.Count);
			foreach (var statement in knowledgeBase.Statements)
			{
				var statementType = statement.GetType();
				var childConcepts = statement.GetChildConcepts().Select(c => conceptMapping[c]).ToList();

				// Note: check method Single() below means, that test knowledge base can not contain statement duplicates.
				restored.Statements.Single(s => statementType == s.GetType() && childConcepts.SequenceEqual(s.GetChildConcepts()));
			}
		}
	}
}
