﻿using Inventor.Semantics;
using Inventor.Semantics.Concepts;
using Inventor.Semantics.Localization;
using Samples.Semantics.Sample07.CustomModule.Localization;

namespace Samples.Semantics.Sample07.CustomModule
{
	internal static class CustomConcepts
	{
		public static readonly IConcept Custom = new SystemConcept(
			"{Custom.Custom}",
			new LocalizedStringConstant(language => language.GetExtension<ILanguageCustomModule>().Concepts.CustomName),
			new LocalizedStringConstant(language => language.GetExtension<ILanguageCustomModule>().Concepts.CustomHint));
	}
}