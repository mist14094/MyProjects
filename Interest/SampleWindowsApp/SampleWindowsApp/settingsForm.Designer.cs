using System;
using System.Drawing;
using System.Windows.Forms;

namespace DealStore
{
    partial class settingsForm
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

            this.groupBox2 = new GroupBox();
            this.ddlVendors = new ComboBox();
            this.ddlRoundPrice = new ComboBox();
            this.label4 = new Label();
            this.label3 = new Label();
            this.txtScanPrice = new TextBox();
            this.btnClearForm = new Button();
            this.label2 = new Label();
            this.btnSaveSettings = new Button();
            this.label5 = new Label();
            this.label1 = new Label();
            this.label6 = new Label();
            this.groupBox1 = new GroupBox();
            this.button1 = new Button();
            this.txtNetworkPath = new TextBox();
            this.btnClearNetworkForm = new Button();
            this.btnSaveNetworkPath = new Button();
            this.label10 = new Label();
            this.btnMainMenu = new Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox2.BackColor =  SystemColors.ActiveBorder;
            this.groupBox2.Controls.Add(this.txtScanPrice);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.ddlVendors);
            this.groupBox2.Controls.Add(this.ddlRoundPrice);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnClearForm);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnSaveSettings);
            
            this.groupBox2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.groupBox2.Location = new Point(12, 0x2e);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(760, 0xa1);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.ddlVendors.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlVendors.FormattingEnabled = true;
            this.ddlVendors.Items.AddRange(new object[] { "Yes", "No" });
            this.ddlVendors.Location = new Point(0x8a, 0x36);
            this.ddlVendors.Name = "ddlVendors";
            this.ddlVendors.Size = new Size(0x111, 0x15);
            this.ddlVendors.TabIndex = 0x1f;
            this.ddlRoundPrice.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlRoundPrice.FormattingEnabled = true;
            this.ddlRoundPrice.Items.AddRange(new object[] { "Yes", "No" });
            this.ddlRoundPrice.Location = new Point(0x8a, 0x51);
            this.ddlRoundPrice.Name = "ddlRoundPrice";
            this.ddlRoundPrice.Size = new Size(0x111, 0x15);
            this.ddlRoundPrice.TabIndex = 2;
            this.label4.AutoSize = true;
            this.label4.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label4.Location = new Point(7, 0x54);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x7e, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Round Scan Price?";
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label3.Location = new Point(0x1a1, 30);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x39, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "(1-100)";
            this.label3.Visible = false;
            this.txtScanPrice.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.txtScanPrice.Location = new Point(0x8a, 0x1b);
            this.txtScanPrice.Name = "txtScanPrice";
            this.txtScanPrice.Size = new Size(0x111, 0x15);
            this.txtScanPrice.TabIndex = 0;
            this.txtScanPrice.Visible = false;
            this.txtScanPrice.KeyPress += new KeyPressEventHandler(this.textBox1_KeyPress);
            this.btnClearForm.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnClearForm.Location = new Point(0x11a, 0x6c);
            this.btnClearForm.Name = "btnClearForm";
            this.btnClearForm.Size = new Size(0x81, 0x17);
            this.btnClearForm.TabIndex = 4;
            this.btnClearForm.Text = "Clear Form";
            this.btnClearForm.UseVisualStyleBackColor = true;
            this.btnClearForm.Click += new EventHandler(this.btnClearForm_Click);
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label2.Location = new Point(7, 0x39);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Vendor Name:";
            this.btnSaveSettings.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnSaveSettings.Location = new Point(0x9c, 0x6c);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new Size(120, 0x17);
            this.btnSaveSettings.TabIndex = 3;
            this.btnSaveSettings.Text = "Save Settings";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new EventHandler(this.btnSaveSettings_Click);
            this.label5.AutoSize = true;
            this.label5.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label5.Location = new Point(7, 30);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x6d, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Scan Price (%):";
            this.label5.Visible = false;
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Verdana", 12f, FontStyle.Underline, GraphicsUnit.Point, 0);
            this.label1.ForeColor = SystemColors.HighlightText;
            this.label1.Location = new Point(0x14c, 15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x58, 0x12);
            this.label1.TabIndex = 6;
            this.label1.Text = "SETTINGS";
            this.label6.AutoSize = true;
            this.label6.Font = new Font("Verdana", 12f, FontStyle.Underline, GraphicsUnit.Point, 0);
            this.label6.ForeColor = SystemColors.HighlightText;
            this.label6.Location = new Point(300, 0x108);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0xb2, 0x12);
            this.label6.TabIndex = 8;
            this.label6.Text = "NETWORK SETTINGS";
            this.groupBox1.BackColor = SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtNetworkPath);
            this.groupBox1.Controls.Add(this.btnClearNetworkForm);
            this.groupBox1.Controls.Add(this.btnSaveNetworkPath);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.groupBox1.Location = new Point(12, 0x12b);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(760, 0x71);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.button1.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.button1.Location = new Point(480, 0x1b);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x63, 0x17);
            this.button1.TabIndex = 5;
            this.button1.Text = "Test Path";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.txtNetworkPath.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.txtNetworkPath.Location = new Point(0x8a, 0x1b);
            this.txtNetworkPath.Name = "txtNetworkPath";
            this.txtNetworkPath.Size = new Size(0x150, 0x15);
            this.txtNetworkPath.TabIndex = 0;
            this.btnClearNetworkForm.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnClearNetworkForm.Location = new Point(0x159, 0x36);
            this.btnClearNetworkForm.Name = "btnClearNetworkForm";
            this.btnClearNetworkForm.Size = new Size(0x81, 0x17);
            this.btnClearNetworkForm.TabIndex = 4;
            this.btnClearNetworkForm.Text = "Clear Form";
            this.btnClearNetworkForm.UseVisualStyleBackColor = true;
            this.btnClearNetworkForm.Click += new EventHandler(this.btnClearNetworkForm_Click);
            this.btnSaveNetworkPath.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnSaveNetworkPath.Location = new Point(220, 0x36);
            this.btnSaveNetworkPath.Name = "btnSaveNetworkPath";
            this.btnSaveNetworkPath.Size = new Size(0x77, 0x17);
            this.btnSaveNetworkPath.TabIndex = 3;
            this.btnSaveNetworkPath.Text = "Save Settings";
            this.btnSaveNetworkPath.UseVisualStyleBackColor = true;
            this.btnSaveNetworkPath.Click += new EventHandler(this.btnSaveNetworkPath_Click);
            this.label10.AutoSize = true;
            this.label10.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label10.Location = new Point(7, 30);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x62, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Network Path:";
            this.btnMainMenu.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnMainMenu.Location = new Point(0x283, 0x1ff);
            this.btnMainMenu.Name = "btnMainMenu";
            this.btnMainMenu.Size = new Size(0x81, 0x17);
            this.btnMainMenu.TabIndex = 0x13;
            this.btnMainMenu.Text = "Main Menu";
            this.btnMainMenu.UseVisualStyleBackColor = true;
            this.btnMainMenu.Click += new EventHandler(this.btnMainMenu_Click);
            base.AutoScaleDimensions = new SizeF(7f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.DarkBlue;
            base.ClientSize = new Size(0x30b, 0x21d);
            base.ControlBox = false;
            base.Controls.Add(this.btnMainMenu);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.label1);
            this.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.MaximizeBox = false;
            this.MaximumSize = new Size(0x31b, 580);
            this.MinimumSize = new Size(0x31b, 580);
            base.Name = "settingsForm";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Settings";
            base.WindowState = FormWindowState.Maximized;
            base.Load += new EventHandler(this.settingsForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}