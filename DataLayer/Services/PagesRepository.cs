using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    public class PagesRepository : IPageRepository
    {
        LearningDBEntities _db;
        public PagesRepository(LearningDBEntities db)
        {
            _db = db;
        }
        public IEnumerable<Pages> GetAllPage()
        {
            return _db.Pages;
        }

        public Pages GetPageById(int pageId)
        {
            return _db.Pages.Find(pageId);

        }

        public bool InsertPage(Pages page)
        {
            try
            {
                _db.Pages.Add(page);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdatePage(Pages page)
        {
            try
            {

                _db.Entry(page).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool DeletePage(Pages page)
        {
            try
            {

                _db.Entry(page).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeletePage(int pageId)
        {
            try
            {

                var page = GetPageById(pageId);
                DeletePage(page);
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

        public void Dispose()
        {
            _db.Dispose();
        }

        public IEnumerable<Pages> LastNews(int take = 3)
        {
            return _db.Pages.OrderByDescending(p => p.CreateDate).Take(take);
        }
    }
}
