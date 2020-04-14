using System;
using Application.Interfaces;
using Application.Notifications.Models;

namespace Infrastructure
{
    public class NotificationService : INotificationService
    {
        public void Send(Message message)
        {
            Console.Write($"Notification message: {message}");
        }
    }
}