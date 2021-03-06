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
	public sealed class WhatProcessor : QuestionProcessor<WhatQuestion>
	{
		public override IAnswer Process(IQuestionProcessingContext<WhatQuestion> context)
		{
			var question = context.Question;
			var allStatements = context.KnowledgeBase.Statements.Enumerate(context.ActiveContexts);

			var statements = allStatements.Enumerate<IsStatement>(context.ActiveContexts).Where(c => c.Descendant == question.Concept).ToList();
			if (statements.Any())
			{
				var result = new FormattedText();
				var difference = new List<SignValueStatement>();
				foreach (var statement in statements)
				{
					foreach (var sign in HasSignStatement.GetSigns(allStatements, statement.Ancestor, false))
					{
						var diff = SignValueStatement.GetSignValue(allStatements, question.Concept, sign.Sign);
						if (diff != null)
						{
							difference.Add(diff);
						}
					}
					if (difference.Count > 0)
					{
						result.Add(() => context.Language.Answers.IsDescriptionWithSign, new Dictionary<String, INamed>
						{
							{ Strings.ParamChild, question.Concept },
							{ Strings.ParamParent, statement.Ancestor },
						});
						foreach (var diff in difference)
						{
							result.Add(() => context.Language.Answers.IsDescriptionWithSignValue, new Dictionary<String, INamed>
							{
								{ Strings.ParamSign, diff.Sign },
								{ Strings.ParamValue, diff.Value },
							});
						}
					}
					else
					{
						result.Add(() => context.Language.Answers.IsDescription, new Dictionary<String, INamed>
						{
							{ Strings.ParamChild, question.Concept },
							{ Strings.ParamParent, statement.Ancestor },
						});
					}
					result.Add(() => String.Empty, new Dictionary<String, INamed>());
				}
				return new Answer(result, new Explanation(difference), false);
			}
			else
			{
				return Answer.CreateUnknown(context.Language);
			}
		}
	}
}
