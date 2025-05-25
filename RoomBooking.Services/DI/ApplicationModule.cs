using Microsoft.Extensions.DependencyInjection;
using RoomBooking.Application.Interfaces.Services;
using RoomBooking.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Application.DI
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoomAppService, RoomAppService>();
            return services;
        }
    }
}
