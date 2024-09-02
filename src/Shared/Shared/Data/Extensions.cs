using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Data
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMigration<TContext>(this IApplicationBuilder app) where TContext : DbContext
        {
            MigrateDatabaseAsync<TContext>(app.ApplicationServices).GetAwaiter().GetResult();
            return app;
        }

        private static async Task MigrateDatabaseAsync<TContext>(IServiceProvider app) where TContext : DbContext
        {
            using (IServiceScope scope = app.CreateScope())
            {
                TContext? context = scope.ServiceProvider.GetRequiredService<TContext>();
                await context.Database.MigrateAsync();
            }
        }
    }
}
