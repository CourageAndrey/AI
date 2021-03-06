﻿using System;

namespace Inventor.Core.Questions
{
	public sealed class IsValueQuestion : IQuestion
	{
		public IConcept Concept
		{ get; }

		public IsValueQuestion(IConcept concept)
		{
			if (concept == null) throw new ArgumentNullException(nameof(concept));

			Concept = concept;
		}
	}
}
