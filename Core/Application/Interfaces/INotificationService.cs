
using GovHospitalApp.Core.Application.Notifications.Models;

namespace GovHospitalApp.Core.Application.Interface
{
    public interface INotificationService
    {
        void Send(Message message);
    }
}
