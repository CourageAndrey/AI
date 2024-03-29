﻿using System;
using System.Collections.Generic;

using AabSemantics.Localization;

namespace AabSemantics.Text
{
	public abstract class TextBase : IText
	{
		public sealed override String ToString()
		{
			return TextRenders.PlainString.RenderText(this, Language.Default).ToString();
		}

		public abstract IDictionary<String, IKnowledge> GetParameters();
	}
}
