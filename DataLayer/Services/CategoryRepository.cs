using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Repositories;

namespace DataLayer.Services
{
    public class CategoryRepository:ICategoryRepository
    {

        LearningDBEntities _db;
        public CategoryRepository(LearningDBEntities db)
        {
            _db = db;
        }
        public bool DeleteCategory(Categories item)
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
        public bool DeleteCategory(int item)
        {
            try
            {
                var items = FindCategoryByID(item);
                DeleteCategory(items);
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
        public Categories FindCategoryByID(int id)
        {
            return _db.Categories.Find(id);
        }

        public IEnumerable<Categories> GetAllCategories(int? id)
        {
            if (id == null)
            {
                return _db.Categories.Where(c => c.ParentID == null);
            }
            else
            {
                return _db.Categories.Where(c => c.ParentID == id);
            }
        }

        public IEnumerable<Categories> GetAllCategories()
        {
            return _db.Categories;
        }

        public bool InsertCategory(Categories item)
        {
            try
            {
                _db.Categories.Add(item);
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

        public bool UpdateCategory(Categories item)
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
