﻿using System;
using System.Collections.Generic;
using System.Linq;

using Inventor.Core.Answers;
using Inventor.Core.Base;
using Inventor.Core.Localization;
using Inventor.Core.Statements;

namespace Inventor.Core.Questions
{
	public sealed class HasSignsQuestion : Question<HasSignsQuestion, HasSignStatement>
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

		protected override IAnswer CreateAnswer(IQuestionProcessingContext<HasSignsQuestion> context, ICollection<HasSignStatement> statements)
		{
			return new BooleanAnswer(
				statements.Any(),
				new FormattedText(
					() => String.Format(statements.Any() ? context.Language.Answers.HasSignsTrue : context.Language.Answers.HasSignsFalse, Recursive ? context.Language.Answers.RecursiveTrue : context.Language.Answers.RecursiveFalse),
					new Dictionary<String, INamed>
					{
						{ Strings.ParamConcept, Concept },
					}),
				new Explanation(statements));
		}

		protected override Boolean DoesStatementMatch(HasSignStatement statement)
		{
			return statement.Concept == Concept;
		}

		protected override Boolean NeedToCheckTransitives(ICollection<HasSignStatement> statements)
		{
			return Recursive;
		}

		protected override IEnumerable<NestedQuestion> GetNestedQuestions(IQuestionProcessingContext<HasSignsQuestion> context)
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

		protected override IAnswer ProcessChildAnswers(IQuestionProcessingContext<HasSignsQuestion> questionProcessingContext, ICollection<HasSignStatement> statements, ICollection<ChildAnswer> childAnswers)
		{
			var resultStatements = new List<HasSignStatement>(statements);
			var additionalStatements = new List<IStatement>();

			foreach (var childAnswer in childAnswers)
			{
				if (((BooleanAnswer) childAnswer.Answer).Result)
				{
					var answerStatements = childAnswer.Answer.Explanation.Statements.OfType<HasSignStatement>().ToList();
					resultStatements.AddRange(answerStatements);
					additionalStatements.AddRange(childAnswer.Answer.Explanation.Statements.Except(answerStatements));
					additionalStatements.AddRange(childAnswer.TransitiveStatements);
				}
			}

			var result = CreateAnswer(questionProcessingContext, resultStatements);
			result.Explanation.Expand(additionalStatements);
			return result;
		}
	}
}
