using System;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

namespace WebApp.App_Code
{
    public static class ImageManager
    {
        private static readonly string[] AllowedExtensions =
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".webp"
        };

        public static string SaveImage(
            FileUpload fileUpload,
            string folderPath)
        {
            if (!fileUpload.HasFile)
                return null;

            string extension =
                Path.GetExtension(
                    fileUpload.FileName)
                .ToLower();

            if (!AllowedExtensions.Contains(extension))
                throw new Exception(
                    "فرمت تصویر معتبر نیست.");

            string fileName =
                Guid.NewGuid() + extension;

            string imageUrl =
                folderPath + fileName;

            fileUpload.SaveAs(
                System.Web.HttpContext.Current.Server.MapPath(imageUrl));

            return imageUrl;
        }
    }
}