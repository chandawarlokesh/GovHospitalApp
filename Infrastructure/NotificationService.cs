using GovHospitalApp.Core.Application.Interface;
using GovHospitalApp.Core.Application.Notifications.Models;
using System;

namespace GovHospitalApp.Infrastructure
{
    public class NotificationService : INotificationService
    {
        public void Send(Message message)
        {
            Console.Write($"Notification message: {message}");
        }
    }
}
