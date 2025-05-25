using Microsoft.Extensions.DependencyInjection;
using RoomBooking.Domain.Interfaces.Services;
using RoomBooking.Domain.Services;
using RoomsReservation.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Domain.DI
{
    public static class DomainModule
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<IRoomReservationService, RoomReservationService>();
            services.AddScoped<IRoomService, RoomService>();
            return services;
        }
    }
}
