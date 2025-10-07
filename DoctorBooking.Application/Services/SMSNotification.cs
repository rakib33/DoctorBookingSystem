using DoctorBooking.Application.Interfaces;
using DoctorBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorBooking.Application.Services
{
    public class SMSNotification : INotification
    {
        public Task<bool> Send(Appointment apt)
        {
            //send sms
            //configure sms gateway, etc.
            return Task.FromResult(true);
        }
    }  
}
