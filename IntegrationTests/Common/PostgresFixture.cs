using Xunit;
using Testcontainers.PostgreSql;

namespace IntegrationTests.Common;

public class PostgresFixture : IAsyncLifetime
{
    public PostgreSqlContainer Container { get; private set; } = null!;

    public string ConnectionString => Container.GetConnectionString();
    
    public async Task InitializeAsync()
    {
        Container = new PostgreSqlBuilder()
            .WithDatabase("testdb")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .WithImage("postgres:11.22-alpine3.19") // твоя версия Postgres
            .WithCleanUp(true) // удалять контейнер после остановки
            .Build();

        await Container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await Container.DisposeAsync();
    }
}