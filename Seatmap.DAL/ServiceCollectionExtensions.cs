using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.DAL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSeatmapContext(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<SeatMapContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DbConnection"));
            });
    }
}
