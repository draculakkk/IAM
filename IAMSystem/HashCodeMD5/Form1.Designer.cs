namespace HashCodeMD5
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtvalue = new System.Windows.Forms.TextBox();
            this.txtjiami = new System.Windows.Forms.TextBox();
            this.btnMD5 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "明文：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "密文：";
            // 
            // txtvalue
            // 
            this.txtvalue.Location = new System.Drawing.Point(59, 49);
            this.txtvalue.Name = "txtvalue";
            this.txtvalue.Size = new System.Drawing.Size(427, 21);
            this.txtvalue.TabIndex = 1;
            // 
            // txtjiami
            // 
            this.txtjiami.Location = new System.Drawing.Point(59, 84);
            this.txtjiami.Name = "txtjiami";
            this.txtjiami.Size = new System.Drawing.Size(427, 21);
            this.txtjiami.TabIndex = 1;
            // 
            // btnMD5
            // 
            this.btnMD5.Location = new System.Drawing.Point(59, 128);
            this.btnMD5.Name = "btnMD5";
            this.btnMD5.Size = new System.Drawing.Size(75, 23);
            this.btnMD5.TabIndex = 2;
            this.btnMD5.Text = "MD5加密";
            this.btnMD5.UseVisualStyleBackColor = true;
            this.btnMD5.Click += new System.EventHandler(this.btnMD5_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(140, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Base64加密";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnMD5);
            this.Controls.Add(this.txtjiami);
            this.Controls.Add(this.txtvalue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtvalue;
        private System.Windows.Forms.TextBox txtjiami;
        private System.Windows.Forms.Button btnMD5;
        private System.Windows.Forms.Button button1;
    }
}

