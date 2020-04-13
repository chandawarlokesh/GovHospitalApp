using GovHospitalApp.Core.Application.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace GovHospitalApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>();
            return services;
        }
    }
}
