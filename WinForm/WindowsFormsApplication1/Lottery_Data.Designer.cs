namespace WindowsFormsApplication1
{
    partial class Lottery_Data
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
            if(disposing && (components != null))
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
            this.dgv_lottery = new System.Windows.Forms.DataGridView();
            this.btn_get = new System.Windows.Forms.Button();
            this.btn_export = new System.Windows.Forms.Button();
            this.btn_excel = new System.Windows.Forms.Button();
            this.chk_date = new System.Windows.Forms.CheckBox();
            this.chk_red1 = new System.Windows.Forms.CheckBox();
            this.chk_red2 = new System.Windows.Forms.CheckBox();
            this.chk_red3 = new System.Windows.Forms.CheckBox();
            this.chk_red4 = new System.Windows.Forms.CheckBox();
            this.chk_red5 = new System.Windows.Forms.CheckBox();
            this.chk_red6 = new System.Windows.Forms.CheckBox();
            this.chk_blue = new System.Windows.Forms.CheckBox();
            this.com_lot = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_lottery)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_lottery
            // 
            this.dgv_lottery.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_lottery.Location = new System.Drawing.Point(12, 71);
            this.dgv_lottery.Name = "dgv_lottery";
            this.dgv_lottery.RowTemplate.Height = 23;
            this.dgv_lottery.Size = new System.Drawing.Size(704, 344);
            this.dgv_lottery.TabIndex = 11;
            // 
            // btn_get
            // 
            this.btn_get.Location = new System.Drawing.Point(117, 20);
            this.btn_get.Name = "btn_get";
            this.btn_get.Size = new System.Drawing.Size(75, 23);
            this.btn_get.TabIndex = 1;
            this.btn_get.Text = "开始获取";
            this.btn_get.UseVisualStyleBackColor = true;
            this.btn_get.Click += new System.EventHandler(this.btn_get_Click);
            // 
            // btn_export
            // 
            this.btn_export.Location = new System.Drawing.Point(512, 18);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(85, 23);
            this.btn_export.TabIndex = 10;
            this.btn_export.Text = "导出选中数据";
            this.btn_export.UseVisualStyleBackColor = true;
            this.btn_export.Click += new System.EventHandler(this.btn_export_Click);
            // 
            // btn_excel
            // 
            this.btn_excel.Location = new System.Drawing.Point(603, 18);
            this.btn_excel.Name = "btn_excel";
            this.btn_excel.Size = new System.Drawing.Size(91, 23);
            this.btn_excel.TabIndex = 12;
            this.btn_excel.Text = "导出到EXCEL";
            this.btn_excel.UseVisualStyleBackColor = true;
            this.btn_excel.Click += new System.EventHandler(this.btn_excel_Click);
            // 
            // chk_date
            // 
            this.chk_date.AutoSize = true;
            this.chk_date.Checked = true;
            this.chk_date.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_date.Location = new System.Drawing.Point(198, 12);
            this.chk_date.Name = "chk_date";
            this.chk_date.Size = new System.Drawing.Size(72, 16);
            this.chk_date.TabIndex = 2;
            this.chk_date.Text = "开奖日期";
            this.chk_date.UseVisualStyleBackColor = true;
            this.chk_date.CheckedChanged += new System.EventHandler(this.chk_date_CheckedChanged);
            // 
            // chk_red1
            // 
            this.chk_red1.AutoSize = true;
            this.chk_red1.Location = new System.Drawing.Point(198, 34);
            this.chk_red1.Name = "chk_red1";
            this.chk_red1.Size = new System.Drawing.Size(42, 16);
            this.chk_red1.TabIndex = 3;
            this.chk_red1.Text = "红1";
            this.chk_red1.UseVisualStyleBackColor = true;
            this.chk_red1.CheckedChanged += new System.EventHandler(this.chk_date_CheckedChanged);
            // 
            // chk_red2
            // 
            this.chk_red2.AutoSize = true;
            this.chk_red2.Location = new System.Drawing.Point(246, 34);
            this.chk_red2.Name = "chk_red2";
            this.chk_red2.Size = new System.Drawing.Size(42, 16);
            this.chk_red2.TabIndex = 4;
            this.chk_red2.Text = "红2";
            this.chk_red2.UseVisualStyleBackColor = true;
            this.chk_red2.CheckedChanged += new System.EventHandler(this.chk_date_CheckedChanged);
            // 
            // chk_red3
            // 
            this.chk_red3.AutoSize = true;
            this.chk_red3.Location = new System.Drawing.Point(294, 34);
            this.chk_red3.Name = "chk_red3";
            this.chk_red3.Size = new System.Drawing.Size(42, 16);
            this.chk_red3.TabIndex = 5;
            this.chk_red3.Text = "红3";
            this.chk_red3.UseVisualStyleBackColor = true;
            this.chk_red3.CheckedChanged += new System.EventHandler(this.chk_date_CheckedChanged);
            // 
            // chk_red4
            // 
            this.chk_red4.AutoSize = true;
            this.chk_red4.Location = new System.Drawing.Point(342, 34);
            this.chk_red4.Name = "chk_red4";
            this.chk_red4.Size = new System.Drawing.Size(42, 16);
            this.chk_red4.TabIndex = 6;
            this.chk_red4.Text = "红4";
            this.chk_red4.UseVisualStyleBackColor = true;
            this.chk_red4.CheckedChanged += new System.EventHandler(this.chk_date_CheckedChanged);
            // 
            // chk_red5
            // 
            this.chk_red5.AutoSize = true;
            this.chk_red5.Location = new System.Drawing.Point(390, 34);
            this.chk_red5.Name = "chk_red5";
            this.chk_red5.Size = new System.Drawing.Size(42, 16);
            this.chk_red5.TabIndex = 7;
            this.chk_red5.Text = "红5";
            this.chk_red5.UseVisualStyleBackColor = true;
            this.chk_red5.CheckedChanged += new System.EventHandler(this.chk_date_CheckedChanged);
            // 
            // chk_red6
            // 
            this.chk_red6.AutoSize = true;
            this.chk_red6.Location = new System.Drawing.Point(438, 34);
            this.chk_red6.Name = "chk_red6";
            this.chk_red6.Size = new System.Drawing.Size(42, 16);
            this.chk_red6.TabIndex = 8;
            this.chk_red6.Text = "红6";
            this.chk_red6.UseVisualStyleBackColor = true;
            this.chk_red6.CheckedChanged += new System.EventHandler(this.chk_date_CheckedChanged);
            // 
            // chk_blue
            // 
            this.chk_blue.AutoSize = true;
            this.chk_blue.Location = new System.Drawing.Point(276, 12);
            this.chk_blue.Name = "chk_blue";
            this.chk_blue.Size = new System.Drawing.Size(48, 16);
            this.chk_blue.TabIndex = 9;
            this.chk_blue.Text = "蓝球";
            this.chk_blue.UseVisualStyleBackColor = true;
            this.chk_blue.CheckedChanged += new System.EventHandler(this.chk_date_CheckedChanged);
            // 
            // com_lot
            // 
            this.com_lot.FormattingEnabled = true;
            this.com_lot.Location = new System.Drawing.Point(12, 20);
            this.com_lot.Name = "com_lot";
            this.com_lot.Size = new System.Drawing.Size(99, 20);
            this.com_lot.TabIndex = 14;
            this.com_lot.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Lottery_Data
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 427);
            this.Controls.Add(this.com_lot);
            this.Controls.Add(this.btn_excel);
            this.Controls.Add(this.btn_export);
            this.Controls.Add(this.chk_blue);
            this.Controls.Add(this.chk_red6);
            this.Controls.Add(this.chk_red5);
            this.Controls.Add(this.chk_red4);
            this.Controls.Add(this.chk_red3);
            this.Controls.Add(this.chk_red2);
            this.Controls.Add(this.chk_red1);
            this.Controls.Add(this.chk_date);
            this.Controls.Add(this.btn_get);
            this.Controls.Add(this.dgv_lottery);
            this.Name = "Lottery_Data";
            this.Text = "Lottery_Data";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Lottery_Data_FormClosing);
            this.Load += new System.EventHandler(this.Lottery_Data_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_lottery)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_lottery;
        private System.Windows.Forms.Button btn_get;
        private System.Windows.Forms.Button btn_export;
        private System.Windows.Forms.Button btn_excel;
        private System.Windows.Forms.CheckBox chk_date;
        private System.Windows.Forms.CheckBox chk_red1;
        private System.Windows.Forms.CheckBox chk_red2;
        private System.Windows.Forms.CheckBox chk_red3;
        private System.Windows.Forms.CheckBox chk_red4;
        private System.Windows.Forms.CheckBox chk_red5;
        private System.Windows.Forms.CheckBox chk_red6;
        private System.Windows.Forms.CheckBox chk_blue;
        private System.Windows.Forms.ComboBox com_lot;
    }
}