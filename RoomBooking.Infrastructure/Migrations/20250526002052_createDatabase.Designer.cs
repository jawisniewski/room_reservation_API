﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RoomBooking.Infrastructure.Configs;

#nullable disable

namespace RoomBooking.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250526002052_createDatabase")]
    partial class createDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RoomBooking.Domain.Entitis.Room.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("Layout")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("TableCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("RoomsReservation.Domain.Entitis.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("From")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("To")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("RoomsReservation.Domain.Entitis.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RoomBooking.Domain.Entitis.Room.Room", b =>
                {
                    b.OwnsMany("RoomBooking.Domain.ValueObjects.Equipment", "Equipments", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Quantity")
                                .HasColumnType("int");

                            b1.Property<Guid>("RoomId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Id");

                            b1.HasIndex("RoomId");

                            b1.ToTable("Equipment");

                            b1.WithOwner()
                                .HasForeignKey("RoomId");

                            b1.OwnsOne("RoomBooking.Domain.ValueObjects.EquipmentType", "Type", b2 =>
                                {
                                    b2.Property<Guid>("EquipmentId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("EquipmentId");

                                    b2.ToTable("Equipment");

                                    b2.WithOwner()
                                        .HasForeignKey("EquipmentId");
                                });

                            b1.Navigation("Type")
                                .IsRequired();
                        });

                    b.OwnsOne("RoomBooking.Domain.ValueObjects.RoomReservationLimit", "ReservationLimit", b1 =>
                        {
                            b1.Property<Guid>("RoomId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("MaxTime")
                                .HasColumnType("int");

                            b1.Property<int>("MinTime")
                                .HasColumnType("int");

                            b1.HasKey("RoomId");

                            b1.ToTable("Rooms");

                            b1.WithOwner()
                                .HasForeignKey("RoomId");
                        });

                    b.Navigation("Equipments");

                    b.Navigation("ReservationLimit");
                });

            modelBuilder.Entity("RoomsReservation.Domain.Entitis.Reservation", b =>
                {
                    b.HasOne("RoomBooking.Domain.Entitis.Room.Room", "Room")
                        .WithMany("Reservations")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RoomsReservation.Domain.Entitis.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RoomBooking.Domain.Entitis.Room.Room", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("RoomsReservation.Domain.Entitis.User", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
