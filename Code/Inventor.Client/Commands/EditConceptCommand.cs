﻿using Inventor.Client.TreeNodes;

namespace Inventor.Client.Commands
{
	public class EditConceptCommand : BaseEditCommand
	{
		#region Properties

		public ViewModels.Concept ViewModel
		{ get; }

		public ViewModels.Concept PreviousVersion
		{ get; }

		#endregion

		public EditConceptCommand(ViewModels.Concept viewModel, ViewModels.Concept previousVersion, SemanticNetworkNode semanticNetworkNode, InventorApplication application)
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
