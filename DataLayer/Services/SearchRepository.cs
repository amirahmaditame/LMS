using DataLayer.Repositories;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    public class SearchRepository : ISearchRepository
    {
        LearningDBEntities _db;
        public SearchRepository(LearningDBEntities db)
        {
            _db = db;
        }

        public int courseCount()
        {
            return _db.Courses.Count();
        }

        public List<Categories> homeDetails()
        {
            List<Categories> categoryList = new List<Categories>();
            categoryList = _db.Categories.Where(c => c.ParentID == null).ToList();
            return categoryList;

        }

        public IEnumerable<SearchCoursesListViewModel> searchCourses(string q)
        {
            List<SearchCoursesListViewModel> CoursesList = new List<SearchCoursesListViewModel>();
            var AllCourses = _db.Courses.Select(x => new SearchCoursesListViewModel()
            {
                CourseName = x.CourseName,
                ImageName = x.ImageName,
                Price = x.Price


            }).ToList();
            CoursesList.AddRange(AllCourses.Where(p => p.CourseName.Contains(q)));

            return CoursesList;

        }

        public int teacherCount()
        {
            return _db.Users.Where(t => t.RoleID == 2).Count();
        }

        public int userCount()
        {
            return _db.Users.Where(t => t.RoleID == 3).Count();
        }
    }
}
