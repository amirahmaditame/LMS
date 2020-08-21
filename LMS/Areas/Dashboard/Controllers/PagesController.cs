using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.Repositories;
using DataLayer.Services;

namespace LMS.Areas.Dashboard.Controllers
{
    public class PagesController : Controller
    {

        private LearningDBEntities db = new LearningDBEntities();
        private IPageRepository _pageRepository;

        public PagesController()
        {
            _pageRepository = new PagesRepository(db);
        }



        // GET: Dashboard/Pages
        public ActionResult Index()
        {
            return View(_pageRepository.GetAllPage());
        }

      
        // GET: Dashboard/Pages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Pages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PageId,Title,ShortDescription,Text")] Pages pages , HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {

                pages.Visit = 0;
                pages.CreateDate = DateTime.Now;

                if (imgUp != null)
                {
                    pages.ImageName = Guid.NewGuid() + Path.GetExtension(imgUp.FileName);
                    imgUp.SaveAs(Server.MapPath("/Pageimages/" + pages.ImageName));
                }


                _pageRepository.InsertPage(pages);
                _pageRepository.save();
                return RedirectToAction("Index");
            }

            return View(pages);
        }

        // GET: Dashboard/Pages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pages pages = _pageRepository.GetPageById(id.Value);
            if (pages == null)
            {
                return HttpNotFound();
            }
            return View(pages);
        }

        // POST: Dashboard/Pages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PageId,Title,ShortDescription,Text,Visit,ImageName,CreateDate")] Pages pages , HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {

                if (imgUp != null)
                {
                    if (pages.ImageName != null)
                    {
                        System.IO.File.Delete(Server.MapPath("/Pageimages/" + pages.ImageName));
                    }
                    pages.ImageName = Guid.NewGuid() + Path.GetExtension(imgUp.FileName);
                    imgUp.SaveAs(Server.MapPath("/Pageimages/" + pages.ImageName));
                }

                _pageRepository.UpdatePage(pages);
                _pageRepository.save();
                return RedirectToAction("Index");
            }
           
            return View(pages);
        }

        // GET: Dashboard/Pages/Delete/5


        public void Delete(int id)
        {
            var page = _pageRepository.GetPageById(id);
            if (page.ImageName != null)
            {
                System.IO.File.Delete(Server.MapPath("/Pageimages/" + page.ImageName));
            }
            _pageRepository.DeletePage(page);
            _pageRepository.save();
            
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _pageRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
