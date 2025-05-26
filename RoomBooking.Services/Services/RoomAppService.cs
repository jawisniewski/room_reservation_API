using AutoMapper;
using Microsoft.Extensions.Logging;
using RoomBooking.Application.Common;
using RoomBooking.Application.DTOs.Room;
using RoomBooking.Application.Enums;
using RoomBooking.Application.Interfaces.Services;
using RoomBooking.Domain.Exceptions;
using RoomBooking.Domain.Interfaces.Services;
using RoomBooking.Domain.ValueObjects;
using RoomBooking.Services.Interfaces.Repositories;
using RoomsReservation.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Application.Services
{
    public class RoomAppService : IRoomAppService
    {
        private readonly IRoomService _roomService;
        private readonly IRoomQueryRepository _roomRepository;
        private readonly ILogger<RoomAppService> _logger;
        private readonly IMapper _mapper;
        public RoomAppService(IRoomService roomService, ILogger<RoomAppService> logger, IRoomQueryRepository roomRepository, IMapper mapper)
        {
            _roomService = roomService;
            _logger = logger;
            _roomRepository = roomRepository;
            _mapper = mapper;
        }
        public async Task<Result<Guid>> CreateAsync(BaseRoomRequest createRoomRequest)
        {
            try
            {
                var equipments = createRoomRequest.Equipments?
                    .Select(e => new Equipment(EquipmentType.FromName(e.Name), e.Quantity)).ToList() ?? new List<Equipment>();
                var roomReservationLimit = createRoomRequest.ReservationLimit != null ?
                    new RoomReservationLimit(createRoomRequest.ReservationLimit.MinTime, createRoomRequest.ReservationLimit.MaxTime)
                    : null;

                var result = await _roomService.CreateAsync(createRoomRequest.Name, createRoomRequest.Capacity,
                    createRoomRequest.TableCount, createRoomRequest.Layout, equipments, roomReservationLimit!).ConfigureAwait(false);

                return Result<Guid>.Success(result);
            }
            catch (DomainException ex)
            {
                _logger.LogError(ex, "Domain error occurred while creating the room.");
                return Result<Guid>.Failure(ex.Message, Enums.StatusCode.UnprocessableEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating the room.");
                return Result<Guid>.Failure("An unexpected error occurred while creating the room.", Enums.StatusCode.BadRequest);
            }
        }
        public async Task<Result> UpdateAsync(UpdateRoomRequest updateRoomRequest)
        {
            try
            {
                var equipments = updateRoomRequest.Equipments?
                    .Select(e => new Equipment(EquipmentType.FromName(e.Name), e.Quantity)).ToList() ?? new List<Equipment>();

                var roomReservationLimit = updateRoomRequest.ReservationLimit != null ?
                    new RoomReservationLimit(updateRoomRequest.ReservationLimit.MinTime, updateRoomRequest.ReservationLimit.MaxTime)
                    : null;

                var result = await _roomService.UpdateAsync(updateRoomRequest.Id, updateRoomRequest.Name, updateRoomRequest.Capacity,
                    updateRoomRequest.TableCount, updateRoomRequest.Layout, equipments, roomReservationLimit!).ConfigureAwait(false);

                if (result == false)
                {
                    return Result.Failure("Room cannot be updated.", Enums.StatusCode.BadRequest);
                }

                return Result.Success();
            }
            catch (DomainException ex)
            {
                _logger.LogError(ex, "Domain error occurred while updating the room.");
                return Result.Failure(ex.Message, Enums.StatusCode.UnprocessableEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating the room.");
                return Result.Failure("An unexpected error occurred while updating the room.", Enums.StatusCode.BadRequest);
            }
        }
        public async Task<Result> DeleteAsync(Guid roomId)
        {
            try
            {
                if (!await _roomService.DeleteAsync(roomId).ConfigureAwait(false))
                    return Result.Failure("Room cannot be deleted.", StatusCode.BadRequest);

                return Result.Success();
            }
            catch (DomainException ex)
            {
                _logger.LogError(ex, "Domain error occurred while deleting the room.");
                return Result.Failure(ex.Message, StatusCode.UnprocessableEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting the room.");
                return Result.Failure("An unexpected error occurred while deleting the room.", Enums.StatusCode.BadRequest);
            }
        }

        public async Task<Result<IEnumerable<RoomResponse>>> GetAvailableRoomsAsync(DateTime from, DateTime to, RoomFilters roomFilter)
        {
            try
            {
                var rooms = await _roomRepository.GetAvailableRoomsAsync(from, to, roomFilter).ConfigureAwait(false);

                var roomsDto =_mapper.Map<IEnumerable<RoomResponse>>(rooms);

                return Result<IEnumerable<RoomResponse>>.Success(roomsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching available rooms.");
                return Result<IEnumerable<RoomResponse>>.Failure("An unexpected error occurred while fetching available rooms.", Enums.StatusCode.BadRequest);
            }
        }
    }
}
