
namespace WindowsFormsApplication1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_data = new System.Windows.Forms.DataGridView();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_emule = new System.Windows.Forms.Button();
            this.btn_music = new System.Windows.Forms.Button();
            this.btn_douban = new System.Windows.Forms.Button();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.btn_mids = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btn_jokey = new System.Windows.Forms.Button();
            this.t1 = new System.Windows.Forms.TextBox();
            this.t2 = new System.Windows.Forms.TextBox();
            this.t3 = new System.Windows.Forms.TextBox();
            this.t4 = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.btn_bdy = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_data)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
            this.groupBox1.Controls.Add(this.dgv_data);
            this.groupBox1.Controls.Add(this.comboBox3);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Location = new System.Drawing.Point(12, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(704, 318);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据展示";
            // 
            // dgv_data
            // 
            this.dgv_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_data.Location = new System.Drawing.Point(6, 107);
            this.dgv_data.Name = "dgv_data";
            this.dgv_data.RowTemplate.Height = 23;
            this.dgv_data.Size = new System.Drawing.Size(692, 205);
            this.dgv_data.TabIndex = 4;
            this.dgv_data.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_data_CellClick);
            this.dgv_data.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_data_CellDoubleClick);
            this.dgv_data.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_data_CellValueChanged);
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(288, 31);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(121, 20);
            this.comboBox3.TabIndex = 5;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(145, 31);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 20);
            this.comboBox2.TabIndex = 5;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 31);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.button1.Location = new System.Drawing.Point(18, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "打开控制台";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_emule
            // 
            this.btn_emule.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_emule.Location = new System.Drawing.Point(92, 11);
            this.btn_emule.Name = "btn_emule";
            this.btn_emule.Size = new System.Drawing.Size(68, 23);
            this.btn_emule.TabIndex = 2;
            this.btn_emule.Text = "种子搜索";
            this.btn_emule.UseVisualStyleBackColor = true;
            this.btn_emule.Click += new System.EventHandler(this.btn_emule_Click);
            // 
            // btn_music
            // 
            this.btn_music.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_music.Location = new System.Drawing.Point(166, 11);
            this.btn_music.Name = "btn_music";
            this.btn_music.Size = new System.Drawing.Size(68, 23);
            this.btn_music.TabIndex = 3;
            this.btn_music.Text = "图灵机器人";
            this.btn_music.UseVisualStyleBackColor = true;
            this.btn_music.Click += new System.EventHandler(this.btn_music_Click);
            // 
            // btn_douban
            // 
            this.btn_douban.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_douban.Location = new System.Drawing.Point(240, 11);
            this.btn_douban.Name = "btn_douban";
            this.btn_douban.Size = new System.Drawing.Size(68, 23);
            this.btn_douban.TabIndex = 4;
            this.btn_douban.Text = "豆瓣FM";
            this.btn_douban.UseVisualStyleBackColor = true;
            this.btn_douban.Click += new System.EventHandler(this.btn_douban_Click);
            // 
            // skinEngine1
            // 
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = null;
            // 
            // btn_mids
            // 
            this.btn_mids.Location = new System.Drawing.Point(314, 12);
            this.btn_mids.Name = "btn_mids";
            this.btn_mids.Size = new System.Drawing.Size(75, 23);
            this.btn_mids.TabIndex = 6;
            this.btn_mids.Text = "MID TEST";
            this.btn_mids.UseVisualStyleBackColor = true;
            this.btn_mids.Click += new System.EventHandler(this.btn_mids_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(395, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "双色球数据获取";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(511, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "label1";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(18, 40);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(68, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "天气";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(92, 40);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(68, 23);
            this.button4.TabIndex = 10;
            this.button4.Text = "今日头条";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btn_jokey
            // 
            this.btn_jokey.Location = new System.Drawing.Point(166, 40);
            this.btn_jokey.Name = "btn_jokey";
            this.btn_jokey.Size = new System.Drawing.Size(68, 23);
            this.btn_jokey.TabIndex = 11;
            this.btn_jokey.Text = "最新笑话";
            this.btn_jokey.UseVisualStyleBackColor = true;
            this.btn_jokey.Click += new System.EventHandler(this.btn_jokey_Click);
            // 
            // t1
            // 
            this.t1.Location = new System.Drawing.Point(259, 42);
            this.t1.Name = "t1";
            this.t1.Size = new System.Drawing.Size(100, 21);
            this.t1.TabIndex = 12;
            this.t1.TextChanged += new System.EventHandler(this.t1_TextChanged);
            // 
            // t2
            // 
            this.t2.Location = new System.Drawing.Point(365, 42);
            this.t2.Name = "t2";
            this.t2.Size = new System.Drawing.Size(100, 21);
            this.t2.TabIndex = 12;
            this.t2.TextChanged += new System.EventHandler(this.t2_TextChanged);
            // 
            // t3
            // 
            this.t3.Location = new System.Drawing.Point(471, 42);
            this.t3.Name = "t3";
            this.t3.Size = new System.Drawing.Size(100, 21);
            this.t3.TabIndex = 12;
            this.t3.TextChanged += new System.EventHandler(this.t3_TextChanged);
            // 
            // t4
            // 
            this.t4.Location = new System.Drawing.Point(577, 42);
            this.t4.Name = "t4";
            this.t4.Size = new System.Drawing.Size(100, 21);
            this.t4.TabIndex = 12;
            this.t4.TextChanged += new System.EventHandler(this.t4_TextChanged);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(683, 42);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(27, 21);
            this.button5.TabIndex = 13;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // btn_bdy
            // 
            this.btn_bdy.Location = new System.Drawing.Point(558, 11);
            this.btn_bdy.Name = "btn_bdy";
            this.btn_bdy.Size = new System.Drawing.Size(75, 23);
            this.btn_bdy.TabIndex = 14;
            this.btn_bdy.Text = "百度云";
            this.btn_bdy.UseVisualStyleBackColor = true;
            this.btn_bdy.Click += new System.EventHandler(this.btn_bdy_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 403);
            this.Controls.Add(this.btn_bdy);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.t4);
            this.Controls.Add(this.t3);
            this.Controls.Add(this.t2);
            this.Controls.Add(this.t1);
            this.Controls.Add(this.btn_jokey);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_mids);
            this.Controls.Add(this.btn_douban);
            this.Controls.Add(this.btn_music);
            this.Controls.Add(this.btn_emule);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_data)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv_data;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_emule;
        private System.Windows.Forms.Button btn_music;
        private System.Windows.Forms.Button btn_douban;
        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btn_mids;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btn_jokey;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TextBox t1;
        private System.Windows.Forms.TextBox t2;
        private System.Windows.Forms.TextBox t3;
        private System.Windows.Forms.TextBox t4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button btn_bdy;
    }
}

