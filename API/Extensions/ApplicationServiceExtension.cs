using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationSevices(this IServiceCollection services,
                 IConfiguration config)
    {
     services.AddScoped<ITokenService,TokenService>();
    services.AddDbContext<DataContext>(opt =>
    {
      opt.UseSqlite(config.GetConnectionString("Defaultconnection"));
    }
    );
    services.AddCors();
    return services;
     }
    }
}