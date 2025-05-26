using Microsoft.Extensions.Logging;
using RoomBooking.Application.Common;
using RoomBooking.Application.DTOs.Reservation;
using RoomBooking.Application.Interfaces.Services;
using RoomBooking.Domain.Exceptions;
using RoomBooking.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Application.Services
{
    public class ReservationAppService : IReservationAppService
    {
        private readonly IRoomReservationService _roomReservationService;
        private readonly ILogger<ReservationAppService> _logger;
        public ReservationAppService(IRoomReservationService roomReservationService, ILogger<ReservationAppService> logger)
        {
            _roomReservationService = roomReservationService;
            _logger = logger;
        }
        public async Task<Result> CancelAsync(Guid reservationId, Guid userId)
        {
            try
            {
                var result = await _roomReservationService.CancelReservation(reservationId, userId);
                if (!result)
                    return Result.Failure("Reservation not found or you do not have permission to cancel it.", Enums.StatusCode.NotFound);

                return Result.Success();
            }
            catch (DomainException ex)
            {
                _logger.LogError(ex, "Domain error occurred while canceling the reservation.");
                return Result.Failure(ex.Message, Enums.StatusCode.UnprocessableEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while canceling the reservation.");
                return Result.Failure("An error occurred while canceling the reservation.", Enums.StatusCode.BadRequest);
            }
        }

        public async Task<Result<Guid>> CreateAsync(BaseReservationRequest reservation, Guid userId)
        {
            try
            {
                var result = await _roomReservationService
                    .ReserveRoom(
                        reservation.RoomId,
                        reservation.From,
                        reservation.To,
                        userId);

                if (result == Guid.Empty)
                {
                    return Result<Guid>.Failure("Failed to create reservation. Please check the room availability and your reservation details.", Enums.StatusCode.UnprocessableEntity);
                }
                return Result<Guid>.Success(result);
            }
            catch (DomainException ex)
            {
                _logger.LogError(ex, "Domain error occurred while creating the reservation.");
                return Result<Guid>.Failure(ex.Message, Enums.StatusCode.UnprocessableEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating the reservation.");
                return Result<Guid>.Failure("An unexpected error occurred while creating the reservation.", Enums.StatusCode.BadRequest);
            }
        }

        public async Task<Result> UpdateAsync(UpdateReservationRequest updateReservation, Guid userId)
        {
            try
            {
                var result = await _roomReservationService
                    .UpdateReservation(
                        updateReservation.Id,
                        updateReservation.RoomId, 
                        updateReservation.From,
                        updateReservation.To,
                        userId);

                if (!result)
                {
                    return Result.Failure("Failed to update reservation. Please check the reservation details.", Enums.StatusCode.UnprocessableEntity);
                }

                return Result.Success();
            }
            catch (DomainException ex)
            {
                _logger.LogError(ex, "Domain error occurred while updating the reservation.");
                return Result.Failure(ex.Message, Enums.StatusCode.UnprocessableEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating the reservation.");
                return Result.Failure("An unexpected error occurred while updating the reservation.", Enums.StatusCode.BadRequest);
            }
        }
    }
}
