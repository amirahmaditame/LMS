using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataLayer
{
    public class PagesMetaData
    {
        public int PageId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
       
        public string Title { get; set; }

        [Display(Name = "توضیح کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        
        public string ShortDescription { get; set; }

        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Text { get; set; }

        [Display(Name = "بازدید")]
        
        public int Visit { get; set; }

        [Display(Name = "تصویر")]
       
        public string ImageName { get; set; }

        [Display(Name = "تاریخ")]
    
        public System.DateTime CreateDate { get; set; }
    }
    [MetadataType(typeof(PagesMetaData))]
    public partial class Pages
    {

    }
}
