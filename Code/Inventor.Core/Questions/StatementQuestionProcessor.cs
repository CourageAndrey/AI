﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventor.Core.Questions
{
	public class StatementQuestionProcessor<QuestionT, StatementT>
		where QuestionT : IQuestion
		where StatementT : IStatement
	{
		#region Properties

		public IQuestionProcessingContext<QuestionT> Context
		{ get; }

		public ICollection<StatementT> Statements
		{ get; }

		public ICollection<ChildAnswer> ChildAnswers
		{ get; private set; }

		#endregion

		public StatementQuestionProcessor(IQuestionProcessingContext context, Func<StatementT, Boolean> match)
		{
			Context = (IQuestionProcessingContext<QuestionT>) context;

			Statements = context.SemanticNetwork.Statements
				.Enumerate<StatementT>(context.ActiveContexts)
				.Where(match)
				.ToList();

			ChildAnswers = new ChildAnswer[0];
		}

		public StatementQuestionProcessor<QuestionT, StatementT> ProcessTransitives(
			Func<ICollection<StatementT>, Boolean> needToProcess,
			Func<IQuestionProcessingContext<QuestionT>, IEnumerable<NestedQuestion>> getNestedQuestions)
		{
			if (needToProcess(Statements))
			{
				ChildAnswers = new List<ChildAnswer>();
				foreach (var nested in getNestedQuestions(Context))
				{
					var answer = nested.Question.Ask(Context);
					if (!answer.IsEmpty)
					{
						ChildAnswers.Add(new ChildAnswer(nested.Question, answer, nested.TransitiveStatements));
					}
				}
			}
			else
			{
				ChildAnswers = new ChildAnswer[0];
			}
			return this;
		}

		public IAnswer Select(Func<IQuestionProcessingContext<QuestionT>, ICollection<StatementT>, ICollection<ChildAnswer>, IAnswer> formatter)
		{
			return formatter(Context, Statements, ChildAnswers);
		}
	}
}