﻿using System.Linq;

using Inventor.Semantics.Mathematics.Attributes;
using Inventor.Semantics.Modules.Boolean.Attributes;
using Inventor.Semantics.WPF.ViewModels;

namespace Inventor.Semantics.WPF.Controls
{
	public partial class ComparisonStatementControl : IStatementEditor
	{
		public ComparisonStatementControl()
		{
			InitializeComponent();

			_comboBoxLeftValue.MakeAutoComplete();
			_comboBoxRightValue.MakeAutoComplete();
			_comboBoxComparisonSign.MakeAutoComplete();
		}

		public void Initialize(ISemanticNetwork semanticNetwork, ILanguage language)
		{
			var wrappedConcepts = semanticNetwork.Concepts.Where(c => c.HasAttribute<IsValueAttribute>()).Select(c => new ConceptItem(c, language)).ToList();
			_comboBoxLeftValue.ItemsSource = wrappedConcepts;
			_comboBoxRightValue.ItemsSource = wrappedConcepts;
			_comboBoxComparisonSign.ItemsSource = semanticNetwork.Concepts.Where(c => c.HasAttribute<IsComparisonSignAttribute>()).Select(c => new ConceptItem(c, language)).ToList();

			var languageEditing = language.GetExtension<IWpfUiModule>().Ui.Editing;
			_groupID.Header = languageEditing.PropertyID;
			_groupLeftValue.Header = languageEditing.PropertyLeftValue;
			_groupRightValue.Header = languageEditing.PropertyRightValue;
			_groupComparisonSign.Header = languageEditing.PropertyComparisonSign;
		}

		public StatementViewModel Statement
		{
			get { return _contextControl.DataContext as ViewModels.Statements.ComparisonStatement; }
			set
			{
				_contextControl.DataContext = value;
				_idControl.IsReadOnly = value.BoundStatement?.Context is Semantics.Contexts.SystemContext;
			}
		}
	}
}
