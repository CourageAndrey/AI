﻿using NUnit.Framework;

using AabSemantics.Answers;
using AabSemantics.Localization;

namespace AabSemantics.Tests.Answers
{
	[TestFixture]
	public class UnknownAnswerTest
	{
		[Test]
		public void GivenEmptyAnswer_WhenCheckIt_ThenItIsEmpty()
		{
			// arrange
			var language = Language.Default;
			var representer = TextRepresenters.PlainString;

			// act
			var emptyAnswer = Answer.CreateUnknown();
			string representation = representer.Represent(emptyAnswer.Description, language).ToString();

			// assert
			Assert.IsTrue(emptyAnswer.IsEmpty);
			Assert.IsTrue(representation.Contains(language.Questions.Answers.Unknown));
			Assert.AreEqual(0, emptyAnswer.Explanation.Statements.Count);
		}
	}
}
