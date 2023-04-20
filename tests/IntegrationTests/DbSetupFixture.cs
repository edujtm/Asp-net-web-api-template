using AutoMapper;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository;
using Respawn;
using Respawn.Graph;

namespace IntegrationTests
{

    [SetUpFixture]
    public class DbSetupFixture
    {
        const string imageName = "mcr.microsoft.com/mssql/server:2019-latest";
        public static string? ConnString { get; private set; }

        private static Respawner _checkpoint = null!;
        public static IMapper Mapper { get; private set; }

        private IContainer? _container;

        public static RepositoryContext? Context { get; private set; }

        [OneTimeSetUp]
        public async Task SetupDbContainer()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<FakeCustomer.FakeCustomerProfile>());
            Mapper = config.CreateMapper();

            TestContext.Error.WriteLine("Creating database container...");
            _container = new ContainerBuilder()
              .WithImage(imageName)
              .WithEnvironment("SA_PASSWORD", "root@123")
              .WithEnvironment("ACCEPT_EULA", "Y")
              .WithEnvironment("MSSQL_PID", "Developer")
              .WithPortBinding(1433, false)
              // The MSSQL container responds to docker before it's actually ready for connections
              // So we need to wait for this message to be logged. See: https://github.com/microsoft/mssql-docker/issues/625
              .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged(".*The tempdb database has .*"))
              .Build();

            await _container.StartAsync();

            TestContext.Error.WriteLine("Reading configuration...");
            var configuration = new ConfigurationBuilder()
              .AddJsonFile("testsettings.json")
              .AddEnvironmentVariables()
              .Build();

            var connections = configuration.GetSection("ConnectionStrings");
            ConnString = connections.GetValue<string>("TestDB") ?? "";

            TestContext.Error.WriteLine("Creating context...");
            var options = new DbContextOptionsBuilder<RepositoryContext>()
              .UseSqlServer(ConnString)
              .Options;

            Context = new RepositoryContext(options);
            Context.Database.EnsureCreated();

            _checkpoint = Respawner.CreateAsync(ConnString, new RespawnerOptions
            {
                TablesToIgnore = new Table[] { "__EFmigrationsHistory" }
            }).GetAwaiter().GetResult();
        }

        public static async Task ResetState()
        {
            await _checkpoint.ResetAsync(ConnString!);
        }

        [OneTimeTearDown]
        public async Task Teardown()
        {
            Context?.Database.EnsureDeleted();
            Context?.Dispose();

            await _container!.StopAsync();
        }
    }
}