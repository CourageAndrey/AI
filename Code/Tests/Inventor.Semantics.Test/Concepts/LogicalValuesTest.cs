﻿using System;

using NUnit.Framework;

using Inventor.Semantics.Modules.Boolean.Concepts;

namespace Inventor.Semantics.Test.Concepts
{
	[TestFixture]
	public class LogicalValuesTest
	{
		[Test]
		public void OnlyLogicalValuesSuit()
		{
			foreach (var concept in SystemConcepts.GetAll())
			{
				if (!LogicalValues.All.Contains(concept))
				{
					Assert.Throws<InvalidOperationException>(() => { concept.ToBoolean(); });
					Assert.Throws<InvalidOperationException>(() => { concept.Invert(); });
				}
			}
		}

		[Test]
		public void DoubleInversionDoNothing()
		{
			foreach (var value in LogicalValues.All)
			{
				Assert.AreSame(value, value.Invert().Invert());
			}
		}

		[Test]
		public void ConversionToBooleanAndBackDoNothing()
		{
			foreach (var value in LogicalValues.All)
			{
				Assert.AreSame(value, value.ToBoolean().ToLogicalValue());
			}

			foreach (var value in new[] { true, false })
			{
				Assert.AreEqual(value, value.ToLogicalValue().ToBoolean());
			}
		}
	}
}
