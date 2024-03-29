﻿using System.Windows;

using AabSemantics.Extensions.WPF.Controls;
using AabSemantics.Extensions.WPF.Dialogs;
using AabSemantics.Modules.Processes.Localization;

namespace AabSemantics.Extensions.WPF.ViewModels.Statements
{
	public class ProcessesStatement : StatementViewModel<Modules.Processes.Statements.ProcessesStatement>
	{
		#region Properties

		public ConceptItem ProcessA
		{ get; set; }

		public ConceptItem ProcessB
		{ get; set; }

		public ConceptItem SequenceSign
		{ get; set; }

		#endregion

		#region Constructors

		public ProcessesStatement(ILanguage language)
			: this(string.Empty, null, null, null, language)
		{ }

		public ProcessesStatement(Modules.Processes.Statements.ProcessesStatement statement, ILanguage language)
			: this(statement.ID, new ConceptItem(statement.ProcessA, language), new ConceptItem(statement.ProcessB, language), new ConceptItem(statement.SequenceSign, language), language)
		{
			BoundObject = statement;
		}

		public ProcessesStatement(string id, ConceptItem processA, ConceptItem processB, ConceptItem sequenceSign, ILanguage language)
			: base(id, language)
		{
			ProcessA = processA;
			ProcessB = processB;
			SequenceSign = sequenceSign;
		}

		#endregion

		#region Implementation of IViewModel

		public override Window CreateEditDialog(Window owner, ISemanticNetwork semanticNetwork, ILanguage language)
		{
			var control = new ProcessesStatementControl
			{
				Statement = this,
			};
			control.Initialize(semanticNetwork, language);
			var dialog = new EditDialog
			{
				Owner = owner,
				Editor = control,
				Title = language.GetExtension<ILanguageProcessesModule>().Statements.Names.Processes,
				SizeToContent = SizeToContent.WidthAndHeight,
				MinWidth = 200,
				MinHeight = 100,
				WindowStartupLocation = WindowStartupLocation.CenterOwner,
			};
			dialog.Localize(language);
			return dialog;
		}

		protected override AabSemantics.Modules.Processes.Statements.ProcessesStatement CreateStatementImplementation()
		{
			return new AabSemantics.Modules.Processes.Statements.ProcessesStatement(ID, ProcessA.Concept, ProcessB.Concept, SequenceSign.Concept);
		}

		public override void ApplyUpdate()
		{
			BoundObject.Update(ID, ProcessA.Concept, ProcessB.Concept, SequenceSign.Concept);
		}

		#endregion

		public override StatementViewModel Clone()
		{
			return new ProcessesStatement(ID, ProcessA, ProcessB, SequenceSign, _language);
		}
	}
}
