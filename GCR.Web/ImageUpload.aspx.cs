using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Helpers;
using GCR.Core;
using GCR.Core.Security;
using Ninject;

namespace GCR.Web
{
    public partial class ImageUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.ImageUploader.DeleteOldTemporaryFiles();

                this.ImageUploader.CropConstraint = new FixedCropConstraint(this.CropWidth, this.CropHeight);
                this.ImageUploader.CropConstraint.DefaultImageSelectionStrategy = CropConstraintImageSelectionStrategy.Slice;
                this.ImageUploader.PreviewFilter = null; //Use the selected image
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.ImageUploader.ClearTemporaryFiles();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ImageUploader.HasNewImage)
            {
                // Generate the main image            
                string webPath = this.ImagePath;
                string path = Server.MapPath(this.ImagePath);

                // Get the original file name (but always use the .jpg extension)
                string filename = IOHelper.GetUniqueFileName(path, Path.GetFileNameWithoutExtension(this.ImageUploader.SourceImageClientFileName) + ImageArchiver.GetFileExtensionFromImageFormatId(ImageFormat.Jpeg.Guid));

                path = System.IO.Path.Combine(path, filename);
                webPath = System.IO.Path.Combine(webPath, filename);
                this.ImageUploader.SaveProcessedImageToFileSystem(path);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveSuccess", "Upload_SaveSuccess('" + ResolveUrl(webPath) + "');", true);
            }

            this.ImageUploader.ClearTemporaryFiles();
        }


        public int CropWidth
        {
            get 
            {
                return TypeConverter.Convert<int>(this.Request.QueryString["w"], true, 0);
            }
        }

        public int CropHeight
        {
            get
            {
                return TypeConverter.Convert<int>(this.Request.QueryString["h"], true, 0);
            }
        }

        public string ImagePath
        {
            get
            {
                var qs = this.Request.QueryString["p"];
                var provider = IoC.Get<ISecurityProvider>();
                return provider.DecryptData(qs);
            }
        }


    }
}