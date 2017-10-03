
namespace WindowsFormsApplication1
{
    partial class P2PSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P2PSearch));
            this.lv_data = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menu_export = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.export = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pro_search = new System.Windows.Forms.ProgressBar();
            this.btn_next = new System.Windows.Forms.Button();
            this.btn_reve = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_runtime = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tx_source = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tx_search = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.tx_page = new System.Windows.Forms.TextBox();
            this.menu_export.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lv_data
            // 
            this.lv_data.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lv_data.ContextMenuStrip = this.menu_export;
            this.lv_data.Location = new System.Drawing.Point(6, 41);
            this.lv_data.Name = "lv_data";
            this.lv_data.Size = new System.Drawing.Size(796, 276);
            this.lv_data.TabIndex = 6;
            this.lv_data.UseCompatibleStateImageBehavior = false;
            this.lv_data.View = System.Windows.Forms.View.Details;
            this.lv_data.SelectedIndexChanged += new System.EventHandler(this.lv_data_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 33;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "资源名称";
            this.columnHeader2.Width = 450;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "文件数/大小";
            this.columnHeader3.Width = 143;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "发布时间";
            this.columnHeader4.Width = 124;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "链接地址";
            this.columnHeader5.Width = 241;
            // 
            // menu_export
            // 
            this.menu_export.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.export});
            this.menu_export.Name = "menu_export";
            this.menu_export.Size = new System.Drawing.Size(137, 26);
            // 
            // export
            // 
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(136, 22);
            this.export.Text = "导出到文本";
            this.export.Click += new System.EventHandler(this.export_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
            this.groupBox1.Controls.Add(this.tx_page);
            this.groupBox1.Controls.Add(this.pro_search);
            this.groupBox1.Controls.Add(this.btn_next);
            this.groupBox1.Controls.Add(this.btn_reve);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lv_data);
            this.groupBox1.Location = new System.Drawing.Point(12, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(808, 326);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "搜索结果";
            // 
            // pro_search
            // 
            this.pro_search.Location = new System.Drawing.Point(595, 12);
            this.pro_search.Name = "pro_search";
            this.pro_search.Size = new System.Drawing.Size(207, 23);
            this.pro_search.TabIndex = 9;
            // 
            // btn_next
            // 
            this.btn_next.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_next.Location = new System.Drawing.Point(544, 12);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(45, 23);
            this.btn_next.TabIndex = 5;
            this.btn_next.Text = "NXT";
            this.btn_next.UseVisualStyleBackColor = true;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // btn_reve
            // 
            this.btn_reve.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_reve.Location = new System.Drawing.Point(436, 12);
            this.btn_reve.Name = "btn_reve";
            this.btn_reve.Size = new System.Drawing.Size(45, 23);
            this.btn_reve.TabIndex = 4;
            this.btn_reve.Text = "PRE";
            this.btn_reve.UseVisualStyleBackColor = true;
            this.btn_reve.Click += new System.EventHandler(this.btn_reve_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "↓ 点击ID复制链接,点击右键导出";
            // 
            // lb_runtime
            // 
            this.lb_runtime.AutoSize = true;
            this.lb_runtime.BackColor = System.Drawing.Color.Transparent;
            this.lb_runtime.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lb_runtime.Location = new System.Drawing.Point(607, 32);
            this.lb_runtime.Name = "lb_runtime";
            this.lb_runtime.Size = new System.Drawing.Size(89, 12);
            this.lb_runtime.TabIndex = 9;
            this.lb_runtime.Text = "单击搜索以开始";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox2.BackgroundImage")));
            this.groupBox2.Controls.Add(this.tx_source);
            this.groupBox2.Location = new System.Drawing.Point(12, 387);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(808, 104);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "资源地址";
            // 
            // tx_source
            // 
            this.tx_source.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tx_source.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tx_source.Location = new System.Drawing.Point(6, 20);
            this.tx_source.Multiline = true;
            this.tx_source.Name = "tx_source";
            this.tx_source.Size = new System.Drawing.Size(796, 78);
            this.tx_source.TabIndex = 7;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tx_search
            // 
            this.tx_search.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tx_search.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tx_search.Location = new System.Drawing.Point(33, 27);
            this.tx_search.Name = "tx_search";
            this.tx_search.Size = new System.Drawing.Size(409, 23);
            this.tx_search.TabIndex = 1;
            // 
            // btn_search
            // 
            this.btn_search.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_search.Location = new System.Drawing.Point(448, 26);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(68, 23);
            this.btn_search.TabIndex = 2;
            this.btn_search.Text = "搜  索";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_cancel.Location = new System.Drawing.Point(533, 26);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(68, 23);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "取消搜索";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // tx_page
            // 
            this.tx_page.Location = new System.Drawing.Point(487, 14);
            this.tx_page.Name = "tx_page";
            this.tx_page.Size = new System.Drawing.Size(51, 21);
            this.tx_page.TabIndex = 10;
            this.tx_page.TextChanged += new System.EventHandler(this.tx_page_TextChanged);
            this.tx_page.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tx_page_KeyDown);
            // 
            // P2PSearch
            // 
            this.AcceptButton = this.btn_search;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 517);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.lb_runtime);
            this.Controls.Add(this.tx_search);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "P2PSearch";
            this.Text = "种子搜索";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.P2PSearch_FormClosing);
            this.Load += new System.EventHandler(this.P2PSearch_Load);
            this.menu_export.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lv_data;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_runtime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip menu_export;
        private System.Windows.Forms.ToolStripMenuItem export;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.TextBox tx_search;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Button btn_reve;
        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.TextBox tx_source;
        private System.Windows.Forms.ProgressBar pro_search;
        private System.Windows.Forms.TextBox tx_page;
    }
}