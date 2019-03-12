using DotNetCore.IoC;
using DotNetCoreArchitecture.Application;
using DotNetCoreArchitecture.Database;
using DotNetCoreArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotNetCoreArchitecture.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogger(configuration);
            services.AddHash();
            services.AddJsonWebToken(Guid.NewGuid().ToString(), TimeSpan.FromHours(12));

            services.AddDbContextMigrate<DatabaseContext>(options => options
                .UseSqlServer(configuration.GetConnectionString(nameof(DatabaseContext)), sql => sql.EnableRetryOnFailure())
                .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
            );

            services.AddMatchingInterfaceOrSelf
            (
                typeof(IUserService).Assembly,
                typeof(IUserDomainService).Assembly,
                typeof(IDatabaseUnitOfWork).Assembly
            );
        }
    }
}
