﻿using System.Collections.Generic;

namespace Inventor.Core.Text
{
	public class UnstructuredContainer : TextContainerBase
	{
		#region Constructors

		public UnstructuredContainer(IList<IText> items)
			: base(items)
		{ }

		public UnstructuredContainer(IText item)
			: this(new List<IText> { item })
		{ }

		public UnstructuredContainer()
			: this(new List<IText>())
		{ }

		#endregion
	}
}
