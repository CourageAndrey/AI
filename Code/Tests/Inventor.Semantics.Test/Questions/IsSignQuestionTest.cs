﻿using System.Linq;

using NUnit.Framework;

using Inventor.Semantics.Answers;
using Inventor.Semantics.Concepts;
using Inventor.Semantics.Localization;
using Inventor.Semantics.Modules.Set.Attributes;
using Inventor.Semantics.Modules.Set.Questions;
using Inventor.Semantics.Modules.Set.Statements;
using Inventor.Semantics.Questions;
using Inventor.Semantics.Test.Sample;

namespace Inventor.Semantics.Test.Questions
{
	[TestFixture]
	public class IsSignQuestionTest
	{
		[Test]
		public void ReturnFalseIfNoAttribute()
		{
			// arrange
			var language = Language.Default;
			var semanticNetwork = new TestSemanticNetwork(language).SemanticNetwork;

			var concept = ConceptCreationHelper.CreateConcept();
			semanticNetwork.Concepts.Add(concept);

			// act
			var answer = semanticNetwork.Ask().IfIsSign(concept);

			// assert
			Assert.IsFalse(answer.IsEmpty);
			Assert.IsFalse(((BooleanAnswer) answer).Result);
			Assert.AreEqual(0, answer.Explanation.Statements.Count);
		}

		[Test]
		public void ReturnTrueWithoutExplanationIfNoRelationships()
		{
			// arrange
			var language = Language.Default;
			var semanticNetwork = new TestSemanticNetwork(language).SemanticNetwork;

			var concept = ConceptCreationHelper.CreateConcept();
			concept.WithAttribute(IsSignAttribute.Value);
			semanticNetwork.Concepts.Add(concept);

			// act
			var answer = semanticNetwork.Ask().IfIsSign(concept);

			// assert
			Assert.IsFalse(answer.IsEmpty);
			Assert.IsTrue(((BooleanAnswer) answer).Result);
			Assert.AreEqual(0, answer.Explanation.Statements.Count);
		}

		[Test]
		public void ReturnTrueWithtExplanation()
		{
			// arrange
			var language = Language.Default;
			var semanticNetwork = new TestSemanticNetwork(language);

			// act
			var answer = semanticNetwork.SemanticNetwork.Ask().IfIsSign(semanticNetwork.Sign_AreaType);

			// assert
			Assert.IsFalse(answer.IsEmpty);
			Assert.IsTrue(((BooleanAnswer) answer).Result);
			Assert.IsTrue(answer.Explanation.Statements.OfType<HasSignStatement>().Any());
		}
	}
}
