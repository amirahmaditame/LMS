using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    public class TeacherRepository : ITeacherRepository
    {
        LearningDBEntities _db;
        public TeacherRepository(LearningDBEntities db)
        {
            _db = db;
        }
        public bool DeleteTeacher(Users teacher)
        {
            try
            {

                _db.Entry(teacher).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteTeacher(int teacher)
        {
            try
            {

                var Teacher = GetTeacherById(teacher);
                DeleteTeacher(teacher);
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

        public IEnumerable<Users> GetAllTeacher()
        {
            return _db.Users.Where(t => t.RoleID == 2);
        }

        public Users GetTeacherById(int teacherId)
        {
            return _db.Users.Find(teacherId);
        }

        public bool InsertTeacher(Users teacher)
        {
            try
            {
                _db.Users.Add(teacher);
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

        public bool UpdateTeacher(Users teacher)
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
    }
}
