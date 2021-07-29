﻿using System;
using System.Collections.Generic;
using System.Linq;

using Inventor.Core.Answers;
using Inventor.Core.Base;
using Inventor.Core.Localization;
using Inventor.Core.Statements;

namespace Inventor.Core.Questions
{
	public sealed class EnumeratePartsQuestion : Question<EnumeratePartsQuestion, HasPartStatement>
	{
		#region Properties

		public IConcept Concept
		{ get; }

		#endregion

		public EnumeratePartsQuestion(IConcept concept, IEnumerable<IStatement> preconditions = null)
			: base(preconditions)
		{
			if (concept == null) throw new ArgumentNullException(nameof(concept));

			Concept = concept;
		}

		protected override IAnswer CreateAnswer(IQuestionProcessingContext<EnumeratePartsQuestion> context, ICollection<HasPartStatement> statements, ICollection<ChildAnswer> childAnswers)
		{
			if (!NeedToCheckTransitives(statements))
			{
				if (statements.Any())
				{
					String format;
					var parameters = statements.Select(r => r.Part).ToList().Enumerate(out format);
					parameters.Add(Strings.ParamParent, Concept);
					return new ConceptsAnswer(
						statements.Select(s => s.Part).ToList(),
						new FormattedText(() => context.Language.Answers.EnumerateParts + format + ".", parameters),
						new Explanation(statements));
				}
				else
				{
					return Answer.CreateUnknown(context.Language);
				}
			}
			else
			{
				return ProcessChildAnswers(context, statements, childAnswers);
			}
		}

		protected override Boolean DoesStatementMatch(HasPartStatement statement)
		{
			return statement.Whole == Concept;
		}

		protected override Boolean NeedToCheckTransitives(ICollection<HasPartStatement> statements)
		{
			return statements.Count == 0;
		}

		protected override IAnswer ProcessChildAnswers(IQuestionProcessingContext<EnumeratePartsQuestion> context, ICollection<HasPartStatement> statements, ICollection<ChildAnswer> childAnswers)
		{
			if (childAnswers.Count > 0)
			{
				var answer = childAnswers.First();
				answer.Answer.Explanation.Expand(answer.TransitiveStatements);
				return answer.Answer;
			}
			else
			{
				return Answers.Answer.CreateUnknown(context.Language);
			}
		}
	}
}
