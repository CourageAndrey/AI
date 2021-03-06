﻿namespace Inventor.Client.ViewModels.Questions
{
	[QuestionDescriptor]
	public sealed class IsPartOfQuestion : QuestionViewModel<Core.Questions.IsPartOfQuestion>
	{
		[PropertyDescriptor(true, "QuestionNames.ParamParent")]
		public Core.IConcept Parent
		{ get; set; }

		[PropertyDescriptor(true, "QuestionNames.ParamChild")]
		public Core.IConcept Child
		{ get; set; }

		public override Core.Questions.IsPartOfQuestion BuildQuestion()
		{
			return new Core.Questions.IsPartOfQuestion(Child, Parent);
		}
	}
}
