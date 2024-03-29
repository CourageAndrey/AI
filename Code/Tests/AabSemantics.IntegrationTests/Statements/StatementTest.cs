﻿using NUnit.Framework;

using AabSemantics.Localization;

namespace AabSemantics.IntegrationTests.Statements
{
	[TestFixture]
	public class StatementTest
	{
		[Test]
		public void GivenDifferentStatements_WhenToString_ThenResultContainsTypeAndId()
		{
			// arrange
			var semanticNetwork = new SemanticNetwork(Language.Default);
			semanticNetwork.CreateCombinedTestData();

			// act & assert
			foreach (var statement in semanticNetwork.Statements)
			{
				string info = statement.ToString();

				Assert.IsTrue(info.Contains(statement.GetType().Name));
				Assert.IsTrue(info.Contains(statement.ID));
			}
		}

		[Test]
		public void GivenAllDescribes_WhenCall_ThenSucceed()
		{
			// arrange
			var language = Language.Default;
			var semanticNetwork = new SemanticNetwork(Language.Default);
			semanticNetwork.CreateCombinedTestData();

			var render = TextRenders.PlainString;

			// act & assert
			foreach (var statement in semanticNetwork.Statements)
			{
				foreach (var text in new[] { statement.DescribeTrue(), statement.DescribeFalse(), statement.DescribeQuestion() })
				{
					Assert.IsNotNull(text);
					Assert.False(string.IsNullOrEmpty(render.Render(text, language).ToString()));
				}
			}
		}
	}
}
