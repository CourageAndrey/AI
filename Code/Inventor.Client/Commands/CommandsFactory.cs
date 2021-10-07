﻿using Inventor.Client.TreeNodes;
using Inventor.Client.ViewModels;

namespace Inventor.Client.Commands
{
	public class CommandsFactory
	{
		public IEditCommand CreateAddCommand(IKnowledgeViewModel viewModel, SemanticNetworkNode semanticNetworkNode, InventorApplication application)
		{
			var conceptViewModel = viewModel as Concept;
			if (conceptViewModel != null)
			{
				return new AddConceptCommand(conceptViewModel, semanticNetworkNode, application);
			}

			var statementViewModel = viewModel as StatementViewModel;
			if (statementViewModel != null)
			{
				return new AddStatementCommand(statementViewModel, semanticNetworkNode, application);
			}

			return null;
		}

		public IEditCommand CreateEditCommand(IKnowledgeViewModel viewModel, SemanticNetworkNode semanticNetworkNode, InventorApplication application, IViewModelFactory viewModelFactory)
		{
			var conceptViewModel = viewModel as Concept;
			if (conceptViewModel != null)
			{
				var previousVersion = new Concept(conceptViewModel.BoundObject);
				return new EditConceptCommand(conceptViewModel, previousVersion, semanticNetworkNode, application);
			}

			var statementViewModel = viewModel as StatementViewModel;
			if (statementViewModel != null)
			{
				var previousVersion = viewModelFactory.CreateStatementByInstance(statementViewModel.BoundStatement, application.CurrentLanguage);
				return new EditStatementCommand(statementViewModel, previousVersion, semanticNetworkNode, application);
			}

			return null;
		}

		public IEditCommand CreateDeleteCommand(ExtendedTreeNode node, SemanticNetworkNode semanticNetworkNode, InventorApplication application)
		{
			var conceptNode = node as ConceptNode;
			if (conceptNode != null)
			{
				return new DeleteConceptCommand(conceptNode.Concept, semanticNetworkNode, application);
			}

			var statementNode = node as StatementNode;
			if (statementNode != null)
			{
				return new DeleteStatementCommand(statementNode.Statement, semanticNetworkNode, application);
			}

			return null;
		}

		public IEditCommand CreateRenameCommand(LocalizedString name, SemanticNetworkNode semanticNetworkNode, InventorApplication application)
		{
			return new RenameSemanticNetworkCommand(semanticNetworkNode, name, application);
		}
	}
}