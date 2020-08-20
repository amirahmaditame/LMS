using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class User_PFMetaData
    {
        [Key]
        public int User_PFID { get; set; }
        public int UserID { get; set; }
        [Display(Name = "شماره تلفن")]
        [MaxLength(150)]
        public string PhoneNumber { get; set; }
        [Display(Name = "وبسایت")]
        [MaxLength(350)]
        public string WebSite { get; set; }
        [MaxLength(500)]
        [Display(Name = "درباه من ")]
        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }
        [MaxLength(150)]
        [Display(Name = "تصویر")]
        public string ImageName { get; set; }
    }
    [MetadataType(typeof(User_PFMetaData))]
    public partial class User_PF
    {

    }
}
