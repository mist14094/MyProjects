using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DealStore
{
    public partial class mainForm : Form
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button7;
        private Button button8;
        private Button button9;
        private ToolStripMenuItem dataEntryToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem importToolStripMenuItem;
        private Label label1;
        private Label label2;
        private Label lblLoginUser;
        private string logoutReason = "Manual";
        private ToolStripMenuItem manageUsersToolStripMenuItem;
        private ToolStripMenuItem manageVendorsToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem palletsToolStripMenuItem;
        private PictureBox pictureBox1;
        private ToolStripMenuItem scanToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private Splitter splitter1;
        private Timer timer1;
        private ToolStripMenuItem userGuideToolStripMenuItem;
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Session.userID != 0)
                {
                    this.UpdateActivityLog(this.logoutReason);
                }
            }
            catch (Exception)
            {
            }
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            foreach (Control control in base.Controls)
            {
                if (control is MdiClient)
                {
                    control.ControlRemoved += new ControlEventHandler(this.MDIClient_ControlRemoved);
                    break;
                }
            }
            this.scanToolStripMenuItem.Enabled = false;
            this.palletsToolStripMenuItem.Enabled = false;
            this.importToolStripMenuItem.Enabled = false;
            this.settingsToolStripMenuItem.Enabled = false;
            this.dataEntryToolStripMenuItem.Enabled = false;
            this.manageVendorsToolStripMenuItem.Enabled = false;
            this.manageUsersToolStripMenuItem.Enabled = false;
            this.button1.Enabled = false;
            this.button2.Enabled = false;
            this.button3.Enabled = false;
            this.button4.Enabled = false;
            this.button8.Enabled = false;
            this.button9.Enabled = false;
            try
            {
                string query = "SELECT USER_ID, USER_FULL_NAME FROM StockUser WHERE USER_STATUS = 'Active' ORDER BY USER_FULL_NAME";
                DataTable dataTable = DBAccess.GetDataTable(query, DBAccess.msAccessCon);
                this.ddlUsers.DisplayMember = "USER_FULL_NAME";
                this.ddlUsers.ValueMember = "USER_ID";
                this.ddlUsers.DataSource = dataTable;
                this.gbLogin.Visible = true;
                this.gbLogin.BringToFront();
                this.ddlUsers.Focus();
            }
            catch (Exception)
            {
            }
        }

        private void manageUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.SendToBackAll();
                frmUsers users = null;
                foreach (Form form in base.MdiChildren)
                {
                    if (form is frmUsers)
                    {
                        users = (frmUsers)form;
                        break;
                    }
                }
                if (users != null)
                {
                    users.Close();
                    users = new frmUsers
                    {
                        MdiParent = this,
                        WindowState = FormWindowState.Maximized
                    };
                    users.Show();
                    users.Focus();
                }
                else
                {
                    users = new frmUsers
                    {
                        MdiParent = this
                    };
                    users.Show();
                    users.Focus();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void manageVendorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.SendToBackAll();
                frmVendors vendors = null;
                foreach (Form form in base.MdiChildren)
                {
                    if (form is frmVendors)
                    {
                        vendors = (frmVendors)form;
                        break;
                    }
                }
                if (vendors != null)
                {
                    vendors.Close();
                    vendors = new frmVendors
                    {
                        MdiParent = this,
                        WindowState = FormWindowState.Maximized
                    };
                    vendors.Show();
                    vendors.Focus();
                }
                else
                {
                    vendors = new frmVendors
                    {
                        MdiParent = this
                    };
                    vendors.Show();
                    vendors.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        protected void MDIClient_ControlAdded(object sender, ControlEventArgs e)
        {
            Form control = e.Control as Form;
        }

        protected void MDIClient_ControlRemoved(object sender, ControlEventArgs e)
        {
            this.BringToFrontAll();
            if (Session.isLogOut == "Yes")
            {
                Session.isLogOut = "No";
                this.button7_Click(null, null);
            }
        }

        private void palletsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.SendToBackAll();
                frmPallets pallets = null;
                foreach (Form form in base.MdiChildren)
                {
                    if (form is frmPallets)
                    {
                        pallets = (frmPallets)form;
                        break;
                    }
                }
                if (pallets != null)
                {
                    pallets.Close();
                    pallets = new frmPallets
                    {
                        MdiParent = this,
                        WindowState = FormWindowState.Maximized
                    };
                    pallets.Show();
                    pallets.Focus();
                }
                else
                {
                    pallets = new frmPallets
                    {
                        MdiParent = this
                    };
                    pallets.Show();
                    pallets.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void scanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.SendToBackAll();
                scanForm form = null;
                foreach (Form form2 in base.MdiChildren)
                {
                    if (form2 is scanForm)
                    {
                        form = (scanForm)form2;
                        break;
                    }
                }
                if (form != null)
                {
                    form.Close();
                    form = new scanForm
                    {
                        MdiParent = this,
                        WindowState = FormWindowState.Maximized
                    };
                    form.Show();
                    form.Focus();
                }
                else
                {
                    form = new scanForm
                    {
                        MdiParent = this
                    };
                    form.Show();
                    form.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void SendToBackAll()
        {
            foreach (Form form in base.MdiChildren)
            {
                form.Close();
            }
            this.button1.SendToBack();
            this.button2.SendToBack();
            this.button3.SendToBack();
            this.button4.SendToBack();
            this.button5.SendToBack();
            this.button7.SendToBack();
            this.button8.SendToBack();
            this.button9.SendToBack();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.SendToBackAll();
                settingsForm form = null;
                foreach (Form form2 in base.MdiChildren)
                {
                    if (form2 is settingsForm)
                    {
                        form = (settingsForm)form2;
                        break;
                    }
                }
                if (form != null)
                {
                    form.Close();
                    form = new settingsForm
                    {
                        MdiParent = this,
                        WindowState = FormWindowState.Maximized
                    };
                    form.Show();
                    form.Focus();
                }
                else
                {
                    form = new settingsForm
                    {
                        MdiParent = this
                    };
                    form.Show();
                    form.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Session.userID != 0)
            {
                TimeSpan span = new TimeSpan();
                span = (TimeSpan)(DateTime.Now - Session.dtLoginTime);
                if (span.TotalMinutes >= 15.0)
                {
                    this.logoutReason = "In-Active";
                    this.button7_Click(null, null);
                    MessageBox.Show("Logged Out due to Inactivity.");
                }
            }
        }

        private void UpdateActivityLog(string reason)
        {
            try
            {
                if (Session.activityLogID != 0L)
                {
                    OleDbConnection connection = new OleDbConnection(DBAccess.msAccessCon);
                    connection.Open();
                    string cmdText = "UPDATE ActivityLog SET AL_LOGOUT_TIME = ?, AL_LOGOUT_REASON = ? WHERE ACTIVITY_LOG_ID = ?";
                    OleDbCommand command = new OleDbCommand(cmdText, connection);
                    command.Parameters.Add("@AL_LOGOUT_TIME", OleDbType.Date).Value = DateTime.Now;
                    command.Parameters.Add("@AL_LOGOUT_REASON", OleDbType.VarChar, 10).Value = reason;
                    command.Parameters.Add("@ACTIVITY_LOG_ID", OleDbType.BigInt).Value = Session.activityLogID;
                    command.ExecuteNonQuery();
                    connection.Close();
                    command.Dispose();
                    connection.Dispose();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void userGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                Process.Start(Directory.GetCurrentDirectory() + @"\STOCK SCAN USER GUIDE090412.pdf");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error - " + exception.Message);
            }
        }

        private void BringToFrontAll()
        {
            this.button1.BringToFront();
            this.button2.BringToFront();
            this.button3.BringToFront();
            this.button4.BringToFront();
            this.button5.BringToFront();
            this.button7.BringToFront();
            this.button8.BringToFront();
            this.button9.BringToFront();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if (Session.userID != 0)
                {
                    this.UpdateActivityLog(this.logoutReason);
                }
                base.Close();
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
                this.SendToBackAll();
                scanForm form = null;
                foreach (Form form2 in base.MdiChildren)
                {
                    if (form2 is scanForm)
                    {
                        form = (scanForm)form2;
                        break;
                    }
                }
                if (form != null)
                {
                    form.Close();
                    form = new scanForm
                    {
                        MdiParent = this,
                        WindowState = FormWindowState.Maximized
                    };
                    form.Show();
                    form.Focus();
                }
                else
                {
                    form = new scanForm
                    {
                        MdiParent = this
                    };
                    form.Show();
                    form.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.SendToBackAll();
                frmDataEntry entry = null;
                foreach (Form form in base.MdiChildren)
                {
                    if (form is frmDataEntry)
                    {
                        entry = (frmDataEntry)form;
                        break;
                    }
                }
                if (entry != null)
                {
                    entry.Close();
                    entry = new frmDataEntry
                    {
                        MdiParent = this,
                        WindowState = FormWindowState.Maximized
                    };
                    entry.Show();
                    entry.Focus();
                }
                else
                {
                    entry = new frmDataEntry
                    {
                        MdiParent = this
                    };
                    frmDataEntry fm = new frmDataEntry();
                    fm.Show();
                    entry.Show();
                    entry.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.SendToBackAll();
                settingsForm form = null;
                foreach (Form form2 in base.MdiChildren)
                {
                    if (form2 is settingsForm)
                    {
                        form = (settingsForm)form2;
                        break;
                    }
                }
                if (form != null)
                {
                    form.Close();
                    form = new settingsForm
                    {
                        MdiParent = this,
                        WindowState = FormWindowState.Maximized
                    };
                    form.Show();
                    form.Focus();
                }
                else
                {
                    form = new settingsForm
                    {
                        MdiParent = this
                    };
                    form.Show();
                    form.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.SendToBackAll();
                ImportForm form = null;
                foreach (Form form2 in base.MdiChildren)
                {
                    if (form2 is ImportForm)
                    {
                        form = (ImportForm)form2;
                        break;
                    }
                }
                if (form != null)
                {
                    form.Close();
                    form = new ImportForm
                    {
                        MdiParent = this,
                        WindowState = FormWindowState.Maximized
                    };
                    form.Show();
                    form.Focus();
                }
                else
                {
                    form = new ImportForm
                    {
                        MdiParent = this
                    };
                    form.Show();
                    form.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if (Session.userID != 0)
                {
                    this.UpdateActivityLog(this.logoutReason);
                }
                base.Close();
            }
            catch (Exception)
            {
            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                Session.dtLoginTime = DateTime.Now;
                try
                {
                    if ((this.ddlUsers.SelectedValue.ToString() != "") && (this.txtPassword.Text.Trim() != ""))
                    {
                        DataTable dataTable =
                            DBAccess.GetDataTable(
                                "SELECT USER_ID, IS_MANAGER, IS_SUPERVISOR FROM StockUser WHERE USER_ID = " +
                                this.ddlUsers.SelectedValue.ToString() + " AND USER_PASSWORD = '" +
                                this.txtPassword.Text.Trim() + "'", DBAccess.msAccessCon);
                        if (dataTable.Rows.Count > 0)
                        {
                            try
                            {




                                OleDbConnection connection = new OleDbConnection(DBAccess.msAccessCon);
                                connection.Open();

                                string cmdText =
                                    " DECLARE @AL_USER int; DECLARE @AL_LOGIN_TIME DATETIME;  INSERT INTO ActivityLog(AL_USER, AL_LOGIN_TIME) VALUES(?, ?)";
                                OleDbCommand command = new OleDbCommand(cmdText, connection);
                                command.CommandType = CommandType.Text;
                                command.Parameters.AddWithValue("@AL_USER", this.ddlUsers.SelectedValue.ToString());
                                command.Parameters.AddWithValue("@AL_LOGIN_TIME", DateTime.Now);
                                //command.Parameters.Add("@AL_USER", OleDbType.Integer).Value = int.Parse(this.ddlUsers.SelectedValue.ToString());
                                DateTime now = DateTime.Now;
                                //command.Parameters.Add("@AL_LOGIN_TIME", OleDbType.Date).Value = now;
                                command.ExecuteScalar();
                                connection.Close();
                                command.Dispose();
                                connection.Dispose();
                                DataTable table2 =
                                    DBAccess.GetDataTable(
                                        "SELECT ACTIVITY_LOG_ID FROM ActivityLog WHERE AL_USER = " +
                                        this.ddlUsers.SelectedValue.ToString() + " AND AL_LOGIN_TIME BETWEEN '" +
                                        DateTime.Parse(now.ToShortDateString()).ToString() + "' AND '" +
                                        DateTime.Parse(now.ToShortDateString()).AddDays(1.0).AddSeconds(-1.0).ToString() +
                                        "' ORDER BY ACTIVITY_LOG_ID DESC", DBAccess.msAccessCon);
                                if (table2.Rows.Count > 0)
                                {
                                    Session.activityLogID = long.Parse(table2.Rows[0]["ACTIVITY_LOG_ID"].ToString());
                                }
                            }
                            catch (Exception EX)
                            {
                            }
                            this.txtPassword.Text = "";
                            this.gbLogin.Visible = false;
                            this.gbLogin.SendToBack();
                            Session.userID = int.Parse(this.ddlUsers.SelectedValue.ToString());
                            Session.isManager = dataTable.Rows[0]["IS_MANAGER"].ToString();
                            Session.isSupervisor = dataTable.Rows[0]["IS_SUPERVISOR"].ToString();
                            this.scanToolStripMenuItem.Enabled = true;
                            this.palletsToolStripMenuItem.Enabled = true;
                            this.dataEntryToolStripMenuItem.Enabled = true;
                            this.button1.Enabled = true;
                            this.button2.Enabled = true;
                            this.lblLoginUser.Text = "Logon User ID: " + Session.userID.ToString();
                            if (Session.isManager == "Yes")
                            {
                                this.importToolStripMenuItem.Enabled = true;
                                this.settingsToolStripMenuItem.Enabled = true;
                                this.manageVendorsToolStripMenuItem.Enabled = true;
                                this.manageUsersToolStripMenuItem.Enabled = true;
                                this.palletsToolStripMenuItem.Enabled = true;
                                this.button3.Enabled = true;
                                this.button4.Enabled = true;
                                this.button8.Enabled = true;
                                this.button9.Enabled = true;
                            }
                            else
                            {
                                this.importToolStripMenuItem.Enabled = false;
                                this.settingsToolStripMenuItem.Enabled = false;
                                this.manageVendorsToolStripMenuItem.Enabled = false;
                                this.manageUsersToolStripMenuItem.Enabled = false;
                                this.palletsToolStripMenuItem.Enabled = false;
                                this.button3.Enabled = false;
                                this.button4.Enabled = false;
                                this.button8.Enabled = false;
                                this.button9.Enabled = false;
                            }
                            this.BringToFrontAll();
                        }
                        else
                        {
                            MessageBox.Show("Login Failed", "Login");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please input all fields.", "Login");
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Login");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                if ((this.ddlUsers.SelectedValue.ToString() != "") && (this.txtPassword.Text.Trim() != ""))
                {
                    DataTable dataTable = DBAccess.GetDataTable("SELECT USER_ID, IS_MANAGER, IS_SUPERVISOR FROM StockUser WHERE USER_ID = " + this.ddlUsers.SelectedValue.ToString() + " AND USER_PASSWORD = '" + this.txtPassword.Text.Trim() + "'", DBAccess.msAccessCon);
                    if (dataTable.Rows.Count > 0)                                                                                                    
                    {
                        try
                        {




                            OleDbConnection connection = new OleDbConnection(DBAccess.msAccessCon);
                            connection.Open();
                            
                            string cmdText = " DECLARE @AL_USER int; DECLARE @AL_LOGIN_TIME DATETIME;  INSERT INTO ActivityLog(AL_USER, AL_LOGIN_TIME) VALUES(?, ?)";
                            OleDbCommand command = new OleDbCommand(cmdText, connection);
                            command.CommandType = CommandType.Text;
                            command.Parameters.AddWithValue("@AL_USER", this.ddlUsers.SelectedValue.ToString());
                            command.Parameters.AddWithValue("@AL_LOGIN_TIME", DateTime.Now);
                            //command.Parameters.Add("@AL_USER", OleDbType.Integer).Value = int.Parse(this.ddlUsers.SelectedValue.ToString());
                            DateTime now = DateTime.Now;
                            //command.Parameters.Add("@AL_LOGIN_TIME", OleDbType.Date).Value = now;
                            command.ExecuteScalar();
                            connection.Close();
                            command.Dispose();
                            connection.Dispose();
                            DataTable table2 = DBAccess.GetDataTable("SELECT ACTIVITY_LOG_ID FROM ActivityLog WHERE AL_USER = " + this.ddlUsers.SelectedValue.ToString() + " AND AL_LOGIN_TIME BETWEEN '" + DateTime.Parse(now.ToShortDateString()).ToString() + "' AND '" + DateTime.Parse(now.ToShortDateString()).AddDays(1.0).AddSeconds(-1.0).ToString() + "' ORDER BY ACTIVITY_LOG_ID DESC", DBAccess.msAccessCon);
                            if (table2.Rows.Count > 0)
                            {
                                Session.activityLogID = long.Parse(table2.Rows[0]["ACTIVITY_LOG_ID"].ToString());
                            }
                        }
                        catch (Exception EX)
                        {
                        }
                        this.txtPassword.Text = "";
                        this.gbLogin.Visible = false;
                        this.gbLogin.SendToBack();
                        Session.userID = int.Parse(this.ddlUsers.SelectedValue.ToString());
                        Session.isManager = dataTable.Rows[0]["IS_MANAGER"].ToString();
                        Session.isSupervisor = dataTable.Rows[0]["IS_SUPERVISOR"].ToString();
                        this.scanToolStripMenuItem.Enabled = true;
                        this.palletsToolStripMenuItem.Enabled = true;
                        this.dataEntryToolStripMenuItem.Enabled = true;
                        this.button1.Enabled = true;
                        this.button2.Enabled = true;
                        this.lblLoginUser.Text = "Logon User ID: " + Session.userID.ToString();
                        if (Session.isManager == "Yes")
                        {
                            this.importToolStripMenuItem.Enabled = true;
                            this.settingsToolStripMenuItem.Enabled = true;
                            this.manageVendorsToolStripMenuItem.Enabled = true;
                            this.manageUsersToolStripMenuItem.Enabled = true;
                            this.palletsToolStripMenuItem.Enabled = true;
                            this.button3.Enabled = true;
                            this.button4.Enabled = true;
                            this.button8.Enabled = true;
                            this.button9.Enabled = true;
                        }
                        else
                        {
                            this.importToolStripMenuItem.Enabled = false;
                            this.settingsToolStripMenuItem.Enabled = false;
                            this.manageVendorsToolStripMenuItem.Enabled = false;
                            this.manageUsersToolStripMenuItem.Enabled = false;
                            this.palletsToolStripMenuItem.Enabled = false;
                            this.button3.Enabled = false;
                            this.button4.Enabled = false;
                            this.button8.Enabled = false;
                            this.button9.Enabled = false;
                        }
                        this.BringToFrontAll();
                    }
                    else
                    {
                        MessageBox.Show("Login Failed", "Login");
                    }
                }
                else
                {
                    MessageBox.Show("Please input all fields.", "Login");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Login");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.UpdateActivityLog(this.logoutReason);
                this.scanToolStripMenuItem.Enabled = false;
                this.palletsToolStripMenuItem.Enabled = false;
                this.dataEntryToolStripMenuItem.Enabled = false;
                this.importToolStripMenuItem.Enabled = false;
                this.settingsToolStripMenuItem.Enabled = false;
                this.manageVendorsToolStripMenuItem.Enabled = false;
                this.manageUsersToolStripMenuItem.Enabled = false;
                Session.userID = 0;
                this.lblLoginUser.Text = "";
                this.SendToBackAll();
                this.gbLogin.Visible = true;
                this.gbLogin.BringToFront();
                try
                {
                    string query = "SELECT USER_ID, USER_FULL_NAME FROM StockUser WHERE USER_STATUS = 'Active' ORDER BY USER_FULL_NAME";
                    DataTable dataTable = DBAccess.GetDataTable(query, DBAccess.msAccessCon);
                    this.ddlUsers.DisplayMember = "USER_FULL_NAME";
                    this.ddlUsers.ValueMember = "USER_ID";
                    this.ddlUsers.DataSource = dataTable;
                    this.gbLogin.Visible = true;
                    this.gbLogin.BringToFront();
                }
                catch (Exception)
                {
                }
            }
            catch (Exception)
            {
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.SendToBackAll();
                frmVendors vendors = null;
                foreach (Form form in base.MdiChildren)
                {
                    if (form is frmVendors)
                    {
                        vendors = (frmVendors)form;
                        break;
                    }
                }
                if (vendors != null)
                {
                    vendors.Close();
                    vendors = new frmVendors
                    {
                        MdiParent = this,
                        WindowState = FormWindowState.Maximized
                    };
                    vendors.Show();
                    vendors.Focus();
                }
                else
                {
                    vendors = new frmVendors
                    {
                        MdiParent = this
                    };
                    vendors.Show();
                    vendors.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.SendToBackAll();
                frmUsers users = null;
                foreach (Form form in base.MdiChildren)
                {
                    if (form is frmUsers)
                    {
                        users = (frmUsers)form;
                        break;
                    }
                }
                if (users != null)
                {
                    users.Close();
                    users = new frmUsers
                    {
                        MdiParent = this,
                        WindowState = FormWindowState.Maximized
                    };
                    users.Show();
                    users.Focus();
                }
                else
                {
                    users = new frmUsers
                    {
                        MdiParent = this
                    };
                    users.Show();
                    users.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void dataEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.SendToBackAll();
                frmDataEntry entry = null;
                foreach (Form form in base.MdiChildren)
                {
                    if (form is frmDataEntry)
                    {
                        entry = (frmDataEntry)form;
                        break;
                    }
                }
                if (entry != null)
                {
                    entry.Close();
                    entry = new frmDataEntry
                    {
                        MdiParent = this,
                        WindowState = FormWindowState.Maximized
                    };
                    entry.Show();
                    entry.Focus();
                }
                else
                {
                    entry = new frmDataEntry
                    {
                        MdiParent = this,
                         WindowState = FormWindowState.Maximized
                    };
                    entry.BringToFront();
                    entry.Show();
                    entry.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

       
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session.userID != 0)
                {
                    this.UpdateActivityLog(this.logoutReason);
                }
                base.Close();
            }
            catch (Exception)
            {
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Session.dtLoginTime = DateTime.Now;
            try
            {
                this.SendToBackAll();
                ImportForm form = null;
                foreach (Form form2 in base.MdiChildren)
                {
                    if (form2 is ImportForm)
                    {
                        form = (ImportForm)form2;
                        break;
                    }
                }
                if (form != null)
                {
                    form.Close();
                    form = new ImportForm
                    {
                        MdiParent = this,
                        WindowState = FormWindowState.Maximized
                    };
                    form.Show();
                    form.Focus();
                }
                else
                {
                    form = new ImportForm
                    {
                        MdiParent = this
                    };
                    form.Show();
                    form.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

    }
}
