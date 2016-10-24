using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
namespace DealStore
{
    partial class frmUsers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Text = "frmUsers";


            this.groupBox2 = new GroupBox();
            this.cbSupervisor = new ComboBox();
            this.label6 = new Label();
            this.lblName = new Label();
            this.txtUserID = new TextBox();
            this.label4 = new Label();
            this.btnReset = new Button();
            this.ddlManager = new ComboBox();
            this.label3 = new Label();
            this.txtPassword = new TextBox();
            this.label2 = new Label();
            this.btnDelete = new Button();
            this.gvUsers = new DataGridView();
            this.txtName = new TextBox();
            this.btnAdd = new Button();
            this.label5 = new Label();
            this.label1 = new Label();
            this.btnMainMenu = new Button();
            this.ddlStatus = new ComboBox();
            this.label7 = new Label();
            this.groupBox2.SuspendLayout();
            ((ISupportInitialize)this.gvUsers).BeginInit();
            base.SuspendLayout();
            this.groupBox2.BackColor = SystemColors.ActiveBorder;
            this.groupBox2.Controls.Add(this.ddlStatus);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cbSupervisor);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lblName);
            this.groupBox2.Controls.Add(this.txtUserID);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnReset);
            this.groupBox2.Controls.Add(this.ddlManager);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.gvUsers);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.groupBox2.Location = new Point(12, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x28c, 0x1d1);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.cbSupervisor.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbSupervisor.FormattingEnabled = true;
            this.cbSupervisor.Items.AddRange(new object[] { "No", "Yes" });
            this.cbSupervisor.Location = new Point(0x89, 0x86);
            this.cbSupervisor.Name = "cbSupervisor";
            this.cbSupervisor.Size = new Size(0x111, 0x15);
            this.cbSupervisor.TabIndex = 0x12;
            this.label6.AutoSize = true;
            this.label6.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label6.Location = new Point(6, 0x89);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x52, 13);
            this.label6.TabIndex = 0x13;
            this.label6.Text = "Supervisor:";
            this.lblName.AutoSize = true;
            this.lblName.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblName.Location = new Point(0x1a0, 0x38);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(0, 13);
            this.lblName.TabIndex = 0x11;
            this.lblName.Visible = false;
            this.txtUserID.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.txtUserID.Location = new Point(0x89, 0x1a);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.ReadOnly = true;
            this.txtUserID.Size = new Size(0x111, 0x15);
            this.txtUserID.TabIndex = 0;
            this.label4.AutoSize = true;
            this.label4.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label4.Location = new Point(6, 0x1d);
            this.label4.Name = "label4";
            this.label4.Size = new Size(60, 13);
            this.label4.TabIndex = 0x10;
            this.label4.Text = "User ID:";
            this.btnReset.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnReset.Location = new Point(230, 0xbc);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new Size(0x57, 0x17);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new EventHandler(this.btnReset_Click);
            this.ddlManager.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlManager.FormattingEnabled = true;
            this.ddlManager.Items.AddRange(new object[] { "No", "Yes" });
            this.ddlManager.Location = new Point(0x89, 0x6b);
            this.ddlManager.Name = "ddlManager";
            this.ddlManager.Size = new Size(0x111, 0x15);
            this.ddlManager.TabIndex = 3;
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label3.Location = new Point(6, 110);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x43, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Manager:";
            this.txtPassword.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.txtPassword.Location = new Point(0x89, 80);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new Size(0x111, 0x15);
            this.txtPassword.TabIndex = 2;
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label2.Location = new Point(6, 0x53);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x49, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Password:";
            this.btnDelete.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnDelete.Location = new Point(0x22f, 0xbc);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x57, 0x17);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.gvUsers.AllowUserToAddRows = false;
            this.gvUsers.AllowUserToDeleteRows = false;
            this.gvUsers.AllowUserToOrderColumns = true;
            this.gvUsers.AllowUserToResizeColumns = false;
            this.gvUsers.AllowUserToResizeRows = false;
            this.gvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvUsers.Location = new Point(6, 0xd9);
            this.gvUsers.MultiSelect = false;
            this.gvUsers.Name = "gvUsers";
            this.gvUsers.ReadOnly = true;
            this.gvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gvUsers.Size = new Size(640, 0xf2);
            this.gvUsers.TabIndex = 7;
            this.gvUsers.CellClick += new DataGridViewCellEventHandler(this.gvUsers_CellClick);
            this.txtName.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.txtName.Location = new Point(0x89, 0x35);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0x111, 0x15);
            this.txtName.TabIndex = 1;
            this.btnAdd.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnAdd.Location = new Point(0x89, 0xbc);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x57, 0x17);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.label5.AutoSize = true;
            this.label5.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label5.Location = new Point(6, 0x38);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x30, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Name:";
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Verdana", 12f, FontStyle.Underline, GraphicsUnit.Point, 0);
            this.label1.ForeColor = SystemColors.HighlightText;
            this.label1.Location = new Point(0x12a, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3d, 0x12);
            this.label1.TabIndex = 10;
            this.label1.Text = "USERS";
            this.btnMainMenu.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnMainMenu.Location = new Point(0x217, 0x1ff);
            this.btnMainMenu.Name = "btnMainMenu";
            this.btnMainMenu.Size = new Size(0x81, 0x17);
            this.btnMainMenu.TabIndex = 20;
            this.btnMainMenu.Text = "Main Menu";
            this.btnMainMenu.UseVisualStyleBackColor = true;
            this.btnMainMenu.Click += new EventHandler(this.btnMainMenu_Click);
            this.ddlStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlStatus.FormattingEnabled = true;
            this.ddlStatus.Items.AddRange(new object[] { "Active", "Inactive" });
            this.ddlStatus.Location = new Point(0x89, 0xa1);
            this.ddlStatus.Name = "ddlStatus";
            this.ddlStatus.Size = new Size(0x111, 0x15);
            this.ddlStatus.TabIndex = 20;
            this.label7.AutoSize = true;
            this.label7.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label7.Location = new Point(6, 0xa4);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x34, 13);
            this.label7.TabIndex = 0x15;
            this.label7.Text = "Status:";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.DarkBlue;
            base.ClientSize = new Size(0x30b, 0x222);
            base.ControlBox = false;
            base.Controls.Add(this.btnMainMenu);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.label1);
            this.MaximumSize = new Size(0x31b, 580);
            this.MinimumSize = new Size(0x31b, 580);
            base.Name = "frmUsers";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Manage Users";
            base.WindowState = FormWindowState.Maximized;
            base.Load += new EventHandler(this.frmUsers_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((ISupportInitialize)this.gvUsers).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}