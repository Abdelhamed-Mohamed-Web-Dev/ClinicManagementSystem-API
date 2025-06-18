using Domain.Entities;
using Service.Abstraction.NotificationService;
using Service.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.NotificationService
{
	public class NotificationService(IUnitOfWork unitOfWork, IMapper mapper) : INotificationService
	{
		public async Task CreateNotification(Notifications notification)
		{
			await unitOfWork.GetRepository<Notifications, int>().AddAsync(notification);
			await unitOfWork.SaveChangesAsync();
		}

		public async Task<IEnumerable<NotificationsDto>> GetAllNotifications(int? doctorId, int? patientId)
		{
			var notifications = await unitOfWork.GetRepository<Notifications, int>().GetAllAsync(new NotificationSpecifications(doctorId, patientId));
			return mapper.Map<IEnumerable<NotificationsDto>>(notifications);
		}

		public async Task<NotificationsDto> GetNotification(int id)
		{
			var notification = await unitOfWork.GetRepository<Notifications, int>().GetAsync(id);
			return notification == null ?
				throw new NotificationNotFoundException(id) :
				mapper.Map<NotificationsDto>(notification);
		}

		public async Task<string> ReadNotification(int id)
		{
			var notification = await unitOfWork.GetRepository<Notifications, int>().GetAsync(id);
			if (notification == null) throw new NotificationNotFoundException(id);
			notification.IsRead = true;
			unitOfWork.GetRepository<Notifications, int>().Update(notification);
			await unitOfWork.SaveChangesAsync();
			return "Read";
		}
		public async Task<string> DeleteNotification(int id)
		{
			var notification = await unitOfWork.GetRepository<Notifications, int>().GetAsync(id);
			if (notification == null) throw new NotificationNotFoundException(id);
			unitOfWork.GetRepository<Notifications, int>().Delete(notification);
			await unitOfWork.SaveChangesAsync();
			return "Deleted";
		}
	}
}
