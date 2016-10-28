using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DealStore
{
    public partial class loginForm : Form
    {
        private Button btnClearForm;
        private ComboBox ddlUsers;
        private GroupBox groupBox2;
        private Label label2;
        private Label label4;
        private TextBox txtPassword;

        public loginForm()
        {
            InitializeComponent();
        }
        private void btnClearForm_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.ddlUsers.SelectedValue.ToString() != "") && (this.txtPassword.Text.Trim() != ""))
                {
                    if (DBAccess.GetDataTable("SELECT USER_ID FROM StockUser WHERE USER_ID = " + this.ddlUsers.SelectedValue.ToString() + " AND USER_PASSWORD = '" + this.txtPassword.Text.Trim() + "'", DBAccess.msAccessCon).Rows.Count > 0)
                    {
                        try
                        {
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
                                    MdiParent = Form.ActiveForm
                                };
                                form.Show();
                                form.Focus();
                            }
                        }
                        catch (Exception)
                        {
                        }
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
            catch (Exception)
            {
            }
        }
        private void loginForm_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT USER_ID, USER_FULL_NAME FROM StockUser WHERE USER_STATUS = 'Active' ORDER BY USER_FULL_NAME";
                DataTable dataTable = DBAccess.GetDataTable(query, DBAccess.msAccessCon);
                this.ddlUsers.DisplayMember = "USER_FULL_NAME";
                this.ddlUsers.ValueMember = "USER_ID";
                this.ddlUsers.DataSource = dataTable;
            }
            catch (Exception)
            {
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if ((this.ddlUsers.SelectedValue.ToString() != "") && (this.txtPassword.Text.Trim() != ""))
                {
                    if (DBAccess.GetDataTable("SELECT USER_ID FROM StockUser WHERE USER_ID = " + this.ddlUsers.SelectedValue.ToString() + " AND USER_PASSWORD = '" + this.txtPassword.Text.Trim() + "'", DBAccess.msAccessCon).Rows.Count > 0)
                    {
                        try
                        {
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
                                    MdiParent = Form.ActiveForm
                                };
                                form.Show();
                                form.Focus();
                            }
                        }
                        catch (Exception)
                        {
                        }
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
            catch (Exception)
            {
            }
        }
    }
}
