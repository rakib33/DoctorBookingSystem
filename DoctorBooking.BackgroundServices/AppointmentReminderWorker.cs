using DoctorBooking.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBooking.BackgroundServices
{
    /// <summary>
    /// This is singleton service that runs in the background to send appointment reminders.
    /// </summary>
    public class AppointmentReminderWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AppointmentReminderWorker> _log;

        public AppointmentReminderWorker(IServiceProvider serviceProvider, ILogger<AppointmentReminderWorker> log)
        {
            _serviceProvider = serviceProvider;
            _log = log;
        }

        protected override async Task ExecuteAsync(CancellationToken token)
        {
            _log.LogInformation("AppointmentReminderWorker started.");
            while (!token.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();

                // Resolve the factory for INotification first, then invoke it with the required argument.
                var notificationFactory = scope.ServiceProvider.GetRequiredService<Func<string, INotification>>();
                var smsNotification = notificationFactory("sms");
                var emailNotification = notificationFactory("email");

                var bookingService = scope.ServiceProvider.GetRequiredService<IBookingService>();

                var upcoming = await bookingService.GetUpcomingAppointments(DateTime.UtcNow.AddHours(24));
                foreach (var appt in upcoming)
                {
                    _log.LogInformation($"Reminder: appt {appt.Id} for patient {appt.Patient.Email} at {appt.ScheduleSlot.StartTime}");
                    // Hook for email/SMS calls
                   var sms_result = await smsNotification.Send(appt);
                   var email_result = await emailNotification.Send(appt);
                }
                await Task.Delay(TimeSpan.FromMinutes(20), token);
            }
        }
    }

}
