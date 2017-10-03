namespace BarCodeGenerate
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.barcodeControl2 = new Cobainsoft.Windows.Forms.BarcodeControl();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.tx_code = new ControlExs.QQTextBox();
            this.btn_general = new ControlExs.QQButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // barcodeControl2
            // 
            this.barcodeControl2.AddOnCaption = null;
            this.barcodeControl2.AddOnData = "5";
            this.barcodeControl2.BackColor = System.Drawing.Color.White;
            this.barcodeControl2.BarcodeType = Cobainsoft.Windows.Forms.BarcodeType.EAN13;
            this.barcodeControl2.CopyRight = "";
            this.barcodeControl2.Data = "548956784256";
            this.barcodeControl2.Font = new System.Drawing.Font("Arial", 9F);
            this.barcodeControl2.ForeColor = System.Drawing.Color.Black;
            this.barcodeControl2.InvalidDataAction = Cobainsoft.Windows.Forms.InvalidDataAction.DisplayInvalid;
            this.barcodeControl2.Location = new System.Drawing.Point(25, 284);
            this.barcodeControl2.LowerTopTextBy = 0F;
            this.barcodeControl2.Name = "barcodeControl2";
            this.barcodeControl2.PixelAligned = false;
            this.barcodeControl2.RaiseBottomTextBy = 0F;
            this.barcodeControl2.Size = new System.Drawing.Size(203, 61);
            this.barcodeControl2.TabIndex = 6;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.White;
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Location = new System.Drawing.Point(22, 281);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(284, 68);
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.White;
            this.label32.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(229, 298);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(69, 14);
            this.label32.TabIndex = 9;
            this.label32.Text = "Prod00001";
            this.label32.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.White;
            this.label35.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(229, 321);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(69, 14);
            this.label35.TabIndex = 10;
            this.label35.Text = "Prod00001";
            this.label35.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tx_code
            // 
            this.tx_code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tx_code.EmptyTextTip = null;
            this.tx_code.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            this.tx_code.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tx_code.Location = new System.Drawing.Point(22, 46);
            this.tx_code.Name = "tx_code";
            this.tx_code.Size = new System.Drawing.Size(206, 23);
            this.tx_code.TabIndex = 11;
            // 
            // btn_general
            // 
            this.btn_general.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_general.Location = new System.Drawing.Point(238, 45);
            this.btn_general.Name = "btn_general";
            this.btn_general.Size = new System.Drawing.Size(68, 23);
            this.btn_general.TabIndex = 12;
            this.btn_general.Text = "生成";
            this.btn_general.UseVisualStyleBackColor = true;
            this.btn_general.Click += new System.EventHandler(this.btn_general_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(22, 101);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(284, 165);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(22, 75);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(281, 20);
            this.comboBox1.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 368);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_general);
            this.Controls.Add(this.tx_code);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.barcodeControl2);
            this.Controls.Add(this.pictureBox3);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private Cobainsoft.Windows.Forms.BarcodeControl barcodeControl2;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label35;
        private ControlExs.QQTextBox tx_code;
        private ControlExs.QQButton btn_general;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

