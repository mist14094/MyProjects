using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace DealStore
{
    public partial class frmVendors : Form
    {
        private Button btnAdd;
        private Button btnAddLoads;
        private Button btnClose;
        private Button btnDelete;
        private Button btnMainMenu;
        private Button btnReset;
        private Button button1;
        private Button button2;
        private GroupBox gbVendorLoads;
        private GroupBox groupBox2;
        private DataGridView gvVendorLoads;
        private DataGridView gvVendors;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblVendorID;
        private TextBox txtVendorDefaultDiscount;
        private TextBox txtVendorLoadName;
        private TextBox txtVendorName;

        public frmVendors()
        {
            InitializeComponent();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if ((this.txtVendorName.Text.Trim() == "") || (this.txtVendorDefaultDiscount.Text.Trim() == ""))
                {
                    MessageBox.Show("Please input all fields.", "Vendors");
                }
                else
                {
                    int num = 0;
                    num = int.Parse(this.txtVendorDefaultDiscount.Text.Trim());
                    if ((num < 1) || (num > 100))
                    {
                        MessageBox.Show("Please enter valid scan price.", "Vendors");
                        this.txtVendorDefaultDiscount.Focus();
                    }
                    else
                    {
                        OleDbConnection connection2;
                        string str2;
                        OleDbCommand command2;
                        DataTable dataTable = new DataTable();
                        OleDbConnection connection = new OleDbConnection(DBAccess.msAccessCon);
                        connection.Open();
                        string cmdText = "SELECT * FROM Vendor WHERE VENDOR_NAME = @VENDOR_NAME";
                        cmdText = "SELECT * FROM Vendor WHERE VENDOR_NAME = ?";
                        OleDbCommand selectCommand = new OleDbCommand(cmdText, connection);
                        selectCommand.Parameters.Add("@VENDOR_NAME", OleDbType.VarChar, 50).Value = this.txtVendorName.Text.Trim();
                        new OleDbDataAdapter(selectCommand).Fill(dataTable);
                        connection.Close();
                        selectCommand.Dispose();
                        connection.Dispose();
                        if (dataTable.Rows.Count > 0)
                        {
                            connection2 = new OleDbConnection(DBAccess.msAccessCon);
                            connection2.Open();
                            str2 = "UPDATE Vendor SET VENDOR_DEFAULT_DISCOUNT = @VENDOR_DEFAULT_DISCOUNT, MODIFIEDBY = @MODIFIEDBY, MODIFIEDDATE = @MODIFIEDDATE WHERE VENDOR_ID = @VENDOR_ID";
                            str2 = "UPDATE Vendor SET VENDOR_DEFAULT_DISCOUNT = ?, MODIFIEDBY = ?, MODIFIEDDATE = ? WHERE VENDOR_ID = ?";
                            command2 = new OleDbCommand(str2, connection2);
                            command2.Parameters.Add("@VENDOR_DEFAULT_DISCOUNT", OleDbType.Integer).Value = num;
                            command2.Parameters.Add("@MODIFIEDBY", OleDbType.Integer).Value = Session.userID;
                            command2.Parameters.Add("@MODIFIEDDATE", OleDbType.Date).Value = DateTime.Now;
                            command2.Parameters.Add("@VENDOR_ID", OleDbType.Integer).Value = int.Parse(dataTable.Rows[0]["VENDOR_ID"].ToString());
                            int num2 = command2.ExecuteNonQuery();
                            MessageBox.Show("Vendor updated successfully.", "Vendors");
                            this.FillVendors();
                            this.btnReset_Click(null, null);
                            connection2.Close();
                            command2.Dispose();
                            connection2.Dispose();
                        }
                        else
                        {
                            connection2 = new OleDbConnection(DBAccess.msAccessCon);
                            connection2.Open();
                            str2 = "INSERT INTO Vendor(VENDOR_NAME, VENDOR_DEFAULT_DISCOUNT, CREATEDBY, CREATEDATE, MODIFIEDBY, MODIFIEDDATE) VALUES(@VENDOR_NAME, @VENDOR_DEFAULT_DISCOUNT, @CREATEDBY, @CREATEDATE, @MODIFIEDBY, @MODIFIEDDATE)";
                            str2 = "INSERT INTO Vendor(VENDOR_NAME, VENDOR_DEFAULT_DISCOUNT, CREATEDBY, CREATEDATE, MODIFIEDBY, MODIFIEDDATE) VALUES(?, ?, ?, ?, ?, ?)";
                            command2 = new OleDbCommand(str2, connection2);
                            command2.Parameters.Add("@VENDOR_NAME", OleDbType.VarChar, 50).Value = this.txtVendorName.Text.Trim();
                            command2.Parameters.Add("@VENDOR_DEFAULT_DISCOUNT", OleDbType.Integer).Value = num;
                            command2.Parameters.Add("@CREATEDBY", OleDbType.Integer).Value = Session.userID;
                            command2.Parameters.Add("@CREATEDATE", OleDbType.VarChar, 0x19).Value = DateTime.Now;
                            command2.Parameters.Add("@MODIFIEDBY", OleDbType.Integer).Value = Session.userID;
                            command2.Parameters.Add("@MODIFIEDDATE", OleDbType.Date).Value = DateTime.Now;
                            if (command2.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Vendor added successfully.", "Vendors");
                                this.FillVendors();
                                this.btnReset_Click(null, null);
                            }
                            else
                            {
                                MessageBox.Show("Vendor addition failed.", "Vendors");
                            }
                            connection2.Close();
                            command2.Dispose();
                            connection2.Dispose();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnAddLoads_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                DataGridViewRow row = this.gvVendors.SelectedRows[0];
                Session.VendorID = int.Parse(row.Cells[0].Value.ToString());
                this.FillVendorLoads();
                this.gbVendorLoads.Visible = true;
                this.txtVendorLoadName.Text = "";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Vendor");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            this.gbVendorLoads.Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                DataGridViewRow row = this.gvVendors.SelectedRows[0];
                int num = int.Parse(row.Cells[0].Value.ToString());
                OleDbConnection connection = new OleDbConnection(DBAccess.msAccessCon);
                connection.Open();
                OleDbCommand command = new OleDbCommand("DELETE FROM Vendor WHERE VENDOR_ID = " + num.ToString(), connection);
                int num2 = command.ExecuteNonQuery();
                connection.Close();
                command.Dispose();
                connection.Dispose();
                if (num2 > 0)
                {
                    MessageBox.Show("Vendor deleted successfully.", "Vendors");
                    this.FillVendors();
                }
                else
                {
                    MessageBox.Show("Vendor deletion failed.", "Vendors");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Vendor deletion failed. " + exception.Message, "Vendors");
            }
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            base.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.lblVendorID.Text = this.txtVendorName.Text = this.txtVendorDefaultDiscount.Text = "";
                this.btnAdd.Text = "Add";
            }
            catch (Exception)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                DataGridViewRow row = this.gvVendorLoads.SelectedRows[0];
                int num = int.Parse(row.Cells[0].Value.ToString());
                OleDbConnection connection = new OleDbConnection(DBAccess.msAccessCon);
                connection.Open();
                OleDbCommand command = new OleDbCommand("DELETE FROM VendorLoad WHERE VENDOR_LOAD_ID = " + num.ToString(), connection);
                int num2 = command.ExecuteNonQuery();
                connection.Close();
                command.Dispose();
                connection.Dispose();
                if (num2 > 0)
                {
                    MessageBox.Show("Vendor Load deleted successfully.", "Vendor Loads");
                    this.FillVendorLoads();
                }
                else
                {
                    MessageBox.Show("Vendor Load deletion failed.", "Vendor Loads");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Vendor Load deletion failed. " + exception.Message, "Vendor Loads");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if (this.txtVendorLoadName.Text.Trim() == "")
                {
                    MessageBox.Show("Please input all fields.", "Vendor Loads");
                }
                else
                {
                    DataTable dataTable = new DataTable();
                    OleDbConnection connection = new OleDbConnection(DBAccess.msAccessCon);
                    connection.Open();
                    string cmdText = "SELECT * FROM VendorLoad WHERE VENDOR_LOAD_NAME = @VENDOR_LOAD_NAME AND VENDOR_LOAD_VENDOR_ID = @VENDOR_LOAD_VENDOR_ID";
                    cmdText = "SELECT * FROM VendorLoad WHERE VENDOR_LOAD_NAME = ? AND VENDOR_LOAD_VENDOR_ID = ?";
                    OleDbCommand selectCommand = new OleDbCommand(cmdText, connection);
                    selectCommand.Parameters.Add("@VENDOR_LOAD_NAME", OleDbType.VarChar, 0xff).Value = this.txtVendorLoadName.Text.Trim();
                    selectCommand.Parameters.Add("@VENDOR_LOAD_VENDOR_ID", OleDbType.Integer).Value = Session.VendorID;
                    new OleDbDataAdapter(selectCommand).Fill(dataTable);
                    connection.Close();
                    selectCommand.Dispose();
                    connection.Dispose();
                    if (dataTable.Rows.Count > 0)
                    {
                        MessageBox.Show("Vendor Load already exist.", "Vendor Loads");
                    }
                    else
                    {
                        OleDbConnection connection2 = new OleDbConnection(DBAccess.msAccessCon);
                        connection2.Open();
                        string str2 = "INSERT INTO VendorLoad(VENDOR_LOAD_NAME, VENDOR_LOAD_VENDOR_ID, CREATEDBY, CREATEDATE, MODIFIEDBY, MODIFIEDDATE) VALUES(@VENDOR_NAME, @VENDOR_LOAD_VENDOR_ID, @CREATEDBY, @CREATEDATE, @MODIFIEDBY, @MODIFIEDDATE)";
                        str2 = "INSERT INTO VendorLoad(VENDOR_LOAD_NAME, VENDOR_LOAD_VENDOR_ID, CREATEDBY, CREATEDATE, MODIFIEDBY, MODIFIEDDATE) VALUES(?, ?, ?, ?, ?, ?)";
                        OleDbCommand command2 = new OleDbCommand(str2, connection2);
                        command2.Parameters.Add("@VENDOR_LOAD_NAME", OleDbType.VarChar, 0xff).Value = this.txtVendorLoadName.Text.Trim();
                        command2.Parameters.Add("@VENDOR_LOAD_VENDOR_ID", OleDbType.Integer).Value = Session.VendorID;
                        command2.Parameters.Add("@CREATEDBY", OleDbType.Integer).Value = Session.userID;
                        command2.Parameters.Add("@CREATEDATE", OleDbType.VarChar, 0x19).Value = DateTime.Now;
                        command2.Parameters.Add("@MODIFIEDBY", OleDbType.Integer).Value = Session.userID;
                        command2.Parameters.Add("@MODIFIEDDATE", OleDbType.Date).Value = DateTime.Now;
                        if (command2.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Vendor Load added successfully.", "Vendor Loads");
                            this.FillVendorLoads();
                        }
                        else
                        {
                            MessageBox.Show("Vendor Load addition failed.", "Vendor Loads");
                        }
                        connection2.Close();
                        command2.Dispose();
                        connection2.Dispose();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

      

        private void FillVendorLoads()
        {
            Session.dtLoginTime = DateTime.Now;
            DataTable dataTable = DBAccess.GetDataTable("SELECT VENDOR_LOAD_ID, VENDOR_LOAD_NAME FROM VendorLoad WHERE VENDOR_LOAD_VENDOR_ID = " + Session.VendorID + " ORDER BY VENDOR_LOAD_ID", DBAccess.msAccessCon);
            this.gvVendorLoads.DataSource = dataTable;
            this.gvVendorLoads.Visible = true;
            this.UpdateVendorLoadHeaders();
        }

        private void FillVendors()
        {
            Session.dtLoginTime = DateTime.Now;
            string query = "SELECT VENDOR_ID, VENDOR_NAME, VENDOR_DEFAULT_DISCOUNT FROM Vendor ORDER BY VENDOR_NAME";
            DataTable dataTable = DBAccess.GetDataTable(query, DBAccess.msAccessCon);
            this.gvVendors.DataSource = dataTable;
            this.gvVendors.Visible = true;
            this.UpdateVendorHeaders();
        }

        private void frmVendors_Load(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.FillVendors();
            }
            catch (Exception)
            {
            }
        }

        private void gvVendors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.lblVendorID.Text = this.gvVendors.Rows[e.RowIndex].Cells[0].Value.ToString();
                this.txtVendorName.Text = this.gvVendors.Rows[e.RowIndex].Cells[1].Value.ToString();
                this.txtVendorDefaultDiscount.Text = this.gvVendors.Rows[e.RowIndex].Cells[2].Value.ToString();
                this.btnAdd.Text = "Update";
            }
            catch (Exception)
            {
            }
        }


        private void txtVendorDefaultDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void UpdateVendorHeaders()
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.gvVendors.Columns[0].HeaderText = "ID";
                this.gvVendors.Columns[0].Visible = false;
                this.gvVendors.Columns[1].HeaderText = "Vendor Name";
                this.gvVendors.Columns[1].Width = 0x1f7;
                this.gvVendors.Columns[2].HeaderText = "Scan Price";
                this.gvVendors.Columns[2].Width = 200;
            }
            catch (Exception)
            {
            }
        }

        private void UpdateVendorLoadHeaders()
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.gvVendorLoads.Columns[0].HeaderText = "ID";
                this.gvVendorLoads.Columns[0].Visible = false;
                this.gvVendorLoads.Columns[1].HeaderText = "Vendor Load Name";
                this.gvVendorLoads.Columns[1].Width = 0x2bf;
            }
            catch (Exception)
            {
            }
        }

    }
}
