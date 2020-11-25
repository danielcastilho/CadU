using System.Collections.Generic;
using System.Threading.Tasks;
using CadU.General.Infrastructure.Core;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Linq;
using System.Data;
using System;
using Dapper.FastCrud;
using Dapper.FastCrud.Configuration;
//using Dapper.Contrib.Extensions;

namespace CadU.General.DapperORM
{

  public class GenericRepository<T, K> : IGenericRepository<T, K> where T : class, new()
  {

    private IConfiguration _configuration;
    private NpgsqlConnection _connection;

    protected IDbConnection Connection => _connection as IDbConnection;
    public GenericRepository(IConfiguration configuration)
    {
      _configuration = configuration;
      _connection = new NpgsqlConnection(_configuration.GetConnectionString("CadU"));
    }
    static GenericRepository()
    {
      //SqlMapperExtensions.TableNameMapper = (type) => $"\"{type.Name}\"";
      OrmConfiguration.DefaultDialect = SqlDialect.PostgreSql;
      OrmConfiguration.Conventions = new CadU.General.Dapper.Configurations.CadUOrmConvention();
    }

    /// <summary>
    /// Add an entity in database
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<int> AddAsync(T entity)
    {
      await _connection.InsertAsync<T>(entity);
      return 1;
    }

    public virtual async Task<bool> DeleteAsync(T entity)
    {
      return await _connection.DeleteAsync(entity);
    }

    public virtual Task<IEnumerable<T>> GetAllAsync()
    {
      return _connection.FindAsync<T>();
    }

    public virtual async Task<T> GetByIdAsync(K id)
    {
      return (await _connection.FindAsync<T>((x)=>x.Where($"Id=@Id").WithParameters(new {Id = id}).Top(1))).FirstOrDefault();
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
      return await _connection.UpdateAsync(entity);
    }

  }
}
