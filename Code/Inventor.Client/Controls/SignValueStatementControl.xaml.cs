﻿using System.Linq;

using Inventor.Client.ViewModels;
using Inventor.Core;

namespace Inventor.Client.Controls
{
	public partial class SignValueStatementControl
	{
		public SignValueStatementControl()
		{
			InitializeComponent();
		}

		public void Initialize(IKnowledgeBase knowledgeBase, ILanguage language)
		{
			var wrappedConcepts = knowledgeBase.Concepts.Select(c => new ConceptItem(c, language)).ToList();
			_comboBoxConcept.ItemsSource = wrappedConcepts;
			_comboBoxSign.ItemsSource = wrappedConcepts;
			_comboBoxValue.ItemsSource = wrappedConcepts;

			_groupConcept.Header = language.Ui.Editing.PropertyConcept;
			_groupSign.Header = language.Ui.Editing.PropertySign;
			_groupValue.Header = language.Ui.Editing.PropertyValue;
		}

		public ViewModels.SignValueStatement EditValue
		{
			get { return _contextControl.DataContext as ViewModels.SignValueStatement; }
			set { _contextControl.DataContext = value; }
		}
	}
}