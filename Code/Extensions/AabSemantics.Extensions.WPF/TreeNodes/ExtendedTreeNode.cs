﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows.Media;

namespace AabSemantics.Extensions.WPF.TreeNodes
{
	public abstract class ExtendedTreeNode : INotifyPropertyChanged
	{
		#region Properties

		public abstract String Text
		{ get; }

		public abstract String Tooltip
		{ get; }

		public abstract ImageSource Icon
		{ get; }

		public ObservableCollection<ExtendedTreeNode> Children
		{ get; } = new ObservableCollection<ExtendedTreeNode>();

		public System.Boolean IsSelected
		{ get; set; }

		public System.Boolean IsExpanded
		{ get; set; }

		#endregion

		#region Implementation of INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		public void RefreshView()
		{
			var handler = Volatile.Read(ref PropertyChanged);
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(null));
			}
		}

		#endregion
	}
}
