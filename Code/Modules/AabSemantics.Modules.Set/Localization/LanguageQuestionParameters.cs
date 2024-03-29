﻿using System;
using System.Xml.Serialization;

namespace AabSemantics.Modules.Set.Localization
{
	public interface ILanguageQuestionParameters
	{
		String Sign
		{ get; }

		String Area
		{ get; }

		String Concept1
		{ get; }

		String Concept2
		{ get; }
	}

	[XmlType("SetsQuestionParameters")]
	public class LanguageQuestionParameters : ILanguageQuestionParameters
	{
		#region Properties

		[XmlElement]
		public String Sign
		{ get; set; }

		[XmlElement]
		public String Area
		{ get; set; }

		[XmlElement]
		public String Concept1
		{ get; set; }

		[XmlElement]
		public String Concept2
		{ get; set; }

		#endregion

		internal static LanguageQuestionParameters CreateDefault()
		{
			return new LanguageQuestionParameters
			{
				Sign = "SIGN",
				Area = "SUBJECT_AREA",
				Concept1 = "CONCEPT 1",
				Concept2 = "CONCEPT 2",
			};
		}
	}
}
