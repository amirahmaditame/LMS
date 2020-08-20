using DataLayer.Repositories;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<SearchCoursesListViewModel> searchCourses(string q)
        {
            //List<SearchCoursesListViewModel> CoursesList = new List<SearchCoursesListViewModel>();
            //var AllCourses = _db.Courses.Select(x => new SearchCoursesListViewModel()
            //{
            //   CourseName = x.CourseName,
            //   Price = x.Price,
            //  ImageName = x.ImageName,
            //  Teachername = x.Users.UserName

            //}).ToList();
            //CoursesList.AddRange(AllCourses.Where(p => p.CourseName.Contains(q)));

            return null;
        }
    }
}
