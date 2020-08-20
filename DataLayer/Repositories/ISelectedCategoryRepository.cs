using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface ISelectedCategoryRepository : IDisposable
    {
        //Courses GetCourseById(int courseId);
        IEnumerable<Selected_Category> GetCategoryBuID(int selectedCategoryId);
        bool DeleteSelectedCategoryByCourseID(int CourseId);
        bool InsertSelectedCategory(Selected_Category selectedCategory);
        bool UpdateSelectedCategory(Selected_Category selectedCategory);
        bool DeleteSelectedCategory(Selected_Category selectedCategory);
        bool DeleteSelectedCategory(int selectedCategoryId);
        void save();
    }
}
