using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFoundExceptions
{
	public class NotificationNotFoundException(int id) :
		NotFoundException($"Notification ({id}) Isn't Found")
	{
	}
}
