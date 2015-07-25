using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using AUBusinessAccess;
using Image = System.Web.UI.WebControls.Image;

namespace AutomatedEmail
{
    public partial class ConvertRFIDImages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // SaveImage();
            AUBusinessAccess.JarvisBusinessLr lr = new JarvisBusinessLr();
            var dt = lr.GetAllCatagoryImageName();
            string[] filePaths = Directory.GetFiles(@"C:\Users\vramalingam\Desktop\RFIDImages\", "*.jpg");
            foreach (var jpgFiles in filePaths)
            {


                var imageName = dt.Select("Barcode = '" + Path.GetFileNameWithoutExtension(jpgFiles) + "'");
                if (imageName.Any())
                {
                   // Response.Write(Path.GetFileNameWithoutExtension(jpgFiles)+", Exist" + "<br/>");
                }
                else
                {
                    Response.Write(Path.GetFileNameWithoutExtension(jpgFiles) +"<br/>");
                }
                
            }

        }

        private static void SaveImage()
        {
            AUBusinessAccess.JarvisBusinessLr lr = new JarvisBusinessLr();
            var dtImages = lr.GetProductImage();
            foreach (DataRow dtrw in dtImages.Rows)
            {
                MemoryStream stream = new MemoryStream();
                byte[] image = (byte[])dtrw["ProductImage"];
                stream.Write(image, 0, image.Length);
                Bitmap bitmap = new Bitmap(stream);
                bitmap.Save(@"C:\Users\vramalingam\Desktop\RFIDImages\" + dtrw["UPC"] + ".jpg", ImageFormat.Jpeg);
            }
        }
    }
}