using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;


namespace DealStore
{
    public partial class frmDataEntry : Form
    {
        private Button btnClearForm;
        private Button btnGenerateReport;
        private Button btnMainMenu;
        private Button btnSave;
        private Button btnUserVendorSummaryReport;
        private CheckBox cbNoPrice;
        private ComboBox ddlReportVendors;
        private ComboBox ddlVendors;
        private DateTimePicker dpEndDate;
        private DateTimePicker dpStartDate;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label1;
        private Label label11;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label8;
        private Label label9;
        private TextBox txtPrice;
        private TextBox txtProduct;
        private TextBox txtUPC;
        public frmDataEntry()
        {
            this.components = null;
            InitializeComponent();
        }
        private void btnClearForm_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.txtUPC.Text = this.txtProduct.Text = this.txtPrice.Text = "";
                this.txtUPC.Focus();
            }
            catch (Exception)
            {
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if (this.ddlReportVendors.Text != "")
                {
                    if (MessageBox.Show("Please confirm to export data?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        DataTable dataTable = new DataTable();
                        string str = "";
                        if (this.dpStartDate.Checked && this.dpEndDate.Checked)
                        {
                            str = " AND NEW_STOCK_ENTRY_DATE BETWEEN '" + DateTime.Parse(this.dpStartDate.Value.ToShortDateString()).ToString() + "' AND '" + DateTime.Parse(this.dpEndDate.Value.ToShortDateString()).AddDays(1.0).AddSeconds(-1.0).ToString() + "'";
                        }
                        else if (!(!this.dpStartDate.Checked || this.dpEndDate.Checked))
                        {
                            str = " AND NEW_STOCK_ENTRY_DATE BETWEEN '" + DateTime.Parse(this.dpStartDate.Value.ToShortDateString()).ToString() + "' AND '" + DateTime.Parse(this.dpStartDate.Value.ToShortDateString()).AddDays(1.0).AddSeconds(-1.0).ToString() + "'";
                        }
                        if (this.cbNoPrice.Checked)
                        {
                            str = str + " AND NEW_STOCK_PRICE IS NULL";
                        }
                        dataTable = DBAccess.GetDataTable(("SELECT NEW_STOCK_PRODUCT, NEW_STOCK_PRICE, NEW_STOCK_UPC FROM NewStock WHERE NEW_STOCK_VENDOR = '" + this.ddlReportVendors.Text.Replace("'", "''") + "'") + str + " ORDER BY 1", DBAccess.msAccessCon);
                        if (dataTable.Rows.Count > 0)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            ExcelPackage package = new ExcelPackage();
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Compilation");
                            worksheet.Cells["A1"].Value = "Product";
                            worksheet.Cells["A1"].Worksheet.Column(1).Width = 50.0;
                            worksheet.Cells["B1"].Value = "Price";
                            worksheet.Cells["B1"].Worksheet.Column(2).Width = 15.0;
                            worksheet.Cells["C1"].Value = "UPC";
                            worksheet.Cells["C1"].Worksheet.Column(3).Width = 30.0;
                            long num = 2L;
                            foreach (DataRow row in dataTable.Rows)
                            {
                                worksheet.Cells["A" + num.ToString()].Value = row["NEW_STOCK_PRODUCT"].ToString();
                                worksheet.Cells["B" + num.ToString()].Value = row["NEW_STOCK_PRICE"];
                                worksheet.Cells["C" + num.ToString()].Value = row["NEW_STOCK_UPC"].ToString();
                                num += 1L;
                            }
                            dataTable = DBAccess.GetDataTable(("SELECT StockUser.USER_FULL_NAME, COUNT(StockUser.USER_FULL_NAME) AS COUNTING, SUM(NewStock.NEW_STOCK_PRICE) AS AMOUNT FROM NewStock, StockUser WHERE NewStock.NEW_STOCK_USER = StockUser.USER_ID AND NewStock.NEW_STOCK_VENDOR = '" + this.ddlReportVendors.Text.Replace("'", "''") + "'") + str + " GROUP BY StockUser.USER_FULL_NAME ORDER BY 1", DBAccess.msAccessCon);
                            worksheet = package.Workbook.Worksheets.Add("User - Summary");
                            worksheet.Cells["A1"].Value = "User Name";
                            worksheet.Cells["A1"].Worksheet.Column(1).Width = 30.0;
                            worksheet.Cells["B1"].Value = "' of Items";
                            worksheet.Cells["B1"].Worksheet.Column(2).Width = 15.0;
                            worksheet.Cells["C1"].Value = "Total Amount";
                            worksheet.Cells["C1"].Worksheet.Column(3).Width = 20.0;
                            num = 2L;
                            foreach (DataRow row in dataTable.Rows)
                            {
                                worksheet.Cells["A" + num.ToString()].Value = row["USER_FULL_NAME"].ToString();
                                worksheet.Cells["B" + num.ToString()].Value = row["COUNTING"];
                                worksheet.Cells["C" + num.ToString()].Value = row["AMOUNT"];
                                num += 1L;
                            }
                            dataTable = DBAccess.GetDataTable(("SELECT NewStock.NEW_STOCK_VENDOR, COUNT(NewStock.NEW_STOCK_VENDOR) AS COUNTING, SUM(NewStock.NEW_STOCK_PRICE) AS AMOUNT FROM NewStock WHERE NewStock.NEW_STOCK_VENDOR = '" + this.ddlReportVendors.Text.Replace("'", "''") + "'") + str + " GROUP BY NewStock.NEW_STOCK_VENDOR ORDER BY 1", DBAccess.msAccessCon);
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
                                worksheet.Cells["A" + num.ToString()].Value = row["NEW_STOCK_VENDOR"].ToString();
                                worksheet.Cells["B" + num.ToString()].Value = row["COUNTING"];
                                worksheet.Cells["C" + num.ToString()].Value = row["AMOUNT"];
                                num += 1L;
                            }
                            string fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Data Entry - " + this.ddlReportVendors.Text + ".xlsx";
                            FileInfo file = new FileInfo(fileName);
                            package.SaveAs(file);
                            MessageBox.Show("Report created successfully at " + fileName, "Data Entry");
                        }
                        else
                        {
                            MessageBox.Show("No data found for report.", "Data Entry");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please input all fields.", "Data Entry");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error while creating report - " + exception.Message, "Data Entry");
            }
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            base.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OleDbConnection connection;
            string str;
            OleDbCommand command;
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if (((this.txtUPC.Text.Trim() != "") && (this.txtProduct.Text.Trim() != "")) && (this.ddlVendors.Text != ""))
                {
                    connection = new OleDbConnection(DBAccess.msAccessCon);
                    connection.Open();
                    str = "INSERT INTO NewStock(NEW_STOCK_UPC, NEW_STOCK_PRODUCT, NEW_STOCK_PRICE, NEW_STOCK_VENDOR, NEW_STOCK_USER, NEW_STOCK_ENTRY_DATE) VALUES(@NEW_STOCK_UPC, @NEW_STOCK_PRODUCT, @NEW_STOCK_PRICE, @NEW_STOCK_VENDOR, @NEW_STOCK_USER, @NEW_STOCK_ENTRY_DATE)";
                    str = "INSERT INTO NewStock(NEW_STOCK_UPC, NEW_STOCK_PRODUCT, NEW_STOCK_PRICE, NEW_STOCK_VENDOR, NEW_STOCK_USER, NEW_STOCK_ENTRY_DATE) VALUES(?, ?, ?, ?, ?, ?)";
                    command = new OleDbCommand(str, connection);
                    command.Parameters.Add("@NEW_STOCK_UPC", OleDbType.VarChar, 0x19).Value = this.txtUPC.Text.Trim();
                    command.Parameters.Add("@NEW_STOCK_PRODUCT", OleDbType.VarChar, 0xff).Value = this.txtProduct.Text.Trim();
                    if (this.txtPrice.Text.Trim() == "")
                    {
                        command.Parameters.Add("@NEW_STOCK_PRICE", OleDbType.Decimal).Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters.Add("@NEW_STOCK_PRICE", OleDbType.Decimal).Value = decimal.Parse(this.txtPrice.Text.Trim());
                    }
                    command.Parameters.Add("@NEW_STOCK_VENDOR", OleDbType.VarChar, 50).Value = this.ddlVendors.Text;
                    command.Parameters.Add("@NEW_STOCK_USER", OleDbType.Integer).Value = Session.userID;
                    command.Parameters.Add("@NEW_STOCK_ENTRY_DATE", OleDbType.Date).Value = DateTime.Now;
                    command.ExecuteNonQuery();
                    connection.Close();
                    connection.Dispose();
                    MessageBox.Show("Data inserted successfully.", "Data Entry");
                    this.btnClearForm_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Please input all fields.", "Data Entry");
                }
            }
            catch (Exception exception)
            {
                if (exception.Message.ToLower().Contains("duplicate"))
                {
                    if (MessageBox.Show("UPC already exist. Do you want to override?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        connection = new OleDbConnection(DBAccess.msAccessCon);
                        connection.Open();
                        string cmdText = "DELETE FROM NewStock WHERE NEW_STOCK_UPC = @NEW_STOCK_UPC";
                        cmdText = "DELETE FROM NewStock WHERE NEW_STOCK_UPC = ?";
                        OleDbCommand command2 = new OleDbCommand(cmdText, connection);
                        command2.Parameters.Add("@NEW_STOCK_UPC", OleDbType.VarChar, 0x19).Value = this.txtUPC.Text.Trim();
                        command2.ExecuteNonQuery();
                        str = "INSERT INTO NewStock(NEW_STOCK_UPC, NEW_STOCK_PRODUCT, NEW_STOCK_PRICE, NEW_STOCK_VENDOR, NEW_STOCK_USER, NEW_STOCK_ENTRY_DATE) VALUES(@NEW_STOCK_UPC, @NEW_STOCK_PRODUCT, @NEW_STOCK_PRICE, @NEW_STOCK_VENDOR, @NEW_STOCK_USER, @NEW_STOCK_ENTRY_DATE)";
                        str = "INSERT INTO NewStock(NEW_STOCK_UPC, NEW_STOCK_PRODUCT, NEW_STOCK_PRICE, NEW_STOCK_VENDOR, NEW_STOCK_USER, NEW_STOCK_ENTRY_DATE) VALUES(?, ?, ?, ?, ?, ?)";
                        command = new OleDbCommand(str, connection);
                        command.Parameters.Add("@NEW_STOCK_UPC", OleDbType.VarChar, 0x19).Value = this.txtUPC.Text.Trim();
                        command.Parameters.Add("@NEW_STOCK_PRODUCT", OleDbType.VarChar, 0xff).Value = this.txtProduct.Text.Trim();
                        if (this.txtPrice.Text.Trim() == "")
                        {
                            command.Parameters.Add("@NEW_STOCK_PRICE", OleDbType.Decimal).Value = DBNull.Value;
                        }
                        else
                        {
                            command.Parameters.Add("@NEW_STOCK_PRICE", OleDbType.Decimal).Value = decimal.Parse(this.txtPrice.Text.Trim());
                        }
                        command.Parameters.Add("@NEW_STOCK_VENDOR", OleDbType.VarChar, 50).Value = this.ddlVendors.Text;
                        command.Parameters.Add("@NEW_STOCK_USER", OleDbType.Integer).Value = Session.userID;
                        command.Parameters.Add("@NEW_STOCK_ENTRY_DATE", OleDbType.Date).Value = DateTime.Now;
                        command.ExecuteNonQuery();
                        connection.Close();
                        connection.Dispose();
                        MessageBox.Show("Data inserted successfully.", "Data Entry");
                        this.btnClearForm_Click(null, null);
                    }
                }
                else
                {
                    MessageBox.Show(exception.Message, "Data Entry");
                }
            }
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
                        str = " AND NEW_STOCK_ENTRY_DATE BETWEEN '" + DateTime.Parse(this.dpStartDate.Value.ToShortDateString()).ToString() + "' AND '" + DateTime.Parse(this.dpEndDate.Value.ToShortDateString()).AddDays(1.0).AddSeconds(-1.0).ToString() + "'";
                    }
                    else if (!(!this.dpStartDate.Checked || this.dpEndDate.Checked))
                    {
                        str = " AND NEW_STOCK_ENTRY_DATE BETWEEN '" + DateTime.Parse(this.dpStartDate.Value.ToShortDateString()).ToString() + "' AND '" + DateTime.Parse(this.dpStartDate.Value.ToShortDateString()).AddDays(1.0).AddSeconds(-1.0).ToString() + "'";
                    }
                    string str2 = "SELECT StockUser.USER_FULL_NAME, COUNT(StockUser.USER_FULL_NAME) AS COUNTING, SUM(NewStock.NEW_STOCK_PRICE) AS AMOUNT FROM NewStock, StockUser WHERE NewStock.NEW_STOCK_USER = StockUser.USER_ID ";
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
                        dataTable = DBAccess.GetDataTable("SELECT NewStock.NEW_STOCK_VENDOR, COUNT(NewStock.NEW_STOCK_VENDOR) AS COUNTING, SUM(NewStock.NEW_STOCK_PRICE) AS AMOUNT FROM NewStock WHERE NewStock.NEW_STOCK_ID > 0 " + str + " GROUP BY NewStock.NEW_STOCK_VENDOR ORDER BY 1", DBAccess.msAccessCon);
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
                            worksheet.Cells["A" + num.ToString()].Value = row["NEW_STOCK_VENDOR"].ToString();
                            worksheet.Cells["B" + num.ToString()].Value = row["COUNTING"];
                            worksheet.Cells["C" + num.ToString()].Value = row["AMOUNT"];
                            num += 1L;
                        }
                        string fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\User & Vendor Summary (New Stock).xlsx";
                        FileInfo file = new FileInfo(fileName);
                        package.SaveAs(file);
                        MessageBox.Show("Report created successfully at " + fileName, "Data Entry");
                    }
                    else
                    {
                        MessageBox.Show("No data found for report.", "Data Entry");
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error while creating report - " + exception.Message, "Data Entry");
            }
        }

      

        private void frmDataEntry_Load(object sender, EventArgs e)
        {
        //    MessageBox.Show("Loading");
            Session.dtLoginTime = DateTime.Now;
            try
            {
                string query = "SELECT VENDOR_ID, VENDOR_NAME FROM Vendor ORDER BY VENDOR_NAME";
                DataTable dataTable = DBAccess.GetDataTable(query, DBAccess.msAccessCon);
                this.ddlVendors.DisplayMember = "VENDOR_NAME";
                this.ddlVendors.ValueMember = "VENDOR_NAME";
                this.ddlVendors.DataSource = dataTable;
                this.ddlReportVendors.DisplayMember = "VENDOR_NAME";
                this.ddlReportVendors.ValueMember = "VENDOR_NAME";
                this.ddlReportVendors.DataSource = dataTable;
            }
            catch (Exception)
            {
            }
        }

    }
}
