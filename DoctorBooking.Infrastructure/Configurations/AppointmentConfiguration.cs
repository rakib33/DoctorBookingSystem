using DoctorBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBooking.Infrastructure.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.CreatedDate).IsRequired();

            builder.Property(a => a.Status)
                   .HasConversion<int>()
                   .IsRequired();

            builder.HasOne(a => a.ScheduleSlot)
                   .WithOne(s => s.Appointment)
                   .HasForeignKey<Appointment>(a => a.ScheduleSlotId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Patient)
                   .WithMany(p => p.Appointments)
                   .HasForeignKey(a => a.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Enforce one appointment per slot
            builder.HasIndex(a => a.ScheduleSlotId).IsUnique();

            // Fast retrieval of appointments per patient
            builder.HasIndex(a => a.PatientId);
        }
    }

}
