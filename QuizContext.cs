using backend.Controllers;
using Microsoft.EntityFrameworkCore;
using backend;
using backend.Models;

namespace backend
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options)
        {
            
        }

        /** Use the same class for DB, Biz and HTTP is a bad idea*/
        public DbSet<Question> Questions { get; set; }

        /** Use the same class for DB, Biz and HTTP is a bad idea*/
        public DbSet<Quiz> Quiz { get; set; }
    }
}