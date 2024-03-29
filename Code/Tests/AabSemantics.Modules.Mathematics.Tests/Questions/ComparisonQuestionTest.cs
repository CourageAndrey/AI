﻿using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using AabSemantics.Answers;
using AabSemantics.Concepts;
using AabSemantics.Localization;
using AabSemantics.Modules.Boolean.Concepts;
using AabSemantics.Modules.Boolean.Localization;
using AabSemantics.Modules.Classification.Localization;
using AabSemantics.Modules.Mathematics.Concepts;
using AabSemantics.Modules.Mathematics.Localization;
using AabSemantics.Modules.Mathematics.Questions;
using AabSemantics.Modules.Mathematics.Statements;
using AabSemantics.Questions;

namespace AabSemantics.Modules.Mathematics.Tests.Questions
{
	[TestFixture]
	public class ComparisonQuestionTest
	{
		[Test]
		public void GivenNullArguments_WhenTryToCreateQuestion_ThenFail()
		{
			// arrange
			IConcept concept = "test".CreateConceptByName();

			// act && assert
			Assert.Throws<ArgumentNullException>(() => new ComparisonQuestion(null, concept));
			Assert.Throws<ArgumentNullException>(() => new ComparisonQuestion(concept, null));
		}

		[Test]
		public void GivenHowCompared_WhenBeingAsked_ThenBuildAndAskQuestion()
		{
			// arrange
			var language = Language.Default;
			var semanticNetwork = new SemanticNetwork(language).CreateMathematicsTestData();

			// act
			var questionRegular = new ComparisonQuestion(semanticNetwork.Number0, semanticNetwork.Number2);
			var answerRegular = (StatementAnswer) questionRegular.Ask(semanticNetwork.SemanticNetwork.Context);

			var answerBuilder = (StatementAnswer) semanticNetwork.SemanticNetwork.Ask().HowCompared(semanticNetwork.Number0, semanticNetwork.Number2);

			// assert
			Assert.AreEqual(answerRegular.Result, answerBuilder.Result);
			Assert.IsTrue(answerRegular.Explanation.Statements.SequenceEqual(answerBuilder.Explanation.Statements));
		}

		[Test]
		public void GivenNoInformation_WhenBeingAsked_ThenReturnEmpty()
		{
			// arrange
			var textRender = TextRenders.PlainString;

			var language = Language.Default;
			language.Extensions.Add(LanguageBooleanModule.CreateDefault());
			language.Extensions.Add(LanguageClassificationModule.CreateDefault());
			language.Extensions.Add(LanguageMathematicsModule.CreateDefault());

			var semanticNetwork = new SemanticNetwork(language);

			var question = new ComparisonQuestion(1.CreateConceptByObject(), 2.CreateConceptByObject());

			// act
			var answer = question.Ask(semanticNetwork.Context);
			var description = textRender.RenderText(answer.Description, language).ToString();

			// assert
			Assert.IsTrue(answer.IsEmpty);
			Assert.IsTrue(description.Contains(language.Questions.Answers.Unknown));
			Assert.AreEqual(0, answer.Explanation.Statements.Count);
		}

		[Test]
		public void GivenIncomparableValues_WhenBeingAsked_ThenReturnEmpty()
		{
			// arrange
			var language = Language.Default;
			var semanticNetwork = new SemanticNetwork(language).CreateMathematicsTestData();

			// act
			var answer = semanticNetwork.SemanticNetwork.Ask().HowCompared(ComparisonSigns.IsNotEqualTo, LogicalValues.False);

			// assert
			Assert.IsTrue(answer.IsEmpty);
			Assert.AreEqual(0, answer.Explanation.Statements.Count);
		}

		[Test]
		public void GivenEqualConditions_WhenBeingAsked_ThenReturnResult()
		{
			// arrange
			var language = Language.Default;
			var semanticNetwork = new SemanticNetwork(language).CreateMathematicsTestData();

			// act
			var answer1 = semanticNetwork.SemanticNetwork.Ask().HowCompared(semanticNetwork.Number0, semanticNetwork.NumberZero);

			var answer2 = semanticNetwork.SemanticNetwork.Ask().HowCompared(semanticNetwork.NumberZero, semanticNetwork.Number0);

			// assert
			var explanation1 = (ComparisonStatement) answer1.Explanation.Statements.Single();
			var explanation2 = (ComparisonStatement) answer2.Explanation.Statements.Single();
			Assert.AreSame(explanation1, explanation2);
			Assert.IsTrue(explanation1.GetChildConcepts().Contains(semanticNetwork.Number0));
			Assert.IsTrue(explanation1.GetChildConcepts().Contains(semanticNetwork.NumberZero));
			Assert.IsTrue(explanation1.GetChildConcepts().Contains(ComparisonSigns.IsEqualTo));

			var statement1 = (ComparisonStatement) ((StatementAnswer) answer1).Result;
			Assert.AreSame(semanticNetwork.Number0, statement1.LeftValue);
			Assert.AreSame(semanticNetwork.NumberZero, statement1.RightValue);
			Assert.AreSame(ComparisonSigns.IsEqualTo, statement1.ComparisonSign);

			var statement2 = (ComparisonStatement) ((StatementAnswer) answer2).Result;
			Assert.AreSame(semanticNetwork.NumberZero, statement2.LeftValue);
			Assert.AreSame(semanticNetwork.Number0, statement2.RightValue);
			Assert.AreSame(ComparisonSigns.IsEqualTo, statement2.ComparisonSign);
		}

		[Test]
		public void GivenGreaterAndLessConditions_WhenBeingAsked_ThenReturnResult()
		{
			// arrange
			var language = Language.Default;
			var semanticNetwork = new SemanticNetwork(language).CreateMathematicsTestData();

			var orderedNumbersWith0 = new List<IConcept>
			{
				semanticNetwork.Number0,
				semanticNetwork.Number1,
				semanticNetwork.Number2,
				semanticNetwork.Number3,
				semanticNetwork.Number4,
			};

			var orderedNumbersWithZero = new List<IConcept>
			{
				semanticNetwork.NumberZero,
				semanticNetwork.Number1,
				semanticNetwork.Number2,
				semanticNetwork.Number3,
				semanticNetwork.Number4,
			};

			foreach (var orderedNumbers in new[] { orderedNumbersWith0, orderedNumbersWithZero })
			{
				for (int l = 0; l < orderedNumbers.Count; l++)
				{
					for (int r = 0; r < orderedNumbers.Count; r++)
					{
						if (l != r) // because "A=A" automatic precondition is not defined
						{
							// act
							var answer = semanticNetwork.SemanticNetwork.Ask().HowCompared(orderedNumbers[l], orderedNumbers[r]);

							// assert
							var explanation = answer.Explanation.Statements;
							int expectedExplanationStatementsCount = Math.Abs(l - r);
							if (orderedNumbers == orderedNumbersWithZero && (l == 0 || r == 0))
							{
								expectedExplanationStatementsCount++;
							}
							Assert.AreEqual(expectedExplanationStatementsCount, explanation.Count);
							Assert.AreEqual(explanation.Count, explanation.OfType<ComparisonStatement>().Count());

							var statement = (ComparisonStatement)((StatementAnswer)answer).Result;
							Assert.AreSame(orderedNumbers[l], statement.LeftValue);
							Assert.AreSame(orderedNumbers[r], statement.RightValue);
							Assert.AreSame(l > r ? ComparisonSigns.IsGreaterThan : ComparisonSigns.IsLessThan, statement.ComparisonSign);
						}
					}
				}
			}
		}

		[Test]
		public void GivenConditionsWithLeAndGe_WhenBeingAsked_ThenReturnResult()
		{
			// arrange
			var language = Language.Default;
			var semanticNetwork = new SemanticNetwork(language).CreateMathematicsTestData();

			var comparisons = new[]
			{
				new Tuple<IConcept, IConcept, IConcept>(semanticNetwork.Number1or2, semanticNetwork.Number0, ComparisonSigns.IsGreaterThan),
				new Tuple<IConcept, IConcept, IConcept>(semanticNetwork.Number1or2, semanticNetwork.Number1, ComparisonSigns.IsGreaterThanOrEqualTo),
				new Tuple<IConcept, IConcept, IConcept>(semanticNetwork.Number1or2, semanticNetwork.Number2, ComparisonSigns.IsLessThanOrEqualTo),
				new Tuple<IConcept, IConcept, IConcept>(semanticNetwork.Number1or2, semanticNetwork.Number3, ComparisonSigns.IsLessThan),
				new Tuple<IConcept, IConcept, IConcept>(semanticNetwork.Number1or2, semanticNetwork.Number4, ComparisonSigns.IsLessThan),
				new Tuple<IConcept, IConcept, IConcept>(semanticNetwork.Number1or2, semanticNetwork.Number2or3, ComparisonSigns.IsLessThanOrEqualTo),
				new Tuple<IConcept, IConcept, IConcept>(semanticNetwork.Number1or2, semanticNetwork.Number3or4, ComparisonSigns.IsLessThan),
				new Tuple<IConcept, IConcept, IConcept>(semanticNetwork.Number2or3, semanticNetwork.Number0, ComparisonSigns.IsGreaterThan),
				new Tuple<IConcept, IConcept, IConcept>(semanticNetwork.Number2or3, semanticNetwork.Number1, ComparisonSigns.IsGreaterThan),
				new Tuple<IConcept, IConcept, IConcept>(semanticNetwork.Number2or3, semanticNetwork.Number2, ComparisonSigns.IsGreaterThanOrEqualTo),
				new Tuple<IConcept, IConcept, IConcept>(semanticNetwork.Number2or3, semanticNetwork.Number3, ComparisonSigns.IsLessThanOrEqualTo),
				new Tuple<IConcept, IConcept, IConcept>(semanticNetwork.Number2or3, semanticNetwork.Number4, ComparisonSigns.IsLessThan),
				new Tuple<IConcept, IConcept, IConcept>(semanticNetwork.Number2or3, semanticNetwork.Number1or2, ComparisonSigns.IsGreaterThanOrEqualTo),
				new Tuple<IConcept, IConcept, IConcept>(semanticNetwork.Number2or3, semanticNetwork.Number3or4, ComparisonSigns.IsLessThanOrEqualTo),
				new Tuple<IConcept, IConcept, IConcept>(semanticNetwork.Number3or4, semanticNetwork.Number0, ComparisonSigns.IsGreaterThan),
				new Tuple<IConcept, IConcept, IConcept>(semanticNetwork.Number3or4, semanticNetwork.Number1, ComparisonSigns.IsGreaterThan),
				new Tuple<IConcept, IConcept, IConcept>(semanticNetwork.Number3or4, semanticNetwork.Number2, ComparisonSigns.IsGreaterThan),
				new Tuple<IConcept, IConcept, IConcept>(semanticNetwork.Number3or4, semanticNetwork.Number3, ComparisonSigns.IsGreaterThanOrEqualTo),
				new Tuple<IConcept, IConcept, IConcept>(semanticNetwork.Number3or4, semanticNetwork.Number4, ComparisonSigns.IsLessThanOrEqualTo),
				new Tuple<IConcept, IConcept, IConcept>(semanticNetwork.Number3or4, semanticNetwork.Number1or2, ComparisonSigns.IsGreaterThan),
				new Tuple<IConcept, IConcept, IConcept>(semanticNetwork.Number3or4, semanticNetwork.Number2or3, ComparisonSigns.IsGreaterThanOrEqualTo),
			};

			foreach (var comparison in comparisons)
			{
				// act
				var answer1 = semanticNetwork.SemanticNetwork.Ask().HowCompared(comparison.Item1, comparison.Item2);
				var statement1 = (ComparisonStatement) ((StatementAnswer) answer1).Result;

				var answer2 = semanticNetwork.SemanticNetwork.Ask().HowCompared(comparison.Item2, comparison.Item1);
				var statement2 = (ComparisonStatement) ((StatementAnswer) answer2).Result;

				// assert
				Assert.AreSame(comparison.Item1, statement1.LeftValue);
				Assert.AreSame(comparison.Item2, statement1.RightValue);
				Assert.AreSame(comparison.Item3, statement1.ComparisonSign);

				Assert.AreSame(comparison.Item2, statement2.LeftValue);
				Assert.AreSame(comparison.Item1, statement2.RightValue);
				Assert.AreSame(ComparisonSigns.Revert(comparison.Item3), statement2.ComparisonSign);
			}
		}
	}
}
