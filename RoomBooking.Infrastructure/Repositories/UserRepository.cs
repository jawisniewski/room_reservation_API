using Microsoft.EntityFrameworkCore;
using RoomBooking.Infrastructure.Configs;
using RoomBooking.Services.Interfaces.Repositories;
using RoomsReservation.Domain.Entitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<User> _users;
        public UserRepository(AppDbContext context) {
            _users = context.Set<User>();
        }
        public Task<User?> GetByEmailAsync(string email)
        {
            return _users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
