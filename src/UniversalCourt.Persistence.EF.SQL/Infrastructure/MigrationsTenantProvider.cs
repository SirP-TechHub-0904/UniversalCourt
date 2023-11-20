using UniversalCourt.Infrastructure.TenantSupport;

namespace UniversalCourt.Persistence.EF.SQL.Infrastructure;

public class MigrationsTenantProvider : ITenantProvider
{
    public string? CurrentTenant => null;

    public string DbSchemaName => "dbo"; 

    public string ConnectionString => "Persist Security Info=True;Integrated Security=true;Server=JSS\\SQLEXPRESS;Database=universaldemo2;";

    public IDisposable BeginScope(string tenant)
    {
        throw new NotImplementedException();
    }
}
