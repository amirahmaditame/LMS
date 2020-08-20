using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace DataLayer
{
    public class SubCoursesMetaData
    {
        [Key]
        public int SubCourseID { get; set; }
        public int CourseID { get; set; }
        [Display(Name = "نام")]
        [MaxLength(150)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string SubCourseTitle { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "توضیحات")]
        [MaxLength(150)]
        [DataType(DataType.MultilineText)]
        public string SubCourseDescription { get; set; }
        [Display(Name = "رایگان با پولی")]
        public bool CashOrFree { get; set; }
        [Display(Name = "ویدیو")]
        public string VideoName { get; set; }
    }
    [MetadataType(typeof(SubCoursesMetaData))]
    public partial class SubCourse
    {

    }
}
