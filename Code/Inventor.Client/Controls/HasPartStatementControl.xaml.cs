﻿using System.Linq;

using Inventor.Client.ViewModels;
using Inventor.Core;

namespace Inventor.Client.Controls
{
	public partial class HasPartStatementControl : IStatementEditor
	{
		public HasPartStatementControl()
		{
			InitializeComponent();

			_comboBoxWhole.MakeAutoComplete();
			_comboBoxPart.MakeAutoComplete();
		}

		public void Initialize(ISemanticNetwork semanticNetwork, ILanguage language)
		{
			var wrappedConcepts = semanticNetwork.Concepts.Select(c => new ConceptItem(c, language)).ToList();
			_comboBoxWhole.ItemsSource = wrappedConcepts;
			_comboBoxPart.ItemsSource = wrappedConcepts;

			var languageEditing = language.Ui.Editing;
			_groupID.Header = languageEditing.PropertyID;
			_groupWhole.Header = languageEditing.PropertyAncestor;
			_groupPart.Header = languageEditing.PropertyDescendant;
		}

		public StatementViewModel Statement
		{
			get { return _contextControl.DataContext as ViewModels.Statements.HasPartStatement; }
			set
			{
				_contextControl.DataContext = value;
				_idControl.IsReadOnly = value.BoundStatement?.Context is Core.Contexts.SystemContext;
			}
		}
	}
}
