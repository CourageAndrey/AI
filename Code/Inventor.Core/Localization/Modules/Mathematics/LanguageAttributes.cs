﻿using System;
using System.Xml.Serialization;

namespace Inventor.Core.Localization.Modules.Mathematics
{
	public interface ILanguageAttributes
	{
		String IsComparisonSign
		{ get; }
	}

	[XmlType("MathematicsAttributes")]
	public class LanguageAttributes : ILanguageAttributes
	{
		#region Properties

		[XmlElement]
		public String IsComparisonSign
		{ get; set; }

		#endregion

		internal static LanguageAttributes CreateDefault()
		{
			return new LanguageAttributes
			{
				IsComparisonSign = "Is Comparison Sign",
			};
		}
	}
}
