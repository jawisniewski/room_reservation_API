using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomBooking.API.Extensions;
using RoomBooking.Application.Common;
using RoomBooking.Application.DTOs.Room;
using RoomBooking.Application.Interfaces.Services;
using RoomBooking.Application.Services;

namespace RoomBooking.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RoomController : Controller
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IRoomAppService _roomAppService;
        public RoomController(ILogger<RoomController> logger, IRoomAppService roomAppService)
        {
            _logger = logger;
            _roomAppService = roomAppService;
        }
        /// <summary>
        /// Creates a new room with the provided details.
        /// </summary>
        /// <param name="createRoomRequest"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> CreateRoom([FromBody] BaseRoomRequest createRoomRequest)
        {
            var result = await _roomAppService.CreateAsync(createRoomRequest);

            return result.ToActionResult();
        }
        /// <summary>
        /// Updates an existing room with the provided details.
        /// </summary>
        /// <param name="updateRoomRequest"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateRoom([FromBody] UpdateRoomRequest updateRoomRequest)
        {
            var result = await _roomAppService.UpdateAsync(updateRoomRequest);

            return result.ToActionResult();
        }
        /// <summary>
        /// Deletes a room by its unique identifier.
        /// </summary>
        /// <param name="roomId"></param>
        /// <response code="200">Room deleted</response>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpDelete("Delete/{roomId}")]
        public async Task<IActionResult> DeleteRoom(Guid roomId)
        {
            var result = await _roomAppService.DeleteAsync(roomId);

            return result.ToActionResult();
        }

        /// <summary>
        /// Gets a list of available rooms based on the specified date range and filters.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="roomFilter"></param>
        /// <response code="200">Available Room list</response>
        /// <response code="400">Validation error</response>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<RoomResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpGet("GetAvailableRooms/{from}/{to}")]
        public async Task<IActionResult> GetAvailableRooms([FromRoute] DateTime from, [FromRoute] DateTime to, [FromQuery] RoomFilters roomFilter)
        {
            var result = await _roomAppService.GetAvailableRoomsAsync(from, to, roomFilter);

            return result.ToActionResult();
        }
    }
}
