﻿using System;
using System.Xml.Serialization;

namespace AabSemantics.Extensions.WPF.Localization
{
	public interface ILanguageMainForm
	{
		String Title
		{ get; }

		String CreateNew
		{ get; }

		String Load
		{ get; }

		String Save
		{ get; }

		String SaveAs
		{ get; }

		String CreateTest
		{ get; }

		String DescribeKnowledge
		{ get; }

		String CheckKnowledge
		{ get; }

		String AskQuestion
		{ get; }

		String SelectLanguage
		{ get; }

		String ContextMenuRename
		{ get; }

		String ContextMenuKnowledgeAdd
		{ get; }

		String ContextMenuKnowledgeEdit
		{ get; }

		String ContextMenuKnowledgeDelete
		{ get; }

		String SavePromt
		{ get; }

		String SaveTitle
		{ get; }
	}

	[XmlType]
	public class LanguageMainForm : ILanguageMainForm
	{
		#region Properties

		[XmlElement]
		public String Title
		{ get; set; }

		[XmlElement]
		public String CreateNew
		{ get; set; }

		[XmlElement]
		public String Load
		{ get; set; }

		[XmlElement]
		public String Save
		{ get; set; }

		[XmlElement]
		public String SaveAs
		{ get; set; }

		[XmlElement]
		public String CreateTest
		{ get; set; }

		[XmlElement]
		public String DescribeKnowledge
		{ get; set; }

		[XmlElement]
		public String CheckKnowledge
		{ get; set; }

		[XmlElement]
		public String AskQuestion
		{ get; set; }

		[XmlElement]
		public String SelectLanguage
		{ get; set; }

		[XmlElement]
		public String ContextMenuRename
		{ get; set; }

		[XmlElement]
		public String ContextMenuKnowledgeAdd
		{ get; set; }

		[XmlElement]
		public String ContextMenuKnowledgeEdit
		{ get; set; }

		[XmlElement]
		public String ContextMenuKnowledgeDelete
		{ get; set; }

		[XmlElement]
		public String SavePromt
		{ get; set; }

		[XmlElement]
		public String SaveTitle
		{ get; set; }

		#endregion

		internal static LanguageMainForm CreateDefault()
		{
			return new LanguageMainForm
			{
				Title = "Auxiliary tool \"Inventor\"",
				CreateNew = "Create new semantic network",
				Load = "Open...",
				Save = "Save",
				SaveAs = "Save As...",
				CreateTest = "Create test semantic network",
				DescribeKnowledge = "Describe all knowledge...",
				CheckKnowledge = "Check consistency of knowledge...",
				AskQuestion = "Ask question...",
				SelectLanguage = "Language:",
				ContextMenuRename = "Rename...",
				ContextMenuKnowledgeAdd = "Add...",
				ContextMenuKnowledgeEdit = "Edit...",
				ContextMenuKnowledgeDelete = "Delete",
				SavePromt = "File has been modified. Save changes?",
				SaveTitle = "Saving changes",
			};
		}
	}
}