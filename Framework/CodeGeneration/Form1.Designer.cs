namespace CodeGeneration
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
            this.tx_add = new System.Windows.Forms.TextBox();
            this.tx_user = new System.Windows.Forms.TextBox();
            this.tx_pass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_conn = new System.Windows.Forms.Button();
            this.com_dbs = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.la_dbs = new System.Windows.Forms.FlowLayoutPanel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_nextstep = new System.Windows.Forms.Button();
            this.btn_checkall = new System.Windows.Forms.Button();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tx_add
            // 
            this.tx_add.Location = new System.Drawing.Point(92, 38);
            this.tx_add.Name = "tx_add";
            this.tx_add.Size = new System.Drawing.Size(257, 21);
            this.tx_add.TabIndex = 1;
            // 
            // tx_user
            // 
            this.tx_user.Location = new System.Drawing.Point(92, 97);
            this.tx_user.Name = "tx_user";
            this.tx_user.Size = new System.Drawing.Size(257, 21);
            this.tx_user.TabIndex = 2;
            // 
            // tx_pass
            // 
            this.tx_pass.Location = new System.Drawing.Point(92, 127);
            this.tx_pass.Name = "tx_pass";
            this.tx_pass.Size = new System.Drawing.Size(257, 21);
            this.tx_pass.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "服务器地址：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "用户名：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "密  码：";
            // 
            // btn_conn
            // 
            this.btn_conn.Location = new System.Drawing.Point(247, 154);
            this.btn_conn.Name = "btn_conn";
            this.btn_conn.Size = new System.Drawing.Size(102, 23);
            this.btn_conn.TabIndex = 5;
            this.btn_conn.Text = "连  接";
            this.btn_conn.UseVisualStyleBackColor = true;
            this.btn_conn.Click += new System.EventHandler(this.btn_conn_Click);
            // 
            // com_dbs
            // 
            this.com_dbs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_dbs.FormattingEnabled = true;
            this.com_dbs.Location = new System.Drawing.Point(92, 183);
            this.com_dbs.Name = "com_dbs";
            this.com_dbs.Size = new System.Drawing.Size(257, 20);
            this.com_dbs.TabIndex = 6;
            this.com_dbs.SelectedIndexChanged += new System.EventHandler(this.com_dbs_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "选择数据库：";
            // 
            // la_dbs
            // 
            this.la_dbs.AutoScroll = true;
            this.la_dbs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.la_dbs.Location = new System.Drawing.Point(13, 209);
            this.la_dbs.Name = "la_dbs";
            this.la_dbs.Size = new System.Drawing.Size(336, 227);
            this.la_dbs.TabIndex = 5;
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(92, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(257, 20);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Text = "SQLSERVER";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "数据库类型：";
            // 
            // btn_nextstep
            // 
            this.btn_nextstep.Location = new System.Drawing.Point(247, 442);
            this.btn_nextstep.Name = "btn_nextstep";
            this.btn_nextstep.Size = new System.Drawing.Size(102, 23);
            this.btn_nextstep.TabIndex = 7;
            this.btn_nextstep.Text = "选好了，下一步";
            this.btn_nextstep.UseVisualStyleBackColor = true;
            this.btn_nextstep.Click += new System.EventHandler(this.btn_nextstep_Click);
            // 
            // btn_checkall
            // 
            this.btn_checkall.Location = new System.Drawing.Point(13, 442);
            this.btn_checkall.Name = "btn_checkall";
            this.btn_checkall.Size = new System.Drawing.Size(102, 23);
            this.btn_checkall.TabIndex = 8;
            this.btn_checkall.Text = "全  选";
            this.btn_checkall.UseVisualStyleBackColor = true;
            this.btn_checkall.Click += new System.EventHandler(this.btn_checkall_Click);
            // 
            // skinEngine1
            // 
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = null;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Windows 身份验证",
            "SqlServer 身份证验"});
            this.comboBox2.Location = new System.Drawing.Point(92, 68);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(257, 20);
            this.comboBox2.TabIndex = 9;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "登陆类型：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 473);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_checkall);
            this.Controls.Add(this.btn_nextstep);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.la_dbs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.com_dbs);
            this.Controls.Add(this.btn_conn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tx_pass);
            this.Controls.Add(this.tx_user);
            this.Controls.Add(this.tx_add);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "SUC代码生成器";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tx_add;
        private System.Windows.Forms.TextBox tx_user;
        private System.Windows.Forms.TextBox tx_pass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_conn;
        private System.Windows.Forms.ComboBox com_dbs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel la_dbs;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_nextstep;
        private System.Windows.Forms.Button btn_checkall;
        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label6;
    }
}

