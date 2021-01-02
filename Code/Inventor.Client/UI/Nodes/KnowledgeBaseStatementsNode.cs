using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

using Inventor.Client.Properties;
using Inventor.Core;
using Inventor.Core.Localization;

using Sef.UI;

namespace Inventor.Client.UI.Nodes
{
	public sealed class KnowledgeBaseStatementsNode : ExtendedTreeNode
	{
		#region Properties

		public override string Text
		{ get { return LanguageEx.CurrentEx.Misc.NameCategoryStatements; } }

		public override string Tooltip
		{ get { return LanguageEx.CurrentEx.Misc.NameCategoryStatements; } }

		public override ImageSource Icon
		{ get { return _icon ?? (_icon = Resources.Folder.ToSource()); } }

		public KnowledgeBase KnowledgeBase
		{ get { return _knowledgeBase; } }

		private static ImageSource _icon;
		private readonly KnowledgeBase _knowledgeBase;

		#endregion

		public KnowledgeBaseStatementsNode(KnowledgeBase knowledgeBase)
		{
			_knowledgeBase = knowledgeBase;
			foreach (var statement in knowledgeBase.Statements)
			{
				Children.Add(new StatementNode(statement));
			}
			knowledgeBase.StatementAdded += StatementAdded;
			knowledgeBase.StatementRemoved += StatementRemoved;
		}

		private void StatementAdded(IList<Statement> list, Statement item)
		{
			Children.Add(new StatementNode(item));
		}

		private void StatementRemoved(IList<Statement> list, Statement item)
		{
			Children.Remove(Children.OfType<StatementNode>().First(r => r.Statement == item));
		}

		public void Clear()
		{
			_knowledgeBase.StatementAdded -= StatementAdded;
			_knowledgeBase.StatementRemoved -= StatementRemoved;
		}

		public List<ExtendedTreeNode> Find(Statement statement, ExtendedTreeNode parent)
		{
			var child = Children.OfType<StatementNode>().FirstOrDefault(rn => rn.Statement == statement);
			return child != null
				? new List<ExtendedTreeNode> { parent, this, child }
				: new List<ExtendedTreeNode>();
		}
	}
}