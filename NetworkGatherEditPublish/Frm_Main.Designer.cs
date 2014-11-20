namespace NetworkGatherEditPublish
{
    partial class Frm_Main
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
            this.buttonGetUrls = new System.Windows.Forms.Button();
            this.txtBoxCnblogsBlogID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // buttonGetUrls
            // 
            this.buttonGetUrls.Location = new System.Drawing.Point(451, 8);
            this.buttonGetUrls.Name = "buttonGetUrls";
            this.buttonGetUrls.Size = new System.Drawing.Size(190, 29);
            this.buttonGetUrls.TabIndex = 0;
            this.buttonGetUrls.Text = "获取全部博文网址（博客园）";
            this.buttonGetUrls.UseVisualStyleBackColor = true;
            this.buttonGetUrls.Click += new System.EventHandler(this.buttonGetUrls_Click);
            // 
            // txtBoxCnblogsBlogID
            // 
            this.txtBoxCnblogsBlogID.Location = new System.Drawing.Point(120, 12);
            this.txtBoxCnblogsBlogID.Name = "txtBoxCnblogsBlogID";
            this.txtBoxCnblogsBlogID.Size = new System.Drawing.Size(305, 21);
            this.txtBoxCnblogsBlogID.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "博客园博主ID：";
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxLog.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBoxLog.Location = new System.Drawing.Point(12, 53);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(875, 349);
            this.richTextBoxLog.TabIndex = 4;
            this.richTextBoxLog.Text = "";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // Frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 414);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBoxCnblogsBlogID);
            this.Controls.Add(this.buttonGetUrls);
            this.Name = "Frm_Main";
            this.Text = "网络采编发实用技术示例";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGetUrls;
        private System.Windows.Forms.TextBox txtBoxCnblogsBlogID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

