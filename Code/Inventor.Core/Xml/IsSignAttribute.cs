﻿using System.Xml.Serialization;

namespace Inventor.Core.Xml
{
	[XmlType]
	public class IsSignAttribute : Attribute
	{
		public override IAttribute Load()
		{
			return Attributes.IsSignAttribute.Value;
		}
	}
}
