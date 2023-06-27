﻿using System;

using NUnit.Framework;

using AabSemantics.Modules.Boolean.Concepts;
using AabSemantics.Modules.Mathematics.Concepts;

namespace AabSemantics.Modules.Mathematics.Tests.Concepts
{
	[TestFixture]
	public class ComparisonSignsTest
	{
		[Test]
		public void OnlyComparisonSignsSuit()
		{
			foreach (var concept in LogicalValues.All)
			{
				Assert.Throws<InvalidOperationException>(() => { concept.Contradicts(ComparisonSigns.IsEqualTo); });
				Assert.Throws<InvalidOperationException>(() => { ComparisonSigns.IsEqualTo.Contradicts(concept); });

				Assert.Throws<InvalidOperationException>(() => { ComparisonSigns.Revert(concept); });

				Assert.Throws<InvalidOperationException>(() => { concept.CanBeReverted(); });

				Assert.Throws<InvalidOperationException>(() => { ComparisonSigns.CompareThreeValues(concept, ComparisonSigns.IsEqualTo); });
				Assert.Throws<InvalidOperationException>(() => { ComparisonSigns.CompareThreeValues(ComparisonSigns.IsEqualTo, concept); });
			}
		}

		[Test]
		public void DoubleReversionDoNothing()
		{
			foreach (var sign in ComparisonSigns.All)
			{
				Assert.AreSame(sign, ComparisonSigns.Revert(ComparisonSigns.Revert(sign)));
			}
		}
	}
}
