namespace Calc
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
            this.tx_result = new System.Windows.Forms.TextBox();
            this.Delete = new System.Windows.Forms.Button();
            this.clearE = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.MAdd = new System.Windows.Forms.Button();
            this.MDiv = new System.Windows.Forms.Button();
            this.seven = new System.Windows.Forms.Button();
            this.eight = new System.Windows.Forms.Button();
            this.nine = new System.Windows.Forms.Button();
            this.chu = new System.Windows.Forms.Button();
            this.kaifang = new System.Windows.Forms.Button();
            this.four = new System.Windows.Forms.Button();
            this.five = new System.Windows.Forms.Button();
            this.six = new System.Windows.Forms.Button();
            this.cheng = new System.Windows.Forms.Button();
            this.baifenbi = new System.Windows.Forms.Button();
            this.one = new System.Windows.Forms.Button();
            this.two = new System.Windows.Forms.Button();
            this.three = new System.Windows.Forms.Button();
            this.div = new System.Windows.Forms.Button();
            this.eq = new System.Windows.Forms.Button();
            this.zero = new System.Windows.Forms.Button();
            this.dot = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tx_result
            // 
            this.tx_result.BackColor = System.Drawing.Color.White;
            this.tx_result.Location = new System.Drawing.Point(1, 1);
            this.tx_result.Multiline = true;
            this.tx_result.Name = "tx_result";
            this.tx_result.ReadOnly = true;
            this.tx_result.Size = new System.Drawing.Size(264, 44);
            this.tx_result.TabIndex = 0;
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(1, 51);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(48, 34);
            this.Delete.TabIndex = 1;
            this.Delete.Text = "←";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.eq_Click);
            // 
            // clearE
            // 
            this.clearE.Location = new System.Drawing.Point(55, 51);
            this.clearE.Name = "clearE";
            this.clearE.Size = new System.Drawing.Size(48, 34);
            this.clearE.TabIndex = 1;
            this.clearE.Text = "CE";
            this.clearE.UseVisualStyleBackColor = true;
            this.clearE.Click += new System.EventHandler(this.eq_Click);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(109, 51);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(48, 34);
            this.Clear.TabIndex = 1;
            this.Clear.Text = "C";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.eq_Click);
            // 
            // MAdd
            // 
            this.MAdd.Location = new System.Drawing.Point(163, 51);
            this.MAdd.Name = "MAdd";
            this.MAdd.Size = new System.Drawing.Size(48, 34);
            this.MAdd.TabIndex = 1;
            this.MAdd.Text = "M+";
            this.MAdd.UseVisualStyleBackColor = true;
            this.MAdd.Click += new System.EventHandler(this.eq_Click);
            // 
            // MDiv
            // 
            this.MDiv.Location = new System.Drawing.Point(217, 51);
            this.MDiv.Name = "MDiv";
            this.MDiv.Size = new System.Drawing.Size(48, 34);
            this.MDiv.TabIndex = 1;
            this.MDiv.Text = "M-";
            this.MDiv.UseVisualStyleBackColor = true;
            this.MDiv.Click += new System.EventHandler(this.eq_Click);
            // 
            // seven
            // 
            this.seven.Location = new System.Drawing.Point(1, 91);
            this.seven.Name = "seven";
            this.seven.Size = new System.Drawing.Size(48, 34);
            this.seven.TabIndex = 1;
            this.seven.Text = "7";
            this.seven.UseVisualStyleBackColor = true;
            this.seven.Click += new System.EventHandler(this.eq_Click);
            // 
            // eight
            // 
            this.eight.Location = new System.Drawing.Point(55, 91);
            this.eight.Name = "eight";
            this.eight.Size = new System.Drawing.Size(48, 34);
            this.eight.TabIndex = 1;
            this.eight.Text = "8";
            this.eight.UseVisualStyleBackColor = true;
            this.eight.Click += new System.EventHandler(this.eq_Click);
            // 
            // nine
            // 
            this.nine.Location = new System.Drawing.Point(109, 91);
            this.nine.Name = "nine";
            this.nine.Size = new System.Drawing.Size(48, 34);
            this.nine.TabIndex = 1;
            this.nine.Text = "9";
            this.nine.UseVisualStyleBackColor = true;
            this.nine.Click += new System.EventHandler(this.eq_Click);
            // 
            // chu
            // 
            this.chu.Location = new System.Drawing.Point(163, 91);
            this.chu.Name = "chu";
            this.chu.Size = new System.Drawing.Size(48, 34);
            this.chu.TabIndex = 1;
            this.chu.Text = "/";
            this.chu.UseVisualStyleBackColor = true;
            this.chu.Click += new System.EventHandler(this.eq_Click);
            // 
            // kaifang
            // 
            this.kaifang.Location = new System.Drawing.Point(217, 91);
            this.kaifang.Name = "kaifang";
            this.kaifang.Size = new System.Drawing.Size(48, 34);
            this.kaifang.TabIndex = 1;
            this.kaifang.Text = "√";
            this.kaifang.UseVisualStyleBackColor = true;
            this.kaifang.Click += new System.EventHandler(this.eq_Click);
            // 
            // four
            // 
            this.four.Location = new System.Drawing.Point(1, 131);
            this.four.Name = "four";
            this.four.Size = new System.Drawing.Size(48, 34);
            this.four.TabIndex = 1;
            this.four.Text = "4";
            this.four.UseVisualStyleBackColor = true;
            this.four.Click += new System.EventHandler(this.eq_Click);
            // 
            // five
            // 
            this.five.Location = new System.Drawing.Point(55, 131);
            this.five.Name = "five";
            this.five.Size = new System.Drawing.Size(48, 34);
            this.five.TabIndex = 1;
            this.five.Text = "5";
            this.five.UseVisualStyleBackColor = true;
            this.five.Click += new System.EventHandler(this.eq_Click);
            // 
            // six
            // 
            this.six.Location = new System.Drawing.Point(109, 131);
            this.six.Name = "six";
            this.six.Size = new System.Drawing.Size(48, 34);
            this.six.TabIndex = 1;
            this.six.Text = "6";
            this.six.UseVisualStyleBackColor = true;
            this.six.Click += new System.EventHandler(this.eq_Click);
            // 
            // cheng
            // 
            this.cheng.Location = new System.Drawing.Point(163, 131);
            this.cheng.Name = "cheng";
            this.cheng.Size = new System.Drawing.Size(48, 34);
            this.cheng.TabIndex = 1;
            this.cheng.Text = "*";
            this.cheng.UseVisualStyleBackColor = true;
            this.cheng.Click += new System.EventHandler(this.eq_Click);
            // 
            // baifenbi
            // 
            this.baifenbi.Location = new System.Drawing.Point(217, 131);
            this.baifenbi.Name = "baifenbi";
            this.baifenbi.Size = new System.Drawing.Size(48, 34);
            this.baifenbi.TabIndex = 1;
            this.baifenbi.Text = "%";
            this.baifenbi.UseVisualStyleBackColor = true;
            this.baifenbi.Click += new System.EventHandler(this.eq_Click);
            // 
            // one
            // 
            this.one.Location = new System.Drawing.Point(1, 171);
            this.one.Name = "one";
            this.one.Size = new System.Drawing.Size(48, 34);
            this.one.TabIndex = 1;
            this.one.Text = "1";
            this.one.UseVisualStyleBackColor = true;
            this.one.Click += new System.EventHandler(this.eq_Click);
            // 
            // two
            // 
            this.two.Location = new System.Drawing.Point(55, 171);
            this.two.Name = "two";
            this.two.Size = new System.Drawing.Size(48, 34);
            this.two.TabIndex = 1;
            this.two.Text = "2";
            this.two.UseVisualStyleBackColor = true;
            this.two.Click += new System.EventHandler(this.eq_Click);
            // 
            // three
            // 
            this.three.Location = new System.Drawing.Point(109, 171);
            this.three.Name = "three";
            this.three.Size = new System.Drawing.Size(48, 34);
            this.three.TabIndex = 1;
            this.three.Text = "3";
            this.three.UseVisualStyleBackColor = true;
            this.three.Click += new System.EventHandler(this.eq_Click);
            // 
            // div
            // 
            this.div.Location = new System.Drawing.Point(163, 171);
            this.div.Name = "div";
            this.div.Size = new System.Drawing.Size(48, 34);
            this.div.TabIndex = 1;
            this.div.Text = "-";
            this.div.UseVisualStyleBackColor = true;
            this.div.Click += new System.EventHandler(this.eq_Click);
            // 
            // eq
            // 
            this.eq.Location = new System.Drawing.Point(217, 171);
            this.eq.Name = "eq";
            this.eq.Size = new System.Drawing.Size(48, 74);
            this.eq.TabIndex = 1;
            this.eq.Text = "确认";
            this.eq.UseVisualStyleBackColor = true;
            this.eq.Click += new System.EventHandler(this.eq_Click_1);
            // 
            // zero
            // 
            this.zero.Location = new System.Drawing.Point(1, 211);
            this.zero.Name = "zero";
            this.zero.Size = new System.Drawing.Size(102, 34);
            this.zero.TabIndex = 1;
            this.zero.Text = "0";
            this.zero.UseVisualStyleBackColor = true;
            this.zero.Click += new System.EventHandler(this.eq_Click);
            // 
            // dot
            // 
            this.dot.Location = new System.Drawing.Point(109, 211);
            this.dot.Name = "dot";
            this.dot.Size = new System.Drawing.Size(48, 34);
            this.dot.TabIndex = 1;
            this.dot.Text = ".";
            this.dot.UseVisualStyleBackColor = true;
            this.dot.Click += new System.EventHandler(this.eq_Click);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(163, 211);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(48, 34);
            this.add.TabIndex = 1;
            this.add.Text = "+";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.eq_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 251);
            this.Controls.Add(this.eq);
            this.Controls.Add(this.baifenbi);
            this.Controls.Add(this.kaifang);
            this.Controls.Add(this.MDiv);
            this.Controls.Add(this.add);
            this.Controls.Add(this.div);
            this.Controls.Add(this.cheng);
            this.Controls.Add(this.chu);
            this.Controls.Add(this.MAdd);
            this.Controls.Add(this.dot);
            this.Controls.Add(this.three);
            this.Controls.Add(this.six);
            this.Controls.Add(this.nine);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.two);
            this.Controls.Add(this.five);
            this.Controls.Add(this.eight);
            this.Controls.Add(this.clearE);
            this.Controls.Add(this.zero);
            this.Controls.Add(this.one);
            this.Controls.Add(this.four);
            this.Controls.Add(this.seven);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.tx_result);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(283, 289);
            this.MinimumSize = new System.Drawing.Size(283, 289);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tx_result;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.Button clearE;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button MAdd;
        private System.Windows.Forms.Button MDiv;
        private System.Windows.Forms.Button seven;
        private System.Windows.Forms.Button eight;
        private System.Windows.Forms.Button nine;
        private System.Windows.Forms.Button chu;
        private System.Windows.Forms.Button kaifang;
        private System.Windows.Forms.Button four;
        private System.Windows.Forms.Button five;
        private System.Windows.Forms.Button six;
        private System.Windows.Forms.Button cheng;
        private System.Windows.Forms.Button baifenbi;
        private System.Windows.Forms.Button one;
        private System.Windows.Forms.Button two;
        private System.Windows.Forms.Button three;
        private System.Windows.Forms.Button div;
        private System.Windows.Forms.Button eq;
        private System.Windows.Forms.Button zero;
        private System.Windows.Forms.Button dot;
        private System.Windows.Forms.Button add;
    }
}

