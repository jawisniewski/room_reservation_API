using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomBooking.API.Extensions;
using RoomBooking.Application.DTOs.Room;
using RoomBooking.Application.Interfaces.Services;
using RoomBooking.Application.Services;

namespace RoomBooking.API.Controllers
{
    [Authorize]
    public class RoomController : Controller
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IRoomAppService _roomAppService;
        public RoomController(ILogger<RoomController> logger , IRoomAppService roomAppService) {
            _logger = logger;
            _roomAppService = roomAppService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateRoom([FromBody] BaseRoomRequest createRoomRequest)
        {
            var result = await _roomAppService.CreateAsync(createRoomRequest);

            return result.ToActionResult();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateRoom([FromBody] UpdateRoomRequest updateRoomRequest)
        {
            var result = await _roomAppService.UpdateAsync(updateRoomRequest);
            return result.ToActionResult();
        }

        [HttpDelete("Delete/{roomId}")]
        public async Task<IActionResult> DeleteRoom(Guid roomId)
        {
            var result = await _roomAppService.DeleteAsync(roomId);
            return result.ToActionResult();
        }
    }
}
