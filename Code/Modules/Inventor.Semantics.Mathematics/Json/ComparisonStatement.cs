﻿using System;
using System.Runtime.Serialization;

using Inventor.Semantics.Serialization;

namespace Inventor.Semantics.Mathematics.Json
{
	[DataContract]
	public class ComparisonStatement : Serialization.Json.Statement<Statements.ComparisonStatement>
	{
		#region Properties

		[DataMember]
		public String LeftValue
		{ get; private set; }

		[DataMember]
		public String RightValue
		{ get; private set; }

		[DataMember]
		public String ComparisonSign
		{ get; private set; }

		#endregion

		#region Constructors

		public ComparisonStatement()
		{ }

		public ComparisonStatement(Statements.ComparisonStatement statement)
		{
			ID = statement.ID;
			LeftValue = statement.LeftValue.ID;
			RightValue = statement.RightValue.ID;
			ComparisonSign = statement.ComparisonSign.ID;
		}

		#endregion

		protected override Statements.ComparisonStatement SaveImplementation(ConceptIdResolver conceptIdResolver)
		{
			return new Statements.ComparisonStatement(
				ID,
				conceptIdResolver.GetConceptById(LeftValue),
				conceptIdResolver.GetConceptById(RightValue),
				conceptIdResolver.GetConceptById(ComparisonSign));
		}
	}
}