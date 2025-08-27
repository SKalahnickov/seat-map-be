using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seatmap.Common.Utils
{
    public static class EnvironmentHelper
    {
        public static bool IsDevelopmentDebug =>
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "DevelopmentDebug";
    }
}
