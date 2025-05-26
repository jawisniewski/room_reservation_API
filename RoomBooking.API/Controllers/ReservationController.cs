using Microsoft.AspNetCore.Mvc;
using RoomBooking.API.Extensions;
using RoomBooking.Application.DTOs.Reservation;
using RoomBooking.Application.DTOs.Room;
using RoomBooking.Application.Interfaces.Services;
using RoomBooking.Domain.Entitis.Room;

namespace RoomBooking.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : Controller
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly IReservationAppService _reservationAppService;
        public ReservationController(ILogger<ReservationController> logger, IReservationAppService reservationAppService)
        {
            _logger = logger;
            _reservationAppService = reservationAppService;
        }

        /// <summary>
        /// Creates a new reservation with the provided details.
        /// </summary>
        /// <param name="createRoomRequest"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] BaseReservationRequest reservationRequest)
        {

            var result = await _reservationAppService.CreateAsync(reservationRequest, User.GetUserId());

            return result.ToActionResult();
        }
        /// <summary>
        /// Updates an existing reservation with the provided details.
        /// </summary>
        /// <param name="updateReservationRequest"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateRoom([FromBody] UpdateReservationRequest updateReservationRequest)
        {
            var result = await _reservationAppService.UpdateAsync(updateReservationRequest, User.GetUserId());

            return result.ToActionResult();
        }
        /// <summary>
        /// Deletes a reservation by its unique identifier.
        /// </summary>
        /// <param name="reservationId"></param>
        /// <response code="200">Reservation deleted</response>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpDelete("Delete/{reservationId}")]
        public async Task<IActionResult> DeleteRoom(Guid reservationId)
        {
            var result = await _reservationAppService.CancelAsync(reservationId, User.GetUserId());

            return result.ToActionResult();
        }

    }
}
