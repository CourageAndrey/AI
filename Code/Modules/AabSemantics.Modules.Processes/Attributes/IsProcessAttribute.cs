﻿namespace AabSemantics.Modules.Processes.Attributes
{
	public class IsProcessAttribute : IAttribute
	{
		private IsProcessAttribute()
		{ }

		public static readonly IsProcessAttribute Value = new IsProcessAttribute();
	}
}
