﻿using System.Xml.Serialization;

using Inventor.Semantics;
using Inventor.Semantics.Xml;

namespace Inventor.Mathematics.Xml
{
	[XmlType("IsComparisonSign")]
	public class IsComparisonSignAttribute : Attribute
	{
		public override IAttribute Load()
		{
			return Attributes.IsComparisonSignAttribute.Value;
		}
	}
}
