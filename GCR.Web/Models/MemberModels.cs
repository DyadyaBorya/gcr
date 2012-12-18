using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCR.Web.Models
{
    public class MemberViewModel
    {
        public int MemberId { get; set; }

        [Required]
        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Member Since")]
        public int MemberSince { get; set; }

        [StringLength(400)]
        public string Bio { get; set; }

        public string Photo { get; set; }

        [Display(Name = "Has Photo")]
        public bool HasPhoto { get { return !string.IsNullOrEmpty(this.Photo); } }

        [Display(Name = "Full Name")]
        public string FullName { get { return this.FirstName + " " + this.LastName; } }

        public string PhotoForDisplay { get { return HasPhoto ? this.Photo : "~/Content/Images/NoPhoto.png"; } }


        public static MemberViewModel ToViewModel(GCR.Core.Entities.Member member, MemberViewModel model = null)
        {
            if (model == null)
            {
                model = new MemberViewModel();
            }
            model.FirstName = member.FirstName;
            model.LastName = member.LastName;
            model.MemberSince = member.MemberSince;
            model.IsActive = member.IsActive;
            model.Bio = member.Bio;
            model.Photo = member.Photo;
            return model;
        }

        public static GCR.Core.Entities.Member ToModel(MemberViewModel model, GCR.Core.Entities.Member member = null)
        {
            if (member == null)
            {
                member = new GCR.Core.Entities.Member();
            }
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