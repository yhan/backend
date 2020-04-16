using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly ILogger<QuestionsController> _logger;

        public QuestionsController(ILogger<QuestionsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public Answer Accept(Question question)
        {
            _logger.LogDebug($"Received: {question.Text}");
            return new Answer();
        }

    }

    public class Answer
    {
        public string Text { get; } = "UNIVERSAL RESPONSE :)";
    }

    public class Question
    {
        public string Text { get; set; }
    }
}