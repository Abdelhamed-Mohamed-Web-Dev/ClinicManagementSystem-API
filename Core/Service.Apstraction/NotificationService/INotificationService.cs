using Domain.Entities;
using Shared.NotificationModels;

namespace Service.Abstraction.NotificationService
{
    public interface INotificationService
	{
		public Task CreateNotification(Notifications notification);
		public Task<NotificationsDto> GetNotification(int id);
		public Task<IEnumerable<NotificationsDto>> GetAllNotifications(int? doctorId, int? patientId);
		public Task<string> ReadNotification(int id);
		public Task<string> DeleteNotification(int id);
	}
}
