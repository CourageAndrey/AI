﻿using System;
using System.Collections.Generic;
using System.Linq;

using Inventor.Core.Answers;
using Inventor.Core.Base;
using Inventor.Core.Localization;
using Inventor.Core.Statements;
using Inventor.Core.Questions;

namespace Inventor.Core.Processors
{
	public sealed class DescribeSubjectAreaProcessor : QuestionProcessor<DescribeSubjectAreaQuestion, GroupStatement>
	{
		protected override IAnswer CreateAnswer(IQuestionProcessingContext<DescribeSubjectAreaQuestion> context, ICollection<GroupStatement> statements)
		{
			if (statements.Any())
			{
				String format;
				var parameters = statements.Select(r => r.Concept).ToList().Enumerate(out format);
				parameters.Add(Strings.ParamAnswer, context.Question.Concept);
				return new ConceptsAnswer(
					statements.Select(s => s.Concept).ToList(),
					new FormattedText(() => context.Language.Answers.SubjectAreaConcepts + format + ".", parameters),
					new Explanation(statements));
			}
			else
			{
				return Answer.CreateUnknown(context.Language);
			}
		}

		protected override Boolean DoesStatementMatch(IQuestionProcessingContext<DescribeSubjectAreaQuestion> context, GroupStatement statement)
		{
			return statement.Area == context.Question.Concept;
		}
	}
}
