using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CategoryMetaData
    {
        [Key]
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")] [Display(Name ="دسته بندی")]
        public string Name { get; set; }
        public Nullable<int> ParentID { get; set; }
    }

    [MetadataType(typeof(CategoryMetaData))]
    public partial class Categories
    {

    }
}
