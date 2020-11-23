using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadU.General.Infrastructure.Core
{
  public interface IGenericRepository<T,K> where T : class
  {
    Task<T> GetByIdAsync(K id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<int> AddAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
  }
}