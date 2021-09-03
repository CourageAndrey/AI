﻿using System.Linq;

using Inventor.Client.ViewModels;
using Inventor.Core;

namespace Inventor.Client.Controls
{
	public partial class GroupStatementControl : IStatementEditor
	{
		public GroupStatementControl()
		{
			InitializeComponent();

			_comboBoxConcept.MakeAutoComplete();
			_comboBoxArea.MakeAutoComplete();
		}

		public void Initialize(ISemanticNetwork semanticNetwork, ILanguage language)
		{
			var wrappedConcepts = semanticNetwork.Concepts.Select(c => new ConceptItem(c, language)).ToList();
			_comboBoxConcept.ItemsSource = wrappedConcepts;
			_comboBoxArea.ItemsSource = wrappedConcepts;

			_groupID.Header = language.Ui.Editing.PropertyID;
			_groupArea.Header = language.Ui.Editing.PropertyArea;
			_groupConcept.Header = language.Ui.Editing.PropertyConcept;
		}

		public StatementViewModel Statement
		{
			get { return _contextControl.DataContext as ViewModels.Statements.GroupStatement; }
			set
			{
				_contextControl.DataContext = value;
				_idControl.IsReadOnly = value.BoundStatement?.Context is Core.Contexts.SystemContext;
			}
		}
	}
}
