﻿using System;
using System.Collections.Generic;

namespace Inventor.Core
{
	public interface IContext
	{
		ILanguage Language
		{ get; }

		ICollection<IStatement> Scope
		{ get; }

		IContext Parent
		{ get; }

		ICollection<IContext> ActiveContexts
		{ get; }

		ICollection<IContext> Children
		{ get; }

		Boolean IsSystem
		{ get; }
	}

	public interface ISystemContext : IContext
	{
		IKnowledgeBaseContext Instantiate(IKnowledgeBase knowledgeBase, IStatementRepository statementRepository, IQuestionRepository questionRepository, IAttributeRepository attributeRepository);
	}

	public interface IKnowledgeBaseContext : IContext
	{
		IKnowledgeBase KnowledgeBase
		{ get; }

		IStatementRepository StatementRepository
		{ get; }

		IQuestionRepository QuestionRepository
		{ get; }

		IAttributeRepository AttributeRepository
		{ get; }

		IQuestionProcessingContext CreateQuestionContext(IQuestion question);
	}

	public interface IQuestionProcessingContext : IKnowledgeBaseContext, IDisposable
	{
		IQuestion Question
		{ get; }
	}

	public interface IQuestionProcessingContext<out QuestionT> : IQuestionProcessingContext
		where QuestionT : IQuestion
	{
		new QuestionT Question
		{ get; }
	}
}
