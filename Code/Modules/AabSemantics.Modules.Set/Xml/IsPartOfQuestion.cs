﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using AabSemantics.Serialization;
using AabSemantics.Serialization.Xml;

namespace AabSemantics.Modules.Set.Xml
{
	[XmlType]
	public class IsPartOfQuestion : Question<Questions.IsPartOfQuestion>
	{
		#region Properties

		[XmlElement]
		public String Parent
		{ get; set; }

		[XmlElement]
		public String Child
		{ get; set; }

		#endregion

		#region Constructors

		public IsPartOfQuestion()
		{ }

		public IsPartOfQuestion(Questions.IsPartOfQuestion question)
			: base(question)
		{
			Parent = question.Parent.ID;
			Child = question.Child.ID;
		}

		#endregion

		protected override Questions.IsPartOfQuestion SaveImplementation(ConceptIdResolver conceptIdResolver, StatementIdResolver statementIdResolver, IEnumerable<IStatement> preconditions)
		{
			return new Questions.IsPartOfQuestion(
				conceptIdResolver.GetConceptById(Child),
				conceptIdResolver.GetConceptById(Parent),
				preconditions);
		}
	}
}
