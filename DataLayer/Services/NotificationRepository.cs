﻿using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Management;

namespace DataLayer.Services
{
    public class NotificationRepository : INotificationRepository
    {
        LearningDBEntities _db;
        public NotificationRepository(LearningDBEntities db)
        {
            _db = db;
        }
        public bool DeleteNotifications(Notifications item)
        {
            try
            {
                _db.Entry(item).State = System.Data.Entity.EntityState.Deleted;

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteNotifications(int item)
        {

            try
            {
                var items = FindNotificationsByID(item);
                DeleteNotifications(items);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public Notifications FindNotificationsByID(int id)
        {
            return _db.Notifications.Find(id);
        }

        public IEnumerable<Notifications> GetAllnotificationsByUser(string username, string q = "")
        {
            List<Notifications> Notification = new List<Notifications>();
            var UserNotification = _db.Notifications.Where(n => n.Users.UserName == username).ToList();
            Notification.AddRange(UserNotification.Where(p => p.NotificationTitle.Contains(q) || p.NotificationText.Contains(q)));

            return Notification;
        }

        public IEnumerable<Notifications> GetAllnotificationsByUserNotRead(string username)
        {
            return _db.Notifications.Where(n => n.Users.UserName == username && n.IsRead == false);
        }

        public bool InsertNotifications(Notifications item)
        {

            try
            {
                _db.Notifications.Add(item);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void save()
        {
            _db.SaveChanges();
        }

        public bool UpdateNotifications(Notifications item)
        {
            try
            {
                _db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
