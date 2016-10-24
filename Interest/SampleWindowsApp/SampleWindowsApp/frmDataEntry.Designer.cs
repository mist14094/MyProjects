using System.Windows.Forms;
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
using OfficeOpenXml;



namespace DealStore
{
    partial class frmDataEntry
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
            this.label4 = new Label();
            this.txtPrice = new TextBox();
            this.txtProduct = new TextBox();
            this.label3 = new Label();
            this.label2 = new Label();
            this.txtUPC = new TextBox();
            this.label5 = new Label();
            this.btnClearForm = new Button();
            this.btnSave = new Button();
            this.label1 = new Label();
            this.groupBox3 = new GroupBox();
            this.btnUserVendorSummaryReport = new Button();
            this.dpEndDate = new DateTimePicker();
            this.label8 = new Label();
            this.dpStartDate = new DateTimePicker();
            this.label9 = new Label();
            this.ddlReportVendors = new ComboBox();
            this.btnGenerateReport = new Button();
            this.label6 = new Label();
            this.label11 = new Label();
            this.btnMainMenu = new Button();
            this.cbNoPrice = new CheckBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.groupBox2.BackColor =  SystemColors.ActiveBorder;
            this.groupBox2.Controls.Add(this.ddlVendors);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtPrice);
            this.groupBox2.Controls.Add(this.txtProduct);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtUPC);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btnClearForm);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.groupBox2.Location = new Point(12, 0x31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x28c, 0xbb);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.ddlVendors.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlVendors.FormattingEnabled = true;
            this.ddlVendors.Items.AddRange(new object[] { "Yes", "No" });
            this.ddlVendors.Location = new Point(0x79, 0x68);
            this.ddlVendors.Name = "ddlVendors";
            this.ddlVendors.Size = new Size(0x130, 0x15);
            this.ddlVendors.TabIndex = 12;
            this.label4.AutoSize = true;
            this.label4.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label4.Location = new Point(6, 0x6b);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x39, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Vendor:";
            this.txtPrice.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.txtPrice.Location = new Point(0x79, 0x4d);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new Size(0x76, 0x15);
            this.txtPrice.TabIndex = 2;
            this.txtProduct.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.txtProduct.Location = new Point(0x79, 50);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new Size(0x130, 0x15);
            this.txtProduct.TabIndex = 1;
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label3.Location = new Point(6, 80);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x2c, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Price:";
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label2.Location = new Point(6, 0x35);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x3d, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Product:";
            this.txtUPC.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.txtUPC.Location = new Point(0x79, 0x17);
            this.txtUPC.Name = "txtUPC";
            this.txtUPC.Size = new Size(0x130, 0x15);
            this.txtUPC.TabIndex = 0;
            this.label5.AutoSize = true;
            this.label5.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label5.Location = new Point(6, 0x1a);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x24, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "UPC:";
            this.btnClearForm.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnClearForm.Location = new Point(0xf7, 0x8f);
            this.btnClearForm.Name = "btnClearForm";
            this.btnClearForm.Size = new Size(0x81, 0x17);
            this.btnClearForm.TabIndex = 5;
            this.btnClearForm.Text = "Clear Form";
            this.btnClearForm.UseVisualStyleBackColor = true;
            this.btnClearForm.Click += new EventHandler(this.btnClearForm_Click);
            this.btnSave.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnSave.Location = new Point(0x79, 0x8f);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(120, 0x17);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Verdana", 12f, FontStyle.Underline, GraphicsUnit.Point, 0);
            this.label1.ForeColor = SystemColors.HighlightText;
            this.label1.Location = new Point(0x128, 0x13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x70, 0x12);
            this.label1.TabIndex = 8;
            this.label1.Text = "DATA ENTRY";
            this.groupBox3.BackColor = SystemColors.ActiveBorder;
            this.groupBox3.Controls.Add(this.cbNoPrice);
            this.groupBox3.Controls.Add(this.btnUserVendorSummaryReport);
            this.groupBox3.Controls.Add(this.dpEndDate);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.dpStartDate);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.ddlReportVendors);
            this.groupBox3.Controls.Add(this.btnGenerateReport);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.groupBox3.Location = new Point(12, 0x13e);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x28c, 160);
            this.groupBox3.TabIndex = 0x11;
            this.groupBox3.TabStop = false;
            this.btnUserVendorSummaryReport.Font = new Font("Verdana", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnUserVendorSummaryReport.Location = new Point(0x112, 0x70);
            this.btnUserVendorSummaryReport.Name = "btnUserVendorSummaryReport";
            this.btnUserVendorSummaryReport.Size = new Size(0x148, 0x21);
            this.btnUserVendorSummaryReport.TabIndex = 0x12;
            this.btnUserVendorSummaryReport.Text = "User && Vendor Summary Report";
            this.btnUserVendorSummaryReport.UseVisualStyleBackColor = true;
            this.btnUserVendorSummaryReport.Click += new EventHandler(this.btnUserVendorSummaryReport_Click);
            this.dpEndDate.Format = DateTimePickerFormat.Short;
            this.dpEndDate.Location = new Point(0x192, 0x33);
            this.dpEndDate.Name = "dpEndDate";
            this.dpEndDate.ShowCheckBox = true;
            this.dpEndDate.Size = new Size(200, 20);
            this.dpEndDate.TabIndex = 15;
            this.label8.AutoSize = true;
            this.label8.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label8.Location = new Point(0x147, 0x39);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x45, 13);
            this.label8.TabIndex = 0x11;
            this.label8.Text = "End Date:";
            this.dpStartDate.Format = DateTimePickerFormat.Short;
            this.dpStartDate.Location = new Point(0x79, 0x33);
            this.dpStartDate.Name = "dpStartDate";
            this.dpStartDate.ShowCheckBox = true;
            this.dpStartDate.Size = new Size(200, 20);
            this.dpStartDate.TabIndex = 14;
            this.label9.AutoSize = true;
            this.label9.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label9.Location = new Point(9, 0x39);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x4d, 13);
            this.label9.TabIndex = 0x10;
            this.label9.Text = "Start Date:";
            this.ddlReportVendors.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ddlReportVendors.FormattingEnabled = true;
            this.ddlReportVendors.Items.AddRange(new object[] { "Yes", "No" });
            this.ddlReportVendors.Location = new Point(0x79, 0x18);
            this.ddlReportVendors.Name = "ddlReportVendors";
            this.ddlReportVendors.Size = new Size(0x1e1, 0x15);
            this.ddlReportVendors.TabIndex = 13;
            this.btnGenerateReport.Font = new Font("Verdana", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnGenerateReport.Location = new Point(0x79, 0x70);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new Size(0x93, 0x21);
            this.btnGenerateReport.TabIndex = 1;
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.UseVisualStyleBackColor = true;
            this.btnGenerateReport.Click += new EventHandler(this.btnGenerateReport_Click);
            this.label6.AutoSize = true;
            this.label6.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label6.Location = new Point(9, 0x1b);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x39, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Vendor:";
            this.label11.AutoSize = true;
            this.label11.Font = new Font("Verdana", 12f, FontStyle.Underline, GraphicsUnit.Point, 0);
            this.label11.ForeColor = SystemColors.HighlightText;
            this.label11.Location = new Point(0x13b, 0x11f);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x49, 0x12);
            this.label11.TabIndex = 0x10;
            this.label11.Text = "REPORT";
            this.btnMainMenu.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnMainMenu.Location = new Point(0x217, 0x1ff);
            this.btnMainMenu.Name = "btnMainMenu";
            this.btnMainMenu.Size = new Size(0x81, 0x17);
            this.btnMainMenu.TabIndex = 0x12;
            this.btnMainMenu.Text = "Main Menu";
            this.btnMainMenu.UseVisualStyleBackColor = true;
            this.btnMainMenu.Click += new EventHandler(this.btnMainMenu_Click);
            this.cbNoPrice.AutoSize = true;
            this.cbNoPrice.Location = new Point(0x79, 0x59);
            this.cbNoPrice.Name = "cbNoPrice";
            this.cbNoPrice.Size = new Size(0xa5, 0x11);
            this.cbNoPrice.TabIndex = 0x13;
            this.cbNoPrice.Text = "Only Items with No Price";
            this.cbNoPrice.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.DarkBlue;
            base.ClientSize = new Size(0x30b, 0x222);
            base.ControlBox = false;
            base.Controls.Add(this.btnMainMenu);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.label11);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.label1);
            base.MaximizeBox = false;
            this.MaximumSize = new Size(0x31b, 580);
            this.MinimumSize = new Size(0x31b, 580);
            base.Name = "frmDataEntry";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Data Entry";
            base.WindowState = FormWindowState.Maximized;
            base.Load += new EventHandler(this.frmDataEntry_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();

            

        }

        #endregion
    }
}