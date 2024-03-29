﻿using System.ComponentModel;
using System.Threading;

using AabSemantics.Metadata;

namespace AabSemantics.Extensions.WPF.ViewModels
{
	public class ConceptAttribute : INotifyPropertyChanged
	{
		#region Properties

		public string Name
		{ get; }

		public IAttribute Value
		{ get; }

		public bool IsOn
		{ get; set; }

		#endregion

		public ConceptAttribute(AttributeDefinition attributeDefinition, ILanguage language, bool isOn)
		{
			if (attributeDefinition != null)
			{
				Name = attributeDefinition.GetName(language);
				Value = attributeDefinition.Value;
			}
			else
			{
				Name = language.Attributes.None;
				Value = null;
			}

			IsOn = isOn;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		internal void SwitchOff()
		{
			IsOn = false;
			var handler = Volatile.Read(ref PropertyChanged);
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(nameof(IsOn)));
			}
		}
	}
}
