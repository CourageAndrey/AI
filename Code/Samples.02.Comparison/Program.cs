﻿using System;
using System.Collections.Generic;

using Inventor.Core;
using Inventor.Core.Attributes;
using Inventor.Core.Base;
using Inventor.Core.Localization;

namespace Samples._02.Comparison
{
	class Program
	{
		static void Main(string[] args)
		{
			#region Preparation section

			Console.WriteLine("This sample demonstrates how to compare concepts.");
			Console.WriteLine("It works with usual numbers.");
			Console.WriteLine();

			ISemanticNetwork semanticNetwork = new SemanticNetwork(Language.Default);

			Console.WriteLine();

			#endregion

			#region Define concepts

			var numbers = new List<IConcept>();
			for (int i = 0; i <= 10; i++)
			{
				string n = i.ToString();
				IConcept number = n.CreateConcept().WithAttribute(IsValueAttribute.Value);
				numbers.Add(number);
				semanticNetwork.Concepts.Add(number);
			}

			#endregion

			#region Define statements

			for (int i = 0; i < 5; i++)
			{
				semanticNetwork.DeclareThat(numbers[i]).IsLessThan(numbers[i + 1]);
			}
			for (int i = 10; i > 5; i--)
			{
				semanticNetwork.DeclareThat(numbers[i]).IsGreaterThan(numbers[i - 1]);
			}

			#endregion

			#region Ask questions

			// Now we can work with our Semantic Network and ask questions.
			writeAnswer(
				"Question 1: 0 vs 10?",
				semanticNetwork.Ask().HowCompared(numbers[0], numbers[10])); // There are no explicit knowledge about this.

			writeAnswer(
				"Question 2: 10 vs 0?",
				semanticNetwork.Ask().HowCompared(numbers[10], numbers[0])); // vice versa, but with the same result

			#endregion

			Console.WriteLine();
			Console.WriteLine("Press any key to exit...");
			Console.ReadKey();
		}

		private static void writeAnswer(string title, IAnswer answer)
		{
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
			Console.WriteLine();
			Console.WriteLine(new string('=', 50));
			Console.WriteLine();

			Console.WriteLine(title);

			Console.WriteLine(answer.Description.ToString());

			Console.WriteLine("Explanation:");
			foreach (var statement in answer.Explanation.Statements)
			{
				Console.WriteLine(statement.DescribeTrue());
			}
		}
	}
}
