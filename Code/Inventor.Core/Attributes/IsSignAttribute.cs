﻿namespace Inventor.Core.Attributes
{
	public class IsSignAttribute : IAttribute
	{
		private IsSignAttribute()
		{ }

		public static readonly IsSignAttribute Value = new IsSignAttribute();
	}
}
