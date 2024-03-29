﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using AabSemantics.Serialization;
using AabSemantics.Serialization.Json;

namespace AabSemantics.Modules.Classification.Json
{
	[DataContract]
	public class IsQuestion : Question<Questions.IsQuestion>
	{
		#region Properties

		[DataMember]
		public String Child
		{ get; set; }

		[DataMember]
		public String Parent
		{ get; set; }

		#endregion

		#region Constructors

		public IsQuestion()
			: base()
		{ }

		public IsQuestion(Questions.IsQuestion question)
			: base(question)
		{
			Parent = question.Parent.ID;
			Child = question.Child.ID;
		}

		#endregion

		protected override Questions.IsQuestion SaveImplementation(ConceptIdResolver conceptIdResolver, StatementIdResolver statementIdResolver, IEnumerable<IStatement> preconditions)
		{
			return new Questions.IsQuestion(
				conceptIdResolver.GetConceptById(Child),
				conceptIdResolver.GetConceptById(Parent),
				preconditions);
		}
	}
}
