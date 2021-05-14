using System;

namespace GoalBook.SharedKernel
{
    /// <summary>
    /// Предоставляет базовое взаимодействие с сущностью.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public virtual Guid Id { get; set; }
    }
}
