using Microsoft.AspNetCore.Mvc;
using RoomBooking.API.Extensions;
using RoomBooking.Application.DTOs.Auth;
using RoomBooking.Application.Interfaces.Services;
using RoomBooking.Application.Services;

namespace RoomBooking.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest authRequest)
        {
            var result = await _authService.AuthenticateAsync(authRequest);

            return result.ToActionResult();
        }
    }
}
