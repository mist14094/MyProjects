using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;
namespace DealStore
{
    public partial class ImportForm : Form
    {
        private Button btnBrowse;
        private Button btnEPReport;
        private Button btnExportData;
        private Button btnExportLabelData;
        private Button btnGenerateReport;
        private Button btnImport;
        private Button btnMainMenu;
        private Button btnUserVendorSummaryReport;
        private Button button1;
        private CheckBox cbAllVendors;
        private ComboBox ddlImportVendorLoad;
        private ComboBox ddlImportVendors;
        private ComboBox ddlLoadLoadReport;
        private ComboBox ddlVendorLoadReport;
        private ComboBox ddlVendors;
        private DateTimePicker dpEndDate;
        private DateTimePicker dpEPEndDate;
        private DateTimePicker dpEPStartDate;
        private DateTimePicker dpImportDate;
        private DateTimePicker dpStartDate;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox txtExcelFile;
        public ImportForm()
        {
            InitializeComponent();
        }


        private void btnBrowse_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                string fileName = string.Empty;
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Filter = "Excel File (*.xlsx;*.xls)|*.xlsx;*.xls",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    Title = "Select Excel File"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = dialog.FileName;
                }
                if (fileName != string.Empty)
                {
                    this.txtExcelFile.Text = fileName;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error browsing file.", "Browse");
            }
        }

        private void btnEPReport_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                DataTable dataTable = DBAccess.GetDataTable("SELECT StockUser.USER_ID, StockUser.USER_FULL_NAME, FORMAT(ActivityLog.AL_LOGIN_TIME, \"mm/dd/yyyy\") AS ActivityDate, ActivityLog.AL_LOGIN_TIME, ActivityLog.AL_LOGOUT_TIME, 0.00 AS HOURS_WORKED, 0 AS ITEMS_SCANNED, 0.00 AS AVERAGE_PER_HOUR, 0.00 AS DOLLAR_VALUE FROM ActivityLog, StockUser WHERE ActivityLog.AL_USER = StockUser.USER_ID AND ActivityLog.AL_LOGIN_TIME IS NOT NULL AND ActivityLog.AL_LOGOUT_TIME IS NOT NULL AND ActivityLog.AL_LOGIN_TIME BETWEEN '" + DateTime.Parse(this.dpEPStartDate.Value.ToShortDateString()).ToString() + "' AND '" + DateTime.Parse(this.dpEPEndDate.Value.ToShortDateString()).AddDays(1.0).AddSeconds(-1.0).ToString() + "' ORDER BY 3, 2", DBAccess.msAccessCon);
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        DateTime time = DateTime.Parse(row["AL_LOGIN_TIME"].ToString());
                        DateTime time2 = DateTime.Parse(row["AL_LOGOUT_TIME"].ToString());
                        TimeSpan span = new TimeSpan();
                        span = (TimeSpan)(time2 - time);
                        double totalSeconds = span.TotalSeconds;
                        row["HOURS_WORKED"] = totalSeconds;
                        DataTable table2 = DBAccess.GetDataTable("SELECT COUNT(LABELED_PRODUCT_ID) AS ITEMS_SCANNED, SUM(LABELED_PRODUCT_PRICE) AS DOLLAR_VALUE FROM LabeledProduct WHERE LABELED_PRODUCT_USER = " + row["USER_ID"].ToString() + " AND LABELED_PRODUCT_DATE BETWEEN '" + time.ToString() + "' AND '" + time2.ToString() + "'", DBAccess.msAccessCon);
                        if (table2.Rows.Count > 0)
                        {
                            row["ITEMS_SCANNED"] = table2.Rows[0]["ITEMS_SCANNED"];
                            try
                            {
                                if (double.Parse(table2.Rows[0]["DOLLAR_VALUE"].ToString()) > 0.0)
                                {
                                    row["DOLLAR_VALUE"] = table2.Rows[0]["DOLLAR_VALUE"];
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    DataTable table3 = dataTable.Clone();
                    table3.ImportRow(dataTable.Rows[0]);
                    for (int i = 1; i < dataTable.Rows.Count; i++)
                    {
                        int num4 = 0;
                        foreach (DataRow row2 in table3.Rows)
                        {
                            if ((row2["USER_ID"].ToString() == dataTable.Rows[i]["USER_ID"].ToString()) && (row2["ActivityDate"].ToString() == dataTable.Rows[i]["ActivityDate"].ToString()))
                            {
                                row2["HOURS_WORKED"] = double.Parse(row2["HOURS_WORKED"].ToString()) + double.Parse(dataTable.Rows[i]["HOURS_WORKED"].ToString());
                                row2["ITEMS_SCANNED"] = int.Parse(row2["ITEMS_SCANNED"].ToString()) + int.Parse(dataTable.Rows[i]["ITEMS_SCANNED"].ToString());
                                row2["DOLLAR_VALUE"] = double.Parse(row2["DOLLAR_VALUE"].ToString()) + double.Parse(dataTable.Rows[i]["DOLLAR_VALUE"].ToString());
                                num4 = 1;
                                break;
                            }
                        }
                        if (num4 == 0)
                        {
                            table3.ImportRow(dataTable.Rows[i]);
                        }
                    }
                    Cursor.Current = Cursors.WaitCursor;
                    ExcelPackage package = new ExcelPackage();
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Performance");
                    worksheet.Cells["A1"].Value = "Date";
                    worksheet.Cells["A1"].Worksheet.Column(1).Width = 15.0;
                    worksheet.Cells["B1"].Value = "Name";
                    worksheet.Cells["B1"].Worksheet.Column(2).Width = 25.0;
                    worksheet.Cells["C1"].Value = "Hours Worked";
                    worksheet.Cells["C1"].Worksheet.Column(3).Width = 15.0;
                    worksheet.Cells["D1"].Value = "Items Scanned";
                    worksheet.Cells["D1"].Worksheet.Column(4).Width = 15.0;
                    worksheet.Cells["E1"].Value = "Average Per Hour";
                    worksheet.Cells["E1"].Worksheet.Column(5).Width = 20.0;
                    worksheet.Cells["F1"].Value = "Dollar Value";
                    worksheet.Cells["F1"].Worksheet.Column(6).Width = 15.0;
                    long num5 = 2L;
                    foreach (DataRow row in table3.Rows)
                    {
                        worksheet.Cells["A" + num5.ToString()].Value = row["ActivityDate"].ToString();
                        worksheet.Cells["B" + num5.ToString()].Value = row["USER_FULL_NAME"].ToString();
                        worksheet.Cells["C" + num5.ToString()].Value = Math.Round((double)((double.Parse(row["HOURS_WORKED"].ToString()) / 60.0) / 60.0), 2);
                        worksheet.Cells["D" + num5.ToString()].Value = row["ITEMS_SCANNED"];
                        worksheet.Cells["E" + num5.ToString()].Formula = "=ROUND(D" + num5.ToString() + " / C" + num5.ToString() + ", 2)";
                        worksheet.Cells["F" + num5.ToString()].Value = row["DOLLAR_VALUE"];
                        num5 += 1L;
                    }
                    string fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Employee Performance Report.xlsx";
                    FileInfo file = new FileInfo(fileName);
                    package.SaveAs(file);
                    MessageBox.Show("Report created successfully at " + fileName, "Export");
                }
                else
                {
                    MessageBox.Show("No data found for report.", "Export");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error while creating report - " + exception.Message, "Export");
            }
        }

        private void btnExportData_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if (MessageBox.Show("Please confirm to export data?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataTable dataTable = new DataTable();
                    string query = "SELECT * FROM Product";
                    dataTable = DBAccess.GetDataTable(query, DBAccess.msAccessCon);
                    if (dataTable.Rows.Count > 0)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        ExcelPackage package = new ExcelPackage();
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Compilation");
                        worksheet.Cells["A1"].Value = "Product";
                        worksheet.Cells["A1"].Worksheet.Column(1).Width = 50.0;
                        worksheet.Cells["B1"].Value = "Price";
                        worksheet.Cells["B1"].Worksheet.Column(2).Width = 10.0;
                        worksheet.Cells["C1"].Value = "UPC";
                        worksheet.Cells["C1"].Worksheet.Column(3).Width = 15.0;
                        int num = 2;
                        foreach (DataRow row in dataTable.Rows)
                        {
                            worksheet.Cells["A" + num.ToString()].Value = row["PRODUCT_NAME"].ToString();
                            worksheet.Cells["B" + num.ToString()].Value = row["PRODUCT_PRICE"];
                            worksheet.Cells["C" + num.ToString()].Value = row["PRODUCT_UPC"].ToString();
                            num++;
                        }
                        string fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Stock - " + DateTime.Now.ToString("MMddyyyy") + ".xlsx";
                        FileInfo file = new FileInfo(fileName);
                        package.SaveAs(file);
                        MessageBox.Show("Data exported successfully at " + fileName, "Export");
                    }
                    else
                    {
                        MessageBox.Show("No data found to export.", "Export");
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error while exporting data - " + exception.Message, "Export");
            }
        }

        private void btnExportLabelData_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if (MessageBox.Show("Please confirm to export data?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataTable dataTable = new DataTable();
                    string query = "SELECT Product.PRODUCT_NAME, Product.PRODUCT_PRICE, Product.PRODUCT_UPC, LabeledProduct.LABELED_PRODUCT_PRICE, LabeledProduct.LABELED_PRODUCT_VENDOR, LabeledProduct.LABELED_PRODUCT_DATE FROM Product, LabeledProduct WHERE Product.PRODUCT_UPC = LabeledProduct.LABELED_PRODUCT_UPC ORDER BY 3, 6 DESC ";
                    dataTable = DBAccess.GetDataTable(query, DBAccess.msAccessCon);
                    if (dataTable.Rows.Count > 0)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        ExcelPackage package = new ExcelPackage();
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Inventory");
                        worksheet.Cells["A1"].Value = "Product";
                        worksheet.Cells["A1"].Worksheet.Column(1).Width = 50.0;
                        worksheet.Cells["B1"].Value = "Retail Price";
                        worksheet.Cells["B1"].Worksheet.Column(2).Width = 25.0;
                        worksheet.Cells["C1"].Value = "UPC";
                        worksheet.Cells["C1"].Worksheet.Column(3).Width = 15.0;
                        worksheet.Cells["D1"].Value = "Discount Price";
                        worksheet.Cells["D1"].Worksheet.Column(4).Width = 15.0;
                        worksheet.Cells["E1"].Value = "Vendor Name";
                        worksheet.Cells["E1"].Worksheet.Column(5).Width = 20.0;
                        int num = 2;
                        int num2 = 0;
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if ((num2 > 0) && (row["PRODUCT_UPC"].ToString() == dataTable.Rows[num2 - 1]["PRODUCT_UPC"].ToString()))
                            {
                                num2++;
                            }
                            else
                            {
                                worksheet.Cells["A" + num.ToString()].Value = row["PRODUCT_NAME"].ToString();
                                worksheet.Cells["B" + num.ToString()].Value = row["PRODUCT_PRICE"];
                                worksheet.Cells["C" + num.ToString()].Value = row["PRODUCT_UPC"].ToString();
                                worksheet.Cells["D" + num.ToString()].Value = row["LABELED_PRODUCT_PRICE"];
                                worksheet.Cells["E" + num.ToString()].Value = row["LABELED_PRODUCT_VENDOR"].ToString();
                                num++;
                                num2++;
                            }
                        }
                        string fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Inventory - " + DateTime.Now.ToString("MMddyyyy") + ".xlsx";
                        FileInfo file = new FileInfo(fileName);
                        package.SaveAs(file);
                        MessageBox.Show("Data exported successfully at " + fileName, "Export");
                    }
                    else
                    {
                        MessageBox.Show("No data found to export.", "Export");
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error while exporting data - " + exception.Message, "Export");
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if (MessageBox.Show("Please confirm to export data?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataTable dataTable = new DataTable();
                    string str = "";
                    if (this.dpStartDate.Checked && this.dpEndDate.Checked)
                    {
                        str = " AND LabeledProduct.LABELED_PRODUCT_DATE BETWEEN '" + DateTime.Parse(this.dpStartDate.Value.ToShortDateString()).ToString() + "' AND '" + DateTime.Parse(this.dpEndDate.Value.ToShortDateString()).AddDays(1.0).AddSeconds(-1.0).ToString() + "'";
                    }
                    else if (!(!this.dpStartDate.Checked || this.dpEndDate.Checked))
                    {
                        str = " AND LabeledProduct.LABELED_PRODUCT_DATE BETWEEN '" + DateTime.Parse(this.dpStartDate.Value.ToShortDateString()).ToString() + "' AND '" + DateTime.Parse(this.dpStartDate.Value.ToShortDateString()).AddDays(1.0).AddSeconds(-1.0).ToString() + "'";
                    }
                    string query = "SELECT LabeledProduct.LABELED_PRODUCT_ID, LabeledProduct.LABELED_PRODUCT_VENDOR AS LABELED_PRODUCT_VENDOR, LabeledProduct.LABELED_PRODUCT_UPC AS LABELED_PRODUCT_UPC, Product.PRODUCT_NAME AS PRODUCT_NAME, Product.PRODUCT_PRICE AS PRODUCT_PRICE, LabeledProduct.LABELED_PRODUCT_PRICE AS LABELED_PRODUCT_PRICE, LabeledProduct.LABELED_PRODUCT_DATE AS LABELED_PRODUCT_DATE, LabeledProduct.IS_SELLABLE AS IS_SELLABLE FROM Product INNER JOIN LabeledProduct ON Product.PRODUCT_UPC = LabeledProduct.LABELED_PRODUCT_UPC WHERE LabeledProduct.LABELED_PRODUCT_ID > 0 ";
                    if (!this.cbAllVendors.Checked)
                    {
                        query = query + " AND LabeledProduct.LABELED_PRODUCT_VENDOR = '" + this.ddlVendors.Text.Replace("'", "''") + "'";
                    }
                    query = query + str + " GROUP BY LabeledProduct.LABELED_PRODUCT_ID, LabeledProduct.LABELED_PRODUCT_VENDOR, LabeledProduct.LABELED_PRODUCT_UPC, Product.PRODUCT_NAME, Product.PRODUCT_PRICE, LabeledProduct.LABELED_PRODUCT_PRICE, LabeledProduct.LABELED_PRODUCT_DATE, LabeledProduct.IS_SELLABLE ORDER BY 2, 3, 7";
                    dataTable = DBAccess.GetDataTable(query, DBAccess.msAccessCon);
                    if (dataTable.Rows.Count > 0)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        ExcelPackage package = new ExcelPackage();
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Retail");
                        worksheet.Cells["A1"].Value = "Vendor";
                        worksheet.Cells["A1"].Worksheet.Column(1).Width = 20.0;
                        worksheet.Cells["B1"].Value = "UPC";
                        worksheet.Cells["B1"].Worksheet.Column(2).Width = 20.0;
                        worksheet.Cells["C1"].Value = "Product Name";
                        worksheet.Cells["C1"].Worksheet.Column(3).Width = 50.0;
                        worksheet.Cells["D1"].Value = "Retail Price";
                        worksheet.Cells["D1"].Worksheet.Column(4).Width = 15.0;
                        worksheet.Cells["E1"].Value = "Printed Price";
                        worksheet.Cells["E1"].Worksheet.Column(5).Width = 15.0;
                        worksheet.Cells["F1"].Value = "Scan Date";
                        worksheet.Cells["F1"].Worksheet.Column(6).Width = 25.0;
                        worksheet.Cells["G1"].Value = "Sellable?";
                        worksheet.Cells["G1"].Worksheet.Column(6).Width = 15.0;
                        long num = 2L;
                        long num3 = 0L;
                        long num2 = num;
                        foreach (DataRow row in dataTable.Rows)
                        {
                            worksheet.Cells["A" + num.ToString()].Value = row["LABELED_PRODUCT_VENDOR"].ToString();
                            worksheet.Cells["B" + num.ToString()].Value = row["LABELED_PRODUCT_UPC"].ToString();
                            worksheet.Cells["C" + num.ToString()].Value = row["PRODUCT_NAME"].ToString();
                            worksheet.Cells["D" + num.ToString()].Value = row["PRODUCT_PRICE"];
                            worksheet.Cells["E" + num.ToString()].Value = row["LABELED_PRODUCT_PRICE"];
                            worksheet.Cells["F" + num.ToString()].Value = row["LABELED_PRODUCT_DATE"].ToString();
                            worksheet.Cells["G" + num.ToString()].Value = row["IS_SELLABLE"].ToString();
                            num += 1L;
                        }
                        num3 = num - 1L;
                        if (num2 > num3)
                        {
                            worksheet.Cells["C" + num.ToString()].Value = "Total:";
                            worksheet.Cells["D" + num.ToString()].Value = 0;
                            worksheet.Cells["E" + num.ToString()].Value = 0;
                        }
                        else
                        {
                            worksheet.Cells["C" + num.ToString()].Value = "Total:";
                            worksheet.Cells["D" + num.ToString()].Formula = "SUM(D" + num2.ToString() + ":D" + num3.ToString() + ")";
                            worksheet.Cells["E" + num.ToString()].Formula = "SUM(E" + num2.ToString() + ":E" + num3.ToString() + ")";
                        }
                        dataTable = DBAccess.GetDataTable("SELECT MAX(LABELED_PRODUCT_VENDOR) AS LABELED_PRODUCT_VENDOR, LABELED_PRODUCT_UPC, PRODUCT_NAME, AVG(PRODUCT_PRICE) AS PRODUCT_PRICE, AVG(LABELED_PRODUCT_PRICE) AS LABELED_PRODUCT_PRICE, COUNT(LABELED_PRODUCT_PRICE) AS QUANTITY, IS_SELLABLE FROM (" + query + ") GROUP BY LABELED_PRODUCT_UPC, PRODUCT_NAME, IS_SELLABLE", DBAccess.msAccessCon);
                        worksheet = package.Workbook.Worksheets.Add("Retail - Summary");
                        worksheet.Cells["A1"].Value = "Vendor";
                        worksheet.Cells["A1"].Worksheet.Column(1).Width = 20.0;
                        worksheet.Cells["B1"].Value = "UPC";
                        worksheet.Cells["B1"].Worksheet.Column(2).Width = 20.0;
                        worksheet.Cells["C1"].Value = "Product Name";
                        worksheet.Cells["C1"].Worksheet.Column(3).Width = 50.0;
                        worksheet.Cells["D1"].Value = "Retail Price";
                        worksheet.Cells["D1"].Worksheet.Column(4).Width = 15.0;
                        worksheet.Cells["E1"].Value = "Printed Price";
                        worksheet.Cells["E1"].Worksheet.Column(5).Width = 15.0;
                        worksheet.Cells["F1"].Value = "Quantity";
                        worksheet.Cells["F1"].Worksheet.Column(6).Width = 15.0;
                        worksheet.Cells["G1"].Value = "Sellable?";
                        worksheet.Cells["G1"].Worksheet.Column(6).Width = 15.0;
                        num = 2L;
                        num2 = 0L;
                        num3 = 0L;
                        num2 = num;
                        foreach (DataRow row in dataTable.Rows)
                        {
                            worksheet.Cells["A" + num.ToString()].Value = row["LABELED_PRODUCT_VENDOR"].ToString();
                            worksheet.Cells["B" + num.ToString()].Value = row["LABELED_PRODUCT_UPC"].ToString();
                            worksheet.Cells["C" + num.ToString()].Value = row["PRODUCT_NAME"].ToString();
                            worksheet.Cells["D" + num.ToString()].Formula = "=F" + num.ToString() + "*" + row["PRODUCT_PRICE"].ToString();
                            worksheet.Cells["E" + num.ToString()].Formula = "=F" + num.ToString() + "*" + row["LABELED_PRODUCT_PRICE"].ToString();
                            worksheet.Cells["F" + num.ToString()].Value = row["QUANTITY"];
                            worksheet.Cells["G" + num.ToString()].Value = row["IS_SELLABLE"].ToString();
                            num += 1L;
                        }
                        num3 = num - 1L;
                        if (num2 > num3)
                        {
                            worksheet.Cells["C" + num.ToString()].Value = "Total:";
                            worksheet.Cells["D" + num.ToString()].Value = 0;
                            worksheet.Cells["E" + num.ToString()].Value = 0;
                            worksheet.Cells["F" + num.ToString()].Value = 0;
                        }
                        else
                        {
                            worksheet.Cells["C" + num.ToString()].Value = "Total:";
                            worksheet.Cells["D" + num.ToString()].Formula = "SUM(D" + num2.ToString() + ":D" + num3.ToString() + ")";
                            worksheet.Cells["E" + num.ToString()].Formula = "SUM(E" + num2.ToString() + ":E" + num3.ToString() + ")";
                            worksheet.Cells["F" + num.ToString()].Formula = "SUM(F" + num2.ToString() + ":F" + num3.ToString() + ")";
                        }
                        string fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Retail - " + DateTime.Now.ToString("MMddyyyy") + ".xlsx";
                        FileInfo file = new FileInfo(fileName);
                        package.SaveAs(file);
                        MessageBox.Show("Report created successfully at " + fileName, "Export");
                    }
                    else
                    {
                        MessageBox.Show("No data found for report.", "Export");
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error while creating report - " + exception.Message, "Export");
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if (((this.txtExcelFile.Text != "") && (this.ddlImportVendors.Text != "")) && (this.ddlImportVendorLoad.Text != ""))
                {
                    if (MessageBox.Show("Please confirm to import data?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        string msExcelCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + this.txtExcelFile.Text.Trim() + ";Extended Properties=Excel 12.0;";
                        string query = "SELECT * FROM [Compilation$]";
                        DataTable dataTable = DBAccess.GetDataTable(query, msExcelCon);
                        if (dataTable.Rows.Count > 0)
                        {
                            OleDbConnection connection = new OleDbConnection(DBAccess.msAccessCon);
                            connection.Open();
                            foreach (DataRow row in dataTable.Rows)
                            {
                                try
                                {
                                    if ((((row[0].ToString() != "") && (row[1].ToString() != "")) && (row[2].ToString() != "")) && (row[3].ToString() != ""))
                                    {
                                        string str4;
                                        OleDbCommand command2;
                                        DataTable table2 = new DataTable();
                                        OleDbConnection connection2 = new OleDbConnection(DBAccess.msAccessCon);
                                        connection2.Open();
                                        string cmdText = "SELECT * FROM Product WHERE PRODUCT_UPC = @PRODUCT_UPC AND PRODUCT_VENDOR_LOAD_ID = @PRODUCT_VENDOR_LOAD_ID";
                                        cmdText = "SELECT * FROM Product WHERE PRODUCT_UPC = ? AND PRODUCT_VENDOR_LOAD_ID = ?";
                                        OleDbCommand selectCommand = new OleDbCommand(cmdText, connection2);
                                        selectCommand.Parameters.Add("@PRODUCT_UPC", OleDbType.VarChar, 0x19).Value = row[2].ToString();
                                        selectCommand.Parameters.Add("@PRODUCT_VENDOR_LOAD_ID", OleDbType.Integer).Value = int.Parse(this.ddlImportVendorLoad.SelectedValue.ToString());
                                        new OleDbDataAdapter(selectCommand).Fill(table2);
                                        connection2.Close();
                                        selectCommand.Dispose();
                                        connection2.Dispose();
                                        if (table2.Rows.Count == 0)
                                        {
                                            str4 = "INSERT INTO Product(PRODUCT_NAME, PRODUCT_PRICE, PRODUCT_UPC, PRODUCT_VENDOR, PRODUCT_VENDOR_LOAD_ID, PRODUCT_QUANTITY, PRODUCT_IMPORT_DATE, CREATEDATE) VALUES(@PRODUCT_NAME, @PRODUCT_PRICE, @PRODUCT_UPC, @PRODUCT_VENDOR, @PRODUCT_VENDOR_LOAD_ID, @PRODUCT_QUANTITY, @PRODUCT_IMPORT_DATE, @CREATEDATE)";
                                            str4 = "INSERT INTO Product(PRODUCT_NAME, PRODUCT_PRICE, PRODUCT_UPC, PRODUCT_VENDOR, PRODUCT_VENDOR_LOAD_ID, PRODUCT_QUANTITY, PRODUCT_IMPORT_DATE, CREATEDATE) VALUES(?, ?, ?, ?, ?, ?, ?, ?)";
                                            command2 = new OleDbCommand(str4, connection);
                                            command2.Parameters.Add("@PRODUCT_NAME", OleDbType.VarChar, 0xff).Value = row[0].ToString();
                                            command2.Parameters.Add("@PRODUCT_PRICE", OleDbType.Decimal).Value = Math.Round(Convert.ToDecimal(row[1].ToString()), 2, MidpointRounding.AwayFromZero);
                                            command2.Parameters.Add("@PRODUCT_UPC", OleDbType.VarChar, 0x19).Value = row[2].ToString();
                                            command2.Parameters.Add("@PRODUCT_VENDOR", OleDbType.VarChar, 50).Value = this.ddlImportVendors.Text;
                                            command2.Parameters.Add("@PRODUCT_VENDOR_LOAD_ID", OleDbType.Integer).Value = int.Parse(this.ddlImportVendorLoad.SelectedValue.ToString());
                                            command2.Parameters.Add("@PRODUCT_QUANTITY", OleDbType.Integer).Value = int.Parse(row[3].ToString());
                                            command2.Parameters.Add("@PRODUCT_IMPORT_DATE", OleDbType.Date).Value = this.dpImportDate.Value.Date;
                                            command2.Parameters.Add("@CREATEDATE", OleDbType.Date).Value = DateTime.Now;
                                            command2.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            string str5 = "DELETE FROM Product WHERE PRODUCT_UPC = @PRODUCT_UPC AND PRODUCT_VENDOR_LOAD_ID = @PRODUCT_VENDOR_LOAD_ID";
                                            str5 = "DELETE FROM Product WHERE PRODUCT_UPC = ? AND PRODUCT_VENDOR_LOAD_ID = ?";
                                            command2 = new OleDbCommand(str5, connection);
                                            command2.Parameters.Add("@PRODUCT_UPC", OleDbType.VarChar, 0x19).Value = row[2].ToString();
                                            command2.Parameters.Add("@PRODUCT_VENDOR_LOAD_ID", OleDbType.Integer).Value = int.Parse(this.ddlImportVendorLoad.SelectedValue.ToString());
                                            command2.ExecuteNonQuery();
                                            str4 = "INSERT INTO Product(PRODUCT_NAME, PRODUCT_PRICE, PRODUCT_UPC, PRODUCT_VENDOR, PRODUCT_VENDOR_LOAD_ID, PRODUCT_QUANTITY, PRODUCT_IMPORT_DATE, CREATEDATE) VALUES(@PRODUCT_NAME, @PRODUCT_PRICE, @PRODUCT_UPC, @PRODUCT_VENDOR, @PRODUCT_VENDOR_LOAD_ID, @PRODUCT_QUANTITY, @PRODUCT_IMPORT_DATE, @CREATEDATE)";
                                            str4 = "INSERT INTO Product(PRODUCT_NAME, PRODUCT_PRICE, PRODUCT_UPC, PRODUCT_VENDOR, PRODUCT_VENDOR_LOAD_ID, PRODUCT_QUANTITY, PRODUCT_IMPORT_DATE, CREATEDATE) VALUES(?, ?, ?, ?, ?, ?, ?, ?)";
                                            OleDbCommand command3 = new OleDbCommand(str4, connection);
                                            command3.Parameters.Add("@PRODUCT_NAME", OleDbType.VarChar, 0xff).Value = row[0].ToString();
                                            command3.Parameters.Add("@PRODUCT_PRICE", OleDbType.Decimal).Value = Math.Round(Convert.ToDecimal(row[1].ToString()), 2, MidpointRounding.AwayFromZero);
                                            command3.Parameters.Add("@PRODUCT_UPC", OleDbType.VarChar, 0x19).Value = row[2].ToString();
                                            command3.Parameters.Add("@PRODUCT_VENDOR", OleDbType.VarChar, 50).Value = this.ddlImportVendors.Text;
                                            command3.Parameters.Add("@PRODUCT_VENDOR_LOAD_ID", OleDbType.Integer).Value = int.Parse(this.ddlImportVendorLoad.SelectedValue.ToString());
                                            command3.Parameters.Add("@PRODUCT_QUANTITY", OleDbType.Integer).Value = int.Parse(row[3].ToString());
                                            command3.Parameters.Add("@PRODUCT_IMPORT_DATE", OleDbType.Date).Value = this.dpImportDate.Value.Date;
                                            command3.Parameters.Add("@CREATEDATE", OleDbType.Date).Value = DateTime.Now;
                                            command3.ExecuteNonQuery();
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                            connection.Close();
                            MessageBox.Show("Import completed successfully.", "Import");
                        }
                        else
                        {
                            MessageBox.Show("No data found to import.", "Import");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please input all fields.", "Import");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error while importing.", "Import");
            }
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            base.Close();
        }

        private void btnUserVendorSummaryReport_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if (MessageBox.Show("Please confirm to export data?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataTable dataTable = new DataTable();
                    string str = "";
                    if (this.dpStartDate.Checked && this.dpEndDate.Checked)
                    {
                        str = " AND LabeledProduct.LABELED_PRODUCT_DATE BETWEEN '" + DateTime.Parse(this.dpStartDate.Value.ToShortDateString()).ToString() + "' AND '" + DateTime.Parse(this.dpEndDate.Value.ToShortDateString()).AddDays(1.0).AddSeconds(-1.0).ToString() + "'";
                    }
                    else if (!(!this.dpStartDate.Checked || this.dpEndDate.Checked))
                    {
                        str = " AND LabeledProduct.LABELED_PRODUCT_DATE BETWEEN '" + DateTime.Parse(this.dpStartDate.Value.ToShortDateString()).ToString() + "' AND '" + DateTime.Parse(this.dpStartDate.Value.ToShortDateString()).AddDays(1.0).AddSeconds(-1.0).ToString() + "'";
                    }
                    string str2 = "SELECT StockUser.USER_FULL_NAME, COUNT(StockUser.USER_FULL_NAME) AS COUNTING, SUM(LabeledProduct.LABELED_PRODUCT_PRICE) AS AMOUNT FROM LabeledProduct, StockUser WHERE LabeledProduct.LABELED_PRODUCT_USER = StockUser.USER_ID ";
                    dataTable = DBAccess.GetDataTable(str2 + str + " GROUP BY StockUser.USER_FULL_NAME ORDER BY 1", DBAccess.msAccessCon);
                    if (dataTable.Rows.Count > 0)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        ExcelPackage package = new ExcelPackage();
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("User - Summary");
                        worksheet.Cells["A1"].Value = "User Name";
                        worksheet.Cells["A1"].Worksheet.Column(1).Width = 30.0;
                        worksheet.Cells["B1"].Value = "' of Items";
                        worksheet.Cells["B1"].Worksheet.Column(2).Width = 15.0;
                        worksheet.Cells["C1"].Value = "Total Amount";
                        worksheet.Cells["C1"].Worksheet.Column(3).Width = 20.0;
                        long num = 2L;
                        foreach (DataRow row in dataTable.Rows)
                        {
                            worksheet.Cells["A" + num.ToString()].Value = row["USER_FULL_NAME"].ToString();
                            worksheet.Cells["B" + num.ToString()].Value = row["COUNTING"];
                            worksheet.Cells["C" + num.ToString()].Value = row["AMOUNT"];
                            num += 1L;
                        }
                        dataTable = DBAccess.GetDataTable("SELECT LabeledProduct.LABELED_PRODUCT_VENDOR, COUNT(LabeledProduct.LABELED_PRODUCT_VENDOR) AS COUNTING, SUM(LabeledProduct.LABELED_PRODUCT_PRICE) AS AMOUNT FROM LabeledProduct WHERE LabeledProduct.LABELED_PRODUCT_ID > 0 " + str + " GROUP BY LabeledProduct.LABELED_PRODUCT_VENDOR ORDER BY 1", DBAccess.msAccessCon);
                        worksheet = package.Workbook.Worksheets.Add("Vendor - Summary");
                        worksheet.Cells["A1"].Value = "Vendor";
                        worksheet.Cells["A1"].Worksheet.Column(1).Width = 30.0;
                        worksheet.Cells["B1"].Value = "' of Items";
                        worksheet.Cells["B1"].Worksheet.Column(2).Width = 15.0;
                        worksheet.Cells["C1"].Value = "Total Amount";
                        worksheet.Cells["C1"].Worksheet.Column(3).Width = 20.0;
                        num = 2L;
                        foreach (DataRow row in dataTable.Rows)
                        {
                            worksheet.Cells["A" + num.ToString()].Value = row["LABELED_PRODUCT_VENDOR"].ToString();
                            worksheet.Cells["B" + num.ToString()].Value = row["COUNTING"];
                            worksheet.Cells["C" + num.ToString()].Value = row["AMOUNT"];
                            num += 1L;
                        }
                        string fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\User & Vendor Summary (Scanned).xlsx";
                        FileInfo file = new FileInfo(fileName);
                        package.SaveAs(file);
                        MessageBox.Show("Report created successfully at " + fileName, "Export");
                    }
                    else
                    {
                        MessageBox.Show("No data found for report.", "Export");
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error while creating report - " + exception.Message, "Export");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if ((this.ddlVendorLoadReport.Text == "") || (this.ddlLoadLoadReport.Text == ""))
                {
                    MessageBox.Show("Please input all fields.", "Export");
                }
                else if (MessageBox.Show("Please confirm to export data?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataTable dataTable = new DataTable();
                    dataTable = DBAccess.GetDataTable("SELECT PRODUCT_ID, PRODUCT_NAME, PRODUCT_UPC, PRODUCT_VENDOR, PRODUCT_QUANTITY FROM Product WHERE PRODUCT_VENDOR_LOAD_ID = " + this.ddlLoadLoadReport.SelectedValue.ToString() + " ORDER BY PRODUCT_UPC", DBAccess.msAccessCon);
                    if (dataTable.Rows.Count > 0)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        ExcelPackage package = new ExcelPackage();
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Load");
                        worksheet.Cells["A1"].Value = "Vendor";
                        worksheet.Cells["A1"].Worksheet.Column(1).Width = 20.0;
                        worksheet.Cells["B1"].Value = "UPC";
                        worksheet.Cells["B1"].Worksheet.Column(2).Width = 20.0;
                        worksheet.Cells["C1"].Value = "Product Name";
                        worksheet.Cells["C1"].Worksheet.Column(3).Width = 50.0;
                        worksheet.Cells["D1"].Value = "Quantity";
                        worksheet.Cells["D1"].Worksheet.Column(4).Width = 15.0;
                        worksheet.Cells["E1"].Value = "Scanned";
                        worksheet.Cells["E1"].Worksheet.Column(5).Width = 15.0;
                        long num = 2L;
                        foreach (DataRow row in dataTable.Rows)
                        {
                            worksheet.Cells["A" + num.ToString()].Value = row["PRODUCT_VENDOR"].ToString();
                            worksheet.Cells["B" + num.ToString()].Value = row["PRODUCT_UPC"].ToString();
                            worksheet.Cells["C" + num.ToString()].Value = row["PRODUCT_NAME"].ToString();
                            worksheet.Cells["D" + num.ToString()].Value = row["PRODUCT_QUANTITY"];
                            int count = 0;
                            try
                            {
                                DataTable table2 = new DataTable();
                                count = DBAccess.GetDataTable("SELECT LABELED_PRODUCT_ID FROM LabeledProduct WHERE LABELED_PRODUCT_PRODUCT_ID = " + row["PRODUCT_ID"].ToString(), DBAccess.msAccessCon).Rows.Count;
                            }
                            catch (Exception)
                            {
                                count = 0;
                            }
                            worksheet.Cells["E" + num.ToString()].Value = count;
                            num += 1L;
                        }
                        string fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Load Report - " + this.ddlLoadLoadReport.Text + ".xlsx";
                        FileInfo file = new FileInfo(fileName);
                        package.SaveAs(file);
                        MessageBox.Show("Report created successfully at " + fileName, "Export");
                    }
                    else
                    {
                        MessageBox.Show("No data found for report.", "Export");
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error while creating report - " + exception.Message, "Export");
            }
        }

        private void cbAllVendors_CheckedChanged(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            if (this.cbAllVendors.Checked)
            {
                this.ddlVendors.Enabled = false;
            }
            else
            {
                this.ddlVendors.Enabled = true;
            }
        }

        private void ddlImportVendors_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            this.FillVendorLoads(this.ddlImportVendors.Text);
        }

        private void ddlVendorLoadReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            this.FillVendorLoadsReport(this.ddlVendorLoadReport.Text);
        }


        private void FillVendorLoads(string vendorName)
        {
            this.ddlImportVendorLoad.DataSource = null;
            this.ddlImportVendorLoad.Items.Clear();
            DataTable dataTable = new DataTable();
            OleDbConnection connection = new OleDbConnection(DBAccess.msAccessCon);
            connection.Open();
            string cmdText = "SELECT * FROM Vendor WHERE VENDOR_NAME = @VENDOR_NAME";
            cmdText = "SELECT * FROM Vendor WHERE VENDOR_NAME = ?";
            OleDbCommand selectCommand = new OleDbCommand(cmdText, connection);
            selectCommand.Parameters.Add("@VENDOR_NAME", OleDbType.VarChar, 50).Value = vendorName;
            new OleDbDataAdapter(selectCommand).Fill(dataTable);
            connection.Close();
            selectCommand.Dispose();
            connection.Dispose();
            if (dataTable.Rows.Count > 0)
            {
                DataTable table2 = DBAccess.GetDataTable("SELECT VENDOR_LOAD_ID, VENDOR_LOAD_NAME FROM VendorLoad WHERE VENDOR_LOAD_VENDOR_ID = " + dataTable.Rows[0]["VENDOR_ID"].ToString() + " ORDER BY VENDOR_LOAD_ID", DBAccess.msAccessCon);
                this.ddlImportVendorLoad.DisplayMember = "VENDOR_LOAD_NAME";
                this.ddlImportVendorLoad.ValueMember = "VENDOR_LOAD_ID";
                this.ddlImportVendorLoad.DataSource = table2;
            }
        }

        private void FillVendorLoadsReport(string vendorName)
        {
            this.ddlLoadLoadReport.DataSource = null;
            this.ddlLoadLoadReport.Items.Clear();
            DataTable dataTable = new DataTable();
            OleDbConnection connection = new OleDbConnection(DBAccess.msAccessCon);
            connection.Open();
            string cmdText = "SELECT * FROM Vendor WHERE VENDOR_NAME = @VENDOR_NAME";
            cmdText = "SELECT * FROM Vendor WHERE VENDOR_NAME = ?";
            OleDbCommand selectCommand = new OleDbCommand(cmdText, connection);
            selectCommand.Parameters.Add("@VENDOR_NAME", OleDbType.VarChar, 50).Value = vendorName;
            new OleDbDataAdapter(selectCommand).Fill(dataTable);
            connection.Close();
            selectCommand.Dispose();
            connection.Dispose();
            if (dataTable.Rows.Count > 0)
            {
                DataTable table2 = DBAccess.GetDataTable("SELECT VENDOR_LOAD_ID, VENDOR_LOAD_NAME FROM VendorLoad WHERE VENDOR_LOAD_VENDOR_ID = " + dataTable.Rows[0]["VENDOR_ID"].ToString() + " ORDER BY VENDOR_LOAD_ID", DBAccess.msAccessCon);
                this.ddlLoadLoadReport.DisplayMember = "VENDOR_LOAD_NAME";
                this.ddlLoadLoadReport.ValueMember = "VENDOR_LOAD_ID";
                this.ddlLoadLoadReport.DataSource = table2;
            }
        }

        private void importForm_Load(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if (Session.isManager == "No")
                {
                    base.Close();
                }
                string query = "SELECT VENDOR_ID, VENDOR_NAME FROM Vendor ORDER BY VENDOR_NAME";
                DataTable dataTable = DBAccess.GetDataTable(query, DBAccess.msAccessCon);
                this.ddlVendors.DisplayMember = "VENDOR_NAME";
                this.ddlVendors.ValueMember = "VENDOR_NAME";
                this.ddlVendors.DataSource = dataTable;
                this.ddlImportVendors.DisplayMember = "VENDOR_NAME";
                this.ddlImportVendors.ValueMember = "VENDOR_NAME";
                this.ddlImportVendors.DataSource = dataTable;
                this.ddlVendorLoadReport.DisplayMember = "VENDOR_NAME";
                this.ddlVendorLoadReport.ValueMember = "VENDOR_NAME";
                this.ddlVendorLoadReport.DataSource = dataTable;
                this.FillVendorLoads(this.ddlImportVendors.Text);
                this.FillVendorLoadsReport(this.ddlVendorLoadReport.Text);
            }
            catch (Exception)
            {
            }
        }
    }
}
