﻿using System;

namespace Inventor.Core.Text
{
	public class HeaderDecorator : TextDecoratorBase
	{
		public Byte Level
		{ get; }

		public HeaderDecorator(IText innerText, Byte level)
			: base(innerText)
		{
			Level = level;
		}
	}
}
