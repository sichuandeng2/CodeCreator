namespace CodeCreator
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtconstr = new System.Windows.Forms.TextBox();
            this.btncon = new System.Windows.Forms.Button();
            this.cmTable = new System.Windows.Forms.ComboBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtPreview = new System.Windows.Forms.Button();
            this.txtDAL = new System.Windows.Forms.TextBox();
            this.txtBLL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAuotoKey = new System.Windows.Forms.TextBox();
            this.txtnamespace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreateCode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtconstr
            // 
            this.txtconstr.Location = new System.Drawing.Point(12, 12);
            this.txtconstr.Name = "txtconstr";
            this.txtconstr.Size = new System.Drawing.Size(467, 21);
            this.txtconstr.TabIndex = 0;
            this.txtconstr.Text = "data source=.;database=test;uid=sa;pwd=jerry";
            // 
            // btncon
            // 
            this.btncon.Location = new System.Drawing.Point(501, 12);
            this.btncon.Name = "btncon";
            this.btncon.Size = new System.Drawing.Size(75, 23);
            this.btncon.TabIndex = 1;
            this.btncon.Text = "连接";
            this.btncon.UseVisualStyleBackColor = true;
            this.btncon.Click += new System.EventHandler(this.btncon_Click);
            // 
            // cmTable
            // 
            this.cmTable.FormattingEnabled = true;
            this.cmTable.Location = new System.Drawing.Point(12, 49);
            this.cmTable.Name = "cmTable";
            this.cmTable.Size = new System.Drawing.Size(258, 20);
            this.cmTable.TabIndex = 2;
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(12, 86);
            this.txtModel.Multiline = true;
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(241, 373);
            this.txtModel.TabIndex = 3;
            // 
            // txtPreview
            // 
            this.txtPreview.Location = new System.Drawing.Point(292, 46);
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.Size = new System.Drawing.Size(75, 23);
            this.txtPreview.TabIndex = 4;
            this.txtPreview.Text = "预览";
            this.txtPreview.UseVisualStyleBackColor = true;
            this.txtPreview.Click += new System.EventHandler(this.txtPreview_Click);
            // 
            // txtDAL
            // 
            this.txtDAL.Location = new System.Drawing.Point(277, 86);
            this.txtDAL.Multiline = true;
            this.txtDAL.Name = "txtDAL";
            this.txtDAL.Size = new System.Drawing.Size(241, 373);
            this.txtDAL.TabIndex = 5;
            // 
            // txtDLL
            // 
            this.txtBLL.Location = new System.Drawing.Point(545, 86);
            this.txtBLL.Multiline = true;
            this.txtBLL.Name = "txtDLL";
            this.txtBLL.Size = new System.Drawing.Size(241, 373);
            this.txtBLL.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(513, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "自增列";
            // 
            // txtAuotoKey
            // 
            this.txtAuotoKey.Location = new System.Drawing.Point(558, 49);
            this.txtAuotoKey.Name = "txtAuotoKey";
            this.txtAuotoKey.Size = new System.Drawing.Size(45, 21);
            this.txtAuotoKey.TabIndex = 8;
            this.txtAuotoKey.Text = "id";
            // 
            // txtnamespace
            // 
            this.txtnamespace.Location = new System.Drawing.Point(668, 49);
            this.txtnamespace.Name = "txtnamespace";
            this.txtnamespace.Size = new System.Drawing.Size(106, 21);
            this.txtnamespace.TabIndex = 10;
            this.txtnamespace.Text = "CodeCreator";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(609, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "命名空间";
            // 
            // btnCreateCode
            // 
            this.btnCreateCode.Location = new System.Drawing.Point(404, 47);
            this.btnCreateCode.Name = "btnCreateCode";
            this.btnCreateCode.Size = new System.Drawing.Size(75, 23);
            this.btnCreateCode.TabIndex = 11;
            this.btnCreateCode.Text = "生成";
            this.btnCreateCode.UseVisualStyleBackColor = true;
            this.btnCreateCode.Click += new System.EventHandler(this.btnCreateCode_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 495);
            this.Controls.Add(this.btnCreateCode);
            this.Controls.Add(this.txtnamespace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAuotoKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBLL);
            this.Controls.Add(this.txtDAL);
            this.Controls.Add(this.txtPreview);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.cmTable);
            this.Controls.Add(this.btncon);
            this.Controls.Add(this.txtconstr);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtconstr;
        private System.Windows.Forms.Button btncon;
        private System.Windows.Forms.ComboBox cmTable;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Button txtPreview;
        private System.Windows.Forms.TextBox txtDAL;
        private System.Windows.Forms.TextBox txtBLL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAuotoKey;
        private System.Windows.Forms.TextBox txtnamespace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCreateCode;
    }
}

