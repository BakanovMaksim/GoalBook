using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using GoalBook.Core.Domain.Entities;
using GoalBook.SharedKernel;

using Microsoft.EntityFrameworkCore;

namespace GoalBook.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Предоставляет взаимодействие с таблицей цели.
    /// </summary>
    public class GoalRepository : IRepository<Goal>, IDisposable
    {
        private readonly GoalBookContext _context;

        /// <summary>
        /// Инициализирует поля.
        /// </summary>
        /// <param name="context"> Параметр контекст базы данных. </param>
        public GoalRepository(GoalBookContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("Передана пустая ссылка.", nameof(context));
            }

            _context = context;
        }

        /// <summary>
        /// Возвращает список всех записей.
        /// </summary>
        /// <returns> Список всех записей. </returns>
        public IEnumerable<Goal> GetAll()
        {
            return _context
                .Goal
                .AsNoTracking();
        }

        /// <summary>
        /// Возвращает единственную запись по заданному идентификатору.
        /// </summary>
        /// <param name="id"> Параметр идентификатор. </param>
        /// <returns> Единственная запись. </returns>
        public async Task<Goal> GetByIdAsync(Guid id)
        {
            return await _context
                .Goal
                .FindAsync(id);
        }

        /// <summary>
        /// Добавляет в таблицу новую запись.
        /// </summary>
        /// <param name="entity"> Параметр запись. </param>
        /// <returns></returns>
        public async Task CreateAsync(Goal record)
        {
            if (record == null)
            {
                throw new ArgumentNullException("Передана пустая ссылка.", nameof(record));
            }

            await _context.Goal.AddAsync(record);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновляет данные существующей записи и вовзращает логический результат обновления.
        /// </summary>
        /// <param name="record"> Параметр запись с новыми данными. </param>
        /// <returns> Логический результат. </returns>
        public async Task<bool> UpdateAsync(Goal record)
        {
            if (record == null)
            {
                throw new ArgumentNullException("Передана пустая ссылка.", nameof(record));
            }

            if (await GetByIdAsync(record.Id) == null)
            {
                return false;
            }

            _context.Goal.Update(record);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Удаляет запись по заданному идентификатору и возвращает логический результат удаления.
        /// </summary>
        /// <param name="id"> Параметр идентификатор. </param>
        /// <returns> Логический результат. </returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var goal = await GetByIdAsync(id);

            if (goal == null)
            {
                return false;
            }

            _context.Goal.Remove(goal);
            await _context.SaveChangesAsync();

            return true;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
