using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Management;
using System.Web.Services;


/// <summary>
/// Summary description for SampleService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class UploadPicture : System.Web.Services.WebService {

    public UploadPicture()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {

        return "{\"id\":1461,\"content\":\"Hello, World!\"}";
    }

    [WebMethod]
    public string prUploadPicture(string str, string storenumber)
    {
       string rtn= Base64ToImage(str,storenumber);
        return rtn;
    }

    [WebMethod]
    public string prImageOrganiser()
    {


       
       

        return prImageOrganiserTr();
    }

    public string prImageOrganiserTr()
    {
        string result = "";
        string folderPath = WebConfigurationManager.AppSettings["FolderDirectory"]; ;
        string[] filePaths = Directory.GetFiles(folderPath, "*.jpg", SearchOption.AllDirectories);

        try
        {
            foreach (var filePath in filePaths)
            {
                string base64;
                using (Bitmap bm = new Bitmap(filePath))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bm.Save(ms, ImageFormat.Jpeg);
                        base64 = Convert.ToBase64String(ms.ToArray());
                    }
                }

                if (base64 != null)
                {
                    result = prUploadPicture(base64, Path.GetFileName(Path.GetDirectoryName(filePath)));
                    if (result == "Success")
                    {
                        File.Delete(filePath);
                    }
                }
            }

            
        }
        catch (Exception ex)
        {
            
           result=ex.StackTrace;
        }

        return result;
    }


    public string Base64ToImage(string base64String, string strno)
    {
        try
        {

            if (!Directory.Exists(System.IO.Path.Combine(Server.MapPath("\\"),"Picture" ,strno)))
            {
                Directory.CreateDirectory(System.IO.Path.Combine(Server.MapPath("\\"), "Picture" ,strno));
            }

            if (!Directory.Exists(System.IO.Path.Combine(Server.MapPath("\\"), "Picture", strno, DateTime.Now.ToString("MMddyyyy"))))
            {
                Directory.CreateDirectory(System.IO.Path.Combine(Server.MapPath("\\"), "Picture", strno, DateTime.Now.ToString("MMddyyyy")));
            }

            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);

            image.Save(System.IO.Path.Combine(Server.MapPath("\\"), "Picture", strno, DateTime.Now.ToString("MMddyyyy"), DateTime.Now.ToString("HHmmssfff") + ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
            return "Success";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
     
    }
    
}
