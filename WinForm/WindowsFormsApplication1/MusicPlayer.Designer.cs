namespace WindowsFormsApplication1
{
    partial class MusicPlayer 
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MusicPlayer));
            this.lstb_chat = new System.Windows.Forms.ListBox();
            this.tx_keyword = new System.Windows.Forms.TextBox();
            this.qqButton1 = new System.Windows.Forms.Button();
            this.btn_export = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstb_chat
            // 
            this.lstb_chat.FormattingEnabled = true;
            this.lstb_chat.ItemHeight = 12;
            this.lstb_chat.Location = new System.Drawing.Point(25, 41);
            this.lstb_chat.Name = "lstb_chat";
            this.lstb_chat.Size = new System.Drawing.Size(630, 292);
            this.lstb_chat.TabIndex = 2;
            // 
            // tx_keyword
            // 
            this.tx_keyword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tx_keyword.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tx_keyword.Location = new System.Drawing.Point(25, 12);
            this.tx_keyword.Name = "tx_keyword";
            this.tx_keyword.Size = new System.Drawing.Size(340, 23);
            this.tx_keyword.TabIndex = 1;
            // 
            // qqButton1
            // 
            this.qqButton1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.qqButton1.Location = new System.Drawing.Point(387, 12);
            this.qqButton1.Name = "qqButton1";
            this.qqButton1.Size = new System.Drawing.Size(68, 23);
            this.qqButton1.TabIndex = 0;
            this.qqButton1.Text = "提交";
            this.qqButton1.UseVisualStyleBackColor = true;
            this.qqButton1.Click += new System.EventHandler(this.qqButton1_Click);
            // 
            // btn_export
            // 
            this.btn_export.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_export.Location = new System.Drawing.Point(461, 11);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(78, 23);
            this.btn_export.TabIndex = 3;
            this.btn_export.Text = "导出到Excel";
            this.btn_export.UseVisualStyleBackColor = true;
            this.btn_export.Click += new System.EventHandler(this.btn_export_Click);
            // 
            // MusicPlayer
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(684, 338);
            this.Controls.Add(this.btn_export);
            this.Controls.Add(this.lstb_chat);
            this.Controls.Add(this.tx_keyword);
            this.Controls.Add(this.qqButton1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MusicPlayer";
            this.Text = "图灵机器人";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MusicPlayer_FormClosing);
            this.Load += new System.EventHandler(this.MusicPlayer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button qqButton1;
        private System.Windows.Forms.TextBox tx_keyword;
        private System.Windows.Forms.ListBox lstb_chat;
        private System.Windows.Forms.Button btn_export;
    }
}
