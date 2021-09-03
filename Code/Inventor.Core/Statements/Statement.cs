﻿using System;
using System.Collections.Generic;
using System.Linq;

using Inventor.Core.Localization;
using Inventor.Core.Text.Primitives;

namespace Inventor.Core.Statements
{
	public abstract class Statement : IStatement
	{
		#region Properties

		public ILocalizedString Name
		{ get; }

		public String ID
		{ get; private set; }

		public IContext Context
		{ get; set; }

		public ILocalizedString Hint
		{ get; }

		#endregion

		public abstract IEnumerable<IConcept> GetChildConcepts();

		public override sealed String ToString()
		{
			return this.GetTypeWithId();
		}

		protected Statement(String id, ILocalizedString name, ILocalizedString hint = null)
		{
			Update(id);

			if (name != null)
			{
				Name = name;
			}
			else
			{
				throw new ArgumentNullException(nameof(name));
			}

			Hint = hint ?? LocalizedString.Empty;
		}

		#region Description

		public IText DescribeTrue()
		{
			var formatter = new Func<ILanguage, String>(language => GetDescriptionText(language.TrueStatementFormatStrings) + $" ({Strings.ParamStatement})");

			var parameters = GetDescriptionParameters();
			parameters[Strings.ParamStatement] = this;

			return new FormattedText(formatter, parameters);
		}

		public IText DescribeFalse()
		{
			return new FormattedText(language => GetDescriptionText(language.FalseStatementFormatStrings), GetDescriptionParameters());
		}

		public IText DescribeQuestion()
		{
			return new FormattedText(language => GetDescriptionText(language.QuestionStatementFormatStrings), GetDescriptionParameters());
		}

		protected abstract String GetDescriptionText(ILanguageStatements language);

		protected abstract IDictionary<String, IKnowledge> GetDescriptionParameters();

		#endregion

		public void Update(String id)
		{
			ID = id.EnsureIdIsSet();
		}

		public abstract Boolean CheckUnique(IEnumerable<IStatement> statements);

#pragma warning disable 659
		public abstract override Boolean Equals(Object obj);

		public override Int32 GetHashCode()
		{
// ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
			return base.GetHashCode();
		}
#pragma warning restore 659
	}

	public abstract class Statement<StatementT> : Statement, IEquatable<StatementT>
		where StatementT : Statement<StatementT>
	{
		protected Statement(String id, LocalizedString name, LocalizedString hint = null)
			: base(id, name, hint)
		{ }

		public override sealed Boolean CheckUnique(IEnumerable<IStatement> statements)
		{
			return statements.OfType<StatementT>().Count(Equals) == 1;
		}

		public abstract Boolean Equals(StatementT other);

#pragma warning disable 659
		public override sealed Boolean Equals(Object obj)
		{
			return Equals(obj as StatementT);
		}

		public override Int32 GetHashCode()
		{
			// ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
			return base.GetHashCode();
		}
#pragma warning restore 659
	}
}
