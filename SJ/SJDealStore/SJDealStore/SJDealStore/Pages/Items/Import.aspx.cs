using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SJDealStore.Pages.Items
{
    public partial class Import : System.Web.UI.Page
    {
        DALayer Layer=new DALayer();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private DataTable Import_To_Grid(string FilePath, string Extension, string isHDR)
        {
            string conStr = "";
            switch (Extension)
            {
                case ".XLS": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;
                case ".XLSX": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                              .ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();

            //Bind Data to GridView
            return
                dt;
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (FileUploadControl.HasFile)
            {
                try
                {


                    string FileName = Path.GetFileName(FileUploadControl.PostedFile.FileName);
                    string Extension = Path.GetExtension(FileUploadControl.PostedFile.FileName);
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                    string FilePath = Server.MapPath(FolderPath + FileName);
                    FileUploadControl.SaveAs(FilePath);
                   var dt= Import_To_Grid(FilePath.ToUpper(), Extension.ToUpper(), "No");
                   gdViewExcelValues.DataSource = dt;
                   gdViewExcelValues.DataBind();
                   int masterFileId = int.Parse(Layer.InsertMasterFile(FileName, txtCategoryName.Text));
                    Layer.InsertMasterFileDetails(dt, masterFileId, txtCategoryName.Text);
                    Layer.SjDealsSaveSettings(masterFileId, false, "SJ PRICE : ",
                        "MSRP : ",
                        "34", "8", "7", "4", "5", DateTime.Now.ToString("yyyyMMd"));
         ;

                   StatusLabel.Text = "Upload Successful";
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Its not a Excel File - " + ex.Message;
                }
           
            }
            else
            {
                StatusLabel.Text = "Please select a File to Upload";
            }
        }
    }
}