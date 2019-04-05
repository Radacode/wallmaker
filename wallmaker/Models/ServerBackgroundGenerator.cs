using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace CreateBackDb.Models
{
    public class ServerBackgroundGenerator
    {
        public string GenerateBackground(string serverName)
        {
            string path = HostingEnvironment.MapPath("~/Content/HomeImage.txt");
            StreamReader sr = new StreamReader(path);
            string src = sr.ReadLine();
            sr.Close();
            src = HostingEnvironment.MapPath("~/Content/Images/" + src);

            RectangleF rectf = new RectangleF(600, 600, 800, 800);
            Image image = Image.FromFile(src);

            Graphics g = Graphics.FromImage(image);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawString(serverName, new Font("Tahoma", 60), Brushes.White, rectf);

            g.Flush();
            g.Dispose();

            var stream = new MemoryStream();
            image.Save(stream, ImageFormat.Jpeg);

            byte[] array = stream.ToArray();

            string stringBase64 = Convert.ToBase64String(array);
            return stringBase64;
        }
    }
}