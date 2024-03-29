﻿using Microsoft.AspNetCore.Mvc;

using AabSemantics.Serialization;
using AabSemantics.Serialization.Json;
using AabSemantics.Utils;

namespace AabSemantics.SimpleRestClient.Controllers
{
	[ApiController, Route("[controller]")]
	public class AskQuestionController : ControllerBase
	{
		private readonly ILogger<AskQuestionController> _logger;
		private readonly IDataService _dataService;

		public AskQuestionController(ILogger<AskQuestionController> logger, IDataService dataService)
		{
			_logger = logger.EnsureNotNull(nameof(logger));
			_dataService = dataService.EnsureNotNull(nameof(dataService));
		}

		[HttpGet(Name = "GetAskQuestion")]
		public String Get([FromBody] Question question)
		{
			var semanticNetwork = _dataService.GetSemanticNetwork();

			var conceptsCache = new Dictionary<String, IConcept>();
			foreach (var concept in semanticNetwork.Concepts)
			{
				conceptsCache[concept.ID] = concept;
			}
			var conceptIdResolver = new ConceptIdResolver(conceptsCache);
			var statementIdResolver = new StatementIdResolver(semanticNetwork);

			var deserializedQuestion = question.Save(conceptIdResolver, statementIdResolver);
			var answer = deserializedQuestion.Ask(semanticNetwork.Context);

			var snapshot = Answer.Load(answer, semanticNetwork.Context.Language);

			return snapshot.SerializeToJsonString();
		}
	}
}
