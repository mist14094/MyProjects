using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace DealStore
{
    public partial class frmUsers : Form
    {
        private Button btnAdd;
        private Button btnDelete;
        private Button btnMainMenu;
        private Button btnReset;
        private ComboBox cbSupervisor;
        private ComboBox ddlManager;
        private ComboBox ddlStatus;
        private GroupBox groupBox2;
        private DataGridView gvUsers;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label lblName;
        private TextBox txtName;
        private TextBox txtPassword;
        private TextBox txtUserID;
        public frmUsers()
        {
            InitializeComponent();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if ((((this.txtName.Text.Trim() == "") || (this.txtPassword.Text.Trim() == "")) || ((this.ddlManager.Text == "") || (this.cbSupervisor.Text == ""))) || (this.ddlStatus.Text == ""))
                {
                    MessageBox.Show("Please input all fields.", "Users");
                }
                else
                {
                    DataTable table;
                    OleDbConnection connection;
                    string str;
                    OleDbCommand command;
                    OleDbConnection connection2;
                    string str2;
                    OleDbCommand command2;
                    if (this.txtUserID.Text.Trim() == "")
                    {
                        table = new DataTable();
                        connection = new OleDbConnection(DBAccess.msAccessCon);
                        connection.Open();
                        str = "SELECT * FROM StockUser WHERE USER_FULL_NAME = @USER_FULL_NAME";
                        str = "SELECT * FROM StockUser WHERE USER_FULL_NAME = ?";
                        command = new OleDbCommand(str, connection);
                        command.Parameters.Add("@USER_FULL_NAME", OleDbType.VarChar, 100).Value = this.txtName.Text.Trim();
                        OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                        adapter.Fill(table);
                        connection.Close();
                        command.Dispose();
                        connection.Dispose();
                        if (table.Rows.Count > 0)
                        {
                            MessageBox.Show("User already exist.", "Users");
                        }
                        else
                        {
                            connection2 = new OleDbConnection(DBAccess.msAccessCon);
                            connection2.Open();
                            str2 = "INSERT INTO StockUser(USER_FULL_NAME, USER_PASSWORD, IS_MANAGER, IS_SUPERVISOR, USER_STATUS, CREATEDBY, CREATEDATE, MODIFIEDBY, MODIFIEDDATE) VALUES(@USER_FULL_NAME, @USER_PASSWORD, @IS_MANAGER, @IS_SUPERVISOR, @USER_STATUS, @CREATEDBY, @CREATEDATE, @MODIFIEDBY, @MODIFIEDDATE)";
                            str2 = "INSERT INTO StockUser(USER_FULL_NAME, USER_PASSWORD, IS_MANAGER, IS_SUPERVISOR, USER_STATUS, CREATEDBY, CREATEDATE, MODIFIEDBY, MODIFIEDDATE) VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?)";
                            command2 = new OleDbCommand(str2, connection2);
                            command2.Parameters.Add("@USER_FULL_NAME", OleDbType.VarChar, 100).Value = this.txtName.Text.Trim();
                            command2.Parameters.Add("@USER_PASSWORD", OleDbType.VarChar, 20).Value = this.txtPassword.Text.Trim();
                            command2.Parameters.Add("@IS_MANAGER", OleDbType.VarChar, 3).Value = this.ddlManager.Text;
                            command2.Parameters.Add("@IS_SUPERVISOR", OleDbType.VarChar, 3).Value = this.cbSupervisor.Text;
                            command2.Parameters.Add("@USER_STATUS", OleDbType.VarChar, 8).Value = this.ddlStatus.Text;
                            command2.Parameters.Add("@CREATEDBY", OleDbType.Integer).Value = Session.userID;
                            command2.Parameters.Add("@CREATEDATE", OleDbType.VarChar, 0x19).Value = DateTime.Now;
                            command2.Parameters.Add("@MODIFIEDBY", OleDbType.Integer).Value = Session.userID;
                            command2.Parameters.Add("@MODIFIEDDATE", OleDbType.Date).Value = DateTime.Now;
                            if (command2.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("User added successfully.", "Users");
                                this.FillUsers();
                                this.btnReset_Click(null, null);
                            }
                            else
                            {
                                MessageBox.Show("User addition failed.", "Users");
                            }
                            connection2.Close();
                            command2.Dispose();
                            connection2.Dispose();
                        }
                    }
                    else if (this.txtName.Text != this.lblName.Text)
                    {
                        table = new DataTable();
                        connection = new OleDbConnection(DBAccess.msAccessCon);
                        connection.Open();
                        str = "SELECT * FROM StockUser WHERE USER_FULL_NAME = @USER_FULL_NAME";
                        str = "SELECT * FROM StockUser WHERE USER_FULL_NAME = ?";
                        command = new OleDbCommand(str, connection);
                        command.Parameters.Add("@USER_FULL_NAME", OleDbType.VarChar, 100).Value = this.txtName.Text.Trim();
                        new OleDbDataAdapter(command).Fill(table);
                        connection.Close();
                        command.Dispose();
                        connection.Dispose();
                        if (table.Rows.Count > 0)
                        {
                            MessageBox.Show("User already exist.", "Users");
                        }
                        else
                        {
                            connection2 = new OleDbConnection(DBAccess.msAccessCon);
                            connection2.Open();
                            str2 = "UPDATE StockUser SET USER_FULL_NAME = @USER_FULL_NAME, USER_PASSWORD = @USER_PASSWORD, IS_MANAGER = @IS_MANAGER, IS_SUPERVISOR = @IS_SUPERVISOR, USER_STATUS = @USER_STATUS, MODIFIEDBY = @MODIFIEDBY, MODIFIEDDATE = @MODIFIEDDATE WHERE USER_ID = @USER_ID";
                            str2 = "UPDATE StockUser SET USER_FULL_NAME = ?, USER_PASSWORD = ?, IS_MANAGER = ?, IS_SUPERVISOR = ?, USER_STATUS = ?, MODIFIEDBY = ?, MODIFIEDDATE = ? WHERE USER_ID = ?";
                            command2 = new OleDbCommand(str2, connection2);
                            command2.Parameters.Add("@USER_FULL_NAME", OleDbType.VarChar, 100).Value = this.txtName.Text.Trim();
                            command2.Parameters.Add("@USER_PASSWORD", OleDbType.VarChar, 20).Value = this.txtPassword.Text.Trim();
                            command2.Parameters.Add("@IS_MANAGER", OleDbType.VarChar, 3).Value = this.ddlManager.Text;
                            command2.Parameters.Add("@IS_SUPERVISOR", OleDbType.VarChar, 3).Value = this.cbSupervisor.Text;
                            command2.Parameters.Add("@USER_STATUS", OleDbType.VarChar, 8).Value = this.ddlStatus.Text;
                            command2.Parameters.Add("@MODIFIEDBY", OleDbType.Integer).Value = Session.userID;
                            command2.Parameters.Add("@MODIFIEDDATE", OleDbType.Date).Value = DateTime.Now;
                            command2.Parameters.Add("@USER_ID", OleDbType.Integer).Value = int.Parse(this.txtUserID.Text.Trim());
                            if (command2.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("User updated successfully.", "Users");
                                this.FillUsers();
                                this.btnReset_Click(null, null);
                            }
                            else
                            {
                                MessageBox.Show("User updation failed.", "Users");
                            }
                            connection2.Close();
                            command2.Dispose();
                            connection2.Dispose();
                        }
                    }
                    else
                    {
                        connection2 = new OleDbConnection(DBAccess.msAccessCon);
                        connection2.Open();
                        str2 = "UPDATE StockUser SET USER_FULL_NAME = @USER_FULL_NAME, USER_PASSWORD = @USER_PASSWORD, IS_MANAGER = @IS_MANAGER, IS_SUPERVISOR = @IS_SUPERVISOR, USER_STATUS = @USER_STATUS, MODIFIEDBY = @MODIFIEDBY, MODIFIEDDATE = @MODIFIEDDATE WHERE USER_ID = @USER_ID";
                        str2 = "UPDATE StockUser SET USER_FULL_NAME = ?, USER_PASSWORD = ?, IS_MANAGER = ?, IS_SUPERVISOR = ?, USER_STATUS = ?, MODIFIEDBY = ?, MODIFIEDDATE = ? WHERE USER_ID = ?";
                        command2 = new OleDbCommand(str2, connection2);
                        command2.Parameters.Add("@USER_FULL_NAME", OleDbType.VarChar, 100).Value = this.txtName.Text.Trim();
                        command2.Parameters.Add("@USER_PASSWORD", OleDbType.VarChar, 20).Value = this.txtPassword.Text.Trim();
                        command2.Parameters.Add("@IS_MANAGER", OleDbType.VarChar, 3).Value = this.ddlManager.Text;
                        command2.Parameters.Add("@IS_SUPERVISOR", OleDbType.VarChar, 3).Value = this.cbSupervisor.Text;
                        command2.Parameters.Add("@USER_STATUS", OleDbType.VarChar, 8).Value = this.ddlStatus.Text;
                        command2.Parameters.Add("@MODIFIEDBY", OleDbType.Integer).Value = Session.userID;
                        command2.Parameters.Add("@MODIFIEDDATE", OleDbType.Date).Value = DateTime.Now;
                        command2.Parameters.Add("@USER_ID", OleDbType.Integer).Value = int.Parse(this.txtUserID.Text.Trim());
                        if (command2.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("User updated successfully.", "Users");
                            this.FillUsers();
                            this.btnReset_Click(null, null);
                        }
                        else
                        {
                            MessageBox.Show("User updation failed.", "Users");
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                DataGridViewRow row = this.gvUsers.SelectedRows[0];
                int num = int.Parse(row.Cells[0].Value.ToString());
                OleDbConnection connection = new OleDbConnection(DBAccess.msAccessCon);
                connection.Open();
                OleDbCommand command = new OleDbCommand("DELETE FROM StockUser WHERE USER_ID = " + num.ToString(), connection);
                int num2 = command.ExecuteNonQuery();
                connection.Close();
                command.Dispose();
                connection.Dispose();
                if (num2 > 0)
                {
                    MessageBox.Show("User deleted successfully.", "Users");
                    this.FillUsers();
                    this.btnReset_Click(null, null);
                }
                else
                {
                    MessageBox.Show("User deletion failed.", "Users");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("User deletion failed. " + exception.Message, "Users");
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
                this.txtUserID.Text = this.txtName.Text = this.txtPassword.Text = "";
                this.ddlManager.SelectedIndex = 0;
                this.cbSupervisor.SelectedIndex = 0;
                this.ddlStatus.SelectedIndex = 0;
                this.btnAdd.Text = "Add";
            }
            catch (Exception)
            {
            }
        }


        private void FillUsers()
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                string query = "SELECT USER_ID, USER_FULL_NAME, USER_PASSWORD, IS_MANAGER, IS_SUPERVISOR, USER_STATUS FROM StockUser ORDER BY USER_FULL_NAME";
                DataTable dataTable = DBAccess.GetDataTable(query, DBAccess.msAccessCon);
                this.gvUsers.DataSource = dataTable;
                this.gvUsers.Visible = true;
                this.UpdateUserHeaders();
            }
            catch (Exception)
            {
            }
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            this.ddlManager.SelectedIndex = 0;
            this.cbSupervisor.SelectedIndex = 0;
            this.ddlStatus.SelectedIndex = 0;
            this.FillUsers();
        }

        private void gvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.txtUserID.Text = this.gvUsers.Rows[e.RowIndex].Cells[0].Value.ToString();
                this.txtName.Text = this.gvUsers.Rows[e.RowIndex].Cells[1].Value.ToString();
                this.lblName.Text = this.gvUsers.Rows[e.RowIndex].Cells[1].Value.ToString();
                this.txtPassword.Text = this.gvUsers.Rows[e.RowIndex].Cells[2].Value.ToString();
                this.ddlManager.SelectedIndex = 0;
                if (this.gvUsers.Rows[e.RowIndex].Cells[3].Value.ToString() == "Yes")
                {
                    this.ddlManager.SelectedIndex = 1;
                }
                this.cbSupervisor.SelectedIndex = 0;
                if (this.gvUsers.Rows[e.RowIndex].Cells[4].Value.ToString() == "Yes")
                {
                    this.cbSupervisor.SelectedIndex = 1;
                }
                this.ddlStatus.SelectedIndex = 0;
                if (this.gvUsers.Rows[e.RowIndex].Cells[5].Value.ToString() == "Inactive")
                {
                    this.ddlStatus.SelectedIndex = 1;
                }
                this.btnAdd.Text = "Update";
            }
            catch (Exception)
            {
            }
        }


        private void UpdateUserHeaders()
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.gvUsers.Columns[0].HeaderText = "ID";
                this.gvUsers.Columns[0].Visible = false;
                this.gvUsers.Columns[1].HeaderText = "Name";
                this.gvUsers.Columns[1].Width = 140;
                this.gvUsers.Columns[2].HeaderText = "Password";
                this.gvUsers.Columns[2].Width = 140;
                this.gvUsers.Columns[3].HeaderText = "Manager";
                this.gvUsers.Columns[3].Width = 140;
                this.gvUsers.Columns[4].HeaderText = "Supervisor";
                this.gvUsers.Columns[4].Width = 150;
                this.gvUsers.Columns[5].HeaderText = "Status";
                this.gvUsers.Columns[5].Width = 0x73;
            }
            catch (Exception)
            {
            }
        }

    }
}
