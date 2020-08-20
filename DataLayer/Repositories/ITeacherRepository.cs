using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
   public interface ITeacherRepository:IDisposable
    {
        IEnumerable<Users> GetAllTeacher();
        Users GetTeacherById(int teacherId);
        bool InsertTeacher(Users teacher);
        bool UpdateTeacher(Users teacher);
        bool DeleteTeacher(Users teacher);
        bool DeleteTeacher(int teacher);
        void save();
    }
}
