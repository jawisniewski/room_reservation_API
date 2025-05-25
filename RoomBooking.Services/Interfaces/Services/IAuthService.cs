using RoomBooking.Application.Common;
using RoomBooking.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Application.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<Result<AuthResponse>> AuthenticateAsync(AuthRequest authRequest);
    }
}
