﻿using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using AabSemantics.Localization;
using AabSemantics.Modules.Boolean.Questions;
using AabSemantics.Modules.Classification.Questions;
using AabSemantics.Modules.Set.Tests;
using AabSemantics.Serialization;
using AabSemantics.TestCore;

namespace AabSemantics.Tests.Serialization
{
	[TestFixture]
	public class QuestionsSerializationTest
	{
		private static readonly ILanguage _language;
		private static readonly ISemanticNetwork _semanticNetwork;
		private static readonly ConceptIdResolver _conceptIdResolver;
		private static readonly StatementIdResolver _statementIdResolver;

		static QuestionsSerializationTest()
		{
			_language = Language.Default;

			_semanticNetwork = new SemanticNetwork(_language);
			_semanticNetwork.CreateSetTestData();

			_conceptIdResolver = new ConceptIdResolver(_semanticNetwork.Concepts.ToDictionary(
				concept => concept.ID,
				concept => concept));
			_statementIdResolver = new StatementIdResolver(_semanticNetwork);
		}

		[Test]
		[TestCaseSource(nameof(CreateQuestions))]
		public void GivenDifferentQuestions_WhenSerializeToJson_ThenSucceed(IQuestion question)
		{
			question.CheckJsonSerialization(_conceptIdResolver, _statementIdResolver);
		}

		[Test]
		[TestCaseSource(nameof(CreateQuestions))]
		public void GivenDifferentQuestions_WhenSerializeToXml_ThenSucceed(IQuestion question)
		{
			question.CheckXmlSerialization(_conceptIdResolver, _statementIdResolver);
		}

		public static IEnumerable<IQuestion> CreateQuestions()
		{
			var testStatement = _semanticNetwork.Statements.First();
			var testConcept1 = _semanticNetwork.Concepts.First();
			var testConcept2 = _semanticNetwork.Concepts.Last();

			yield return new CheckStatementQuestion(testStatement, _semanticNetwork.Statements.Except(new[] { testStatement }));
			yield return new EnumerateAncestorsQuestion(testConcept1);
			yield return new EnumerateDescendantsQuestion(testConcept1);
			yield return new IsQuestion(testConcept1, testConcept2);
		}
	}
}
