﻿using System;
using System.Collections.Generic;
using System.Linq;

using AabSemantics.Modules.Classification.Statements;
using AabSemantics.Modules.Set.Attributes;
using AabSemantics.Modules.Set.Localization;
using AabSemantics.Statements;
using AabSemantics.Utils;

namespace AabSemantics.Modules.Set.Statements
{
	public class HasSignStatement : Statement<HasSignStatement>
	{
		#region Properties

		public IConcept Concept
		{ get; private set; }

		public IConcept Sign
		{ get; private set; }

		#endregion

		public HasSignStatement(String id, IConcept concept, IConcept sign)
			: base(
				id,
				new Func<ILanguage, String>(language => language.GetExtension<ILanguageSetModule>().Statements.Names.HasSign),
				new Func<ILanguage, String>(language => language.GetExtension<ILanguageSetModule>().Statements.Hints.HasSign))
		{
			Update(id, concept, sign);
		}

		public void Update(String id, IConcept concept, IConcept sign)
		{
			Update(id);
			Concept = concept.EnsureNotNull(nameof(concept));
			Sign = sign.EnsureNotNull(nameof(sign)).EnsureHasAttribute<IConcept, IsSignAttribute>(nameof(sign));
		}

		public override IEnumerable<IConcept> GetChildConcepts()
		{
			yield return Concept;
			yield return Sign;
		}

		#region Description

		protected override String GetDescriptionTrueText(ILanguage language)
		{
			return language.GetExtension<ILanguageSetModule>().Statements.TrueFormatStrings.HasSign;
		}

		protected override String GetDescriptionFalseText(ILanguage language)
		{
			return language.GetExtension<ILanguageSetModule>().Statements.TrueFormatStrings.HasSign;
		}

		protected override String GetDescriptionQuestionText(ILanguage language)
		{
			return language.GetExtension<ILanguageSetModule>().Statements.TrueFormatStrings.HasSign;
		}

		protected override IDictionary<String, IKnowledge> GetDescriptionParameters()
		{
			return new Dictionary<String, IKnowledge>
			{
				{ AabSemantics.Localization.Strings.ParamConcept, Concept },
				{ Strings.ParamSign, Sign },
			};
		}

		#endregion

		#region Consistency checking

		public override System.Boolean Equals(HasSignStatement other)
		{
			if (ReferenceEquals(this, other)) return true;
			if (other != null)
			{
				return	other.Concept == Concept &&
						other.Sign == Sign;
			}
			else return false;
		}

		public System.Boolean CheckSignDuplication(IEnumerable<HasSignStatement> hasSigns, IEnumerable<IsStatement> clasifications)
		{
			var signs = hasSigns.Where(hs => hs.Concept == Concept).Select(hs => hs.Sign).ToList();
			foreach (var parent in clasifications.GetParentsAllLevels(Concept))
			{
				foreach (var parentSign in hasSigns.Where(hs => hs.Concept == parent).Select(hs => hs.Sign))
				{
					if (signs.Contains(parentSign))
					{
						return false;
					}
				}
			}
			return true;
		}

		#endregion

		public static List<HasSignStatement> GetSigns(IEnumerable<IStatement> statements, IConcept concept, System.Boolean recursive)
		{
			var result = new List<HasSignStatement>();
			var hasSigns = statements.OfType<HasSignStatement>().ToList();
			result.AddRange(hasSigns.Where(sv => sv.Concept == concept));
			if (recursive)
			{
				foreach (var parentSigns in statements.GetParentsOneLevel<IConcept, IsStatement>(concept).Select(c => GetSigns(statements, c, true)))
				{
					result.AddRange(parentSigns);
				}
			}
			return result;
		}
	}
}
