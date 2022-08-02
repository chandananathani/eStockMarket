using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StockMarket.Business.Behaviours;
using System.Reflection;

namespace StockMarket.Business
{
    /// <summary>
    /// class for ApplicationServiceRegistration
    /// </summary>
    public static class ApplicationServiceRegistration
    {
        /// <summary>
        /// method for service configurations
        /// </summary>
        /// <param name="services"></param>
        /// <returns>ServiceCollection</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
