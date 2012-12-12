using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCR.Web.Models
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


        public static MemberViewModel ToViewModel(GCR.Core.Entities.Member member)
        { 
            var model = new MemberViewModel();
            model.FirstName = member.FirstName;
            model.LastName = member.LastName;
            model.MemberSince = member.MemberSince;
            model.IsActive = member.IsActive;
            model.Bio = member.Bio;
            model.Photo = member.Photo;
            return model;
        }

        public static GCR.Core.Entities.Member ToModel(MemberViewModel model)
        {
            var member = new GCR.Core.Entities.Member();
            member.FirstName = model.FirstName;
            member.LastName = model.LastName;
            member.MemberSince = model.MemberSince;
            member.IsActive = model.IsActive;
            member.Bio = model.Bio;
            member.Photo = model.Photo;
            return member;
        }
    }
}