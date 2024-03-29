﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using AabSemantics.Serialization;
using AabSemantics.Serialization.Xml;

namespace AabSemantics.Modules.Set.Xml
{
	[XmlType]
	public class HasSignQuestion : Question<Questions.HasSignQuestion>
	{
		#region Properties

		[XmlElement]
		public String Concept
		{ get; set; }

		[XmlElement]
		public String Sign
		{ get; set; }

		[XmlElement]
		public System.Boolean Recursive
		{ get; set; }

		#endregion

		#region Constructors

		public HasSignQuestion()
		{ }

		public HasSignQuestion(Questions.HasSignQuestion question)
			: base(question)
		{
			Concept = question.Concept.ID;
			Sign = question.Sign.ID;
			Recursive = question.Recursive;
		}

		#endregion

		protected override Questions.HasSignQuestion SaveImplementation(ConceptIdResolver conceptIdResolver, StatementIdResolver statementIdResolver, IEnumerable<IStatement> preconditions)
		{
			return new Questions.HasSignQuestion(
				conceptIdResolver.GetConceptById(Concept),
				conceptIdResolver.GetConceptById(Sign),
				Recursive,
				preconditions);
		}
	}
}
