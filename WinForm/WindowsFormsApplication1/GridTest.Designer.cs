namespace WindowsFormsApplication1
{
    partial class GridTest
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
            this.dgv_ts1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ts1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_ts1
            // 
            this.dgv_ts1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ts1.Location = new System.Drawing.Point(42, 27);
            this.dgv_ts1.Name = "dgv_ts1";
            this.dgv_ts1.RowTemplate.Height = 23;
            this.dgv_ts1.Size = new System.Drawing.Size(453, 185);
            this.dgv_ts1.TabIndex = 0;
            // 
            // GridTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 240);
            this.Controls.Add(this.dgv_ts1);
            this.Name = "GridTest";
            this.Text = "GridTest";
            this.Load += new System.EventHandler(this.GridTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ts1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_ts1;
    }
}