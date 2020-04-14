using Application.Notifications.Models;

namespace Application.Interfaces
{
    public interface INotificationService
    {
        void Send(Message message);
    }
}