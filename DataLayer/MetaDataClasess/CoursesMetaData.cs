using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataLayer
{
    public class CoursesMetaData
    {
        [Key]
        public int CourseID { get; set; }
        [Display(Name = "نام دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250)]
        public string CourseName { get; set; }
        [Display(Name = "قیمت دوره")]
        public Nullable<int> Price { get; set; }
        [Display(Name = "شرکت کنندگان")]
        public Nullable<int> NumberOfStudents { get; set; }
       
        [Display(Name = "سطح دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150)]
        public string CourseLevel { get; set; }
        [MaxLength(150)]
        [Display(Name = "وضعیت دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CourseStatus { get; set; }
        [Display(Name = "آخرین آپدیت")]
        public System.DateTime LastUpdate { get; set; }
        [Display(Name = "تصویر")]
        public string ImageName { get; set; }
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Text { get; set; }
        public int UserID { get; set; }
        [Display(Name = "توضیح مختصر")]
        [MaxLength(300)]
        [DataType(DataType.MultilineText,ErrorMessage = "حداکثر 300 کارکتر مجاز است")]
        public string ShortDescription { get; set; }
    }
    [MetadataType(typeof(CoursesMetaData))]
    public partial class Courses
    {

    }
}
