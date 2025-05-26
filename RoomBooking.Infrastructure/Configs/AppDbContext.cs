using Microsoft.EntityFrameworkCore;
using RoomBooking.Domain.Entitis.Room;
using RoomBooking.Domain.ValueObjects;
using RoomsReservation.Domain.Entitis;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Infrastructure.Configs
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>(r =>
            {
                r.HasKey(r => r.Id);

                r.OwnsMany(r => r.Equipments, eq =>
                    {
                        eq.WithOwner()
                            .HasForeignKey("RoomId");   
                        eq.OwnsOne(eq => eq.Type); 
                        eq.Property<Guid>("Id");
                        eq.HasKey("Id");
                    });
                r.OwnsOne(b => b.ReservationLimit);
                r.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(300);

                r.HasMany(r => r.Reservations)
                    .WithOne(r => r.Room)
                    .HasForeignKey(r => r.RoomId);

                r.HasIndex(u => u.Name).IsUnique();
            });

            modelBuilder.Entity<Reservation>(r =>
            {
                r.HasKey(r => r.Id);
                r.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                r.Property(r => r.From)
                    .IsRequired();

                r.Property(r => r.To)
                    .IsRequired();

                r.HasOne(r => r.Room)
                    .WithMany(r => r.Reservations)
                    .HasForeignKey(r => r.RoomId);

                r.HasOne(r => r.User)
                    .WithMany(u => u.Reservations)
                    .HasForeignKey(r => r.UserId);
            });
        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
    }

}
