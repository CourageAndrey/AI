﻿using System.Xml.Serialization;

using Inventor.Semantics;
using Inventor.Semantics.Xml;

namespace Inventor.Set.Xml
{
	[XmlType("IsSign")]
	public class IsSignAttribute : Attribute
	{
		public override IAttribute Load()
		{
			return Attributes.IsSignAttribute.Value;
		}
	}
}
