using System;
using System.Drawing;
using System.Windows.Forms;

namespace DealStore
{
    partial class loginForm
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
            this.Text = "loginForm";

            this.groupBox2 = new GroupBox();
            this.ddlUsers = new ComboBox();
            this.label4 = new Label();
            this.btnClearForm = new Button();
            this.txtPassword = new TextBox();
            this.label2 = new Label();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox2.BackColor = SystemColors.ActiveBorder;
            this.groupBox2.Controls.Add(this.ddlUsers);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnClearForm);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.groupBox2.Location = new Point(0xb1, 0xd6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x1a9, 0x76);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.ddlUsers.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlUsers.FormattingEnabled = true;
            this.ddlUsers.Items.AddRange(new object[] { "Yes", "No" });
            this.ddlUsers.Location = new Point(0x8a, 0x1b);
            this.ddlUsers.Name = "ddlUsers";
            this.ddlUsers.Size = new Size(0x111, 0x15);
            this.ddlUsers.TabIndex = 2;
            this.label4.AutoSize = true;
            this.label4.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label4.Location = new Point(7, 30);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x4e, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "User Name";
            this.btnClearForm.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnClearForm.Location = new Point(0x8a, 0x51);
            this.btnClearForm.Name = "btnClearForm";
            this.btnClearForm.Size = new Size(0x81, 0x17);
            this.btnClearForm.TabIndex = 4;
            this.btnClearForm.Text = "Login";
            this.btnClearForm.UseVisualStyleBackColor = true;
            this.btnClearForm.Click += new EventHandler(this.btnClearForm_Click);
            this.txtPassword.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.txtPassword.Location = new Point(0x8a, 0x36);
            this.txtPassword.MaxLength = 20;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new Size(0x111, 0x15);
            this.txtPassword.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label2.Location = new Point(7, 0x39);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x49, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password:";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x30b, 0x222);
            base.ControlBox = false;
            base.Controls.Add(this.groupBox2);
            this.MaximumSize = new Size(0x31b, 580);
            this.MinimumSize = new Size(0x31b, 580);
            base.Name = "loginForm";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Login";
            base.WindowState = FormWindowState.Maximized;
            base.Load += new EventHandler(this.loginForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
        }

        #endregion
    }
}