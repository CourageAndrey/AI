﻿using NUnit.Framework;

using Inventor.Semantics.Concepts;
using Inventor.Semantics.Localization;
using Inventor.Semantics.Modules.Classification.Statements;
using Inventor.Semantics.Statements;
using Inventor.Semantics.Mathematics;
using Inventor.Semantics.Processes;
using Inventor.Semantics.Set;
using Inventor.Semantics.Test.Sample;

namespace Inventor.Semantics.Test
{
	[TestFixture]
	public class SemanticNetworkTest
	{
		[OneTimeSetUp]
		public void InitializeModules()
		{
			new Modules.Boolean.BooleanModule().RegisterMetadata();
			new Modules.Classification.ClassificationModule().RegisterMetadata();
			new MathematicsModule().RegisterMetadata();
			new ProcessesModule().RegisterMetadata();
			new SetModule().RegisterMetadata();
		}

		[Test]
		public void GivenConsistentSemanticNetworkWhenCheckConsistensyThenReturnEmptyText()
		{
			// arrange
			var language = Language.Default;
			var semanticNetwork = new TestSemanticNetwork(language).SemanticNetwork;

			// act
			var result = semanticNetwork.CheckConsistency();

			// assert
			Assert.IsTrue(result.ToString().Contains(language.Statements.Consistency.CheckOk));
		}

		[Test]
		public void GivenDuplicatedStatementWhenCheckConsistensyThenReturnDuplication()
		{
			// arrange
			var language = Language.Default;
			var semanticNetwork = new SemanticNetwork(language);

			IConcept concept1, concept2;
			semanticNetwork.Concepts.Add(concept1 = 1.CreateConcept());
			semanticNetwork.Concepts.Add(concept2 = 2.CreateConcept());

			semanticNetwork.DeclareThat(concept1).IsAncestorOf(concept2);
			semanticNetwork.DeclareThat(concept1).IsAncestorOf(concept2);

			// act
			var result = semanticNetwork.CheckConsistency().ToString();

			// assert
			Assert.Less(language.Statements.Consistency.ErrorDuplicate.Length, result.Length);
		}

		[Test]
		public void DescribeRulesEnumeratesThem()
		{
			// arrange
			var language = Language.Default;
			var semanticNetwork = new TestSemanticNetwork(language).SemanticNetwork;

			// act
			var result = semanticNetwork.DescribeRules().ToString();

			// assert
			Assert.Less(4000, result.Length);
		}
	}
}