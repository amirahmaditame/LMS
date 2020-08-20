using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Repositories;
using DataLayer.ViewModels;

namespace DataLayer.Services
{
    public class CoursesRepository : ICoursesRepository
    {
        LearningDBEntities _db;
        public CoursesRepository(LearningDBEntities db)
        {
            _db = db;
        }

        public bool DeleteCourse(Courses courses)
        {
            try
            {

                _db.Entry(courses).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool DeleteCourse(int courseId)
        {
            try
            {
                var course = GetCourseById(courseId);
                DeleteCourse(course);
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
        public IEnumerable<CoursesListViewModel> GetAllCourses(string q = "", int take = 1,int skip=1)
        {
            List<CoursesListViewModel> CoursesList = new List<CoursesListViewModel>();
            var AllCourses = _db.Courses.Select(x => new CoursesListViewModel()
            {
                CourseID = x.CourseID,
                UserID = x.UserID,
                CourseLevel = x.CourseLevel,
                CourseName = x.CourseName,
                CourseStatus = x.CourseStatus,
                LastUpdate = x.LastUpdate,
                NumberOfStudents = x.NumberOfStudents,
                ShortDescription = x.ShortDescription,
                Price = x.Price,
                Text = x.Text,
                ImageName = x.ImageName,
                TeacherImageName = x.Users.Teachers_PF.Select(y=>y.ImageName).ToList().FirstOrDefault(),
                Teachername = x.Users.UserName,
                DiscountValue = x.Discount.Value.Value,
                CategoryName = x.Selected_Category.Select(c=>c.Categories.Name).ToList()
            }).ToList();
            CoursesList.AddRange(AllCourses.Where(p => p.CourseName.Contains(q) || p.Text.Contains(q)));

            return AllCourses.Skip(skip).Take(take).ToList();
        }

        public Courses GetCourseById(int courseId)
        {
            return _db.Courses.Find(courseId);
        }

        public Users GetTeacherForCourses(string username)
        {
            return _db.Users.SingleOrDefault(c=>c.UserName == username);
        }

        public bool InsertCourse(Courses courses)
        {
            try
            {
                _db.Courses.Add(courses);
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

        public bool UpdateCourse(Courses courses)
        {
            try
            {
                _db.Entry(courses).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
