using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using sag.Persistence.Contexts;

namespace sag.Persistence.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddMappings();
        services.AddDbContext(configuration);
        services.AddRepositories();
    }

    //private static void AddMappings(this IServiceCollection services)
    //{
    //    services.AddAutoMapper(Assembly.GetExecutingAssembly());
    //}

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("sagDbConnection");
        var dbVersion = configuration.GetConnectionString("SagDbVersion");

        services.AddDbContext<SagDbContext>(options =>
            options.UseMySql(connectionString, new MySqlServerVersion(dbVersion),
                builder => builder.MigrationsAssembly("sag.api").EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null
                ))
        );
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        /*services
            .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
            .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
            .AddTransient<IPlayerRepository, PlayerRepository>()
            .AddTransient<IClubRepository, ClubRepository>()
            .AddTransient<IStadiumRepository, StadiumRepository>()
            .AddTransient<ICountryRepository, CountryRepository>();*/
    }
}