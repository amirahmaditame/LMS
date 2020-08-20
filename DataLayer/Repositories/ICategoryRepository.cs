using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface ICategoryRepository :IDisposable
    {
        IEnumerable<Categories> GetAllCategories();
        IEnumerable<Categories> GetAllCategories(int? id);
        Categories FindCategoryByID(int id);
        bool InsertCategory(Categories item);
        bool UpdateCategory(Categories item);
        bool DeleteCategory(Categories item);
        bool DeleteCategory(int item);
        void save();
    }
}
