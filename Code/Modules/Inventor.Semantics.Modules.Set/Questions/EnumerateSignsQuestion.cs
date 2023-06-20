﻿using System.Collections.Generic;

using Inventor.Semantics.Modules.Set.Localization;
using Inventor.Semantics.Modules.Set.Statements;
using Inventor.Semantics.Questions;
using Inventor.Semantics.Utils;

namespace Inventor.Semantics.Modules.Set.Questions
{
	public class EnumerateSignsQuestion : Question
	{
		#region Properties

		public IConcept Concept
		{ get; }

		public System.Boolean Recursive
		{ get; }

		#endregion

		public EnumerateSignsQuestion(IConcept concept, System.Boolean recursive, IEnumerable<IStatement> preconditions = null)
			: base(preconditions)
		{
			Concept = concept.EnsureNotNull(nameof(concept));
			Recursive = recursive;
		}

		public override IAnswer Process(IQuestionProcessingContext context)
		{
			return context
				.From<EnumerateSignsQuestion, HasSignStatement>()
				.WithTransitives(
					statements => Recursive,
					question => question.Concept,
					newSubject => new EnumerateSignsQuestion(newSubject, true),
					needToAggregateTransitivesToStatements: true)
				.Where(s => s.Concept == Concept)
				.SelectAllConcepts(
					statement => statement.Sign,
					question => question.Concept,
					Semantics.Localization.Strings.ParamConcept,
					language => language.GetExtension<ILanguageSetModule>().Questions.Answers.ConceptSigns + (Recursive ? language.Questions.Answers.RecursiveTrue : language.Questions.Answers.RecursiveFalse) + ": ");
		}
	}
}
