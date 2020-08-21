using DataLayer.Repositories;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
   public class HomeRepository : IHomeRepository
    {
        LearningDBEntities _db;
        public HomeRepository(LearningDBEntities db)
        {
            _db = db;
        }
        public IEnumerable<CourseListHomeViewModel> GetAllCourses()
        {
            var AllCourses = _db.Courses.Select(x => new CourseListHomeViewModel()
            {
                CourseName = x.CourseName,
                ImageName = x.ImageName,
                NumberOfStudents =x.NumberOfStudents.Value,
                Price = x.Price.Value,
                SubCourseCount = x.SubCourse.Count(),
                TeacherImageName = x.Users.Teachers_PF.Select(y => y.ImageName).ToList().FirstOrDefault(),
                Teachername = x.Users.UserName,
                LastUpdate = x.LastUpdate

                
            }).OrderByDescending(l=>l.LastUpdate).ToList();

            return AllCourses;
        }
    }
}
