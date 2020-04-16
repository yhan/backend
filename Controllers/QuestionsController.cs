using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly QuizContext _quizContext;
        private readonly ILogger<QuestionsController> _logger;

        public QuestionsController(QuizContext quizContext, ILogger<QuestionsController> logger)
        {
            _logger = logger;
            _quizContext = quizContext;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(Question question)
        {
            _quizContext.Add(question);
            await _quizContext.SaveChangesAsync(true);

            Inspect(_quizContext);

            return Ok(question);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Modify(int id, [FromBody]Question question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }

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
    }

    public class Empty
    {
        public static Empty Instance = new Empty();
    }
}