using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCR.Web.Models
{
    public class NewsViewModel
    {
        public int NewsId { get; set; }

        [Required]
        [StringLength(80)]
        public string Title { get; set; }

        [StringLength(200)]
        public string Summary { get; set; }

        public string Link { get; set; }
        public string Article { get; set; }

        [Display(Name="Created")]
        public System.DateTime CreatedOn { get; set; }
        [Display(Name = "Modified")]
        public System.DateTime ModifiedOn { get; set; }
        [Display(Name = "Created")]
        public string CreatedDate { get { return this.CreatedOn.ToString("MMMM d, yyyy h:mm tt"); } }
        [Display(Name = "Modified")]
        public string ModifiedDate { get { return this.ModifiedOn.ToString("MMMM d, yyyy h:mm tt"); } }

        public static NewsViewModel ToViewModel(GCR.Core.Entities.News news, NewsViewModel model = null)
        {
            if (model == null)
            {
                model = new NewsViewModel();
            }
            model.NewsId = news.NewsId;
            model.Title = news.Title;
            model.Link = news.Link;
            model.Summary = news.Summary;
            model.Article = news.Article;
            model.CreatedOn = news.CreatedOn;
            model.ModifiedOn = news.ModifiedOn;

            return model;
        }

        public static GCR.Core.Entities.News ToModel(NewsViewModel model, GCR.Core.Entities.News news = null)
        {
            if (news == null)
            {
                news = new GCR.Core.Entities.News();
            }

            news.Title = model.Title;
            news.Link = model.Link;
            news.Summary = model.Summary;
            news.Article = model.Article;

            return news;
        }
    }
}