using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestPage : System.Web.UI.Page
{
    BusinessLogic.Reports bl = new BusinessLogic.Reports();
    protected void Page_Load(object sender, EventArgs e)
    {

            bl.FolderProcessor(sourceFolderPath: ConfigurationManager.AppSettings["sourceFolderPath"],
            processedFolderPath: ConfigurationManager.AppSettings["processedFolderPath"],
            failedFolderPath: ConfigurationManager.AppSettings["failedFolderPath"],
            importFailedPath: ConfigurationManager.AppSettings["importFailedPath"],
            fileConversionPath: ConfigurationManager.AppSettings["fileConversionPath"]);

    }
}