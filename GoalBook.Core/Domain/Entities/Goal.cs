using GoalBook.Core.Domain.Builders;
using GoalBook.SharedKernel;

using System;

namespace GoalBook.Core.Domain.Entities
{
    /// <summary>
    /// Описывает модель цели.
    /// </summary>
    public class Goal : BaseEntity
    {
        /// <summary>
        /// Наименование.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата создания.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Дата завершения.
        /// </summary>
        public DateTime? DateFinished { get; set; }

        public static GoalBuilder Create()
        {
            return new GoalBuilder();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var goal = obj as Goal;

            if (goal == null)
            {
                return false;
            }

            return (Title == goal.Title)
                && (Description == goal.Description)
                && (DateCreated == goal.DateCreated)
                && (DateFinished == goal.DateFinished);
        }
    }
}
