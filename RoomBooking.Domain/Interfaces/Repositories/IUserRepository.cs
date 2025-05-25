using RoomsReservation.Domain.Entitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Services.Interfaces.Repositories
{
    public interface IUserRepository
    {
       public Task<User?> GetByEmailAsync(string email);
    }
}
