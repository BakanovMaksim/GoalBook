using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoalBook.SharedKernel
{
    public interface IRepository<T>
        where T: BaseEntity, new()
    {
        IEnumerable<T> GetAll();

        Task<T> GetByIdAsync(Guid id);

        Task CreateAsync(T record);

        Task<bool> UpdateAsync(T record);

        Task<bool> DeleteAsync(Guid id);
    }
}
