﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using Inventor.Semantics.Serialization;

namespace Inventor.Semantics.Set.Json
{
	[DataContract]
	public class IsSubjectAreaQuestion : Serialization.Json.Question<Questions.IsSubjectAreaQuestion>
	{
		#region Properties

		[DataMember]
		public String Concept
		{ get; set; }

		[DataMember]
		public String Area
		{ get; set; }

		#endregion

		#region Constructors

		public IsSubjectAreaQuestion()
			: base()
		{ }

		public IsSubjectAreaQuestion(Questions.IsSubjectAreaQuestion question)
			: base(question)
		{
			Concept = question.Concept.ID;
			Area = question.Area.ID;
		}

		#endregion

		protected override Questions.IsSubjectAreaQuestion SaveImplementation(ConceptIdResolver conceptIdResolver, IEnumerable<IStatement> preconditions)
		{
			return new Questions.IsSubjectAreaQuestion(
				conceptIdResolver.GetConceptById(Concept),
				conceptIdResolver.GetConceptById(Area),
				preconditions);
		}
	}
}