using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCR.Web.Models.Member
{
    public class MemberViewModel
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public int MemberSince { get; set; }
        public string Bio { get; set; }
        public string Photo { get; set; }

        public string FullName { get { return this.FirstName + " " + this.LastName; } }
        public string PhotoForDisplay { get { return string.IsNullOrEmpty(this.Photo) ? "~/Content/Images/NoPhoto.png" : this.Photo; } }
    }
}