using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Migrations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMigrationContext(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<MigrationsContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DbConnection"));
            });

        public static void MigrateDatabase(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MigrationsContext>();
            var timeout = context.Database.GetCommandTimeout();
            context.Database.SetCommandTimeout(TimeSpan.FromMinutes(5));
            context.Database.Migrate();
            context.Database.SetCommandTimeout(timeout);
        }
    }
}
