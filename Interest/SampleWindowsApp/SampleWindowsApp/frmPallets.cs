using OfficeOpenXml;
using OfficeOpenXml.Style;
using RichTextBoxPrintCtrl;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace DealStore
{
    public partial class frmPallets : Form
    {
        private Button btnAdd;
        private Button btnClose;
        private Button btnDelete;
        private Button btnDeletePalletItem;
        private Button btnDetails;
        private Button btnGenerateBarcode;
        private Button btnGenerateReport;
        private Button btnMainMenu;
        private ComboBox ddlVendorLoad;
        private ComboBox ddlVendors;
        private GroupBox gbPalletDetails;
        private GroupBox groupBox2;
        private DataGridView gvPalletProducts;
        private DataGridView gvPallets;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lblLoadName;
        private Label lblPallet;
        private Label lblPalletID;
        private Label lblPalletLocation;
        private Label lblTotalItems;
        private Label lblTotalPriceActual;
        private Label lblTotalPriceLabel;
        private Label lblVendorName;
        private PrintDocument printDocument1;
        private RichTextBoxPrintCtrl.RichTextBoxPrintCtrl richTextBoxPrintCtrl1;
        private TextBox txtPallet;
        private TextBox txtPalletLocation;
        public frmPallets()
        {
            InitializeComponent();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if ((((this.txtPallet.Text.Trim() == "") || (this.ddlVendors.Text == "")) || (this.ddlVendorLoad.Text == "")) || (this.txtPalletLocation.Text.Trim() == ""))
                {
                    MessageBox.Show("Please input all fields.", "Pallets");
                }
                else
                {
                    OleDbConnection connection = new OleDbConnection(DBAccess.msAccessCon);
                    connection.Open();
                    string cmdText = "INSERT INTO LoadPallet(LOAD_PALLET_NAME, LOAD_PALLET_LOCATION, LOAD_PALLET_LOAD_ID, CREATEDBY, CREATEDATE, MODIFIEDBY, MODIFIEDDATE) VALUES(@LOAD_PALLET_NAME, @LOAD_PALLET_LOCATION, @LOAD_PALLET_LOAD_ID, @CREATEDBY, @CREATEDATE, @MODIFIEDBY, @MODIFIEDDATE)";
                    cmdText = "INSERT INTO LoadPallet(LOAD_PALLET_NAME, LOAD_PALLET_LOCATION, LOAD_PALLET_LOAD_ID, CREATEDBY, CREATEDATE, MODIFIEDBY, MODIFIEDDATE) VALUES(?, ?, ?, ?, ?, ?, ?)";
                    OleDbCommand command = new OleDbCommand(cmdText, connection);
                    command.Parameters.Add("@LOAD_PALLET_NAME", OleDbType.VarChar, 100).Value = this.txtPallet.Text.Trim();
                    command.Parameters.Add("@LOAD_PALLET_LOCATION", OleDbType.VarChar, 100).Value = this.txtPalletLocation.Text.Trim();
                    command.Parameters.Add("@LOAD_PALLET_LOAD_ID", OleDbType.Integer).Value = int.Parse(this.ddlVendorLoad.SelectedValue.ToString());
                    command.Parameters.Add("@CREATEDBY", OleDbType.Integer).Value = Session.userID;
                    command.Parameters.Add("@CREATEDATE", OleDbType.VarChar, 0x19).Value = DateTime.Now;
                    command.Parameters.Add("@MODIFIEDBY", OleDbType.Integer).Value = Session.userID;
                    command.Parameters.Add("@MODIFIEDDATE", OleDbType.Date).Value = DateTime.Now;
                    if (command.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Pallet added successfully.", "Pallets");
                        this.FillPallets();
                        this.txtPallet.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Pallet addition failed.", "Pallets");
                    }
                    connection.Close();
                    command.Dispose();
                    connection.Dispose();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            this.gbPalletDetails.Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                DataGridViewRow row = this.gvPallets.SelectedRows[0];
                int num = int.Parse(row.Cells[0].Value.ToString());
                OleDbConnection connection = new OleDbConnection(DBAccess.msAccessCon);
                connection.Open();
                OleDbCommand command = new OleDbCommand("DELETE FROM LoadPallet WHERE LOAD_PALLET_ID = " + num.ToString(), connection);
                int num2 = command.ExecuteNonQuery();
                connection.Close();
                command.Dispose();
                connection.Dispose();
                if (num2 > 0)
                {
                    MessageBox.Show("Pallet deleted successfully.", "Pallets");
                    this.FillPallets();
                }
                else
                {
                    MessageBox.Show("Pallet deletion failed.", "Pallets");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Pallet deletion failed. " + exception.Message, "Pallets");
            }
        }

        private void btnDeletePalletItem_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                DataGridViewRow row = this.gvPalletProducts.SelectedRows[0];
                int num = int.Parse(row.Cells[0].Value.ToString());
                OleDbConnection connection = new OleDbConnection(DBAccess.msAccessCon);
                connection.Open();
                OleDbCommand command = new OleDbCommand("DELETE FROM LoadPalletProduct WHERE LOAD_PALLET_PRODUCT_ID = " + num.ToString(), connection);
                int num2 = command.ExecuteNonQuery();
                connection.Close();
                command.Dispose();
                connection.Dispose();
                if (num2 > 0)
                {
                    MessageBox.Show("Item deleted successfully.", "Pallet Details");
                    this.FillPalletDetails();
                }
                else
                {
                    MessageBox.Show("Item deletion failed.", "Pallet Details");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Item deletion failed. " + exception.Message, "Pallet Details");
            }
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                DataGridViewRow row = this.gvPallets.SelectedRows[0];
                Session.PalletID = int.Parse(row.Cells[0].Value.ToString());
                this.FillPalletDetails();
                this.gbPalletDetails.Visible = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Vendor");
            }
        }

        private void btnGenerateBarcode_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            int right = 5;
            int left = 7;
            int top = 0;
            int bottom = 0;
            int width = 0xd0;
            int height = 200;
            string str = "Overstock Miami";
            string str2 = this.lblPallet.Text ?? "";
            string str3 = this.lblVendorName.Text + " Price : $" + decimal.Parse(this.lblTotalPriceActual.Text).ToString("0.00");
            string str4 = "OUR PRICE $" + decimal.Parse(this.lblTotalPriceLabel.Text).ToString("0.00");
            string str5 = "";
            string str6 = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fswiss\fcharset0 Verdana;}{\f1\fcharset0 IDAutomationHC39M;}{\f2\fnil\fcharset0 Calibri;}}{\colortbl ;\red163\green21\blue21;}\paperw4507\paperh2995\margl5\margr5\margt0\margb0\";
            string str7 = @"viewkind4\uc1\pard\sl371\slmult0\lang9\b\f0\fs22 ";
            string str8 = @"\par";
            string str9 = @"\pard\sl0\slmult0\qj\b0\fs14 ";
            string str10 = @"\cf1\par";
            string str11 = @"\pard\sl-331\slmult0\qj\cf0\b\fs16 ";
            string str12 = @"\par";
            string str13 = @"\fs24 ";
            if (str4.Length > 0x11)
            {
                str13 = @"\fs23 ";
            }
            if (str4.Length < 0x11)
            {
                str13 = @"\fs25 ";
            }
            string str14 = @"\fs16 ";
            string str15 = @"\par";
            string str16 = @"\b0 " + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString() + Session.userID.ToString().PadLeft(2, '0') + @"\b\par";
            string str17 = @"\pard\sl-831\slmult0\qj\b0\f1\fs18";
            string str18 = @"\f2\fs22\par";
            string str19 = "}";
            this.richTextBoxPrintCtrl1.Rtf = str6 + str7 + str + str8 + str9 + str2 + str10 + str11 + str3 + str12 + str13 + str4 + str15 + str14 + str16 + str17 + str5 + str18 + str19;
            Margins margins = new Margins(left, right, top, bottom);
            this.printDocument1.DefaultPageSettings.Margins = margins;
            this.printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Custom", width, height);
            this.printDocument1.Print();
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if (MessageBox.Show("Please confirm to export data?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (this.gvPalletProducts.Rows.Count > 0)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        ExcelPackage package = new ExcelPackage();
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Pallet");
                        worksheet.Cells["A1"].Value = "Vendor: ";
                        worksheet.Cells["A1"].Style.Font.Bold = true;
                        worksheet.Cells["B1"].Value = this.lblVendorName.Text;
                        worksheet.Cells["A2"].Value = "Pallet: ";
                        worksheet.Cells["A2"].Style.Font.Bold = true;
                        worksheet.Cells["B2"].Value = this.lblPallet.Text;
                        worksheet.Cells["A3"].Value = "Location: ";
                        worksheet.Cells["A3"].Style.Font.Bold = true;
                        worksheet.Cells["B3"].Value = this.lblPalletLocation.Text;
                        worksheet.Cells["A4"].Value = "Total Items: ";
                        worksheet.Cells["A4"].Style.Font.Bold = true;
                        worksheet.Cells["B4"].Value = this.lblTotalItems.Text;
                        worksheet.Cells["A5"].Value = "Total Price(Actual): ";
                        worksheet.Cells["A5"].Style.Font.Bold = true;
                        worksheet.Cells["B5"].Value = "$" + this.lblTotalPriceActual.Text;
                        worksheet.Cells["A6"].Value = "Total Price(Label): ";
                        worksheet.Cells["A6"].Style.Font.Bold = true;
                        worksheet.Cells["B6"].Value = "$" + this.lblTotalPriceLabel.Text;
                        worksheet.Cells["A8"].Value = "UPC";
                        worksheet.Cells["A8"].Style.Font.Bold = true;
                        worksheet.Cells["A8"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells["A8"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        worksheet.Cells["A8"].Worksheet.Column(1).Width = 20.0;
                        worksheet.Cells["B8"].Value = "Product Name";
                        worksheet.Cells["B8"].Style.Font.Bold = true;
                        worksheet.Cells["B8"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells["B8"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        worksheet.Cells["B8"].Worksheet.Column(2).Width = 50.0;
                        worksheet.Cells["C8"].Value = "Actual Price";
                        worksheet.Cells["C8"].Style.Font.Bold = true;
                        worksheet.Cells["C8"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells["C8"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        worksheet.Cells["C8"].Worksheet.Column(3).Width = 13.0;
                        worksheet.Cells["D8"].Value = "Label Price";
                        worksheet.Cells["D8"].Style.Font.Bold = true;
                        worksheet.Cells["D8"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells["D8"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        worksheet.Cells["D8"].Worksheet.Column(4).Width = 13.0;
                        worksheet.Cells["E8"].Value = "Quantity";
                        worksheet.Cells["E8"].Style.Font.Bold = true;
                        worksheet.Cells["E8"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells["E8"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        worksheet.Cells["E8"].Worksheet.Column(5).Width = 10.0;
                        long num = 9L;
                        foreach (DataGridViewRow row in (IEnumerable)this.gvPalletProducts.Rows)
                        {
                            worksheet.Cells["A" + num.ToString()].Value = row.Cells[1].Value.ToString();
                            worksheet.Cells["B" + num.ToString()].Value = row.Cells[2].Value.ToString();
                            worksheet.Cells["C" + num.ToString()].Value = decimal.Parse(row.Cells[3].Value.ToString()).ToString("0.00");
                            worksheet.Cells["D" + num.ToString()].Value = decimal.Parse(row.Cells[4].Value.ToString()).ToString("0.00");
                            worksheet.Cells["E" + num.ToString()].Value = row.Cells[5].Value;
                            num += 1L;
                        }
                        long num3 = num - 1L;
                        worksheet.Cells["A8:E" + num3.ToString()].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        num3 = num - 1L;
                        worksheet.Cells["A8:E" + num3.ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        num3 = num - 1L;
                        worksheet.Cells["A8:E" + num3.ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        num3 = num - 1L;
                        worksheet.Cells["A8:E" + num3.ToString()].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        num3 = num - 1L;
                        worksheet.Cells["A8:E" + num3.ToString()].Style.Font.Size = 10f;
                        string fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Pallet Report - " + this.lblPallet.Text + ".xlsx";
                        FileInfo file = new FileInfo(fileName);
                        package.SaveAs(file);
                        MessageBox.Show("Report created successfully at " + fileName, "Pallet");
                    }
                    else
                    {
                        MessageBox.Show("No data found for report.", "Pallet");
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error while creating report - " + exception.Message, "Pallet");
            }
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            base.Close();
        }

        private void ddlVendors_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            this.FillVendorLoads(this.ddlVendors.Text);
        }
        private void FillPalletDetails()
        {
            Session.dtLoginTime = DateTime.Now;
            int num = 0;
            decimal num2 = 0M;
            decimal num3 = 0M;
            DataTable dataTable = DBAccess.GetDataTable("SELECT LOAD_PALLET_ID, VENDOR_NAME, VENDOR_LOAD_NAME, LOAD_PALLET_NAME, LOAD_PALLET_LOCATION FROM Vendor, VendorLoad, LoadPallet WHERE Vendor.VENDOR_ID = VendorLoad.VENDOR_LOAD_VENDOR_ID AND VendorLoad.VENDOR_LOAD_ID = LoadPallet.LOAD_PALLET_LOAD_ID AND LoadPallet.LOAD_PALLET_ID = " + Session.PalletID, DBAccess.msAccessCon);
            if (dataTable.Rows.Count > 0)
            {
                this.lblVendorName.Text = dataTable.Rows[0]["VENDOR_NAME"].ToString();
                this.lblLoadName.Text = dataTable.Rows[0]["VENDOR_LOAD_NAME"].ToString();
                this.lblPallet.Text = dataTable.Rows[0]["LOAD_PALLET_NAME"].ToString();
                this.lblPalletLocation.Text = dataTable.Rows[0]["LOAD_PALLET_LOCATION"].ToString();
                this.lblPalletID.Text = dataTable.Rows[0]["LOAD_PALLET_ID"].ToString();
                try
                {
                    DataTable table2 = DBAccess.GetDataTable("SELECT LOAD_PALLET_PRODUCT_ID, PRODUCT_UPC, PRODUCT_NAME, PRODUCT_PRICE, LABELED_PRODUCT_PRICE, LOAD_PALLET_PRODUCT_QUANTITY FROM Product, LabeledProduct, LoadPalletProduct WHERE LoadPalletProduct.LOAD_PALLET_PRODUCT_LABEL_ID = LabeledProduct.LABELED_PRODUCT_ID AND LoadPalletProduct.LOAD_PALLET_PRODUCT_PRODUCT_ID = Product.PRODUCT_ID AND LoadPalletProduct.LOAD_PALLET_PRODUCT_PALLET_ID = " + Session.PalletID + " ORDER BY 2", DBAccess.msAccessCon);
                    this.gvPalletProducts.DataSource = table2;
                    this.gvPalletProducts.Visible = true;
                    this.UpdatePalletDetailsHeaders();
                    if (table2.Rows.Count > 0)
                    {
                        foreach (DataRow row in table2.Rows)
                        {
                            num += int.Parse(row[5].ToString());
                            num2 += decimal.Parse(row[3].ToString()) * int.Parse(row[5].ToString());
                            num3 += decimal.Parse(row[4].ToString()) * int.Parse(row[5].ToString());
                        }
                    }
                    this.lblTotalItems.Text = num.ToString();
                    this.lblTotalPriceActual.Text = num2.ToString();
                    this.lblTotalPriceLabel.Text = num3.ToString();
                }
                catch (Exception)
                {
                }
            }
        }

        private void FillPallets()
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                string query = "SELECT LOAD_PALLET_ID, VENDOR_NAME, VENDOR_LOAD_NAME, LOAD_PALLET_NAME, LOAD_PALLET_LOCATION FROM Vendor, VendorLoad, LoadPallet WHERE Vendor.VENDOR_ID = VendorLoad.VENDOR_LOAD_VENDOR_ID AND VendorLoad.VENDOR_LOAD_ID = LoadPallet.LOAD_PALLET_LOAD_ID ORDER BY 2, 3, 4";
                DataTable dataTable = DBAccess.GetDataTable(query, DBAccess.msAccessCon);
                this.gvPallets.DataSource = dataTable;
                this.gvPallets.Visible = true;
                this.UpdatePalletHeaders();
            }
            catch (Exception)
            {
            }
        }

        private void FillVendorLoads(string vendorName)
        {
            Session.dtLoginTime = DateTime.Now;
            this.ddlVendorLoad.DataSource = null;
            this.ddlVendorLoad.Items.Clear();



            //OleDbConnection conn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Stock.accdb; Jet OLEDB:Database Password = outlet960; ");
            //OleDbCommand comm = new OleDbCommand();
            //OleDbDataAdapter dAdpter = new OleDbDataAdapter(comm);
            //DataTable dt = new DataTable();
            //comm.Connection = conn;
            //comm.CommandText = "SELECT * FROM Vendor WHERE VENDOR_NAME = @VENDOR_NAME";
            //comm.Parameters.AddWithValue("@lNameParam", "NoLastName");
            //comm.Parameters.AddWithValue("@VENDOR_NAME", vendorName);
            //dAdpter.Fill(dt);


            DataTable dataTable = new DataTable();
            OleDbConnection connection = new OleDbConnection(DBAccess.msAccessCon);
            connection.Open();
            //CHANGES
            string cmdText = "DECLARE @VENDOR_NAME VarChar(50);  SELECT * FROM Vendor WHERE VENDOR_NAME = ?";
            OleDbCommand selectCommand = new OleDbCommand(cmdText, connection);
            selectCommand.Parameters.AddWithValue("@VENDOR_NAME",vendorName);

            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = selectCommand;

          //  adapter.Fill(dataTable);
              new OleDbDataAdapter(selectCommand).Fill(dataTable);
            connection.Close();
            selectCommand.Dispose();
            connection.Dispose();
            if (dataTable.Rows.Count > 0)
            {
                DataTable table2 = DBAccess.GetDataTable("SELECT VENDOR_LOAD_ID, VENDOR_LOAD_NAME FROM VendorLoad WHERE VENDOR_LOAD_VENDOR_ID = " + dataTable.Rows[0]["VENDOR_ID"].ToString() + " ORDER BY VENDOR_LOAD_ID", DBAccess.msAccessCon);
                this.ddlVendorLoad.DisplayMember = "VENDOR_LOAD_NAME";
                this.ddlVendorLoad.ValueMember = "VENDOR_LOAD_ID";
                this.ddlVendorLoad.DataSource = table2;
            }
        }

        private void frmPallets_Load(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                string query = "SELECT VENDOR_ID, VENDOR_NAME FROM Vendor ORDER BY VENDOR_NAME";
                DataTable dataTable = DBAccess.GetDataTable(query, DBAccess.msAccessCon);
                this.ddlVendors.DisplayMember = "VENDOR_NAME";
                this.ddlVendors.ValueMember = "VENDOR_NAME";
                this.ddlVendors.DataSource = dataTable;
                this.FillVendorLoads(this.ddlVendors.Text);
                this.FillPallets();
            }
            catch (Exception)
            {
            }
        }

      

        private void UpdatePalletDetailsHeaders()
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.gvPalletProducts.Columns[0].HeaderText = "ID";
                this.gvPalletProducts.Columns[0].Visible = false;
                this.gvPalletProducts.Columns[1].HeaderText = "UPC";
                this.gvPalletProducts.Columns[1].Width = 120;
                this.gvPalletProducts.Columns[2].HeaderText = "Name";
                this.gvPalletProducts.Columns[2].Width = 290;
                this.gvPalletProducts.Columns[3].HeaderText = "Actual Price";
                this.gvPalletProducts.Columns[3].Width = 100;
                this.gvPalletProducts.Columns[4].HeaderText = "Label Price";
                this.gvPalletProducts.Columns[4].Width = 100;
                this.gvPalletProducts.Columns[5].HeaderText = "Quantity";
                this.gvPalletProducts.Columns[5].Width = 60;
            }
            catch (Exception)
            {
            }
        }

        private void UpdatePalletHeaders()
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.gvPallets.Columns[0].HeaderText = "ID";
                this.gvPallets.Columns[0].Visible = false;
                this.gvPallets.Columns[1].HeaderText = "Vendor";
                this.gvPallets.Columns[1].Width = 0xaf;
                this.gvPallets.Columns[2].HeaderText = "Load";
                this.gvPallets.Columns[2].Width = 0xaf;
                this.gvPallets.Columns[3].HeaderText = "Pallet";
                this.gvPallets.Columns[3].Width = 180;
                this.gvPallets.Columns[4].HeaderText = "Location";
                this.gvPallets.Columns[4].Width = 150;
            }
            catch (Exception)
            {
            }
        }
    }
}
