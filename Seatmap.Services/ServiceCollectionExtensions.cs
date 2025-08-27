using Microsoft.Extensions.DependencyInjection;
using Seatmap.Services.Clients;
using Seatmap.Services.Interfaces;
using Seatmap.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSeatmapServices(this IServiceCollection services)
        {
            return services.AddScoped<ISeatmapEditingService, SeatmapEditingService>()
                .AddScoped<ISeatmapSelectionService, SeatmapSelectionService>()
                .AddScoped<IExternalDataClient, SampleExternalDataClient>();
        }
    }
}
