using backend.Controllers;
using Microsoft.EntityFrameworkCore;

namespace backend
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options)
        {
            
        }

        /** Use the same class for DB, Biz and HTTP is a bad idea*/
        public DbSet<Question> Questions { get; set; }
    }
}