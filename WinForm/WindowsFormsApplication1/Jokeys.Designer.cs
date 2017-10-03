namespace WindowsFormsApplication1
{
    partial class Jokeys
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
            this.lb_content = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lb_content
            // 
            this.lb_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_content.FormattingEnabled = true;
            this.lb_content.ItemHeight = 12;
            this.lb_content.Location = new System.Drawing.Point(0, 0);
            this.lb_content.Name = "lb_content";
            this.lb_content.Size = new System.Drawing.Size(373, 413);
            this.lb_content.TabIndex = 0;
            // 
            // Jokeys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 413);
            this.Controls.Add(this.lb_content);
            this.Name = "Jokeys";
            this.Text = "Jokeys";
            this.Load += new System.EventHandler(this.Jokeys_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lb_content;
    }
}