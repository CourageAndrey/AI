﻿namespace Inventor.Client.ViewModels.Questions
{
	[QuestionDescriptor]
	public class EnumeratePartsQuestion : QuestionViewModel<Core.Questions.EnumeratePartsQuestion>
	{
		[PropertyDescriptor(true, "Questions.Parameters.Concept")]
		public Core.IConcept Concept
		{ get; set; }

		public override Core.Questions.EnumeratePartsQuestion BuildQuestionImplementation()
		{
			return new Core.Questions.EnumeratePartsQuestion(Concept);
		}
	}
}
