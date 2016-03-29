using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SJDealStore.Pages.Items
{
    public partial class ShopKeepImport : System.Web.UI.Page
    {
        ShopKeep Layer=new ShopKeep();
        protected void Page_Load(object sender, EventArgs e)
        {
       
        }
        private DataTable Import_To_Grid(string FilePath, string Extension, string isHDR)
        {
            string header = "Yes";

            string pathOnly = Path.GetDirectoryName(FilePath);
            string fileName = Path.GetFileName(FilePath);

            string sql = @"SELECT * FROM [" + fileName + "]";

            using (OleDbConnection connection = new OleDbConnection(
                      @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
                      ";Extended Properties=\"Text;HDR=" + header + "\""))
            using (OleDbCommand command = new OleDbCommand(sql, connection))
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
            {
                DataTable dataTable = new DataTable();
                dataTable.Locale = CultureInfo.CurrentCulture;
                adapter.Fill(dataTable);
                return dataTable;
            }
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

                    var dt = Import_To_Grid(FilePath.ToUpper(), Extension.ToUpper(), "No");
                    Layer.DeleteRecords();


                    foreach (DataRow drow in dt.Rows)
                    {
                        Layer.ShopKeepImport(drow["Stock Item Record ID"].ToString(), drow["Description"].ToString(),
                            drow["Discountable"].ToString(), drow["UPC Code"].ToString(),
                            drow["Price"].ToString(), drow["Cost"].ToString(), drow["Taxable"].ToString(),
                            drow["Price Type"].ToString(), drow["Register Data Status"].ToString(),
                            drow["Quantity on Hand"].ToString(), drow["Inventory Method"].ToString(),
                            drow["Assigned Cost"].ToString(), drow["Order Trigger"].ToString(),
                            drow["Recommended Order"].ToString(), drow["Department"].ToString(),
                            drow["Category"].ToString(),
                            drow["Supplier"].ToString(), drow["Supplier Code"].ToString(),
                            drow["Unit"].ToString(), drow["Tax Group"].ToString());
                    }
                    StatusLabel.Text = "Upload Successful";


                    dt = Layer.GetShopKeepItems();
                    gdViewExcelValues.DataSource = dt;
                    gdViewExcelValues.DataBind();

                    StatusLabel.Text = dt.Rows.Count + " Rows Found";
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=ShopKeepImport.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(Layer.GetShopKeepItemsCSV());

                    Response.Flush();

                    try
                    {
                        Response.End();
                    }
                    catch (Exception ex)
                    {

                    //    StatusLabel.Text = dt.Rows.Count + " Rows Found";
                    }

                    // 

                }
                catch (ThreadAbortException dd)
                {
                  //  StatusLabel.Text
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