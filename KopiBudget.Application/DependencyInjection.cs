using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KopiBudget.Application
{
    public static class DependencyInjection
    {
        #region Public Methods

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblies(
                    Assembly.GetExecutingAssembly()
                );
            });
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

            return services;
        }

        #endregion Public Methods
    }
}