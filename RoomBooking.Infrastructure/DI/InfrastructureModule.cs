using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RoomBooking.Application.Interfaces;
using RoomBooking.Infrastructure.Auth;
using RoomBooking.Infrastructure.Configs;
using RoomBooking.Infrastructure.Repositories;
using RoomBooking.Services.Interfaces.Repositories;
using RoomsReservation.Domain.Interfaces.Repositories;
namespace RoomBooking.Infrastructure.DI
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastracture(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IRoomQueryRepository, RoomRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IHasher, Hasher>();

            return services;
        }
    }
}
