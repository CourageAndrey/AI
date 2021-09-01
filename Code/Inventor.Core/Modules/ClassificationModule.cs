﻿using System;

using Inventor.Core.Statements;
using Inventor.Core.Questions;

namespace Inventor.Core.Modules
{
	public class ClassificationModule : ExtensionModule
	{
		public const String ModuleName = "System.Classification";

		public ClassificationModule()
			: base(ModuleName, new[] { BooleanModule.ModuleName })
		{ }

		protected override void Attach(ISemanticNetwork semanticNetwork)
		{
			semanticNetwork.RegisterStatement<IsStatement>(language => language.StatementNames.Clasification, statement => new Xml.IsStatement(statement as IsStatement));

			semanticNetwork.RegisterQuestion<EnumerateAncestorsQuestion>();
			semanticNetwork.RegisterQuestion<EnumerateDescendantsQuestion>();
			semanticNetwork.RegisterQuestion<IsQuestion>();
		}
	}
}
