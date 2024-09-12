using Carter;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Extensions
{
    public static class CarterExtension
    {
        public static IServiceCollection AddCarterWithAssemblies(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddCarter(configurator: config =>
            {

                foreach (Assembly assembly in assemblies)
                {
                    Type[] modules = assembly.GetTypes()
                    .Where(x => x.IsAssignableTo(typeof(ICarterModule))).ToArray();

                    config.WithModules(modules);
                }

            });

            return services;
        }
    }
}
