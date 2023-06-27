﻿using System.Text.RegularExpressions;

using NUnit.Framework;

using AabSemantics.Localization;
using AabSemantics.Test.Sample;

namespace AabSemantics.Tests.Text
{
	[TestFixture]
	public class HtmlTextRepresenterTest
	{
		private const string ValidHtmRegex = "<(\"[^\"]*\"|'[^']*'|[^'\">])*>";

		[Test]
		public void GivenHtmlRepresenter_WhenRepresentText_ThenGenerateValidHtml()
		{
			// arrange
			var language = Language.Default;
			var semanticNetwork = new TestSemanticNetwork(language).SemanticNetwork;

			var text = semanticNetwork.DescribeRules();

			// act
			var html = TextRepresenters.Html.RepresentText(text, language).ToString();

			// assert
			Assert.IsTrue(Regex.IsMatch(html, ValidHtmRegex));
		}
	}
}
