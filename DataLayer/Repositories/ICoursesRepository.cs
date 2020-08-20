using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.ViewModels;

namespace DataLayer.Repositories
{
    public interface ICoursesRepository:IDisposable
    {
        IEnumerable<CoursesListViewModel> GetAllCourses(string q="");
        Courses GetCourseById(int courseId);
        Users GetTeacherForCourses(string username);
        bool InsertCourse(Courses courses);
        bool UpdateCourse(Courses courses);
        bool DeleteCourse(Courses courses);
        bool DeleteCourse(int courseId);
        void save();
    }
}
