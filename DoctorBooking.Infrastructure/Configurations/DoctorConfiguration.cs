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
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name)
                   .IsRequired()
                   .HasMaxLength(200);
            builder.Property(d => d.Specialization)
                   .HasMaxLength(100);
            builder.HasIndex(d => new { d.ClinicId, d.Name });
        }
    }
}
