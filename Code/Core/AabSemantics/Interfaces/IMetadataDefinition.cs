﻿using System;
using System.Collections.Generic;
using System.Linq;

using AabSemantics.Metadata;
using AabSemantics.Utils;

namespace AabSemantics
{
	public interface IMetadataDefinition
	{
		Type Type
		{ get; }

		List<ISerializationSettings> SerializationSettings
		{ get; }
	}

	public interface IMetadataDefinition<SerializationSettingsT> : IMetadataDefinition
		where SerializationSettingsT : ISerializationSettings
	{
		new List<SerializationSettingsT> SerializationSettings
		{ get; }
	}

	public abstract class MetadataDefinition<SerializationSettingsT> : IMetadataDefinition<SerializationSettingsT>
		where SerializationSettingsT : ISerializationSettings
	{
		#region Properties

		public Type Type
		{ get; }

		List<ISerializationSettings> IMetadataDefinition.SerializationSettings
		{ get { return SerializationSettings.OfType<ISerializationSettings>().ToList(); } }

		public List<SerializationSettingsT> SerializationSettings
		{ get; } = new List<SerializationSettingsT>();

		#endregion

		protected MetadataDefinition(Type type, Type instanceType)
		{
			Type = type.EnsureNotNull(nameof(type));
			type.EnsureContract(instanceType, nameof(type));
		}
	}

	public static class MetadataDefinitionExtensions
	{
		public static SettingsT GetSerializationSettings<SettingsT>(this IMetadataDefinition metadataDefinition)
			where SettingsT : ISerializationSettings
		{
			return metadataDefinition.SerializationSettings.OfType<SettingsT>().Single();
		}
	}
}
