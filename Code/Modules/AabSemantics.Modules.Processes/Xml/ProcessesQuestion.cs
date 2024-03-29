﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using AabSemantics.Serialization;
using AabSemantics.Serialization.Xml;

namespace AabSemantics.Modules.Processes.Xml
{
	[XmlType]
	public class ProcessesQuestion : Question<Questions.ProcessesQuestion>
	{
		#region Properties

		[XmlElement]
		public String ProcessA
		{ get; set; }

		[XmlElement]
		public String ProcessB
		{ get; set; }

		#endregion

		#region Constructors

		public ProcessesQuestion()
		{ }

		public ProcessesQuestion(Questions.ProcessesQuestion question)
			: base(question)
		{
			ProcessA = question.ProcessA.ID;
			ProcessB = question.ProcessB.ID;
		}

		#endregion

		protected override Questions.ProcessesQuestion SaveImplementation(ConceptIdResolver conceptIdResolver, StatementIdResolver statementIdResolver, IEnumerable<IStatement> preconditions)
		{
			return new Questions.ProcessesQuestion(
				conceptIdResolver.GetConceptById(ProcessA),
				conceptIdResolver.GetConceptById(ProcessB),
				preconditions);
		}
	}
}
