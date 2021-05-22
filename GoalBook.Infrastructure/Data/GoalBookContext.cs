using GoalBook.Core.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace GoalBook.Infrastructure.Data
{
    /// <summary>
    /// Реализует подключение к базе данных.
    /// </summary>
    public class GoalBookContext : DbContext
    {
        /// <summary>
        /// Таблица цели.
        /// </summary>
        public DbSet<Goal> Goal { get; set; }

        /// <summary>
        /// Инициализирует подключение.
        /// </summary>
        /// <param name="options"> Параметр настройки. </param>
        public GoalBookContext(DbContextOptions<GoalBookContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
