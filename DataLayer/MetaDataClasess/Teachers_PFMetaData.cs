using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Teachers_PFMetaData
    {
        [Key]
        public int UserID { get; set; }
        [Display(Name = "شماره تلفن")]
        [MaxLength(150)]
        public string PhoneNumber { get; set; }
        [Display(Name = "شهر")]
        [MaxLength(150)]
        public string City { get; set; }
        [MaxLength(150)]
        [Display(Name = "تحصیلات")]
        public string Education { get; set; }
        [Display(Name = "تصویر")]
        [MaxLength(250)]
        public string ImageName { get; set; }

    }
    [MetadataType(typeof(Teachers_PFMetaData))]
    public partial class Teachers_PF
    {

    }
}
