using DoctorBooking.Application.Interfaces;
using DoctorBooking.Application.Services;
using DoctorBooking.BackgroundServices;
using DoctorBooking.Infrastructure.Data;
using DoctorBooking.Infrastructure.Interfaces;
using DoctorBooking.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DoctorBooking.API.Extention
{
    public static class ServiceRegister
    {
        public static void DatabaseConnectionConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));       
        }

        public static void RegisterDependency(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<ISlotRepository, SlotRepository>();
            builder.Services.AddScoped<ISlotService, SlotService>();
            builder.Services.AddScoped<IClinicRepository, ClinicRepository>();
            builder.Services.AddScoped<IClinicService, ClinicService>();
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IPatientService, PatientService>();


            // Register implementations with unique keys
            builder.Services.AddSingleton<SMSNotification>();
            builder.Services.AddSingleton<EmailNotification>();

            // Create factory delegate
            builder.Services.AddSingleton<Func<string, INotification>>(serviceProvider => key =>
            {
                return key switch
                {
                    "sms" => serviceProvider.GetRequiredService<SMSNotification>(),
                    "email" => serviceProvider.GetRequiredService<EmailNotification>(),
                    _ => throw new KeyNotFoundException()
                };
            });

            //background services
            builder.Services.AddHostedService<AppointmentReminderWorker>();
        }

        public static void RegisterCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(_ => true)
                        .AllowCredentials());
            });
        }
    }
}
