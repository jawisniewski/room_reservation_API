using RoomBooking.Application.Common;
using RoomBooking.Application.DTOs.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Application.Interfaces.Services
{
    public interface IRoomAppService
    {
        /// <summary>
        /// Creates a new room with the specified parameters.
        /// </summary>
        /// <param name="name">The name of the room.</param>
        /// <param name="capacity">The capacity of the room.</param>
        /// <param name="tableCount">The number of tables in the room.</param>
        /// <param name="roomLayout">The layout of the room.</param>
        /// <param name="equipments">A list of equipments available in the room.</param>
        /// <returns>The unique identifier of the created room.</returns>
        Task<Result<Guid>> CreateAsync(BaseRoomRequest baseRoomRequest);

        /// <summary>
        /// Updates an existing room with the specified parameters.
        /// </summary>
        /// <param name="roomId">The unique identifier of the room to update.</param>
        /// <param name="name">The new name of the room.</param>
        /// <param name="capacity">The new capacity of the room.</param>
        /// <param name="tableCount">The new number of tables in the room.</param>
        /// <param name="roomLayout">The new layout of the room.</param>
        /// <param name="equipments">A new list of equipments available in the room.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        Task<Result> UpdateAsync(UpdateRoomRequest updateRoomRequest);

        /// <summary>
        /// Deletes a room by its unique identifier.
        /// </summary>
        /// <param name="roomId">The unique identifier of the room to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        Task<Result> DeleteAsync(Guid roomId);

        Task<Result<IEnumerable<RoomResponse>>> GetAvailableRoomsAsync(DateTime from, DateTime to, RoomFilters roomFilter);
    }
}
