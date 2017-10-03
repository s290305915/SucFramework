namespace CodeGeneration
{
    partial class Form2
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbx_tbs = new System.Windows.Forms.ListBox();
            this.lb_tbs = new System.Windows.Forms.Label();
            this.la_db = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toollb_tbname = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolpro_all = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tx_namespc = new System.Windows.Forms.TextBox();
            this.la_curr = new System.Windows.Forms.Label();
            this.dgv_fields = new System.Windows.Forms.DataGridView();
            this.btn_genarel = new System.Windows.Forms.Button();
            this.btn_floder = new System.Windows.Forms.Button();
            this.la_floder = new System.Windows.Forms.Label();
            this.dia_savefloder = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_fields)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.lbx_tbs);
            this.groupBox1.Controls.Add(this.lb_tbs);
            this.groupBox1.Controls.Add(this.la_db);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(153, 412);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库详情";
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(6, 384);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "返回上一步重新选择";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbx_tbs
            // 
            this.lbx_tbs.FormattingEnabled = true;
            this.lbx_tbs.ItemHeight = 12;
            this.lbx_tbs.Location = new System.Drawing.Point(6, 64);
            this.lbx_tbs.Name = "lbx_tbs";
            this.lbx_tbs.Size = new System.Drawing.Size(141, 316);
            this.lbx_tbs.TabIndex = 2;
            this.lbx_tbs.SelectedIndexChanged += new System.EventHandler(this.lbx_tbs_SelectedIndexChanged);
            this.lbx_tbs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbx_tbs_MouseDoubleClick);
            // 
            // lb_tbs
            // 
            this.lb_tbs.AutoSize = true;
            this.lb_tbs.Location = new System.Drawing.Point(6, 24);
            this.lb_tbs.Name = "lb_tbs";
            this.lb_tbs.Size = new System.Drawing.Size(41, 12);
            this.lb_tbs.TabIndex = 2;
            this.lb_tbs.Text = "当前库";
            // 
            // la_db
            // 
            this.la_db.AutoSize = true;
            this.la_db.Location = new System.Drawing.Point(6, 45);
            this.la_db.Name = "la_db";
            this.la_db.Size = new System.Drawing.Size(89, 12);
            this.la_db.TabIndex = 2;
            this.la_db.Text = "当前选择的表：";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toollb_tbname,
            this.toolpro_all});
            this.statusStrip1.Location = new System.Drawing.Point(0, 420);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(733, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toollb_tbname
            // 
            this.toollb_tbname.Name = "toollb_tbname";
            this.toollb_tbname.Size = new System.Drawing.Size(131, 17);
            this.toollb_tbname.Text = "toolStripStatusLabel1";
            // 
            // toolpro_all
            // 
            this.toolpro_all.Name = "toolpro_all";
            this.toolpro_all.Size = new System.Drawing.Size(100, 16);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tx_namespc);
            this.groupBox2.Controls.Add(this.la_curr);
            this.groupBox2.Controls.Add(this.dgv_fields);
            this.groupBox2.Controls.Add(this.btn_genarel);
            this.groupBox2.Controls.Add(this.btn_floder);
            this.groupBox2.Controls.Add(this.la_floder);
            this.groupBox2.Location = new System.Drawing.Point(162, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(559, 412);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "生成配置";
            // 
            // tx_namespc
            // 
            this.tx_namespc.Location = new System.Drawing.Point(131, 59);
            this.tx_namespc.Name = "tx_namespc";
            this.tx_namespc.Size = new System.Drawing.Size(151, 21);
            this.tx_namespc.TabIndex = 5;
            // 
            // la_curr
            // 
            this.la_curr.AutoSize = true;
            this.la_curr.Location = new System.Drawing.Point(252, 98);
            this.la_curr.Name = "la_curr";
            this.la_curr.Size = new System.Drawing.Size(77, 12);
            this.la_curr.TabIndex = 4;
            this.la_curr.Text = "当前正在……";
            // 
            // dgv_fields
            // 
            this.dgv_fields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_fields.Location = new System.Drawing.Point(6, 122);
            this.dgv_fields.Name = "dgv_fields";
            this.dgv_fields.RowTemplate.Height = 23;
            this.dgv_fields.Size = new System.Drawing.Size(547, 284);
            this.dgv_fields.TabIndex = 3;
            // 
            // btn_genarel
            // 
            this.btn_genarel.Location = new System.Drawing.Point(130, 92);
            this.btn_genarel.Name = "btn_genarel";
            this.btn_genarel.Size = new System.Drawing.Size(116, 24);
            this.btn_genarel.TabIndex = 2;
            this.btn_genarel.Text = "开始生成";
            this.btn_genarel.UseVisualStyleBackColor = true;
            this.btn_genarel.Click += new System.EventHandler(this.btn_genarel_Click);
            // 
            // btn_floder
            // 
            this.btn_floder.Location = new System.Drawing.Point(130, 24);
            this.btn_floder.Name = "btn_floder";
            this.btn_floder.Size = new System.Drawing.Size(116, 23);
            this.btn_floder.TabIndex = 1;
            this.btn_floder.Text = "选择生成文件夹：";
            this.btn_floder.UseVisualStyleBackColor = true;
            this.btn_floder.Click += new System.EventHandler(this.btn_floder_Click);
            // 
            // la_floder
            // 
            this.la_floder.AutoSize = true;
            this.la_floder.Location = new System.Drawing.Point(252, 29);
            this.la_floder.Name = "la_floder";
            this.la_floder.Size = new System.Drawing.Size(29, 12);
            this.la_floder.TabIndex = 0;
            this.la_floder.Text = "……";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "② 填写命名空间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "① 选择文件夹：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "③ 开始生成：";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 442);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form2";
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_fields)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label la_db;
        private System.Windows.Forms.ListBox lbx_tbs;
        private System.Windows.Forms.Label lb_tbs;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripStatusLabel toollb_tbname;
        private System.Windows.Forms.ToolStripProgressBar toolpro_all;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_floder;
        private System.Windows.Forms.Label la_floder;
        private System.Windows.Forms.FolderBrowserDialog dia_savefloder;
        private System.Windows.Forms.Button btn_genarel;
        private System.Windows.Forms.DataGridView dgv_fields;
        private System.Windows.Forms.Label la_curr;
        private System.Windows.Forms.TextBox tx_namespc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}