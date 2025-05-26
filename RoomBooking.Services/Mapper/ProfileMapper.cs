using AutoMapper;
using RoomBooking.Application.DTOs.Equipment;
using RoomBooking.Application.DTOs.Room;
using RoomBooking.Domain.Entitis.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Application.Mapper
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper() { 
            CreateMap<Room, RoomResponse>()
                .ForMember(dest => dest.Equipments, opt => opt.MapFrom(src => src.Equipments.Select(e => new EquipmentDto
                {
                    Name = e.Type.Name,
                    Quantity = e.Quantity
                }).ToList()))
                .ForMember(dest => dest.ReservationLimit, opt => opt.MapFrom(src => src.ReservationLimit != null ? new RoomReservationLimitDto
                {
                    MinTime = src.ReservationLimit.MinTime,
                    MaxTime = src.ReservationLimit.MaxTime
                } : null));
        }
    }
}
