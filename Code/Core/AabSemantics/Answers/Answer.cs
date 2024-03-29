﻿using System;

using AabSemantics.Text.Primitives;

namespace AabSemantics.Answers
{
	public class Answer : IAnswer
	{
		#region Properties

		public IText Description
		{ get; }

		public IExplanation Explanation
		{ get; }

		public Boolean IsEmpty
		{ get; }

		#endregion

		public Answer(IText description, IExplanation explanation, Boolean isEmpty)
		{
			Description = description;
			Explanation = explanation;
			IsEmpty = isEmpty;
		}

		public static IAnswer CreateUnknown()
		{
			return new Answer(
				new FormattedText(language => language.Questions.Answers.Unknown),
				new Explanation(Array.Empty<IStatement>()),
				true);
		}
	}
}
