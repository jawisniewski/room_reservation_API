using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(Guid userId, string email);
    }
}
