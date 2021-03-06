﻿namespace Inventor.Client.ViewModels.Questions
{
	[QuestionDescriptor]
	public sealed class HasSignQuestion : QuestionViewModel<Core.Questions.HasSignQuestion>
	{
		[PropertyDescriptor(true, "QuestionNames.ParamConcept")]
		public Core.IConcept Concept
		{ get; set; }

		[PropertyDescriptor(true, "QuestionNames.ParamSign")]
		public Core.IConcept Sign
		{ get; set; }

		[PropertyDescriptor(false, "QuestionNames.ParamRecursive")]
		public bool Recursive
		{ get; set; }

		public override Core.Questions.HasSignQuestion BuildQuestion()
		{
			return new Core.Questions.HasSignQuestion(Concept, Sign, Recursive);
		}
	}
}
