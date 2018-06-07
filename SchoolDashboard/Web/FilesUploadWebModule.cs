using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace SchoolDashboard.Web
{
    public class FilesUploadWebModule : NancyModule
    {
        public FilesUploadWebModule()
        {
            Post["/UploadHolidayPicture"] = UploadHolidayPicture;
        }

        public dynamic UploadHolidayPicture(dynamic x)
        {
            var file = Request.Files.First();
            var img = Image.FromStream(file.Value);
            if (img.Width < 500 && img.Height < 300)
            {
                return Response.AsText("TooSmall");
            }

            img.Save(Path.Combine(Directory.GetCurrentDirectory(), "Web", "Content", "Images", "HolidaysPictures", file.Name));

            return Response.AsText(file.Name);
        }
    }


}
