using GoalBook.Core.Domain.Entities;

using System;

namespace GoalBook.Core.Domain.Builders
{
    /// <summary>
    /// Собирает данные объекта цели.
    /// </summary>
    public class GoalBuilder
    {
        private readonly Goal _goal;

        /// <summary>
        /// Инициализирует поля.
        /// </summary>
        public GoalBuilder()
        {
            _goal = new();
        }

        /// <summary>
        /// Присваивает идентификатор.
        /// </summary>
        /// <param name="id"> Параметр идентификатор. </param>
        /// <returns> Объект строителя. </returns>
        public GoalBuilder SetId(Guid id)
        {
            _goal.Id = id;
            return this;
        }

        /// <summary>
        /// Присваивает наименование.
        /// </summary>
        /// <param name="title"> Параметр наименование. </param>
        /// <returns> Объект строителя. </returns>
        public GoalBuilder SetTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("Передано пустое значение.", nameof(title));
            }

            _goal.Title = title;
            return this;
        }

        /// <summary>
        /// Присваивает описание.
        /// </summary>
        /// <param name="description"> Параметр описание. </param>
        /// <returns> Объект строителя. </returns>
        public GoalBuilder SetDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException("Передано пустое значение.", nameof(description));
            }

            _goal.Description = description;
            return this;
        }

        /// <summary>
        /// Присваивает даты создания и окончания.
        /// </summary>
        /// <param name="dateCreated"> Параметр дата создания. </param>
        /// <param name="dateFinished"> Параметр дата окончания. </param>
        /// <returns> Объект строителя. </returns>
        public GoalBuilder SetDate(DateTime dateCreated, DateTime? dateFinished)
        {
            if ((dateFinished != null) && (dateFinished < DateTime.Now))
            {
                throw new ArgumentException("Некорректное значение даты окончания.", nameof(dateFinished));
            }

            _goal.DateCreated = dateCreated;
            _goal.DateFinished = dateFinished;
            return this;
        }

        /// <summary>
        /// Возвращает собранный объект цели.
        /// </summary>
        /// <returns> Цель. </returns>
        public Goal Build()
        {
            if (_goal.Id == default)
            {
                _goal.Id = Guid.NewGuid();
            }

            return _goal;
        }
    }
}
