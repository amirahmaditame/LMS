using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    public class UserRepository : IUserRepository
    {
        LearningDBEntities _db;
        public UserRepository(LearningDBEntities db)
        {
            _db = db;
        }
        public bool DeleteUser(Users user)
        {
            try
            {

                _db.Entry(user).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteUser(int userId)
        {
            try
            {

                var user = GetUserById(userId);
                DeleteUser(user);
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

        public IEnumerable<Users> GetAllUser()
        {
            return _db.Users.ToList();
        }

        public Users GetUserById(int userId)
        {
            return _db.Users.Find(userId);
        }

        public Users GetUserByUserName(string user)
        {
            return _db.Users.SingleOrDefault(c => c.UserName == user);
        }

        public Teachers_PF GetTeacherByUserNameForProfile(string user)
        {
            var userid = _db.Users.SingleOrDefault(c => c.UserName == user);

            return _db.Teachers_PF.SingleOrDefault(u => u.UserID == userid.UserID);

        }

        public Users GetUserForActiveCode(string activeCode)
        {
            return _db.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
        }

        public Users GetUserForForChangePassword(string oldHashPass, string userName)
        {
            return _db.Users.SingleOrDefault(p => p.Password == oldHashPass && p.UserName == userName);
        }

        public Users GetUserForForgotPassword(string email)
        {
            return _db.Users.SingleOrDefault(u => u.Email == email);
        }

        public Users GetUserForLogin(string email, string hashPassword)
        {
            return _db.Users.SingleOrDefault(u => u.Email == email && u.Password == hashPassword);
        }

        public bool InsertUser(Users user)
        {
            try
            {
                _db.Users.Add(user);
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

        public bool UpdateTeacherProfile(Teachers_PF teacher)
        {
            try
            {
                _db.Entry(teacher).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateUser(Users user)
        {
            try
            {

                _db.Entry(user).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UserExist(string email)
        {
            if (!_db.Users.Any(e => e.Email == email.Trim().ToLower())) return true;

            else return false;

        }

        public User_PF GetUserByUserNameForProfile(string user)
        {
            var userid = _db.Users.SingleOrDefault(c => c.UserName == user);

            return _db.User_PF.SingleOrDefault(u => u.UserID == userid.UserID);
        }

        public bool UpdateUserProfile(User_PF user)
        {
            try
            {
                _db.Entry(user).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
