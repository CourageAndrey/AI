﻿using System.Windows;

using Inventor.Client.Controls;
using Inventor.Client.Dialogs;
using Inventor.Core;

namespace Inventor.Client.ViewModels.Statements
{
	public class IsLessThanOrEqualToStatement : StatementViewModel<Core.Statements.IsLessThanOrEqualToStatement>, IComparisonStatement
	{
		#region Properties

		public ConceptItem LeftValue
		{ get; set; }

		public ConceptItem RightValue
		{ get; set; }

		#endregion

		#region Constructors

		public IsLessThanOrEqualToStatement(ILanguage language)
			: this(null as ConceptItem, null, language)
		{ }

		public IsLessThanOrEqualToStatement(Core.Statements.IsLessThanOrEqualToStatement statement, ILanguage language)
			: this(new ConceptItem(statement.LeftValue, language), new ConceptItem(statement.RightValue, language), language)
		{
			_boundObject = statement;
		}

		public IsLessThanOrEqualToStatement(ConceptItem leftValue, ConceptItem rightValue, ILanguage language)
			: base(language)
		{
			LeftValue = leftValue;
			RightValue = rightValue;
		}

		#endregion

		#region Implementation of IViewModel

		public override Window CreateEditDialog(Window owner, IKnowledgeBase knowledgeBase, ILanguage language)
		{
			var control = new ComparisonStatementControl
			{
				EditValue = this,
			};
			control.Initialize(knowledgeBase, language);
			var dialog = new EditDialog
			{
				Owner = owner,
				Editor = control,
				Title = language.StatementNames.IsLessThanOrEqualTo,
				SizeToContent = SizeToContent.WidthAndHeight,
				MinWidth = 200,
				MinHeight = 100,
				WindowStartupLocation = WindowStartupLocation.CenterOwner,
			};
			dialog.Localize(language);
			return dialog;
		}

		protected override Core.Statements.IsLessThanOrEqualToStatement CreateStatementImplementation()
		{
			return new Core.Statements.IsLessThanOrEqualToStatement(LeftValue.Concept, RightValue.Concept);
		}

		public override void ApplyUpdate()
		{
			_boundObject.Update(LeftValue.Concept, RightValue.Concept);
		}

		#endregion

		public override StatementViewModel Clone()
		{
			return new IsLessThanOrEqualToStatement(LeftValue, RightValue, _language);
		}
	}
}
