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

namespace DealStore
{
    public partial class settingsForm : Form
    {
        private Button btnClearForm;
        private Button btnClearNetworkForm;
        private Button btnMainMenu;
        private Button btnSaveNetworkPath;
        private Button btnSaveSettings;
        private Button button1;
        private ComboBox ddlRoundPrice;
        private ComboBox ddlVendors;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label10;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txtNetworkPath;
        private TextBox txtScanPrice;
        public settingsForm()
        {
            InitializeComponent();
        }


        private void btnClearForm_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            this.txtScanPrice.Text = "";
            this.ddlVendors.Text = "";
            this.ddlRoundPrice.Text = "";
        }

        private void btnClearNetworkForm_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            this.txtNetworkPath.Text = "";
            this.txtNetworkPath.Focus();
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            base.Close();
        }

        private void btnSaveNetworkPath_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if (this.txtNetworkPath.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter network path.", "Database Settings");
                    this.txtNetworkPath.Focus();
                }
                else if (!Directory.Exists(this.txtNetworkPath.Text.Trim() ?? ""))
                {
                    MessageBox.Show("Network path does not exist.", "Database Settings");
                    this.txtNetworkPath.Focus();
                }
                else
                {
                    using (StreamWriter writer = File.CreateText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\StockDBNetworkPath.txt"))
                    {
                        writer.Write(this.txtNetworkPath.Text.Trim());
                        writer.Close();
                        MessageBox.Show("Database settings saved successfully.", "Database Settings");
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error while saving database settings." + exception.Message, "Database Settings");
            }
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if (this.txtScanPrice.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter scan price.", "Settings");
                    this.txtScanPrice.Focus();
                }
                else
                {
                    int num = 0;
                    num = int.Parse(this.txtScanPrice.Text.Trim());
                    if ((num < 1) || (num > 100))
                    {
                        MessageBox.Show("Please enter valid scan price.", "Settings");
                        this.txtScanPrice.Focus();
                    }
                    else if (this.ddlVendors.Text == "")
                    {
                        MessageBox.Show("Please enter vendor name.", "Settings");
                        this.ddlVendors.Focus();
                    }
                    else if (this.ddlRoundPrice.Text == "")
                    {
                        MessageBox.Show("Please select rounding?.", "Settings");
                        this.ddlRoundPrice.Focus();
                    }
                    else
                    {
                        OleDbConnection connection = new OleDbConnection(DBAccess.msAccessCon);
                        connection.Open();
                        string cmdText = "INSERT INTO Setting(DEFAULT_DISCOUNT, VENDOR_NAME, ROUNDING_YN, SETTING_DATE) VALUES(@DEFAULT_DISCOUNT, @VENDOR_NAME, @ROUNDING_YN, @SETTING_DATE)";
                        cmdText = "INSERT INTO Setting(DEFAULT_DISCOUNT, VENDOR_NAME, ROUNDING_YN, SETTING_DATE) VALUES(?, ?, ?, ?)";
                        OleDbCommand command = new OleDbCommand(cmdText, connection);
                        command.Parameters.Add("@DEFAULT_DISCOUNT", OleDbType.Integer).Value = num;
                        command.Parameters.Add("@VENDOR_NAME", OleDbType.VarChar, 50).Value = this.ddlVendors.Text;
                        command.Parameters.Add("@ROUNDING_YN", OleDbType.VarChar, 5).Value = this.ddlRoundPrice.Text;
                        command.Parameters.Add("@SETTING_DATE", OleDbType.Date).Value = DateTime.Now;
                        command.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("Settings saved successfully.", "Settings");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error while saving settings.", "Settings");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if (this.txtNetworkPath.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter network path.", "Test Path");
                }
                else if (Directory.Exists(this.txtNetworkPath.Text.Trim() ?? ""))
                {
                    MessageBox.Show("Successful.", "Test Path");
                }
                else
                {
                    MessageBox.Show("Failed.", "Test Path");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed.", "Test Path");
            }
        }


        private void settingsForm_Load(object sender, EventArgs e)
        {
            string str;
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if (Session.isManager == "No")
                {
                    base.Close();
                }
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
                    this.ddlRoundPrice.Text = table2.Rows[0]["ROUNDING_YN"].ToString();
                }
            }
            catch (Exception)
            {
            }
            try
            {
                using (StreamReader reader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\StockDBNetworkPath.txt"))
                {
                    this.txtNetworkPath.Text = reader.ReadToEnd();
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }


        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (this.components != null))
        //    {
        //        this.components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
