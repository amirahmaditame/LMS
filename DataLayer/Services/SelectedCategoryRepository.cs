using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Repositories;

namespace DataLayer.Services
{
    public class SelectedCategoryRepository : ISelectedCategoryRepository
    {
        LearningDBEntities _db;
        public SelectedCategoryRepository(LearningDBEntities db)
        {
            _db = db;
        }
        public bool DeleteSelectedCategory(Selected_Category selectedCategory)
        {
            try
            {

                _db.Entry(selectedCategory).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteSelectedCategory(int selectedCategoryId)
        {
            try
            {
                var course = _db.Selected_Category.Find(selectedCategoryId);
                DeleteSelectedCategory(course);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteSelectedCategoryByCourseID(int CourseId)
        {
            try
            {
                _db.Selected_Category.Where(g => g.CourseID == CourseId).ToList().ForEach(g => _db.Selected_Category.Remove(g));
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

        public IEnumerable<Selected_Category> GetCategoryBuID(int selectedCategoryId)
        {
           return _db.Selected_Category.Where(c => c.CourseID == selectedCategoryId).ToList();
        }

        public bool InsertSelectedCategory(Selected_Category selectedCategory)
        {
            try
            {
                _db.Selected_Category.Add(selectedCategory);
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

        public bool UpdateSelectedCategory(Selected_Category selectedCategory)
        {
            try
            {
                _db.Entry(selectedCategory).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
