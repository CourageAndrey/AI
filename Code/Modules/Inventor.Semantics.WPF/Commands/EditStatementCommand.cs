﻿using Inventor.Semantics.WPF.TreeNodes;

namespace Inventor.Semantics.WPF.Commands
{
	public class EditStatementCommand : BaseEditCommand
	{
		#region Properties

		public StatementViewModel ViewModel
		{ get; }

		public StatementViewModel PreviousVersion
		{ get; set; }

		#endregion

		public EditStatementCommand(StatementViewModel viewModel, StatementViewModel previousVersion, SemanticNetworkNode semanticNetworkNode, IInventorApplication application)
			: base(semanticNetworkNode, application)
		{
			ViewModel = viewModel;
			PreviousVersion = previousVersion;
		}

		public override void Apply()
		{
			ViewModel.ApplyUpdate();
		}

		public override void Rollback()
		{
			PreviousVersion.ApplyUpdate();
		}
	}
}
