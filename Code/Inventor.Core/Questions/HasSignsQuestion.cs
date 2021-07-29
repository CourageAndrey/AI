﻿using System;
using System.Collections.Generic;
using System.Linq;

using Inventor.Core.Answers;
using Inventor.Core.Base;
using Inventor.Core.Localization;
using Inventor.Core.Statements;

namespace Inventor.Core.Questions
{
	public sealed class HasSignsQuestion : Question
	{
		#region Properties

		public IConcept Concept
		{ get; }

		public Boolean Recursive
		{ get; }

		#endregion

		public HasSignsQuestion(IConcept concept, Boolean recursive, IEnumerable<IStatement> preconditions = null)
			: base(preconditions)
		{
			if (concept == null) throw new ArgumentNullException(nameof(concept));

			Concept = concept;
			Recursive = recursive;
		}

		public override IAnswer Process(IQuestionProcessingContext context)
		{
			return context
				.From<HasSignsQuestion, HasSignStatement>(DoesStatementMatch)
				.ProcessTransitives(NeedToCheckTransitives, GetNestedQuestions)
				.Select(CreateAnswer);
		}

		private IAnswer CreateAnswer(IQuestionProcessingContext<HasSignsQuestion> context, ICollection<HasSignStatement> statements, ICollection<ChildAnswer> childAnswers)
		{
			Boolean result = false;
			var explanation = new List<IStatement>(statements);

			if (statements.Count > 0)
			{
				result = true;
			}

			foreach (var childAnswer in childAnswers)
			{
				if (((BooleanAnswer) childAnswer.Answer).Result)
				{
					result = true;
					explanation.AddRange(childAnswer.Answer.Explanation.Statements);
					explanation.AddRange(childAnswer.TransitiveStatements);
				}
			}

			return new BooleanAnswer(
				result,
				new FormattedText(
					() => String.Format(statements.Any() ? context.Language.Answers.HasSignsTrue : context.Language.Answers.HasSignsFalse, Recursive ? context.Language.Answers.RecursiveTrue : context.Language.Answers.RecursiveFalse),
					new Dictionary<String, INamed>
					{
						{ Strings.ParamConcept, Concept },
					}),
				new Explanation(explanation));
		}

		private Boolean DoesStatementMatch(HasSignStatement statement)
		{
			return statement.Concept == Concept;
		}

		private Boolean NeedToCheckTransitives(ICollection<HasSignStatement> statements)
		{
			return Recursive;
		}

		private IEnumerable<NestedQuestion> GetNestedQuestions(IQuestionProcessingContext<HasSignsQuestion> context)
		{
			if (!Recursive) yield break;

			var alreadyViewedConcepts = new HashSet<IConcept>(context.ActiveContexts.OfType<IQuestionProcessingContext<HasSignsQuestion>>().Select(questionContext => questionContext.Question.Concept));

			var question = context.Question;
			var transitiveStatements = context.KnowledgeBase.Statements.Enumerate<IsStatement>(context.ActiveContexts).Where(isStatement => isStatement.Child == question.Concept);

			foreach (var transitiveStatement in transitiveStatements)
			{
				var parent = transitiveStatement.Parent;
				if (!alreadyViewedConcepts.Contains(parent))
				{
					yield return new NestedQuestion(new HasSignsQuestion(parent, true), new IStatement[] { transitiveStatement });
				}
			}
		}
	}
}
