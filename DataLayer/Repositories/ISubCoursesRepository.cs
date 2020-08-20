using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface ISubCoursesRepository : IDisposable
    {
        IEnumerable<SubCourse> GetAllSubCourses();
        IEnumerable<SubCourse> GetAllSubCoursesByID(int item);
        SubCourse FindSubCoursesByID(int id);
        bool InsertSubCourses(SubCourse item);
        bool UpdateSubCourses(SubCourse item);
        bool DeleteSubCourses(SubCourse item);
        bool DeleteSubCourses(int item);
        void save();
    }
}
