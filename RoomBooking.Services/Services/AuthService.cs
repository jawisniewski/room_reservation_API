using Microsoft.Extensions.Logging;
using RoomBooking.Application.Common;
using RoomBooking.Application.DTOs.Auth;
using RoomBooking.Application.Interfaces;
using RoomBooking.Application.Interfaces.Services;
using RoomBooking.Services.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHasher _hasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ILogger<AuthService> _logger;
        public AuthService(IUserRepository userRepository, IHasher hasher, IJwtTokenGenerator jwtTokenGenerator, ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _hasher = hasher;
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;
        }

        public async Task<Result<AuthResponse>> AuthenticateAsync(AuthRequest request)
        {
            try
            {
                var user = await _userRepository.GetByEmailAsync(request.Email);

                if (user == null)
                    return GetFailureResult();

                if (_hasher.Verify(request.Password, user.Password))
                {
                    var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email);

                    if (string.IsNullOrEmpty(token))
                        return GetFailureResult();

                    return Result<AuthResponse>.Success(new AuthResponse() { Token = token });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while authenticating user with email {Email}", request.Email);            
            }

            return GetFailureResult();
        }

        private Result<AuthResponse> GetFailureResult() =>
            Result<AuthResponse>.Failure("Invalid credentials.", Enums.StatusCode.Unauthorized);
    }
}
