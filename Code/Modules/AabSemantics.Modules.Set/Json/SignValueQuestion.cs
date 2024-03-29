﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using AabSemantics.Serialization;

namespace AabSemantics.Modules.Set.Json
{
	[DataContract]
	public class SignValueQuestion : Serialization.Json.Question<Questions.SignValueQuestion>
	{
		#region Properties

		[DataMember]
		public String Concept
		{ get; set; }

		[DataMember]
		public String Sign
		{ get; set; }

		#endregion

		#region Constructors

		public SignValueQuestion()
			: base()
		{ }

		public SignValueQuestion(Questions.SignValueQuestion question)
			: base(question)
		{
			Concept = question.Concept.ID;
			Sign = question.Sign.ID;
		}

		#endregion

		protected override Questions.SignValueQuestion SaveImplementation(ConceptIdResolver conceptIdResolver, StatementIdResolver statementIdResolver, IEnumerable<IStatement> preconditions)
		{
			return new Questions.SignValueQuestion(
				conceptIdResolver.GetConceptById(Concept),
				conceptIdResolver.GetConceptById(Sign),
				preconditions);
		}
	}
}
