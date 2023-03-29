﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

using Inventor.Semantics.Metadata;
using Inventor.Semantics.Utils;

namespace Inventor.Semantics.Serialization.Xml
{
	[XmlType]
	public abstract class Question
	{
		#region Properties

		[XmlArray(nameof(Preconditions))]
		public List<Statement> Preconditions
		{ get; set; } = new List<Statement>();

		#endregion

		#region Constructors

		protected Question()
		{ }

		protected Question(IQuestion question)
		{
			var statementSerializers = Repositories.Statements.Definitions.ToDictionary(
				definition => definition.Key,
				definition => (StatementXmlSerializationSettings) definition.Value.GetXmlSerializationSettings());

			Preconditions = question.Preconditions.Select(statement => statementSerializers[statement.GetType()].GetXml(statement)).ToList();
		}

		#endregion

		public static Question Load(IQuestion question)
		{
			var definition = Repositories.Questions.Definitions.GetSuitable(question);
			return definition.GetXmlSerializationSettings<QuestionXmlSerializationSettings>().GetXml(question);
		}

		public abstract IQuestion Save(ConceptIdResolver conceptIdResolver, StatementIdResolver statementIdResolver);

		static Question()
		{
			foreach (var serializedType in new[]
				{
					new KeyValuePair<Type, String>(typeof(Question), nameof(Preconditions)),
					new KeyValuePair<Type, String>(typeof(Modules.Boolean.Xml.CheckStatementQuestion), nameof(Modules.Boolean.Xml.CheckStatementQuestion.Statement)),
				})
			{
				var attributeOverrides = new XmlAttributeOverrides();

				var statementAttributes = new XmlAttributes();
				foreach (var definition in Repositories.Statements.Definitions.Values)
				{
					var xmlSettings = definition.GetXmlSerializationSettings<StatementXmlSerializationSettings>();
					statementAttributes.XmlElements.Add(new XmlElementAttribute(xmlSettings.XmlElementName, xmlSettings.XmlType));
				}
				attributeOverrides.Add(serializedType.Key, serializedType.Value, statementAttributes);

				var serializer = new XmlSerializer(serializedType.Key, attributeOverrides);
				serializedType.Key.DefineCustomXmlSerializer(serializer);
			}
		}
	}

	[XmlType]
	public abstract class Question<QuestionT> : Question
		where QuestionT : IQuestion
	{
		#region Constructors

		protected Question()
		{ }

		protected Question(IQuestion question)
			: base(question)
		{ }

		#endregion

		public override IQuestion Save(ConceptIdResolver conceptIdResolver, StatementIdResolver statementIdResolver)
		{
			return SaveImplementation(
				conceptIdResolver,
				statementIdResolver,
				Preconditions.Select(statement => statement.SaveOrReuse(conceptIdResolver, statementIdResolver)));
		}

		protected abstract QuestionT SaveImplementation(ConceptIdResolver conceptIdResolver, StatementIdResolver statementIdResolver, IEnumerable<IStatement> preconditions);
	}
}
