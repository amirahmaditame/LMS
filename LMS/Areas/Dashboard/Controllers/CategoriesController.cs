using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.Repositories;
using DataLayer.Services;

namespace LMS.Areas.Dashboard.Controllers
{
    public class CategoriesController : Controller
    {
        private LearningDBEntities db = new LearningDBEntities();
        private ICategoryRepository _categoryRepository;

        public CategoriesController()
        {
            _categoryRepository = new CategoryRepository(db);
        }

        // GET: Admin/Categories
        public ActionResult Index()
        {
            var categories = _categoryRepository.GetAllCategories(null);
            return View(categories.ToList());
        }
       
        public ActionResult Create(int? id)
        {
            return PartialView(new Categories()
            {
                ParentID = id
            });
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,Name,ParentID")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.InsertCategory(categories);
                _categoryRepository.save();

                return RedirectToAction("Index");
            }

            ViewBag.ParentID = new SelectList(db.Categories, "CategoryID", "Name", categories.ParentID);
            return PartialView(categories);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = _categoryRepository.FindCategoryByID(id.Value);
            if (categories == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentID = new SelectList(db.Categories, "CategoryID", "Name", categories.ParentID);
            return PartialView(categories);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,Name,ParentID")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.UpdateCategory(categories);
                _categoryRepository.save();
                return RedirectToAction("Index");
            }
            ViewBag.ParentID = new SelectList(db.Categories, "CategoryID", "Name", categories.ParentID);
            return View(categories);
        }

        // GET: Admin/Categories/Delete/5
        public void Delete(int id)
        {
            var group = _categoryRepository.FindCategoryByID(id);
            if (group.Categories1.Any())
            {
                foreach (var subGroup in _categoryRepository.GetAllCategories(id))
                {
                    if (subGroup.Categories1.Any())
                    {
                        foreach (var subsubGroup in _categoryRepository.GetAllCategories(subGroup.CategoryID))
                        {
                            _categoryRepository.DeleteCategory(subsubGroup);
                        }
                    }
                    _categoryRepository.DeleteCategory(subGroup);
                }
            }
            _categoryRepository.DeleteCategory(group);
            _categoryRepository.save();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                _categoryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
