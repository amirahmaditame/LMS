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
using DataLayer.ViewModels;
using KooyWebApp_MVC.Classes;

namespace LMS.Areas.Admin.Controllers
{
    public class CoursesController : Controller
    {
        private LearningDBEntities db = new LearningDBEntities();
        private ICoursesRepository _coursesRepository;
        private ICategoryRepository _categoryRepository;
        private ISelectedCategoryRepository _selectedCategoryRepository;
        private ISubCoursesRepository _subCoursesRepository;

        public CoursesController()
        {
            _coursesRepository = new CoursesRepository(db);
            _categoryRepository = new CategoryRepository(db);
            _selectedCategoryRepository = new SelectedCategoryRepository(db);
            _subCoursesRepository = new SubCoursesRepository(db);
        }

        // GET: Admin/Courses
        public ActionResult Index(string q = "", int take = 1, int pageid = 1)
        {
            var CourseList = _coursesRepository.GetAllCourses(q);
            int skip = (pageid - 1) * take;
            ViewBag.PageCount = CourseList.Count() / take;
            ViewBag.pageid = pageid;
            ViewBag.Search = q;

            return View(CourseList.Distinct().Skip(skip).Take(take).ToList());
        }

       
        // GET: Admin/Courses/Create
        public ActionResult Create()
        {
            ViewBag.Category = _categoryRepository.GetAllCategories().ToList();
            return View(new Courses()
            {
                CourseID = 0
            });
        }

        // POST: Admin/Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "CourseID,CourseName,Price,NumberOfStudents,ShortDescription,CourseLevel,CourseStatus,LastUpdate,ImageName,Text")] Courses courses, List<int> SelectCategory, HttpPostedFileBase ImgUp)
        {
            if (ModelState.IsValid)
            {
                var user = _coursesRepository.GetTeacherForCourses(User.Identity.Name);
                courses.NumberOfStudents = 0;
                courses.LastUpdate = DateTime.Now;
                courses.UserID = user.UserID;

                courses.ImageName = "no-image-found.png";
                if (ImgUp != null && ImgUp.IsImage())
                {
                    courses.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUp.FileName);
                    ImgUp.SaveAs(Server.MapPath("/Images/Courses/" + courses.ImageName));

                }
                foreach (var category in SelectCategory)
                {
                    _selectedCategoryRepository.InsertSelectedCategory(new Selected_Category()
                    {
                        CategoryID = category,
                        CourseID = courses.CourseID
                    });
                }
                _coursesRepository.InsertCourse(courses);
                _coursesRepository.save();
                ViewBag.Category = _categoryRepository.GetAllCategories().ToList();

                return View(courses);
            }
            ViewBag.Category = _categoryRepository.GetAllCategories().ToList();
            return View(courses);
        }

        // GET: Admin/Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = _coursesRepository.GetCourseById(id.Value);
            if (courses == null)
            {
                return HttpNotFound();
            }

            ViewBag.SubCourses = _subCoursesRepository.GetAllSubCoursesByID(id.Value).ToList();
            ViewBag.SelectedGroups = courses.Selected_Category.ToList();
            ViewBag.categories = _categoryRepository.GetAllCategories().ToList();
            return View(courses);
        }

        // POST: Admin/Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseID,UserID,CourseName,Price,NumberOfStudents,NumberOfVideos,ShortDescription,CourseLevel,CourseStatus,LastUpdate,ImageName,Text")] Courses courses, List<int> SelectCategory, HttpPostedFileBase ImgUp)
        {
            if (ModelState.IsValid)
            {
                if (ImgUp != null && ImgUp.IsImage())
                {
                    if (courses.ImageName != "no-image-found.png")
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/Courses/" + courses.ImageName));
                    }
                    courses.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUp.FileName);
                    ImgUp.SaveAs(Server.MapPath("/Images/Courses/" + courses.ImageName));

                }

                _selectedCategoryRepository.DeleteSelectedCategoryByCourseID(courses.CourseID);
                if (SelectCategory != null && SelectCategory.Any())
                {
                    foreach (var category in SelectCategory)
                    {
                        _selectedCategoryRepository.InsertSelectedCategory(new Selected_Category()
                        {
                            CategoryID = category,
                            CourseID = courses.CourseID
                        });
                    }
                }

                _coursesRepository.UpdateCourse(courses);
                _coursesRepository.save();

                ViewBag.SubCourses = _subCoursesRepository.GetAllSubCoursesByID(courses.CourseID).ToList();
                ViewBag.SelectedGroups = courses.Selected_Category.ToList();
                ViewBag.categories = _categoryRepository.GetAllCategories().ToList();
                return View(courses);
            }
            ViewBag.SubCourses = _subCoursesRepository.GetAllSubCoursesByID(courses.CourseID).ToList();
            ViewBag.SelectedGroups = courses.Selected_Category.ToList();
            ViewBag.categories = _categoryRepository.GetAllCategories().ToList();
            return View(courses);
        }

        // GET: Admin/Courses/Delete/5
        public void Delete(int? id)
        {

            var Course = _coursesRepository.GetCourseById(id.Value);


            foreach (var category in _selectedCategoryRepository.GetCategoryBuID(id.Value))
            {
                _selectedCategoryRepository.DeleteSelectedCategory(category);
            }
            foreach (var SubCourse in _subCoursesRepository.GetAllSubCoursesByID(id.Value))
            {
                _subCoursesRepository.DeleteSubCourses(SubCourse);
            }
            if (Course.ImageName != "no-image-found.png")
            {
                System.IO.File.Delete(Server.MapPath("/Images/Courses/" + Course.ImageName));
            }

            _coursesRepository.DeleteCourse(Course);
            _coursesRepository.save();
        }

        public ActionResult CreateSubCourse(int? id)
        {
            return PartialView(new SubCourse(){CourseID = id.Value});
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSubCourse([Bind(Include = "SubCourseID,CourseID,SubCourseTitle,SubCourseDescription,CashOrFree,VideoName")] SubCourse subCourse, HttpPostedFileBase VideoUp)
        {
            if (ModelState.IsValid)
            {
                if (VideoUp != null)
                {
                    subCourse.VideoName = Guid.NewGuid().ToString() + Path.GetExtension(VideoUp.FileName);
                    VideoUp.SaveAs(Server.MapPath("/Videos/" + subCourse.VideoName));

                }
                _subCoursesRepository.InsertSubCourses(subCourse);
                _subCoursesRepository.save();

                return new JsonResult
                {
                    Data = "فایل با موفیت آپلود شد"
                };

            }
            else
            {
                return new JsonResult
                {
                    Data = "bad"
                };
            }

        }

        public ActionResult EditSubCourses(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subcourses = _subCoursesRepository.FindSubCoursesByID(id.Value);
            if (subcourses == null)
            {
                return HttpNotFound();
            }
            return PartialView(subcourses);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditSubCourses([Bind(Include = "SubCourseID,CourseID,SubCourseTitle,SubCourseDescription,CashOrFree,VideoName")] SubCourse subCourse, HttpPostedFile VideoUp)
        {
            if (ModelState.IsValid)
            {
                if (VideoUp != null)
                {
                    System.IO.File.Delete(Server.MapPath("/Videos/" + subCourse.VideoName));
                    subCourse.VideoName = Guid.NewGuid().ToString() + Path.GetExtension(VideoUp.FileName);
                    VideoUp.SaveAs(Server.MapPath("/Videos/" + subCourse.VideoName));

                }
                _subCoursesRepository.UpdateSubCourses(subCourse);
                _subCoursesRepository.save();

                return new JsonResult
                {
                    Data = "فایل با موفیت آپلود شد"
                };

            }
            else
            {
                return new JsonResult
                {
                    Data = "bad"
                };
            }
        }
        public void DeleteSubCourses(int? id)
        {
            var SubCourse = _subCoursesRepository.FindSubCoursesByID(id.Value);
            _subCoursesRepository.DeleteSubCourses(SubCourse);
            _subCoursesRepository.save();
        }

     
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                _categoryRepository.Dispose();
                _coursesRepository.Dispose();
                _selectedCategoryRepository.Dispose();
                _subCoursesRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
