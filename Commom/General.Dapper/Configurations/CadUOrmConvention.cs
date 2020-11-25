using Dapper.FastCrud.Configuration;
using Dapper.FastCrud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Dapper.FastCrud.Mappings;

namespace CadU.General.Dapper.Configurations
{
  public class CadUOrmConvention : OrmConventions
  {
    public CadUOrmConvention() : base()
    {

    }

    public override string GetTableName(Type entityType)
    {
      return entityType.Name;
    }

    public override SqlDatabaseOptions GetDatabaseOptions(SqlDialect dialect)
    {
      var dbopt = base.GetDatabaseOptions(dialect);
      return dbopt;
    }

    public override IEnumerable<PropertyDescriptor> GetEntityProperties(Type entityType)
    {
      var props = base.GetEntityProperties(entityType);
      return props;
    }

    public override void ConfigureEntityPropertyMapping(PropertyMapping propertyMapping)
    {
      base.ConfigureEntityPropertyMapping(propertyMapping);
      if (propertyMapping.DatabaseColumnName.Equals("Id", StringComparison.InvariantCultureIgnoreCase))
      {
        propertyMapping.SetPrimaryKey(true);
        propertyMapping.SetDatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
      }
    }













  }
}