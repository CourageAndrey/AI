﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using AabSemantics.Serialization;
using AabSemantics.Serialization.Xml;

namespace AabSemantics.Modules.Set.Xml
{
	[XmlType]
	public class IsSubjectAreaQuestion : Question<Questions.IsSubjectAreaQuestion>
	{
		#region Properties

		[XmlElement]
		public String Concept
		{ get; set; }

		[XmlElement]
		public String Area
		{ get; set; }

		#endregion

		#region Constructors

		public IsSubjectAreaQuestion()
		{ }

		public IsSubjectAreaQuestion(Questions.IsSubjectAreaQuestion question)
			: base(question)
		{
			Concept = question.Concept.ID;
			Area = question.Area.ID;
		}

		#endregion

		protected override Questions.IsSubjectAreaQuestion SaveImplementation(ConceptIdResolver conceptIdResolver, StatementIdResolver statementIdResolver, IEnumerable<IStatement> preconditions)
		{
			return new Questions.IsSubjectAreaQuestion(
				conceptIdResolver.GetConceptById(Concept),
				conceptIdResolver.GetConceptById(Area),
				preconditions);
		}
	}
}
