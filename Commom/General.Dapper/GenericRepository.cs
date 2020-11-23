using System.Collections.Generic;
using System.Threading.Tasks;
using CadU.General.Infrastructure.Core;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;

namespace CadU.General.Dapper
{
  public class GenericRepository<T, K> : IGenericRepository<T, K> where T : class
  {

    private IConfiguration _configuration;
    private NpgsqlConnection _connection;

    protected IDbConnection Connection => _connection as IDbConnection;
    public GenericRepository(IConfiguration configuration)
    {
      _configuration = configuration;
      _connection = new NpgsqlConnection(_configuration.GetConnectionString("CadU"));

    }
    public async Task<int> AddAsync(T entity)
    {
      return await _connection.InsertAsync(entity);
    }

    public async Task<bool> DeleteAsync(T entity)
    {
      return await _connection.DeleteAsync(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
      return await _connection.GetAllAsync<T>();
    }

    public async Task<T> GetByIdAsync(K id)
    {
      return await _connection.GetAsync<T>(id);
    }

    public async Task<bool> UpdateAsync(T entity)
    {
      return await _connection.UpdateAsync(entity);
    }
  }
}
