//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Teachers_PF
    {
        public int Teacher_PFID { get; set; }
        public int UserID { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Education { get; set; }
        public string ImageName { get; set; }
    
        public virtual Users Users { get; set; }
    }
}