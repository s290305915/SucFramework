namespace WindowsFormsApplication1
{
    partial class Weather
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
            this.btn_search = new System.Windows.Forms.Button();
            this.tx_city = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_result = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(223, 16);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 0;
            this.btn_search.Text = "查询";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // tx_city
            // 
            this.tx_city.Location = new System.Drawing.Point(59, 17);
            this.tx_city.Name = "tx_city";
            this.tx_city.Size = new System.Drawing.Size(158, 21);
            this.tx_city.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "城市：";
            // 
            // lb_result
            // 
            this.lb_result.AutoSize = true;
            this.lb_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_result.Location = new System.Drawing.Point(0, 0);
            this.lb_result.Name = "lb_result";
            this.lb_result.Size = new System.Drawing.Size(53, 12);
            this.lb_result.TabIndex = 3;
            this.lb_result.Text = "天气预告";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lb_result);
            this.panel1.Location = new System.Drawing.Point(14, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 510);
            this.panel1.TabIndex = 4;
            // 
            // Weather
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 566);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tx_city);
            this.Controls.Add(this.btn_search);
            this.Name = "Weather";
            this.Text = "Weather";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Weather_FormClosing);
            this.Load += new System.EventHandler(this.Weather_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox tx_city;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_result;
        private System.Windows.Forms.Panel panel1;
    }
}