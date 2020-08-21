using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IPageRepository:IDisposable
    {
        IEnumerable<Pages> GetAllPage();
        Pages GetPageById(int pageId);
        bool InsertPage(Pages page);
        bool UpdatePage(Pages page);
        bool DeletePage(Pages page);
        bool DeletePage(int pageId);
        void save();
        //IEnumerable<Page> TopNews(int take = 4);
        //IEnumerable<Page> PagesInSlider();
        IEnumerable<Pages> LastNews(int take = 3);
        //IEnumerable<Page> ShowPageByGroupId(int groupId);
        //IEnumerable<Page> SearchPage(string Parameter);
    }
}
