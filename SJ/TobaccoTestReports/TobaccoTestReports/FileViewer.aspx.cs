using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;

public partial class FileViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    
        TbcExcelTemplate tbcExcelTemplateClass= new TbcExcelTemplate();
        List<TbcExcelTemplate> lq =  tbcExcelTemplateClass.GetFileDetails(400);
        if (lq.Count > 0)
        {
           // GridView1.DataSource = lq;
          //  GridView1.DataBind();
       //     GridView2.DataSource = lq[0].TbcExcelValues;
      //      GridView2.DataBind();
        }
    }
}