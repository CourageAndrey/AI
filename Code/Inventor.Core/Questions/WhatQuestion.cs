﻿using System;

namespace Inventor.Core.Questions
{
	public sealed class WhatQuestion : IQuestion
	{
		public IConcept Concept
		{ get; }

		public WhatQuestion(IConcept concept)
		{
			if (concept == null) throw new ArgumentNullException(nameof(concept));

			Concept = concept;
		}
	}
}
