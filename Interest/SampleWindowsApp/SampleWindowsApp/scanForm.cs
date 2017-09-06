using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RichTextBoxPrintCtrl;
using System.Data.OleDb;
using System.Drawing.Printing;
namespace DealStore
{
    public partial class scanForm : Form
    {

        private Button btnClearForm;
        private Button btnLogOut;
        private Button btnMainMenu;
        private Button btnSearch;
        private Button btnSearchDesc;
        private CheckBox cbCustomPrice;
        private CheckBox cbDontPrint;
        private CheckBox cbFillPallet;
        private CheckBox cbFillPalletNoPrint;
        private CheckBox cbNewProduct;
        private CheckBox cbNoLoad;
        private CheckBox cbOverrideScanPrice;
        private CheckBox cbOverrideVendor;
        private int checkPrint;
        private ComboBox ddlLabelSize;
        private ComboBox ddlPallets;
        private ComboBox ddlRoundValue;
        private ComboBox ddlSellable;
        private ComboBox ddlVendorLoad;
        private ComboBox ddlVendors;
        private GroupBox gbNewProduct;
        private GroupBox gbOverrideDiscPrice;
        private GroupBox gbOverridePrice;
        private GroupBox gbOverrideVendor;
        private Button Generate;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private DataGridView gvProducts;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lblLoadID;
        private Label lblProductID;
        private Label lblRoundOption;
        private Label lblRoundPrice;
        private int newProduct = 0;
        private PrintDocument printDocument1;
        private RadioButton rbAutoClearNo;
        private RadioButton rbAutoClearYes;
        private RichTextBoxPrintCtrl.RichTextBoxPrintCtrl richTextBoxPrintCtrl1;
        private TextBox txtCustomPrice;
        private TextBox txtDiscountPrice;
        private TextBox txtOverrideScanPrice;
        private TextBox txtOverrideVendor;
        private TextBox txtPrice;
        private TextBox txtProduct;
        private TextBox txtQuantity;
        private TextBox txtScanPrice;
        private TextBox txtUPC;
        public scanForm()
        {
            InitializeComponent();
        }

        private void btnClearForm_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.lblProductID.Text = this.txtUPC.Text = this.txtProduct.Text = this.txtPrice.Text = this.txtDiscountPrice.Text = "";
                this.txtQuantity.Text = "1";
                this.ddlSellable.SelectedIndex = 0;
                this.ddlRoundValue.SelectedIndex = 4;
              //  this.cbNewProduct.Checked = false;
                this.cbNewProduct_CheckedChanged(null, null);
              //  this.cbOverrideScanPrice.Checked = false;
                this.cbOverrideScanPrice_CheckedChanged(null, null);
             //   this.cbCustomPrice.Checked = false;
                this.cbCustomPrice_CheckedChanged(null, null);
                this.txtUPC.Focus();
            }
            catch (Exception)
            {
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            Session.isLogOut = "Yes";
            base.Close();
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            base.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if (this.txtUPC.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter UPC.", "Search");
                }
                else
                {
                    this.newProduct = 0;
                    string query = "SELECT PRODUCT_ID, PRODUCT_NAME, PRODUCT_PRICE, PRODUCT_UPC, PRODUCT_VENDOR_LOAD_ID FROM Product WHERE PRODUCT_UPC LIKE '%" + this.txtUPC.Text.Trim() + "%'";
                    if (!this.cbNoLoad.Checked)
                    {
                        if (this.ddlVendorLoad.Text != "")
                        {
                            query = query + " AND PRODUCT_VENDOR_LOAD_ID = " + this.ddlVendorLoad.SelectedValue.ToString();
                        }
                        else
                        {
                            MessageBox.Show("Please select vendor load.", "Search");
                            return;
                        }
                    }
                    DataTable dataTable = DBAccess.GetDataTable(query, DBAccess.msAccessCon);
                    if (dataTable.Rows.Count > 0)
                    {
                        if (dataTable.Rows.Count == 1)
                        {
                            this.lblProductID.Text = dataTable.Rows[0]["PRODUCT_ID"].ToString();
                            this.txtUPC.Text = dataTable.Rows[0]["PRODUCT_UPC"].ToString();
                            this.txtProduct.Text = dataTable.Rows[0]["PRODUCT_NAME"].ToString();
                            this.txtPrice.Text = dataTable.Rows[0]["PRODUCT_PRICE"].ToString();
                            if ((dataTable.Rows[0]["PRODUCT_VENDOR_LOAD_ID"] != null) && (dataTable.Rows[0]["PRODUCT_VENDOR_LOAD_ID"].ToString() != ""))
                            {
                                this.lblLoadID.Text = dataTable.Rows[0]["PRODUCT_VENDOR_LOAD_ID"].ToString();
                            }
                            this.gvProducts.Visible = false;
                            this.Generate_Click(null, null);
                        }
                        else
                        {
                            this.gvProducts.DataSource = dataTable;
                            this.gvProducts.Visible = true;
                            this.UpdateProductHeaders();
                        }
                    }
                    else
                    {
                        int num = 0;
                        query = "SELECT PRODUCT_ID, PRODUCT_NAME, PRODUCT_PRICE, PRODUCT_UPC, PRODUCT_VENDOR_LOAD_ID FROM Product";
                        if (!this.cbNoLoad.Checked)
                        {
                            if (this.ddlVendorLoad.Text != "")
                            {
                                query = query + " WHERE PRODUCT_VENDOR_LOAD_ID = " + this.ddlVendorLoad.SelectedValue.ToString();
                            }
                            else
                            {
                                MessageBox.Show("Please select vendor load.", "Search");
                                return;
                            }
                        }
                        dataTable = DBAccess.GetDataTable(query, DBAccess.msAccessCon);
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (this.txtUPC.Text.Trim().Contains(row["PRODUCT_UPC"].ToString()))
                            {
                                this.lblProductID.Text = row["PRODUCT_ID"].ToString();
                                this.txtUPC.Text = row["PRODUCT_UPC"].ToString();
                                this.txtProduct.Text = row["PRODUCT_NAME"].ToString();
                                this.txtPrice.Text = row["PRODUCT_PRICE"].ToString();
                                if ((row["PRODUCT_VENDOR_LOAD_ID"] != null) && (row["PRODUCT_VENDOR_LOAD_ID"].ToString() != ""))
                                {
                                    this.lblLoadID.Text = row["PRODUCT_VENDOR_LOAD_ID"].ToString();
                                }
                                this.gvProducts.Visible = false;
                                num = 1;
                                this.Generate_Click(null, null);
                                break;
                            }
                        }
                        if (num == 0)
                        {
                            this.newProduct = 1;
                            string str2 = this.txtUPC.Text.Trim();
                            MessageBox.Show("Record not found.", "Search");
                            this.btnClearForm_Click(null, null);
                            this.txtUPC.Text = str2;
                            this.gvProducts.Visible = false;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnSearchDesc_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            if (this.txtProduct.Text.Trim() == "")
            {
                MessageBox.Show("Please enter product.", "Search");
            }
            else
            {
                this.newProduct = 0;
                DataTable dataTable = new DataTable();
                OleDbConnection connection = new OleDbConnection(DBAccess.msAccessCon);
                connection.Open();
                string cmdText = "SELECT PRODUCT_ID, PRODUCT_NAME, PRODUCT_PRICE, PRODUCT_UPC, PRODUCT_VENDOR_LOAD_ID FROM Product WHERE PRODUCT_NAME LIKE '%'+@PRODUCT_NAME+'%'";
                cmdText = "SELECT PRODUCT_ID, PRODUCT_NAME, PRODUCT_PRICE, PRODUCT_UPC, PRODUCT_VENDOR_LOAD_ID FROM Product WHERE PRODUCT_NAME LIKE '%'+?+'%'";
                if (!this.cbNoLoad.Checked && (this.ddlVendorLoad.Text != ""))
                {
                    cmdText = cmdText + " AND PRODUCT_VENDOR_LOAD_ID = " + this.ddlVendorLoad.SelectedValue.ToString();
                }
                OleDbCommand selectCommand = new OleDbCommand(cmdText, connection);

                selectCommand.Parameters.Add("@PRODUCT_NAME", OleDbType.VarChar, 150).Value = this.txtProduct.Text.Trim();
                new OleDbDataAdapter(selectCommand).Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    if (dataTable.Rows.Count == 1)
                    {
                        this.lblProductID.Text = dataTable.Rows[0]["PRODUCT_ID"].ToString();
                        this.txtUPC.Text = dataTable.Rows[0]["PRODUCT_UPC"].ToString();
                        this.txtProduct.Text = dataTable.Rows[0]["PRODUCT_NAME"].ToString();
                        this.txtPrice.Text = dataTable.Rows[0]["PRODUCT_PRICE"].ToString();
                        if ((dataTable.Rows[0]["PRODUCT_VENDOR_LOAD_ID"] != null) && (dataTable.Rows[0]["PRODUCT_VENDOR_LOAD_ID"].ToString() != ""))
                        {
                            this.lblLoadID.Text = dataTable.Rows[0]["PRODUCT_VENDOR_LOAD_ID"].ToString();
                        }
                        this.gvProducts.Visible = false;
                        this.Generate_Click(null, null);
                    }
                    else
                    {
                        this.gvProducts.DataSource = dataTable;
                        this.gvProducts.Visible = true;
                        this.UpdateProductHeaders();
                    }
                }
                else
                {
                    this.newProduct = 1;
                    string str2 = this.txtProduct.Text.Trim();
                    MessageBox.Show("Record not found.", "Search");
                    this.btnClearForm_Click(null, null);
                    this.txtProduct.Text = str2;
                    this.txtProduct.Focus();
                    this.gvProducts.Visible = false;
                }
            }
        }

        private void CalculateDiscountPrice()
        {
            decimal num = decimal.Parse(this.txtPrice.Text.Trim());
            decimal d = 0M;
            if (this.cbOverrideScanPrice.Checked && (this.txtOverrideScanPrice.Text.Trim() != ""))
            {
                d = (int.Parse(this.txtOverrideScanPrice.Text.Trim()) * num) / 100M;
            }
            else
            {
                d = (int.Parse(this.txtScanPrice.Text.Trim()) * num) / 100M;
            }
            string s = Math.Round(d, 2, MidpointRounding.AwayFromZero).ToString();
            int selectedIndex = this.ddlRoundValue.SelectedIndex;
            if (selectedIndex == 4)
            {
                decimal num4 = decimal.Parse(s);
                if ((num4 > 0M) && (num4 < 1M))
                {
                    selectedIndex = 3;
                }
                else if ((num4 > 1M) && (num4 < 5M))
                {
                    selectedIndex = 2;
                }
                else if ((num4 > 5M) && (num4 < 10M))
                {
                    selectedIndex = 1;
                }
                else if (num4 > 10M)
                {
                    selectedIndex = 0;
                }
            }
            if (this.lblRoundPrice.Text.ToLower() == "yes")
            {
                try
                {
                    decimal num5;
                    decimal num6;
                    decimal num7;
                    decimal num8;
                    decimal num9;
                    decimal num10;
                    decimal num11;
                    decimal num12;
                    switch (selectedIndex)
                    {
                        case 0:
                            this.lblRoundOption.Text = "0.50";
                            num5 = decimal.Parse(s);
                            num6 = Math.Floor(num5);
                            num7 = num5 - num6;
                            num8 = num7 * 100M;
                            num9 = num8 % 50M;
                            if (num9 > 0M)
                            {
                                num10 = 50M - num8;
                                num11 = 0M;
                                if (num10 > 0M)
                                {
                                    num11 = (num8 + num10) / 100M;
                                }
                                else
                                {
                                    num10 = 50M + num10;
                                    num11 = (num8 + num10) / 100M;
                                }
                                num12 = num6 + num11;
                                s = num12.ToString();
                            }
                            goto Label_05C8;

                        case 1:
                            this.lblRoundOption.Text = "0.25";
                            num5 = decimal.Parse(s);
                            num6 = Math.Floor(num5);
                            num7 = num5 - num6;
                            num8 = num7 * 100M;
                            num9 = num8 % 25M;
                            if (num9 > 0M)
                            {
                                num10 = 25M - num9;
                                num11 = 0M;
                                if (num10 > 0M)
                                {
                                    num11 = (num8 + num10) / 100M;
                                }
                                else
                                {
                                    num10 = 25M + num10;
                                    num11 = (num8 + num10) / 100M;
                                }
                                num12 = num6 + num11;
                                s = num12.ToString();
                            }
                            goto Label_05C8;

                        case 2:
                            this.lblRoundOption.Text = "0.10";
                            num5 = decimal.Parse(s);
                            num6 = Math.Floor(num5);
                            num7 = num5 - num6;
                            num8 = num7 * 100M;
                            num9 = num8 % 10M;
                            if (num9 > 0M)
                            {
                                num10 = 10M - num9;
                                num11 = 0M;
                                if (num10 > 0M)
                                {
                                    num11 = (num8 + num10) / 100M;
                                }
                                else
                                {
                                    num10 = 10M + num10;
                                    num11 = (num8 + num10) / 100M;
                                }
                                num12 = num6 + num11;
                                s = num12.ToString();
                            }
                            goto Label_05C8;
                    }
                    if (selectedIndex == 3)
                    {
                        this.lblRoundOption.Text = "0.05";
                        num5 = decimal.Parse(s);
                        num6 = Math.Floor(num5);
                        num7 = num5 - num6;
                        num8 = num7 * 100M;
                        num9 = num8 % 5M;
                        if (num9 > 0M)
                        {
                            num10 = 5M - num9;
                            num11 = 0M;
                            if (num10 > 0M)
                            {
                                num11 = (num8 + num10) / 100M;
                            }
                            else
                            {
                                num10 = 5M + num10;
                                num11 = (num8 + num10) / 100M;
                            }
                            s = (num6 + num11).ToString();
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            Label_05C8:
            this.txtDiscountPrice.Text = s;
        }

        private void cbCustomPrice_CheckedChanged(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            if (this.cbCustomPrice.Checked)
            {
                this.txtCustomPrice.ReadOnly = false;
                this.txtCustomPrice.Focus();
            }
            else
            {
                this.txtCustomPrice.ReadOnly = true;
                this.txtCustomPrice.Text = "";
            }
        }

        private void cbFillPallet_CheckedChanged(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            if (this.cbFillPallet.Checked)
            {
                this.ddlPallets.Enabled = true;
                this.cbFillPalletNoPrint.Enabled = true;
            }
            else
            {
                this.ddlPallets.Enabled = false;
                this.cbFillPalletNoPrint.Enabled = false;
                this.cbFillPalletNoPrint.Checked = false;
            }
        }

        private void cbNewProduct_CheckedChanged(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if (this.cbNewProduct.Checked)
                {
                    this.btnSearch.Enabled = this.btnSearchDesc.Enabled = false;
                }
                else
                {
                    this.btnSearch.Enabled = this.btnSearchDesc.Enabled = true;
                }
            }
            catch (Exception)
            {
            }
        }

        private void cbOverrideScanPrice_CheckedChanged(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            if (this.cbOverrideScanPrice.Checked)
            {
                this.txtOverrideScanPrice.ReadOnly = false;
                this.txtOverrideScanPrice.Focus();
            }
            else
            {
                this.txtOverrideScanPrice.ReadOnly = true;
                this.txtOverrideScanPrice.Text = "";
            }
        }

        private void cbOverrideVendor_CheckedChanged(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            if (this.cbOverrideVendor.Checked)
            {
                this.txtOverrideVendor.ReadOnly = false;
                this.txtOverrideVendor.Focus();
            }
            else
            {
                this.txtOverrideVendor.ReadOnly = true;
                this.txtOverrideVendor.Text = "";
            }
        }

        private void ddlVendorLoad_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            this.FillLoadPallets();
        }

        private void ddlVendors_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            this.FillVendorLoads(this.ddlVendors.Text);
        }

       

        private void FillLoadPallets()
        {
            this.ddlPallets.DataSource = null;
            this.ddlPallets.Items.Clear();
            if (this.ddlVendorLoad.Text != "")
            {
                DataTable dataTable = DBAccess.GetDataTable("SELECT LOAD_PALLET_ID, LOAD_PALLET_NAME FROM LoadPallet WHERE LOAD_PALLET_LOAD_ID = " + this.ddlVendorLoad.SelectedValue.ToString() + " ORDER BY LOAD_PALLET_NAME", DBAccess.msAccessCon);
                this.ddlPallets.DisplayMember = "LOAD_PALLET_NAME";
                this.ddlPallets.ValueMember = "LOAD_PALLET_ID";
                this.ddlPallets.DataSource = dataTable;
            }
        }

        private void FillVendorLoads(string vendorName)
        {
            this.ddlVendorLoad.DataSource = null;
            this.ddlVendorLoad.Items.Clear();
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
                try
                {
                    this.txtScanPrice.Text = dataTable.Rows[0]["VENDOR_DEFAULT_DISCOUNT"].ToString();
                }
                catch (Exception)
                {
                }
                DataTable table2 = DBAccess.GetDataTable("SELECT VENDOR_LOAD_ID, VENDOR_LOAD_NAME FROM VendorLoad WHERE VENDOR_LOAD_VENDOR_ID = " + dataTable.Rows[0]["VENDOR_ID"].ToString() + " ORDER BY VENDOR_LOAD_ID", DBAccess.msAccessCon);
                this.ddlVendorLoad.DisplayMember = "VENDOR_LOAD_NAME";
                this.ddlVendorLoad.ValueMember = "VENDOR_LOAD_ID";
                this.ddlVendorLoad.DataSource = table2;
            }
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if (((this.txtScanPrice.Text.Trim() == "") || (this.lblRoundPrice.Text.Trim() == "")) || (this.ddlVendors.Text == ""))
                {
                    MessageBox.Show("Please enter settings data.", "Generate");
                    this.txtUPC.Focus();
                }
                else
                {
                    if (!this.cbNoLoad.Checked || this.cbFillPallet.Checked)
                    {
                        if (this.ddlVendorLoad.Text == "")
                        {
                            MessageBox.Show("Please select vendor load.", "Generate");
                            return;
                        }
                        if (((this.newProduct != 1) && !this.cbNewProduct.Checked) && (this.lblProductID.Text == ""))
                        {
                            MessageBox.Show("Please select product from load.", "Generate");
                            return;
                        }
                    }
                    if (((this.newProduct == 1) || this.cbNewProduct.Checked) && (this.ddlVendorLoad.Text == ""))
                    {
                        MessageBox.Show("Please select vendor load.", "Generate");
                    }
                    else
                    {
                        string text = this.ddlVendors.Text;
                        if (((this.txtUPC.Text.Trim() == "") || (this.txtProduct.Text.Trim() == "")) || (this.txtPrice.Text.Trim() == ""))
                        {
                            MessageBox.Show("Please enter product data.", "Generate");
                            this.txtUPC.Focus();
                        }
                        else if (this.txtQuantity.Text.Trim() == "")
                        {
                            MessageBox.Show("Please enter quantity.", "Generate");
                            this.txtUPC.Focus();
                        }
                        else if (this.cbOverrideVendor.Checked && (this.txtOverrideVendor.Text.Trim() == ""))
                        {
                            MessageBox.Show("Please enter override vendor.", "Generate");
                            this.txtOverrideVendor.Focus();
                        }
                        else
                        {
                            if (this.cbOverrideVendor.Checked && (this.txtOverrideVendor.Text.Trim() != ""))
                            {
                                text = this.txtOverrideVendor.Text.Trim();
                            }
                            if (this.cbCustomPrice.Checked && (this.txtCustomPrice.Text.Trim() == ""))
                            {
                                MessageBox.Show("Please enter custom price.", "Generate");
                                this.txtCustomPrice.Focus();
                            }
                            else if (this.cbFillPallet.Checked && (this.ddlPallets.Text == ""))
                            {
                                MessageBox.Show("Please select Load Pallet.", "Generate");
                            }
                            else if (this.cbOverrideScanPrice.Checked && (this.txtOverrideScanPrice.Text.Trim() == ""))
                            {
                                MessageBox.Show("Please enter override scan price.", "Search");
                                this.txtOverrideScanPrice.Focus();
                            }
                            else
                            {
                                int num = 0;
                                num = int.Parse(this.txtScanPrice.Text.Trim());
                                if ((num < 1) || (num > 100))
                                {
                                    MessageBox.Show("Please enter valid override scan price.", "Settings");
                                    this.txtOverrideScanPrice.Focus();
                                }
                                else
                                {
                                    this.CalculateDiscountPrice();
                                    if (this.cbCustomPrice.Checked && (this.txtCustomPrice.Text.Trim() != ""))
                                    {
                                        this.txtDiscountPrice.Text = this.txtCustomPrice.Text.Trim();
                                    }
                                    if (!this.cbDontPrint.Checked)
                                    {
                                        string str3;
                                        OleDbCommand command;
                                        OleDbConnection connection2;
                                        int num5;
                                        try
                                        {
                                            if ((this.newProduct == 1) || this.cbNewProduct.Checked)
                                            {
                                                decimal d = decimal.Parse(this.txtPrice.Text.Trim());
                                                OleDbConnection connection = new OleDbConnection(DBAccess.msAccessCon);
                                                connection.Open();
                                                str3 = "INSERT INTO Product(PRODUCT_NAME, PRODUCT_PRICE, PRODUCT_UPC, PRODUCT_VENDOR, PRODUCT_VENDOR_LOAD_ID, PRODUCT_IMPORT_DATE, CREATEDATE) VALUES(@PRODUCT_NAME, @PRODUCT_PRICE, @PRODUCT_UPC, @PRODUCT_VENDOR, @PRODUCT_VENDOR_LOAD_ID, @PRODUCT_IMPORT_DATE, @CREATEDATE)";
                                                str3 = "INSERT INTO Product(PRODUCT_NAME, PRODUCT_PRICE, PRODUCT_UPC, PRODUCT_VENDOR, PRODUCT_VENDOR_LOAD_ID, PRODUCT_IMPORT_DATE, CREATEDATE) VALUES(?, ?, ?, ?, ?, ?, ?)";
                                                command = new OleDbCommand(str3, connection);
                                                command.Parameters.Add("@PRODUCT_NAME", OleDbType.VarChar, 0xff).Value = this.txtProduct.Text.Trim();
                                                command.Parameters.Add("@PRODUCT_PRICE", OleDbType.Decimal).Value = Math.Round(d, 2, MidpointRounding.AwayFromZero);
                                                command.Parameters.Add("@PRODUCT_UPC", OleDbType.VarChar, 0x19).Value = this.txtUPC.Text.Trim();
                                                command.Parameters.Add("@PRODUCT_VENDOR", OleDbType.VarChar, 50).Value = this.ddlVendors.Text;
                                                command.Parameters.Add("@PRODUCT_VENDOR_LOAD_ID", OleDbType.Integer).Value = int.Parse(this.ddlVendorLoad.SelectedValue.ToString());
                                                command.Parameters.Add("@PRODUCT_IMPORT_DATE", OleDbType.Date).Value = DateTime.Now.Date;
                                                command.Parameters.Add("@CREATEDATE", OleDbType.Date).Value = DateTime.Now;
                                                command.ExecuteNonQuery();
                                                connection.Close();
                                                command.Dispose();
                                                connection.Dispose();
                                            }
                                        }
                                        catch (Exception)
                                        {
                                        }
                                        int num3 = int.Parse(this.txtQuantity.Text.Trim());
                                        int num4 = 0;
                                        if (this.cbFillPalletNoPrint.Checked)
                                        {
                                            try
                                            {
                                                connection2 = new OleDbConnection(DBAccess.msAccessCon);
                                                connection2.Open();
                                                str3 = "";
                                                str3 = "SELECT LABELED_PRODUCT_ID FROM LabeledProduct WHERE LABELED_PRODUCT_UPC = @LABELED_PRODUCT_UPC AND LABELED_PRODUCT_VENDOR_LOAD_ID = @LABELED_PRODUCT_VENDOR_LOAD_ID ORDER BY LABELED_PRODUCT_ID DESC";
                                                str3 = "SELECT LABELED_PRODUCT_ID FROM LabeledProduct WHERE LABELED_PRODUCT_UPC = ? AND LABELED_PRODUCT_VENDOR_LOAD_ID = ? ORDER BY LABELED_PRODUCT_ID DESC";
                                                command = new OleDbCommand(str3, connection2);
                                                command.Parameters.Add("@LABELED_PRODUCT_UPC", OleDbType.VarChar, 0x19).Value = this.txtUPC.Text.Trim();
                                                if (this.lblLoadID.Text != "")
                                                {
                                                    command.Parameters.Add("@LABELED_PRODUCT_VENDOR_LOAD_ID", OleDbType.Integer).Value = int.Parse(this.lblLoadID.Text);
                                                }
                                                else
                                                {
                                                    command.Parameters.Add("@LABELED_PRODUCT_VENDOR_LOAD_ID", OleDbType.Integer).Value = DBNull.Value;
                                                }
                                                num4 = (int)command.ExecuteScalar();
                                                connection2.Close();
                                                connection2.Dispose();
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }
                                        if ((num4 == 0) || !this.cbFillPalletNoPrint.Checked)
                                        {
                                            try
                                            {
                                                connection2 = new OleDbConnection(DBAccess.msAccessCon);
                                                connection2.Open();
                                                str3 = "";
                                                str3 = "INSERT INTO LabeledProduct(LABELED_PRODUCT_UPC, LABELED_PRODUCT_PRICE, LABELED_PRICE_LEVEL, LABELED_ROUNDED_YN, LABELED_ROUNDED_UNIT, LABELED_PRODUCT_VENDOR, LABELED_PRODUCT_VENDOR_LOAD_ID, LABELED_PRODUCT_PRODUCT_ID, LABELED_PRODUCT_USER, LABELED_PRODUCT_DATE, IS_SELLABLE) VALUES(@LABELED_PRODUCT_UPC, @LABELED_PRODUCT_PRICE, @LABELED_PRICE_LEVEL, @LABELED_ROUNDED_YN, @LABELED_ROUNDED_UNIT, @LABELED_PRODUCT_VENDOR, @LABELED_PRODUCT_VENDOR_LOAD_ID, @LABELED_PRODUCT_PRODUCT_ID, @LABELED_PRODUCT_USER, @LABELED_PRODUCT_DATE, @IS_SELLABLE)";
                                                str3 = "INSERT INTO LabeledProduct(LABELED_PRODUCT_UPC, LABELED_PRODUCT_PRICE, LABELED_PRICE_LEVEL, LABELED_ROUNDED_YN, LABELED_ROUNDED_UNIT, LABELED_PRODUCT_VENDOR, LABELED_PRODUCT_VENDOR_LOAD_ID, LABELED_PRODUCT_PRODUCT_ID, LABELED_PRODUCT_USER, LABELED_PRODUCT_DATE, IS_SELLABLE) VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                                                string str4 = "Select @@Identity";
                                                command = new OleDbCommand(str3, connection2);
                                                command.Parameters.Add("@LABELED_PRODUCT_UPC", OleDbType.VarChar, 0x19).Value = this.txtUPC.Text.Trim();
                                                command.Parameters.Add("@LABELED_PRODUCT_PRICE", OleDbType.Decimal).Value = decimal.Parse(this.txtDiscountPrice.Text.Trim());
                                                command.Parameters.Add("@LABELED_PRICE_LEVEL", OleDbType.Integer).Value = int.Parse(this.txtScanPrice.Text.Trim());
                                                command.Parameters.Add("@LABELED_ROUNDED_YN", OleDbType.VarChar, 5).Value = this.lblRoundPrice.Text;
                                                command.Parameters.Add("@LABELED_ROUNDED_UNIT", OleDbType.VarChar, 5).Value = this.lblRoundOption.Text;
                                                command.Parameters.Add("@LABELED_PRODUCT_VENDOR", OleDbType.VarChar, 50).Value = text;
                                                if (this.lblLoadID.Text != "")
                                                {
                                                    command.Parameters.Add("@LABELED_PRODUCT_VENDOR_LOAD_ID", OleDbType.Integer).Value = int.Parse(this.lblLoadID.Text);
                                                }
                                                else
                                                {
                                                    command.Parameters.Add("@LABELED_PRODUCT_VENDOR_LOAD_ID", OleDbType.Integer).Value = DBNull.Value;
                                                }
                                                if (this.lblProductID.Text != "")
                                                {
                                                    command.Parameters.Add("@LABELED_PRODUCT_PRODUCT_ID", OleDbType.Integer).Value = int.Parse(this.lblProductID.Text);
                                                }
                                                else
                                                {
                                                    command.Parameters.Add("@LABELED_PRODUCT_PRODUCT_ID", OleDbType.Integer).Value = DBNull.Value;
                                                }
                                                command.Parameters.Add("@LABELED_PRODUCT_USER", OleDbType.Integer).Value = Session.userID;
                                                command.Parameters.Add("@LABELED_PRODUCT_DATE", OleDbType.Date).Value = DateTime.Now;
                                                command.Parameters.Add("@IS_SELLABLE", OleDbType.VarChar, 3).Value = this.ddlSellable.Text;
                                                num5 = 0;
                                                while (num5 < num3)
                                                {
                                                    command.ExecuteNonQuery();
                                                    num5++;
                                                }
                                                command.CommandText = str4;
                                              var  nnum4 = command.ExecuteScalar();

                                                num4 = int.Parse(nnum4.ToString());
                                                connection2.Close();
                                                connection2.Dispose();
                                            }
                                            catch (Exception EX)
                                            {
                                            }
                                        }
                                        if (this.cbFillPallet.Checked && (this.ddlPallets.Text != ""))
                                        {
                                            try
                                            {
                                                connection2 = new OleDbConnection(DBAccess.msAccessCon);
                                                connection2.Open();
                                                str3 = "";
                                                str3 = "INSERT INTO LoadPalletProduct(LOAD_PALLET_PRODUCT_PALLET_ID, LOAD_PALLET_PRODUCT_LABEL_ID, LOAD_PALLET_PRODUCT_PRODUCT_ID, LOAD_PALLET_PRODUCT_QUANTITY, CREATEDBY, CREATEDATE) VALUES(@LOAD_PALLET_PRODUCT_PALLET_ID, @LOAD_PALLET_PRODUCT_LABEL_ID, @LOAD_PALLET_PRODUCT_PRODUCT_ID, @LOAD_PALLET_PRODUCT_QUANTITY, @CREATEDBY, @CREATEDATE)";
                                                str3 = "INSERT INTO LoadPalletProduct(LOAD_PALLET_PRODUCT_PALLET_ID, LOAD_PALLET_PRODUCT_LABEL_ID, LOAD_PALLET_PRODUCT_PRODUCT_ID, LOAD_PALLET_PRODUCT_QUANTITY, CREATEDBY, CREATEDATE) VALUES(?, ?, ?, ?, ?, ?)";
                                                command = new OleDbCommand(str3, connection2);
                                                command.Parameters.Clear();
                                                command.Parameters.Add("@LOAD_PALLET_PRODUCT_PALLET_ID", OleDbType.Integer).Value = int.Parse(this.ddlPallets.SelectedValue.ToString());
                                                command.Parameters.Add("@LOAD_PALLET_PRODUCT_LABEL_ID", OleDbType.Integer).Value = num4;
                                                if (this.lblProductID.Text != "")
                                                {
                                                    command.Parameters.Add("@LOAD_PALLET_PRODUCT_PRODUCT_ID", OleDbType.Integer).Value = int.Parse(this.lblProductID.Text);
                                                }
                                                else
                                                {
                                                    command.Parameters.Add("@LOAD_PALLET_PRODUCT_PRODUCT_ID", OleDbType.Integer).Value = DBNull.Value;
                                                }
                                                command.Parameters.Add("@LOAD_PALLET_PRODUCT_QUANTITY", OleDbType.Integer).Value = num3;
                                                command.Parameters.Add("@CREATEDBY", OleDbType.Integer).Value = Session.userID;
                                                command.Parameters.Add("@CREATEDATE", OleDbType.VarChar, 0x19).Value = DateTime.Now;
                                                command.ExecuteNonQuery();
                                                connection2.Close();
                                                connection2.Dispose();
                                            }
                                            catch (Exception exception)
                                            {
                                            }
                                        }
                                        if (!this.cbFillPalletNoPrint.Checked)
                                        {
                                            string str10;
                                            string str11;
                                            string str12;
                                            string str13;
                                            string str14;
                                            string str15;
                                            string str16;
                                            string str17;
                                            string str18;
                                            string str19;
                                            string str20;
                                            string str21;
                                            string str22;
                                            string str23;
                                            int right = 5;
                                            int left = 7;
                                            int top = 0;
                                            int bottom = 0;
                                            int width = 0x120;
                                            int height = 200;
                                            string str5 = System.Configuration.ConfigurationManager.AppSettings["ReceiptTitle"]; 
                                            string str6 = this.txtProduct.Text.Trim() ?? "";
                                            string str7 = text + " Price : $" + decimal.Parse(this.txtPrice.Text.Trim()).ToString("0.00");
                                            string str8 = "OUR PRICE $" + decimal.Parse(this.txtDiscountPrice.Text.Trim()).ToString("0.00");
                                            string str9 = "*" + this.txtUPC.Text.Trim() + "*";
                                            if (this.ddlLabelSize.SelectedIndex == 0)
                                            {
                                                str10 = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fswiss\fcharset0 Verdana;}{\f1\fcharset0 IDAutomationHC39M;}{\f2\fnil\fcharset0 Calibri;}}{\colortbl ;\red163\green21\blue21;}\paperw4507\paperh2995\margl5\margr5\margt0\margb0\";
                                                str11 = @"viewkind4\uc1\pard\sl371\slmult0\lang9\b\f0\fs22 ";
                                                str12 = @"\par";
                                                str13 = @"\pard\sl0\slmult0\qj\b0\fs14 ";
                                                str14 = @"\cf1\par";
                                                str15 = @"\pard\sl-331\slmult0\qj\cf0\b\fs16 ";
                                                str16 = @"\par";
                                                str17 = @"\fs24 ";
                                                str18 = @"\fs16 ";
                                                str19 = @"\par";
                                                str20 = @"\b0 " + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString() + Session.userID.ToString().PadLeft(2, '0') + @"\b\par";
                                                str21 = @"\pard\sl-831\slmult0\qj\b0\f1\fs22";
                                                str22 = @"\f2\fs22\par";
                                                str23 = "}";
                                                this.richTextBoxPrintCtrl1.Rtf = str10 + str11 + str5 + str12 + str13 + str6 + str14 + str15 + str7 + str16 + str17 + str8 + str19 + str18 + str20 + str21 + str9 + str22 + str23;
                                            }
                                            if (this.ddlLabelSize.SelectedIndex == 1)
                                            {
                                                width = 0x120;
                                                height = 100;
                                                str10 = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fswiss\fcharset0 Verdana;}{\f1\fcharset0 IDAutomationHC39M;}{\f2\fnil\fcharset0 Calibri;}}{\colortbl ;\red163\green21\blue21;}\paperw4507\paperh2995\margl5\margr5\margt0\margb0\";
                                                str11 = @"viewkind4\uc1\pard\sl0\slmult0\lang9\b\f0\fs16 ";
                                                str12 = @"\par";
                                                str13 = @"\pard\sl0\slmult0\qj\b0\fs8 ";
                                                str14 = @"\cf1\par";
                                                str15 = @"\pard\sl0\slmult0\qj\cf0\b\fs10 ";
                                                str16 = @"\par";
                                                str17 = @"\fs16 ";
                                                str18 = @"\fs8 ";
                                                str19 = @"\par";
                                                str20 = @"\b0 " + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString() + Session.userID.ToString().PadLeft(2, '0') + @"\b\par";
                                                str21 = @"\pard\sl0\slmult0\qj\b0\f1\fs16";
                                                str22 = @"\f2\fs22\par";
                                                str23 = "}";
                                                this.richTextBoxPrintCtrl1.Rtf = str10 + str11 + str5 + str12 + str13 + str6 + str14 + str15 + str7 + str16 + str17 + str8 + str19 + str18 + str20 + str21 + str9 + str22 + str23;
                                            }
                                            if (this.ddlLabelSize.SelectedIndex == 2)
                                            {
                                                width = 0xee;
                                                height = 150;
                                                str10 = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fswiss\fcharset0 Verdana;}{\f1\fcharset0 IDAutomationHC39M;}{\f2\fnil\fcharset0 Calibri;}}{\colortbl ;\red163\green21\blue21;}\paperw4507\paperh2995\margl5\margr5\margt0\margb0\";
                                                str11 = @"viewkind4\uc1\pard\sl0\slmult0\lang9\b\f0\fs18 ";
                                                str12 = @"\par";
                                                str13 = @"\pard\sl0\slmult0\qj\b0\fs12 ";
                                                str14 = @"\cf1\par";
                                                str15 = @"\pard\sl0\slmult0\qj\cf0\b\fs14 ";
                                                str16 = @"\par";
                                                str17 = @"\fs20 ";
                                                str18 = @"\fs8 ";
                                                str19 = @"\par";
                                                str20 = @"\b0 " + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString() + Session.userID.ToString().PadLeft(2, '0') + @"\b\par";
                                                str21 = @"\pard\sl0\slmult0\qj\b0\f1\fs20";
                                                str22 = @"\f2\fs22\par";
                                                str23 = "}";
                                                this.richTextBoxPrintCtrl1.Rtf = str10 + str11 + str5 + str12 + str13 + str6 + str14 + str15 + str7 + str16 + str17 + str8 + str19 + str18 + str20 + str21 + str9 + str22 + str23;
                                            }
                                            Margins margins = new Margins(left, right, top, bottom);
                                            this.printDocument1.DefaultPageSettings.Margins = margins;
                                            this.printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Custom", width, height);
                                            for (num5 = 0; num5 < num3; num5++)
                                            {
                                                this.printDocument1.Print();
                                            }
                                        }
                                        this.txtUPC.Focus();
                                        this.lblLoadID.Text = "";
                                        this.lblProductID.Text = "";
                                        if (this.rbAutoClearYes.Checked)
                                        {
                                            this.btnClearForm_Click(null, null);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error while generating barcodes - " + exception.Message, "Generate");
            }
        }

        private void gvProducts_DoubleClick(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                DataTable dataTable = DBAccess.GetDataTable("SELECT PRODUCT_ID, PRODUCT_NAME, PRODUCT_PRICE, PRODUCT_UPC, PRODUCT_VENDOR_LOAD_ID FROM Product WHERE PRODUCT_ID = " + this.gvProducts.SelectedRows[0].Cells[0].Value.ToString(), DBAccess.msAccessCon);
                if (dataTable.Rows.Count > 0)
                {
                    this.lblProductID.Text = dataTable.Rows[0]["PRODUCT_ID"].ToString();
                    this.txtUPC.Text = dataTable.Rows[0]["PRODUCT_UPC"].ToString();
                    this.txtProduct.Text = dataTable.Rows[0]["PRODUCT_NAME"].ToString();
                    this.txtPrice.Text = dataTable.Rows[0]["PRODUCT_PRICE"].ToString();
                    if ((dataTable.Rows[0]["PRODUCT_VENDOR_LOAD_ID"] != null) && (dataTable.Rows[0]["PRODUCT_VENDOR_LOAD_ID"].ToString() != ""))
                    {
                        this.lblLoadID.Text = dataTable.Rows[0]["PRODUCT_VENDOR_LOAD_ID"].ToString();
                    }
                    this.gvProducts.Visible = false;
                    this.Generate_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Record not found.", "Search");
                    this.btnClearForm_Click(null, null);
                    this.gvProducts.Visible = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void gvProducts_KeyDown(object sender, KeyEventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.gvProducts.Visible = false;
                    this.txtUPC.Focus();
                }
            }
            catch (Exception)
            {
            }
        }
        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            this.checkPrint = 0;
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            this.checkPrint = this.richTextBoxPrintCtrl1.Print(this.checkPrint, this.richTextBoxPrintCtrl1.TextLength, e);
            if (this.checkPrint < this.richTextBoxPrintCtrl1.TextLength)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
        }

        private void scanForm_Load(object sender, EventArgs e)
        {
            string str;
            Session.dtLoginTime = DateTime.Now;
            try
            {
                str = "SELECT VENDOR_ID, VENDOR_NAME FROM Vendor ORDER BY VENDOR_NAME";
                DataTable dataTable = DBAccess.GetDataTable(str, DBAccess.msAccessCon);
                this.ddlVendors.DisplayMember = "VENDOR_NAME";
                this.ddlVendors.ValueMember = "VENDOR_NAME";
                this.ddlVendors.DataSource = dataTable;
            }
            catch (Exception)
            {
            }
            try
            {
                str = "SELECT * FROM Setting ORDER BY SETTING_ID DESC";
                DataTable table2 = DBAccess.GetDataTable(str, DBAccess.msAccessCon);
                if (table2.Rows.Count > 0)
                {
                    this.txtScanPrice.Text = table2.Rows[0]["DEFAULT_DISCOUNT"].ToString();
                    this.ddlVendors.Text = table2.Rows[0]["VENDOR_NAME"].ToString();
                    this.lblRoundPrice.Text = table2.Rows[0]["ROUNDING_YN"].ToString();
                    this.ddlRoundValue.SelectedIndex = 0;
                    this.ddlSellable.SelectedIndex = 0;
                    this.ddlLabelSize.SelectedIndex = 0;
                    this.txtUPC.Focus();
                    try
                    {
                        this.cbCustomPrice.Checked = false;
                        this.txtCustomPrice.Text = "";
                        this.rbAutoClearYes.Checked = true;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
            try
            {
                if ((Session.isManager == "Yes") || (Session.isSupervisor == "Yes"))
                {
                    this.gbNewProduct.Visible = true;
                    this.gbOverridePrice.Visible = true;
                    this.gbOverrideDiscPrice.Visible = true;
                    this.gbOverrideVendor.Visible = true;
                    this.txtScanPrice.Enabled = true;
                    this.txtPrice.Enabled = true;
                    this.txtDiscountPrice.Enabled = true;
                }
                else
                {
                    this.gbNewProduct.Visible = false;
                    this.gbOverridePrice.Visible = false;
                    this.gbOverrideDiscPrice.Visible = false;
                    this.gbOverrideVendor.Visible = false;
                    this.txtScanPrice.Enabled = false;
                    this.txtPrice.Enabled = false;
                    this.txtDiscountPrice.Enabled = false;
                }
            }
            catch (Exception)
            {
            }
            try
            {
                this.FillVendorLoads(this.ddlVendors.Text);
                this.ddlRoundValue.SelectedIndex = 4;
                this.cbNoLoad.Checked = false;
            }
            catch (Exception)
            {
            }
        }

        private void txtOverrideScanPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void txtProduct_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.btnSearchDesc_Click(null, null);
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void txtUPC_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.btnSearch_Click(null, null);
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtUPC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void UpdateProductHeaders()
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.gvProducts.Columns[0].HeaderText = "ID";
                this.gvProducts.Columns[0].Visible = false;
                this.gvProducts.Columns[1].HeaderText = "Product";
                this.gvProducts.Columns[1].Width = 0x163;
                this.gvProducts.Columns[2].HeaderText = "Price";
                this.gvProducts.Columns[2].Width = 100;
                this.gvProducts.Columns[3].HeaderText = "UPC";
                this.gvProducts.Columns[3].Width = 0xf8;
                this.gvProducts.Focus();
            }
            catch (Exception)
            {
            }
        }

    }
}
