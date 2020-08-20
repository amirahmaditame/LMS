using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Repositories;

namespace DataLayer.Services
{
    public class SubCoursesRepository : ISubCoursesRepository
    {
        LearningDBEntities _db;
        public SubCoursesRepository(LearningDBEntities db)
        {
            _db = db;
        }

        public bool DeleteSubCourses(SubCourse item)
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

        public bool DeleteSubCourses(int item)
        {
            try
            {
                var SubCourses = FindSubCoursesByID(item);
                DeleteSubCourses(SubCourses);
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

        public SubCourse FindSubCoursesByID(int id)
        {
            return _db.SubCourse.Find(id);
        }

        public IEnumerable<SubCourse> GetAllSubCourses()
        {
            return _db.SubCourse;
        }

        public IEnumerable<SubCourse> GetAllSubCoursesByID(int item)
        {
            return _db.SubCourse.Where(s => s.CourseID == item).ToList();
        }

        public bool InsertSubCourses(SubCourse item)
        {
            try
            {
                _db.SubCourse.Add(item);
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

        public bool UpdateSubCourses(SubCourse item)
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
