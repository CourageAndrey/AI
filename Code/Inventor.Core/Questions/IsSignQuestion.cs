﻿using System;
using System.Collections.Generic;

using Inventor.Core.Attributes;
using Inventor.Core.Localization;
using Inventor.Core.Statements;

namespace Inventor.Core.Questions
{
	public class IsSignQuestion : Question
	{
		#region Properties

		public IConcept Concept
		{ get; }

		#endregion

		public IsSignQuestion(IConcept concept, IEnumerable<IStatement> preconditions = null)
			: base(preconditions)
		{
			if (concept == null) throw new ArgumentNullException(nameof(concept));

			Concept = concept;
		}

		public override IAnswer Process(IQuestionProcessingContext context)
		{
			return context
				.From<IsSignQuestion, HasSignStatement>()
				.Where(s => s.Sign == Concept)
				.SelectBoolean(
					statement => Concept.HasAttribute<IsSignAttribute>(),
					language => language.Answers.SignTrue,
					language => language.Answers.SignFalse,
					new Dictionary<String, INamed>
					{
						{ Strings.ParamConcept, Concept },
					})
				.Answer;
		}
	}
}
