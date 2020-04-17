using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly QuizContext _quizContext;
        private readonly ILogger<QuestionsController> _logger;

        public QuestionsController(QuizContext quizContext, ILogger<QuestionsController> logger)
        {
            _logger = logger;
            _quizContext = quizContext;
        }

        private bool UnknownQuiz(Question question) => _quizContext.Quiz.SingleOrDefault(q => q.Id == question.QuizId) == null;

        [HttpPost]
        public async Task<IActionResult> Create(Question question)
        {
            if (UnknownQuiz(question))
                return BadRequest("Quiz unknown");

            if (string.IsNullOrWhiteSpace(question.Text))
                return BadRequest("Question has no content.");

            _quizContext.Add(question);
            await _quizContext.SaveChangesAsync(true);

            Inspect(_quizContext);

            return Ok(question);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Modify(int id, [FromBody]Question question)
        {
            if (UnknownQuiz(question))
                return BadRequest("Quiz unknown");

            if (id != question.Id)
                return BadRequest();

            _quizContext.Entry(question).State = EntityState.Modified;
            await _quizContext.SaveChangesAsync();

            return Ok(question);
        }

        private void Inspect(QuizContext quizContext)
        {
            var ids = string.Join(", ", quizContext.Questions.Select(x => x.Id));
            _logger.LogDebug($"***DB*** has now: {ids} question");
        }


        [HttpGet]
        public IEnumerable<Question> All()
        {
            return _quizContext.Questions;
        }

        [HttpGet("{quizId}")]
        public IEnumerable<Question> QuestionsOf([FromRoute]int quizId)
        {
            return _quizContext.Questions.Where(q => q.QuizId == quizId);
        }
    }

    public class Empty
    {
        public static Empty Instance = new Empty();
    }
}