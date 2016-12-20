using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RichTextBoxPrintCtrl;
using System.Data.OleDb;
using System.Drawing.Printing;

namespace DealStore
{
    partial class scanForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gvProducts = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbFillPalletNoPrint = new System.Windows.Forms.CheckBox();
            this.ddlPallets = new System.Windows.Forms.ComboBox();
            this.cbFillPallet = new System.Windows.Forms.CheckBox();
            this.lblLoadID = new System.Windows.Forms.Label();
            this.lblRoundOption = new System.Windows.Forms.Label();
            this.lblProductID = new System.Windows.Forms.Label();
            this.cbNoLoad = new System.Windows.Forms.CheckBox();
            this.ddlVendorLoad = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cbDontPrint = new System.Windows.Forms.CheckBox();
            this.gbNewProduct = new System.Windows.Forms.GroupBox();
            this.cbNewProduct = new System.Windows.Forms.CheckBox();
            this.ddlLabelSize = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ddlSellable = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.richTextBoxPrintCtrl1 = new RichTextBoxPrintCtrl.RichTextBoxPrintCtrl();
            this.ddlVendors = new System.Windows.Forms.ComboBox();
            this.ddlRoundValue = new System.Windows.Forms.ComboBox();
            this.rbAutoClearNo = new System.Windows.Forms.RadioButton();
            this.rbAutoClearYes = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.gbOverrideVendor = new System.Windows.Forms.GroupBox();
            this.txtOverrideVendor = new System.Windows.Forms.TextBox();
            this.cbOverrideVendor = new System.Windows.Forms.CheckBox();
            this.gbOverridePrice = new System.Windows.Forms.GroupBox();
            this.txtOverrideScanPrice = new System.Windows.Forms.TextBox();
            this.cbOverrideScanPrice = new System.Windows.Forms.CheckBox();
            this.gbOverrideDiscPrice = new System.Windows.Forms.GroupBox();
            this.txtCustomPrice = new System.Windows.Forms.TextBox();
            this.cbCustomPrice = new System.Windows.Forms.CheckBox();
            this.txtDiscountPrice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblRoundPrice = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtScanPrice = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Generate = new System.Windows.Forms.Button();
            this.btnSearchDesc = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.btnClearForm = new System.Windows.Forms.Button();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUPC = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.btnMainMenu = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProducts)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbNewProduct.SuspendLayout();
            this.gbOverrideVendor.SuspendLayout();
            this.gbOverridePrice.SuspendLayout();
            this.gbOverrideDiscPrice.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label1.Location = new System.Drawing.Point(335, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "SCAN ITEMS";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox2.Controls.Add(this.gvProducts);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.lblLoadID);
            this.groupBox2.Controls.Add(this.lblRoundOption);
            this.groupBox2.Controls.Add(this.lblProductID);
            this.groupBox2.Controls.Add(this.cbNoLoad);
            this.groupBox2.Controls.Add(this.ddlVendorLoad);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.cbDontPrint);
            this.groupBox2.Controls.Add(this.gbNewProduct);
            this.groupBox2.Controls.Add(this.ddlLabelSize);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.ddlSellable);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.btnLogOut);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtQuantity);
            this.groupBox2.Controls.Add(this.richTextBoxPrintCtrl1);
            this.groupBox2.Controls.Add(this.ddlVendors);
            this.groupBox2.Controls.Add(this.ddlRoundValue);
            this.groupBox2.Controls.Add(this.rbAutoClearNo);
            this.groupBox2.Controls.Add(this.rbAutoClearYes);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.gbOverrideVendor);
            this.groupBox2.Controls.Add(this.gbOverridePrice);
            this.groupBox2.Controls.Add(this.gbOverrideDiscPrice);
            this.groupBox2.Controls.Add(this.txtDiscountPrice);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lblRoundPrice);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtScanPrice);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.Generate);
            this.groupBox2.Controls.Add(this.btnSearchDesc);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.btnClearForm);
            this.groupBox2.Controls.Add(this.txtPrice);
            this.groupBox2.Controls.Add(this.txtProduct);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtUPC);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 46);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(760, 459);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // gvProducts
            // 
            this.gvProducts.AllowUserToAddRows = false;
            this.gvProducts.AllowUserToDeleteRows = false;
            this.gvProducts.AllowUserToResizeColumns = false;
            this.gvProducts.AllowUserToResizeRows = false;
            this.gvProducts.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.gvProducts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProducts.Location = new System.Drawing.Point(8, 269);
            this.gvProducts.MultiSelect = false;
            this.gvProducts.Name = "gvProducts";
            this.gvProducts.ReadOnly = true;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvProducts.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvProducts.ShowEditingIcon = false;
            this.gvProducts.Size = new System.Drawing.Size(746, 134);
            this.gvProducts.TabIndex = 11;
            this.gvProducts.Visible = false;
            this.gvProducts.DoubleClick += new System.EventHandler(this.gvProducts_DoubleClick);
            this.gvProducts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvProducts_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.cbFillPalletNoPrint);
            this.groupBox1.Controls.Add(this.ddlPallets);
            this.groupBox1.Controls.Add(this.cbFillPallet);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(537, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 76);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fill Pallet";
            // 
            // cbFillPalletNoPrint
            // 
            this.cbFillPalletNoPrint.AutoSize = true;
            this.cbFillPalletNoPrint.Enabled = false;
            this.cbFillPalletNoPrint.Location = new System.Drawing.Point(88, 20);
            this.cbFillPalletNoPrint.Name = "cbFillPalletNoPrint";
            this.cbFillPalletNoPrint.Size = new System.Drawing.Size(93, 17);
            this.cbFillPalletNoPrint.TabIndex = 32;
            this.cbFillPalletNoPrint.Text = "Do not print";
            this.cbFillPalletNoPrint.UseVisualStyleBackColor = true;
            // 
            // ddlPallets
            // 
            this.ddlPallets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlPallets.Enabled = false;
            this.ddlPallets.FormattingEnabled = true;
            this.ddlPallets.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.ddlPallets.Location = new System.Drawing.Point(6, 43);
            this.ddlPallets.Name = "ddlPallets";
            this.ddlPallets.Size = new System.Drawing.Size(205, 21);
            this.ddlPallets.TabIndex = 31;
            // 
            // cbFillPallet
            // 
            this.cbFillPallet.AutoSize = true;
            this.cbFillPallet.Location = new System.Drawing.Point(7, 20);
            this.cbFillPallet.Name = "cbFillPallet";
            this.cbFillPallet.Size = new System.Drawing.Size(78, 17);
            this.cbFillPallet.TabIndex = 0;
            this.cbFillPallet.Text = "Fill Pallet";
            this.cbFillPallet.UseVisualStyleBackColor = true;
            this.cbFillPallet.CheckedChanged += new System.EventHandler(this.cbFillPallet_CheckedChanged);
            // 
            // lblLoadID
            // 
            this.lblLoadID.AutoSize = true;
            this.lblLoadID.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoadID.Location = new System.Drawing.Point(457, 164);
            this.lblLoadID.Name = "lblLoadID";
            this.lblLoadID.Size = new System.Drawing.Size(0, 13);
            this.lblLoadID.TabIndex = 45;
            this.lblLoadID.Visible = false;
            // 
            // lblRoundOption
            // 
            this.lblRoundOption.AutoSize = true;
            this.lblRoundOption.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoundOption.Location = new System.Drawing.Point(440, 167);
            this.lblRoundOption.Name = "lblRoundOption";
            this.lblRoundOption.Size = new System.Drawing.Size(0, 13);
            this.lblRoundOption.TabIndex = 44;
            this.lblRoundOption.Visible = false;
            // 
            // lblProductID
            // 
            this.lblProductID.AutoSize = true;
            this.lblProductID.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductID.Location = new System.Drawing.Point(432, 167);
            this.lblProductID.Name = "lblProductID";
            this.lblProductID.Size = new System.Drawing.Size(0, 13);
            this.lblProductID.TabIndex = 43;
            this.lblProductID.Visible = false;
            // 
            // cbNoLoad
            // 
            this.cbNoLoad.AutoSize = true;
            this.cbNoLoad.Checked = true;
            this.cbNoLoad.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNoLoad.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNoLoad.Location = new System.Drawing.Point(377, 83);
            this.cbNoLoad.Name = "cbNoLoad";
            this.cbNoLoad.Size = new System.Drawing.Size(154, 21);
            this.cbNoLoad.TabIndex = 42;
            this.cbNoLoad.Text = "Do not use Load";
            this.cbNoLoad.UseVisualStyleBackColor = true;
            // 
            // ddlVendorLoad
            // 
            this.ddlVendorLoad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlVendorLoad.FormattingEnabled = true;
            this.ddlVendorLoad.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.ddlVendorLoad.Location = new System.Drawing.Point(122, 83);
            this.ddlVendorLoad.Name = "ddlVendorLoad";
            this.ddlVendorLoad.Size = new System.Drawing.Size(249, 21);
            this.ddlVendorLoad.TabIndex = 41;
            this.ddlVendorLoad.SelectedIndexChanged += new System.EventHandler(this.ddlVendorLoad_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(7, 86);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 13);
            this.label13.TabIndex = 40;
            this.label13.Text = "Load:";
            // 
            // cbDontPrint
            // 
            this.cbDontPrint.AutoSize = true;
            this.cbDontPrint.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDontPrint.Location = new System.Drawing.Point(122, 219);
            this.cbDontPrint.Name = "cbDontPrint";
            this.cbDontPrint.Size = new System.Drawing.Size(121, 21);
            this.cbDontPrint.TabIndex = 39;
            this.cbDontPrint.Text = "Do not print";
            this.cbDontPrint.UseVisualStyleBackColor = true;
            // 
            // gbNewProduct
            // 
            this.gbNewProduct.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.gbNewProduct.Controls.Add(this.cbNewProduct);
            this.gbNewProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbNewProduct.Location = new System.Drawing.Point(537, 113);
            this.gbNewProduct.Name = "gbNewProduct";
            this.gbNewProduct.Size = new System.Drawing.Size(217, 48);
            this.gbNewProduct.TabIndex = 24;
            this.gbNewProduct.TabStop = false;
            this.gbNewProduct.Text = "New Product";
            // 
            // cbNewProduct
            // 
            this.cbNewProduct.AutoSize = true;
            this.cbNewProduct.Location = new System.Drawing.Point(7, 19);
            this.cbNewProduct.Name = "cbNewProduct";
            this.cbNewProduct.Size = new System.Drawing.Size(121, 17);
            this.cbNewProduct.TabIndex = 38;
            this.cbNewProduct.Text = "Add to Database";
            this.cbNewProduct.UseVisualStyleBackColor = true;
            this.cbNewProduct.CheckedChanged += new System.EventHandler(this.cbNewProduct_CheckedChanged);
            // 
            // ddlLabelSize
            // 
            this.ddlLabelSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlLabelSize.FormattingEnabled = true;
            this.ddlLabelSize.Items.AddRange(new object[] {
            "3 x 2",
            "3 x 1",
            "2.5 x 1.5"});
            this.ddlLabelSize.Location = new System.Drawing.Point(346, 192);
            this.ddlLabelSize.Name = "ddlLabelSize";
            this.ddlLabelSize.Size = new System.Drawing.Size(80, 21);
            this.ddlLabelSize.TabIndex = 37;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(247, 200);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 13);
            this.label11.TabIndex = 36;
            this.label11.Text = "Label Size:";
            // 
            // ddlSellable
            // 
            this.ddlSellable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSellable.FormattingEnabled = true;
            this.ddlSellable.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.ddlSellable.Location = new System.Drawing.Point(346, 164);
            this.ddlSellable.Name = "ddlSellable";
            this.ddlSellable.Size = new System.Drawing.Size(80, 21);
            this.ddlSellable.TabIndex = 35;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(247, 168);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 17);
            this.label10.TabIndex = 34;
            this.label10.Text = "Is Sellable?";
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackColor = System.Drawing.Color.DarkBlue;
            this.btnLogOut.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogOut.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnLogOut.Location = new System.Drawing.Point(625, 413);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(129, 36);
            this.btnLogOut.TabIndex = 33;
            this.btnLogOut.Text = "LOG OUT";
            this.btnLogOut.UseVisualStyleBackColor = false;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(265, 358);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "Quantity:";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.Location = new System.Drawing.Point(337, 355);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(156, 21);
            this.txtQuantity.TabIndex = 32;
            this.txtQuantity.Text = "1";
            this.txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantity_KeyPress);
            // 
            // richTextBoxPrintCtrl1
            // 
            this.richTextBoxPrintCtrl1.Location = new System.Drawing.Point(738, 429);
            this.richTextBoxPrintCtrl1.Name = "richTextBoxPrintCtrl1";
            this.richTextBoxPrintCtrl1.Size = new System.Drawing.Size(17, 20);
            this.richTextBoxPrintCtrl1.TabIndex = 31;
            this.richTextBoxPrintCtrl1.Text = "";
            this.richTextBoxPrintCtrl1.Visible = false;
            // 
            // ddlVendors
            // 
            this.ddlVendors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlVendors.FormattingEnabled = true;
            this.ddlVendors.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.ddlVendors.Location = new System.Drawing.Point(122, 56);
            this.ddlVendors.Name = "ddlVendors";
            this.ddlVendors.Size = new System.Drawing.Size(118, 21);
            this.ddlVendors.TabIndex = 30;
            this.ddlVendors.SelectedIndexChanged += new System.EventHandler(this.ddlVendors_SelectedIndexChanged);
            // 
            // ddlRoundValue
            // 
            this.ddlRoundValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlRoundValue.FormattingEnabled = true;
            this.ddlRoundValue.Items.AddRange(new object[] {
            "0.50",
            "0.25",
            "0.10",
            "0.05",
            "AUTO"});
            this.ddlRoundValue.Location = new System.Drawing.Point(443, 29);
            this.ddlRoundValue.Name = "ddlRoundValue";
            this.ddlRoundValue.Size = new System.Drawing.Size(58, 21);
            this.ddlRoundValue.TabIndex = 29;
            // 
            // rbAutoClearNo
            // 
            this.rbAutoClearNo.AutoSize = true;
            this.rbAutoClearNo.Location = new System.Drawing.Point(460, 54);
            this.rbAutoClearNo.Name = "rbAutoClearNo";
            this.rbAutoClearNo.Size = new System.Drawing.Size(41, 17);
            this.rbAutoClearNo.TabIndex = 28;
            this.rbAutoClearNo.TabStop = true;
            this.rbAutoClearNo.Text = "No";
            this.rbAutoClearNo.UseVisualStyleBackColor = true;
            // 
            // rbAutoClearYes
            // 
            this.rbAutoClearYes.AutoSize = true;
            this.rbAutoClearYes.Location = new System.Drawing.Point(408, 54);
            this.rbAutoClearYes.Name = "rbAutoClearYes";
            this.rbAutoClearYes.Size = new System.Drawing.Size(46, 17);
            this.rbAutoClearYes.TabIndex = 27;
            this.rbAutoClearYes.TabStop = true;
            this.rbAutoClearYes.Text = "Yes";
            this.rbAutoClearYes.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(265, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Clear after Print?";
            // 
            // gbOverrideVendor
            // 
            this.gbOverrideVendor.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.gbOverrideVendor.Controls.Add(this.txtOverrideVendor);
            this.gbOverrideVendor.Controls.Add(this.cbOverrideVendor);
            this.gbOverrideVendor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbOverrideVendor.Location = new System.Drawing.Point(537, 333);
            this.gbOverrideVendor.Name = "gbOverrideVendor";
            this.gbOverrideVendor.Size = new System.Drawing.Size(217, 76);
            this.gbOverrideVendor.TabIndex = 22;
            this.gbOverrideVendor.TabStop = false;
            this.gbOverrideVendor.Text = "Override Vendor";
            // 
            // txtOverrideVendor
            // 
            this.txtOverrideVendor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOverrideVendor.Location = new System.Drawing.Point(7, 43);
            this.txtOverrideVendor.Name = "txtOverrideVendor";
            this.txtOverrideVendor.ReadOnly = true;
            this.txtOverrideVendor.Size = new System.Drawing.Size(126, 21);
            this.txtOverrideVendor.TabIndex = 5;
            // 
            // cbOverrideVendor
            // 
            this.cbOverrideVendor.AutoSize = true;
            this.cbOverrideVendor.Location = new System.Drawing.Point(7, 20);
            this.cbOverrideVendor.Name = "cbOverrideVendor";
            this.cbOverrideVendor.Size = new System.Drawing.Size(144, 17);
            this.cbOverrideVendor.TabIndex = 0;
            this.cbOverrideVendor.Text = "Use Override Vendor";
            this.cbOverrideVendor.UseVisualStyleBackColor = true;
            this.cbOverrideVendor.CheckedChanged += new System.EventHandler(this.cbOverrideVendor_CheckedChanged);
            // 
            // gbOverridePrice
            // 
            this.gbOverridePrice.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.gbOverridePrice.Controls.Add(this.txtOverrideScanPrice);
            this.gbOverridePrice.Controls.Add(this.cbOverrideScanPrice);
            this.gbOverridePrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbOverridePrice.Location = new System.Drawing.Point(537, 167);
            this.gbOverridePrice.Name = "gbOverridePrice";
            this.gbOverridePrice.Size = new System.Drawing.Size(217, 76);
            this.gbOverridePrice.TabIndex = 22;
            this.gbOverridePrice.TabStop = false;
            this.gbOverridePrice.Text = "Override Scan Price (%)";
            // 
            // txtOverrideScanPrice
            // 
            this.txtOverrideScanPrice.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOverrideScanPrice.Location = new System.Drawing.Point(7, 43);
            this.txtOverrideScanPrice.Name = "txtOverrideScanPrice";
            this.txtOverrideScanPrice.ReadOnly = true;
            this.txtOverrideScanPrice.Size = new System.Drawing.Size(126, 21);
            this.txtOverrideScanPrice.TabIndex = 23;
            this.txtOverrideScanPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOverrideScanPrice_KeyPress);
            // 
            // cbOverrideScanPrice
            // 
            this.cbOverrideScanPrice.AutoSize = true;
            this.cbOverrideScanPrice.Location = new System.Drawing.Point(7, 20);
            this.cbOverrideScanPrice.Name = "cbOverrideScanPrice";
            this.cbOverrideScanPrice.Size = new System.Drawing.Size(166, 17);
            this.cbOverrideScanPrice.TabIndex = 0;
            this.cbOverrideScanPrice.Text = "Use Override Scan Price";
            this.cbOverrideScanPrice.UseVisualStyleBackColor = true;
            this.cbOverrideScanPrice.CheckedChanged += new System.EventHandler(this.cbOverrideScanPrice_CheckedChanged);
            // 
            // gbOverrideDiscPrice
            // 
            this.gbOverrideDiscPrice.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.gbOverrideDiscPrice.Controls.Add(this.txtCustomPrice);
            this.gbOverrideDiscPrice.Controls.Add(this.cbCustomPrice);
            this.gbOverrideDiscPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbOverrideDiscPrice.Location = new System.Drawing.Point(537, 250);
            this.gbOverrideDiscPrice.Name = "gbOverrideDiscPrice";
            this.gbOverrideDiscPrice.Size = new System.Drawing.Size(217, 76);
            this.gbOverrideDiscPrice.TabIndex = 21;
            this.gbOverrideDiscPrice.TabStop = false;
            this.gbOverrideDiscPrice.Text = "Override Discount Price";
            // 
            // txtCustomPrice
            // 
            this.txtCustomPrice.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomPrice.Location = new System.Drawing.Point(7, 43);
            this.txtCustomPrice.Name = "txtCustomPrice";
            this.txtCustomPrice.ReadOnly = true;
            this.txtCustomPrice.Size = new System.Drawing.Size(126, 21);
            this.txtCustomPrice.TabIndex = 5;
            // 
            // cbCustomPrice
            // 
            this.cbCustomPrice.AutoSize = true;
            this.cbCustomPrice.Location = new System.Drawing.Point(7, 20);
            this.cbCustomPrice.Name = "cbCustomPrice";
            this.cbCustomPrice.Size = new System.Drawing.Size(187, 17);
            this.cbCustomPrice.TabIndex = 0;
            this.cbCustomPrice.Text = "Use Override Discount Price";
            this.cbCustomPrice.UseVisualStyleBackColor = true;
            this.cbCustomPrice.CheckedChanged += new System.EventHandler(this.cbCustomPrice_CheckedChanged);
            // 
            // txtDiscountPrice
            // 
            this.txtDiscountPrice.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscountPrice.Location = new System.Drawing.Point(122, 192);
            this.txtDiscountPrice.Name = "txtDiscountPrice";
            this.txtDiscountPrice.ReadOnly = true;
            this.txtDiscountPrice.Size = new System.Drawing.Size(118, 21);
            this.txtDiscountPrice.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Discount Price:";
            // 
            // lblRoundPrice
            // 
            this.lblRoundPrice.AutoSize = true;
            this.lblRoundPrice.Font = new System.Drawing.Font("Verdana", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoundPrice.Location = new System.Drawing.Point(405, 31);
            this.lblRoundPrice.Name = "lblRoundPrice";
            this.lblRoundPrice.Size = new System.Drawing.Size(0, 17);
            this.lblRoundPrice.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(265, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Round Scan Price?";
            // 
            // txtScanPrice
            // 
            this.txtScanPrice.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScanPrice.Location = new System.Drawing.Point(122, 29);
            this.txtScanPrice.Name = "txtScanPrice";
            this.txtScanPrice.ReadOnly = true;
            this.txtScanPrice.Size = new System.Drawing.Size(118, 21);
            this.txtScanPrice.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Scan Price (%):";
            // 
            // Generate
            // 
            this.Generate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Generate.Location = new System.Drawing.Point(268, 410);
            this.Generate.Name = "Generate";
            this.Generate.Size = new System.Drawing.Size(225, 42);
            this.Generate.TabIndex = 8;
            this.Generate.Text = "Generate Barcode";
            this.Generate.UseVisualStyleBackColor = true;
            this.Generate.Click += new System.EventHandler(this.Generate_Click);
            // 
            // btnSearchDesc
            // 
            this.btnSearchDesc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchDesc.Location = new System.Drawing.Point(432, 135);
            this.btnSearchDesc.Name = "btnSearchDesc";
            this.btnSearchDesc.Size = new System.Drawing.Size(99, 23);
            this.btnSearchDesc.TabIndex = 3;
            this.btnSearchDesc.Text = "Search";
            this.btnSearchDesc.UseVisualStyleBackColor = true;
            this.btnSearchDesc.Click += new System.EventHandler(this.btnSearchDesc_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(7, 59);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "Vendor:";
            // 
            // btnClearForm
            // 
            this.btnClearForm.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearForm.Location = new System.Drawing.Point(122, 246);
            this.btnClearForm.Name = "btnClearForm";
            this.btnClearForm.Size = new System.Drawing.Size(118, 41);
            this.btnClearForm.TabIndex = 9;
            this.btnClearForm.Text = "Clear Form";
            this.btnClearForm.UseVisualStyleBackColor = true;
            this.btnClearForm.Click += new System.EventHandler(this.btnClearForm_Click);
            // 
            // txtPrice
            // 
            this.txtPrice.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrice.Location = new System.Drawing.Point(122, 164);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(118, 21);
            this.txtPrice.TabIndex = 4;
            // 
            // txtProduct
            // 
            this.txtProduct.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProduct.Location = new System.Drawing.Point(122, 137);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(304, 21);
            this.txtProduct.TabIndex = 2;
            this.txtProduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProduct_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Price:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Product:";
            // 
            // txtUPC
            // 
            this.txtUPC.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUPC.Location = new System.Drawing.Point(122, 110);
            this.txtUPC.Name = "txtUPC";
            this.txtUPC.Size = new System.Drawing.Size(304, 21);
            this.txtUPC.TabIndex = 0;
            this.txtUPC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUPC_KeyDown);
            this.txtUPC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUPC_KeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(432, 108);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(99, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Scan";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "UPC:";
            // 
            // printDocument1
            // 
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // btnMainMenu
            // 
            this.btnMainMenu.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMainMenu.Location = new System.Drawing.Point(643, 511);
            this.btnMainMenu.Name = "btnMainMenu";
            this.btnMainMenu.Size = new System.Drawing.Size(129, 23);
            this.btnMainMenu.TabIndex = 32;
            this.btnMainMenu.Text = "Main Menu";
            this.btnMainMenu.UseVisualStyleBackColor = true;
            this.btnMainMenu.Click += new System.EventHandler(this.btnMainMenu_Click);
            // 
            // scanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkBlue;
            this.ClientSize = new System.Drawing.Size(779, 541);
            this.ControlBox = false;
            this.Controls.Add(this.btnMainMenu);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximumSize = new System.Drawing.Size(795, 580);
            this.MinimumSize = new System.Drawing.Size(795, 580);
            this.Name = "scanForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scan";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.scanForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProducts)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbNewProduct.ResumeLayout(false);
            this.gbNewProduct.PerformLayout();
            this.gbOverrideVendor.ResumeLayout(false);
            this.gbOverrideVendor.PerformLayout();
            this.gbOverridePrice.ResumeLayout(false);
            this.gbOverridePrice.PerformLayout();
            this.gbOverrideDiscPrice.ResumeLayout(false);
            this.gbOverrideDiscPrice.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}