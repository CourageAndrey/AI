﻿namespace Inventor.Client.ViewModels.Questions
{
	[QuestionDescriptor]
	public class IsSignQuestion : QuestionViewModel<Core.Questions.IsSignQuestion>
	{
		[PropertyDescriptor(true, "Questions.Parameters.Concept")]
		public Core.IConcept Concept
		{ get; set; }

		public override Core.Questions.IsSignQuestion BuildQuestionImplementation()
		{
			return new Core.Questions.IsSignQuestion(Concept);
		}
	}
}
