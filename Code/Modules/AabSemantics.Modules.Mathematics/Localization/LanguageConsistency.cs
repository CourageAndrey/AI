﻿using System;
using System.Xml.Serialization;

namespace AabSemantics.Modules.Mathematics.Localization
{
	public interface ILanguageConsistency
	{
		String ErrorComparisonContradiction
		{ get; }
	}

	[XmlType("MathematicsConsistency")]
	public class LanguageConsistency : ILanguageConsistency
	{
		#region Properties

		[XmlElement]
		public String ErrorComparisonContradiction
		{ get; set; }

		#endregion

		internal static LanguageConsistency CreateDefault()
		{
			return new LanguageConsistency
			{
				ErrorComparisonContradiction = $"Impossible to compare {Strings.ParamLeftValue} and {Strings.ParamRightValue}. Possible cases: ",
			};
		}
	}
}
