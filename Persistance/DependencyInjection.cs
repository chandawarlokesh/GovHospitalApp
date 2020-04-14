using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            // services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("GovHospitalApp"));

            services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(configuration.GetValue<string>("Database:ConnectionString"))
            );

            services.AddScoped<IAppDbRepository, AppDbRepository>();
            return services;
        }
    }
}