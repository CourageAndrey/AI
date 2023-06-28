﻿using System.Linq;

using NUnit.Framework;

using AabSemantics.Answers;
using AabSemantics.Concepts;
using AabSemantics.Localization;
using AabSemantics.Modules.Boolean.Concepts;
using AabSemantics.Modules.Classification.Statements;
using AabSemantics.Modules.Set.Attributes;
using AabSemantics.Modules.Set.Questions;
using AabSemantics.Modules.Set.Statements;
using AabSemantics.Questions;
using AabSemantics.Statements;
using AabSemantics.Test.Sample;

namespace AabSemantics.Modules.Set.Tests.Questions
{
	[TestFixture]
	public class EnumerateSignsQuestionTest
	{
		[Test]
		public void GivenNoSigns_WhenBeingAsked_ThenReturnEmpty()
		{
			// arrange
			var language = Language.Default;
			var semanticNetwork = new TestSemanticNetwork(language).SemanticNetwork;

			var questionWithoutRecursion = new EnumerateSignsQuestion(LogicalValues.True, false);

			// act
			var answerWithoutRecursion = questionWithoutRecursion.Ask(semanticNetwork.Context);
			var answerWithRecursion = semanticNetwork.Ask().WhichSignsHas(LogicalValues.True);

			// assert
			Assert.IsTrue(answerWithoutRecursion.IsEmpty);
			Assert.AreEqual(0, answerWithoutRecursion.Explanation.Statements.Count);

			Assert.IsTrue(answerWithRecursion.IsEmpty);
			Assert.AreEqual(0, answerWithRecursion.Explanation.Statements.Count);
		}

		[Test]
		public void GivenParentSignsButNotRecursive_WhenBeingAsked_ThenReturnEmpty()
		{
			// arrange
			var language = Language.Default;
			var semanticNetwork = new TestSemanticNetwork(language);

			var question = new EnumerateSignsQuestion(semanticNetwork.Vehicle_Motorcycle, false);

			// act
			var answer = question.Ask(semanticNetwork.SemanticNetwork.Context);

			// assert
			Assert.IsTrue(answer.IsEmpty);
			Assert.AreEqual(0, answer.Explanation.Statements.Count);
		}

		[Test]
		public void GivenNoRecursion_WhenBeingAsked_ThenReturnOwnSignsOnly()
		{
			// arrange
			var language = Language.Default;
			var semanticNetwork = new TestSemanticNetwork(language);

			var ownSign = ConceptCreationHelper.CreateConcept();
			ownSign.WithAttribute(IsSignAttribute.Value);
			semanticNetwork.SemanticNetwork.Concepts.Add(ownSign);

			var ownSignStatement = semanticNetwork.SemanticNetwork.DeclareThat(ownSign).IsSignOf(semanticNetwork.Vehicle_Motorcycle);

			var question = new EnumerateSignsQuestion(semanticNetwork.Vehicle_Motorcycle, false);

			// act
			var answer = (ConceptsAnswer) question.Ask(semanticNetwork.SemanticNetwork.Context);

			// assert
			Assert.IsFalse(answer.IsEmpty);
			Assert.AreSame(ownSign, answer.Result.Single());
			Assert.AreSame(ownSignStatement, answer.Explanation.Statements.Single());
		}

		[Test]
		public void GivenRecursive_WhenBeingAsked_ThenReturnParentSignsOnly()
		{
			// arrange
			var language = Language.Default;
			var semanticNetwork = new TestSemanticNetwork(language);

			var question = new EnumerateSignsQuestion(semanticNetwork.Vehicle_Motorcycle, true);

			// act
			var answer = (ConceptsAnswer) question.Ask(semanticNetwork.SemanticNetwork.Context);

			// assert
			Assert.IsFalse(answer.IsEmpty);
			Assert.AreEqual(2, answer.Result.Count);
			Assert.AreEqual(1, answer.Explanation.Statements.OfType<IsStatement>().Count());
			Assert.AreEqual(2, answer.Explanation.Statements.OfType<HasSignStatement>().Count());
		}

		[Test]
		public void GivenRecursive_WhenBeingAsked_ThenReturnAllSigns()
		{
			// arrange
			var language = Language.Default;
			var semanticNetwork = new TestSemanticNetwork(language);

			var ownSign = ConceptCreationHelper.CreateConcept();
			ownSign.WithAttribute(IsSignAttribute.Value);
			semanticNetwork.SemanticNetwork.Concepts.Add(ownSign);

			var ownSignStatement = semanticNetwork.SemanticNetwork.DeclareThat(ownSign).IsSignOf(semanticNetwork.Vehicle_Motorcycle);

			// act
			var answer = (ConceptsAnswer) semanticNetwork.SemanticNetwork.Ask().WhichSignsHas(semanticNetwork.Vehicle_Motorcycle);

			// assert
			Assert.IsFalse(answer.IsEmpty);
			Assert.AreEqual(3, answer.Result.Count);
			Assert.IsTrue(answer.Result.Contains(ownSign));
			Assert.IsTrue(answer.Explanation.Statements.Contains(ownSignStatement));
			Assert.AreEqual(1, answer.Explanation.Statements.OfType<IsStatement>().Count());
			Assert.AreEqual(3, answer.Explanation.Statements.OfType<HasSignStatement>().Count());
		}
	}
}