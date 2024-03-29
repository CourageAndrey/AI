﻿using NUnit.Framework;

using AabSemantics.Concepts;
using AabSemantics.Modules.Boolean.Attributes;

namespace AabSemantics.Tests.Concepts
{
	[TestFixture]
	public class AttributesExtensionTest
	{
		[Test]
		public void GivenConcept_WhenAddOrRemoveAttributes_ThenConceptAttributesChange()
		{
			var concept = new Concept();

			// 0. no attributes added
			Assert.AreEqual(0, concept.Attributes.Count);
			Assert.IsFalse(concept.HasAttribute<IsValueAttribute>());

			// 1. add IsValueAttribute
			concept.WithAttribute(IsValueAttribute.Value);
			Assert.AreEqual(1, concept.Attributes.Count);
			Assert.IsTrue(concept.HasAttribute<IsValueAttribute>());

			// 2. remove all attributes
			concept.WithoutAttributes();
			Assert.AreEqual(0, concept.Attributes.Count);
			Assert.IsFalse(concept.HasAttribute<IsValueAttribute>());
		}
	}
}
