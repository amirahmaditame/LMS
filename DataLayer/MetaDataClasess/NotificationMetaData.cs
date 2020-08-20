using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class NotificationMetaData
    {
        public int NotificationID { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string NotificationTitle { get; set; }

        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.MultilineText)]
        public string NotificationText { get; set; }

        public System.DateTime NotificationDate { get; set; }
        public int UserID { get; set; }
    }
    [MetadataType(typeof(NotificationMetaData))]
    public partial class Notifications
    {

    }
}
