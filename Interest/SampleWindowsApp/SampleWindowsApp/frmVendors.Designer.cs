using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DealStore
{
    partial class frmVendors
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
            this.Text = "frmVendors";

            this.label1 = new Label();
            this.label5 = new Label();
            this.btnAdd = new Button();
            this.txtVendorName = new TextBox();
            this.groupBox2 = new GroupBox();
            this.btnReset = new Button();
            this.lblVendorID = new Label();
            this.gbVendorLoads = new GroupBox();
            this.btnClose = new Button();
            this.button1 = new Button();
            this.gvVendorLoads = new DataGridView();
            this.txtVendorLoadName = new TextBox();
            this.button2 = new Button();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.txtVendorDefaultDiscount = new TextBox();
            this.btnAddLoads = new Button();
            this.btnDelete = new Button();
            this.gvVendors = new DataGridView();
            this.btnMainMenu = new Button();
            this.groupBox2.SuspendLayout();
            this.gbVendorLoads.SuspendLayout();
            ((ISupportInitialize)this.gvVendorLoads).BeginInit();
            ((ISupportInitialize)this.gvVendors).BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Verdana", 12f, FontStyle.Underline, GraphicsUnit.Point, 0);
            this.label1.ForeColor = SystemColors.HighlightText;
            this.label1.Location = new Point(0x128, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x57, 0x12);
            this.label1.TabIndex = 8;
            this.label1.Text = "VENDORS";
            this.label5.AutoSize = true;
            this.label5.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label5.Location = new Point(7, 30);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x62, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Vendor Name:";
            this.btnAdd.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnAdd.Location = new Point(0x8a, 0x54);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x57, 0x17);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.txtVendorName.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.txtVendorName.Location = new Point(0x8a, 0x1b);
            this.txtVendorName.Name = "txtVendorName";
            this.txtVendorName.Size = new Size(0x111, 0x15);
            this.txtVendorName.TabIndex = 0;
            this.groupBox2.BackColor = SystemColors.ActiveBorder;
            this.groupBox2.Controls.Add(this.btnReset);
            this.groupBox2.Controls.Add(this.lblVendorID);
            this.groupBox2.Controls.Add(this.gbVendorLoads);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtVendorDefaultDiscount);
            this.groupBox2.Controls.Add(this.btnAddLoads);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.gvVendors);
            this.groupBox2.Controls.Add(this.txtVendorName);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.groupBox2.Location = new Point(10, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x28c, 0x1d1);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.btnReset.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnReset.Location = new Point(0xe7, 0x54);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new Size(0x57, 0x17);
            this.btnReset.TabIndex = 0x18;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new EventHandler(this.btnReset_Click);
            this.lblVendorID.AutoSize = true;
            this.lblVendorID.Location = new Point(0x1a2, 30);
            this.lblVendorID.Name = "lblVendorID";
            this.lblVendorID.Size = new Size(0x29, 13);
            this.lblVendorID.TabIndex = 0x17;
            this.lblVendorID.Text = "label6";
            this.lblVendorID.Visible = false;
            this.gbVendorLoads.BackColor = SystemColors.ActiveBorder;
            this.gbVendorLoads.Controls.Add(this.btnClose);
            this.gbVendorLoads.Controls.Add(this.button1);
            this.gbVendorLoads.Controls.Add(this.gvVendorLoads);
            this.gbVendorLoads.Controls.Add(this.txtVendorLoadName);
            this.gbVendorLoads.Controls.Add(this.button2);
            this.gbVendorLoads.Controls.Add(this.label2);
            this.gbVendorLoads.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.gbVendorLoads.Location = new Point(0, 0);
            this.gbVendorLoads.Name = "gbVendorLoads";
            this.gbVendorLoads.Size = new Size(0x28c, 0x1d1);
            this.gbVendorLoads.TabIndex = 0x16;
            this.gbVendorLoads.TabStop = false;
            this.gbVendorLoads.Visible = false;
            this.btnClose.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnClose.Location = new Point(0x22f, 0x1b4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x57, 0x17);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.button1.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.button1.Location = new Point(0x22f, 0x79);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x57, 0x17);
            this.button1.TabIndex = 2;
            this.button1.Text = "Delete";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.gvVendorLoads.AllowUserToAddRows = false;
            this.gvVendorLoads.AllowUserToDeleteRows = false;
            this.gvVendorLoads.AllowUserToOrderColumns = true;
            this.gvVendorLoads.AllowUserToResizeColumns = false;
            this.gvVendorLoads.AllowUserToResizeRows = false;
            this.gvVendorLoads.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvVendorLoads.Location = new Point(6, 150);
            this.gvVendorLoads.MultiSelect = false;
            this.gvVendorLoads.Name = "gvVendorLoads";
            this.gvVendorLoads.ReadOnly = true;
            this.gvVendorLoads.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gvVendorLoads.Size = new Size(640, 280);
            this.gvVendorLoads.TabIndex = 3;
            this.txtVendorLoadName.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.txtVendorLoadName.Location = new Point(0x8a, 0x1b);
            this.txtVendorLoadName.Name = "txtVendorLoadName";
            this.txtVendorLoadName.Size = new Size(0x111, 0x15);
            this.txtVendorLoadName.TabIndex = 0;
            this.button2.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.button2.Location = new Point(0x8a, 0x36);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x57, 0x17);
            this.button2.TabIndex = 1;
            this.button2.Text = "Add";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label2.Location = new Point(7, 30);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Load Name:";
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label3.Location = new Point(0x1a1, 60);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x39, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "(1-100)";
            this.label4.AutoSize = true;
            this.label4.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label4.Location = new Point(7, 60);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x6d, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Scan Price (%):";
            this.txtVendorDefaultDiscount.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.txtVendorDefaultDiscount.Location = new Point(0x8a, 0x39);
            this.txtVendorDefaultDiscount.Name = "txtVendorDefaultDiscount";
            this.txtVendorDefaultDiscount.Size = new Size(0x111, 0x15);
            this.txtVendorDefaultDiscount.TabIndex = 1;
            this.txtVendorDefaultDiscount.KeyPress += new KeyPressEventHandler(this.txtVendorDefaultDiscount_KeyPress);
            this.btnAddLoads.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnAddLoads.Location = new Point(0x1d2, 0x79);
            this.btnAddLoads.Name = "btnAddLoads";
            this.btnAddLoads.Size = new Size(0x57, 0x17);
            this.btnAddLoads.TabIndex = 4;
            this.btnAddLoads.Text = "Add Loads";
            this.btnAddLoads.UseVisualStyleBackColor = true;
            this.btnAddLoads.Click += new EventHandler(this.btnAddLoads_Click);
            this.btnDelete.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnDelete.Location = new Point(0x22f, 0x79);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x57, 0x17);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.gvVendors.AllowUserToAddRows = false;
            this.gvVendors.AllowUserToDeleteRows = false;
            this.gvVendors.AllowUserToOrderColumns = true;
            this.gvVendors.AllowUserToResizeColumns = false;
            this.gvVendors.AllowUserToResizeRows = false;
            this.gvVendors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvVendors.Location = new Point(6, 150);
            this.gvVendors.MultiSelect = false;
            this.gvVendors.Name = "gvVendors";
            this.gvVendors.ReadOnly = true;
            this.gvVendors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gvVendors.Size = new Size(640, 0x135);
            this.gvVendors.TabIndex = 3;
            this.gvVendors.CellClick += new DataGridViewCellEventHandler(this.gvVendors_CellClick);
            this.btnMainMenu.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnMainMenu.Location = new Point(0x215, 0x1ff);
            this.btnMainMenu.Name = "btnMainMenu";
            this.btnMainMenu.Size = new Size(0x81, 0x17);
            this.btnMainMenu.TabIndex = 0x13;
            this.btnMainMenu.Text = "Main Menu";
            this.btnMainMenu.UseVisualStyleBackColor = true;
            this.btnMainMenu.Click += new EventHandler(this.btnMainMenu_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.DarkBlue;
            base.ClientSize = new Size(0x30b, 0x21d);
            base.ControlBox = false;
            base.Controls.Add(this.btnMainMenu);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.label1);
            this.MaximumSize = new Size(0x31b, 580);
            this.MinimumSize = new Size(0x31b, 580);
            base.Name = "frmVendors";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Manage Vendors";
            base.WindowState = FormWindowState.Maximized;
            base.Load += new EventHandler(this.frmVendors_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbVendorLoads.ResumeLayout(false);
            this.gbVendorLoads.PerformLayout();
            ((ISupportInitialize)this.gvVendorLoads).EndInit();
            ((ISupportInitialize)this.gvVendors).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        
    }

        #endregion
    }
}