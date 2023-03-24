﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using Inventor.Semantics.Serialization;

namespace Inventor.Semantics.Set.Json
{
	[DataContract]
	public class DescribeSubjectAreaQuestion : Serialization.Json.Question<Questions.DescribeSubjectAreaQuestion>
	{
		#region Properties

		[DataMember]
		public String Concept
		{ get; }

		#endregion

		#region Constructors

		public DescribeSubjectAreaQuestion()
			: base()
		{ }

		public DescribeSubjectAreaQuestion(Questions.DescribeSubjectAreaQuestion question)
			: base(question)
		{
			Concept = question.Concept.ID;
		}

		#endregion

		protected override Questions.DescribeSubjectAreaQuestion SaveImplementation(ConceptIdResolver conceptIdResolver, IEnumerable<IStatement> preconditions)
		{
			return new Questions.DescribeSubjectAreaQuestion(
				conceptIdResolver.GetConceptById(Concept),
				preconditions);
		}
	}
}