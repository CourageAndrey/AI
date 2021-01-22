﻿using System.Windows;

using Inventor.Client.Controls;
using Inventor.Client.Dialogs;

namespace Inventor.Client.ViewModels
{
	public class Concept : IViewModel
	{
		#region Properties

		public LocalizedString Name
		{ get; }

		public LocalizedString Hint
		{ get; }

		#endregion

		#region Constructors

		public Concept()
			: this(new LocalizedString(), new LocalizedString())
		{ }

		public Concept(Core.Base.Concept concept)
			: this(LocalizedString.From(concept.Name), LocalizedString.From(concept.Hint))
		{
			_boundObject = concept;
		}

		public Concept(LocalizedString name, LocalizedString hint)
		{
			Name = name;
			Hint = hint;
		}

		#endregion

		#region Implementation of IViewModel

		private Core.Base.Concept _boundObject;

		public Window CreateEditDialog(Window owner, Core.IKnowledgeBase knowledgeBase, Core.ILanguage language)
		{
			var control = new ConceptControl
			{
				EditValue = this,
			};
			control.Initialize(knowledgeBase, language);
			var dialog = new EditDialog
			{
				Owner = owner,
				Editor = control,
				Title = language.Misc.Concept,
				SizeToContent = SizeToContent.WidthAndHeight,
				MinWidth = 200,
				MinHeight = 100,
				WindowStartupLocation = WindowStartupLocation.CenterOwner,
			};
			dialog.Localize(language);
			return dialog;
		}

		public void ApplyCreate(Core.IKnowledgeBase knowledgeBase)
		{
			knowledgeBase.Concepts.Add(_boundObject = new Core.Base.Concept(Name.Create(), Hint.Create()));
		}

		public void ApplyUpdate()
		{
			Name?.Apply(_boundObject.Name);
			Hint?.Apply(_boundObject.Hint);
		}

		#endregion
	}
}