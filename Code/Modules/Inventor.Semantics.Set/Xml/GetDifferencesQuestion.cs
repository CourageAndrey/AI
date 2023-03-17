﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using Inventor.Semantics.Xml;

namespace Inventor.Semantics.Set.Xml
{
	[XmlType]
	public class GetDifferencesQuestion : Question<Questions.GetDifferencesQuestion>
	{
		#region Properties

		[XmlElement]
		public String Concept1
		{ get; }

		[XmlElement]
		public String Concept2
		{ get; }

		#endregion

		#region Constructors

		public GetDifferencesQuestion()
		{ }

		public GetDifferencesQuestion(Questions.GetDifferencesQuestion question)
		{
			Concept1 = question.Concept1.ID;
			Concept2 = question.Concept2.ID;
		}

		#endregion

		protected override Questions.GetDifferencesQuestion SaveImplementation(ConceptIdResolver conceptIdResolver, IEnumerable<IStatement> preconditions)
		{
			return new Questions.GetDifferencesQuestion(
				conceptIdResolver.GetConceptById(Concept1),
				conceptIdResolver.GetConceptById(Concept2),
				preconditions);
		}
	}
}
