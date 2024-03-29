﻿namespace AabSemantics.Extensions.WPF.ViewModels.Questions
{
	[QuestionDescriptor]
	public class IsQuestion : QuestionViewModel<Modules.Classification.Questions.IsQuestion>
	{
		[PropertyDescriptor(true, "Questions.Parameters.Child")]
		public IConcept Child
		{ get; set; }

		[PropertyDescriptor(true, "Questions.Parameters.Parent")]
		public IConcept Parent
		{ get; set; }

		public override Modules.Classification.Questions.IsQuestion BuildQuestionImplementation()
		{
			return new Modules.Classification.Questions.IsQuestion(Child, Parent);
		}
	}
}
