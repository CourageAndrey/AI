﻿using System;
using System.Linq;

using AabSemantics.Answers;
using AabSemantics.Questions;
using AabSemantics.Text.Primitives;

namespace AabSemantics.Sample04.CustomQuestion
{
	public class SelectRandomConceptQuestion : Question
	{
		public override IAnswer Process(IQuestionProcessingContext context)
		{
			var random = new Random(DateTime.Now.Millisecond);
			var allConcepts = context.SemanticNetwork.Concepts.ToList();
			var resultConcept = allConcepts[random.Next(allConcepts.Count)];

			return new ConceptAnswer(
				resultConcept,
				new FormattedText(language => resultConcept.Name.GetValue(language)),
				new Explanation(Array.Empty<IStatement>()));
		}
	}

	public static class SubjectQuestionExtensions
	{
		public static IAnswer ForRandomConcept(this QuestionBuilder builder)
		{
			var question = new SelectRandomConceptQuestion();
			return question.Ask(builder.SemanticNetwork.Context);
		}
	}
}
