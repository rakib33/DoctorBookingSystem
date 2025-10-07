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
    public class ScheduleSlotConfiguration : IEntityTypeConfiguration<ScheduleSlot>
    {
        public void Configure(EntityTypeBuilder<ScheduleSlot> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.StartTime).IsRequired();
            builder.Property(s => s.EndTime).IsRequired();
            // Prevent overlapping slots
            builder.HasIndex(s => new { s.DoctorId, s.StartTime }).IsUnique();
            // Filtered Index: for fast querying of available slots
            builder.HasIndex(s => new { s.DoctorId, s.IsBooked });
                  
        }
    }

}
