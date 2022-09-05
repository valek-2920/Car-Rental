using LastTest.Application.Contracts.Data;
using LastTest.DataAccess.Repository.UnitOfWork.Extensions;
using LastTest.Domain.Models.DataModels;
using LastTest.Infrastructure.Data;
using LastTest.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LastTest.Infrastructure
{
    public static class Injection
    {
        public static IServiceCollection RegisterInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(configuration.GetConnectionString("Default")))
                     .AddUnitOfWork<ApplicationDbContext>()
                     .AddRepository<Car, CarRepository>()
                     .AddRepository<ReservationStatus, StatusRepository>();


            services.AddScoped<IApplicationDbContext>
               (options => options.GetService<ApplicationDbContext>());

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>
                    (TokenOptions.DefaultProvider);

            return services;
        }
    }
}
