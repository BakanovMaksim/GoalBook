using System;
using System.Threading.Tasks;

using GoalBook.Core.Domain.Entities;
using GoalBook.Infrastructure.Data;
using GoalBook.Infrastructure.Data.Repositories;

using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

namespace GoalBook.Tests.Infrastructure
{
    [TestFixture]
    public class GoalRepositoryTests
    {
        [Test]
        public async Task CreateTest()
        {
            var expected = GetGoal();

            using (var repository = GetGoalRepository())
            {
                await repository.CreateAsync(expected);
                var actual = await repository.GetByIdAsync(expected.Id);

                Assert.AreEqual(expected, actual);
            }
        }

        [Test]
        public async Task UpdateTest()
        {
            var goal = GetGoal();

            using (var repository = GetGoalRepository())
            {
                await repository.CreateAsync(goal);
                goal.Description = "update";

                var condition = await repository.UpdateAsync(goal);

                Assert.IsTrue(condition);
            }
        }

        [Test]
        public async Task DeleteTest()
        {
            var goal = GetGoal();

            using (var repository = GetGoalRepository())
            {
                await repository.CreateAsync(goal);

                var condition = await repository.DeleteAsync(goal.Id);

                Assert.IsTrue(condition);
            }
        }

        private static Goal GetGoal()
        {
            return Goal.Create()
                .SetTitle("test")
                .SetDescription("test")
                .SetDate(DateTime.Now, null)
                .Build();
        }

        private GoalRepository GetGoalRepository()
        {
            return new GoalRepository(GetGoalBookContext());
        }

        private GoalBookContext GetGoalBookContext()
        {
            return new GoalBookContext(GetOptions());
        }

        private DbContextOptions<GoalBookContext> GetOptions()
        {
            return new DbContextOptionsBuilder<GoalBookContext>()
                .UseInMemoryDatabase("GoalBook")
                .Options;
        }
    }
}
