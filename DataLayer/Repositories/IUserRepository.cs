using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
   public interface IUserRepository:IDisposable
    {
        IEnumerable<Users> GetAllUser();
        Users GetUserById(int userId);
        Teachers_PF GetTeacherByUserNameForProfile(string user);
        User_PF GetUserByUserNameForProfile(string user);
        Users GetUserByUserName(string user);
        Users GetUserForLogin(string email, string hashPassword);
        Users GetUserForForgotPassword(string email);
        Users GetUserForForChangePassword(string oldHashPass,string userName);
        Users GetUserForActiveCode(string activeCode);
        bool UserExist(string email);
        bool InsertUser(Users user);
        bool UpdateUser(Users user);
        bool UpdateTeacherProfile(Teachers_PF teacher);
        bool UpdateUserProfile(User_PF user);
        bool DeleteUser(Users user);
        bool DeleteUser(int userId);
        void save();
        
    }
}
