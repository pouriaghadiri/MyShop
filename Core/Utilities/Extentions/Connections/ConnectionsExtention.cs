using ShoppingSiteApi.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.Core.Utilities.Extentions.Connections
{
    public static class ConnectionsExtention
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = "ConnectionStrings:MyAppDbConnectionts:Development";
                options.UseSqlServer(configuration[connectionString]);
            });
            return services;
        }
    }
}
