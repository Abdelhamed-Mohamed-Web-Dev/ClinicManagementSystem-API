using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.NotificationModels;

namespace Service.MappingProfiles
{
    public class AdminProfiles : Profile
    {
        public AdminProfiles() {
        
            CreateMap<Notifications,NotificationsDto>().ReverseMap();
        
        }
    }
}
