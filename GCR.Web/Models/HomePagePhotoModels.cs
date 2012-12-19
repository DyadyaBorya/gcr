using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCR.Web.Models
{
    public class HomePagePhotoViewModel
    {
        public int HomePagePhotoId { get; set; }

        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }

        [Required]
        [Display(Name = "Photo")]
        public string PhotoPath { get; set; }

        [Display(Name = "Has Photo")]
        public bool HasPhoto { get { return !string.IsNullOrEmpty(this.PhotoPath); } }

        public string PhotoForDisplay { get { return HasPhoto ? this.PhotoPath : "~/Content/Images/NoPhoto.png"; } }

        public static HomePagePhotoViewModel ToViewModel(GCR.Core.Entities.HomePagePhoto photo, HomePagePhotoViewModel model = null)
        {
            if (model == null)
            {
                model = new HomePagePhotoViewModel();
            }
            model.HomePagePhotoId = photo.HomePagePhotoId;
            model.DisplayOrder = photo.DisplayOrder;
            model.PhotoPath = photo.PhotoPath;

            return model;
        }

        public static GCR.Core.Entities.HomePagePhoto ToModel(HomePagePhotoViewModel model, GCR.Core.Entities.HomePagePhoto photo = null)
        {
            if (photo == null)
            {
                photo = new GCR.Core.Entities.HomePagePhoto();
            }
            photo.DisplayOrder = model.DisplayOrder;
            photo.PhotoPath = model.PhotoPath;

            return photo;
        }
    }

    public class HomePagePhotoOrderViewModel
    {
        public int Id { get; set; }
        public int Order { get; set; }
    }
}