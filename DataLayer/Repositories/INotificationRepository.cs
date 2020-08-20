using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
   public interface INotificationRepository:IDisposable
    {
        IEnumerable<Notifications> GetAllnotificationsByUser(string username,string q);
        IEnumerable<Notifications> GetAllnotificationsByUserNotRead(string username);
        Notifications FindNotificationsByID(int id);
        bool InsertNotifications(Notifications item);
        bool UpdateNotifications(Notifications item);
        bool DeleteNotifications(Notifications item);
        bool DeleteNotifications(int item);
        void save();
    }
}
