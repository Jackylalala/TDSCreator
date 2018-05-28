﻿namespace TDSCreator
{
    partial class frmTDSCreator
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTDSCreator));
            this.btnStart = new System.Windows.Forms.Button();
            this.pnlFiles = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.txtTDSName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.btnStart_Mail = new System.Windows.Forms.Button();
            this.bgdWork = new System.ComponentModel.BackgroundWorker();
            this.txtApprove1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtQA1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtReview1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtReview2 = new System.Windows.Forms.TextBox();
            this.txtReview3 = new System.Windows.Forms.TextBox();
            this.txtQA2 = new System.Windows.Forms.TextBox();
            this.txtApprove2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnStart.Location = new System.Drawing.Point(131, 630);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(178, 31);
            this.btnStart.TabIndex = 43;
            this.btnStart.Tag = "file";
            this.btnStart.Text = "Start(save as file)";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // pnlFiles
            // 
            this.pnlFiles.Location = new System.Drawing.Point(0, 1);
            this.pnlFiles.Margin = new System.Windows.Forms.Padding(0);
            this.pnlFiles.Name = "pnlFiles";
            this.pnlFiles.Size = new System.Drawing.Size(632, 515);
            this.pnlFiles.TabIndex = 1;
            this.pnlFiles.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(191, 227);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(300, 30);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "label5";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatus.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(397, 599);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "撰寫者：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(308, 531);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "版本：";
            // 
            // txtVersion
            // 
            this.txtVersion.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtVersion.Location = new System.Drawing.Point(361, 527);
            this.txtVersion.MaxLength = 8;
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(42, 25);
            this.txtVersion.TabIndex = 33;
            this.txtVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtVersion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVersion_KeyPress);
            // 
            // txtAuthor
            // 
            this.txtAuthor.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtAuthor.Location = new System.Drawing.Point(463, 595);
            this.txtAuthor.MaxLength = 12;
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(102, 25);
            this.txtAuthor.TabIndex = 42;
            this.txtAuthor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTDSName
            // 
            this.txtTDSName.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtTDSName.Location = new System.Drawing.Point(97, 527);
            this.txtTDSName.MaxLength = 20;
            this.txtTDSName.Name = "txtTDSName";
            this.txtTDSName.Size = new System.Drawing.Size(208, 25);
            this.txtTDSName.TabIndex = 32;
            this.txtTDSName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(18, 531);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.TabIndex = 31;
            this.label3.Text = "產品名稱：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(421, 531);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 34;
            this.label4.Text = "日期：";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "yyyy年MM月dd日";
            this.dtpDate.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(474, 527);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(140, 25);
            this.dtpDate.TabIndex = 34;
            this.dtpDate.Value = new System.DateTime(2018, 1, 3, 15, 4, 45, 0);
            // 
            // btnStart_Mail
            // 
            this.btnStart_Mail.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnStart_Mail.Location = new System.Drawing.Point(324, 630);
            this.btnStart_Mail.Name = "btnStart_Mail";
            this.btnStart_Mail.Size = new System.Drawing.Size(178, 31);
            this.btnStart_Mail.TabIndex = 44;
            this.btnStart_Mail.Tag = "mail";
            this.btnStart_Mail.Text = "Start(send via mail)";
            this.btnStart_Mail.UseVisualStyleBackColor = true;
            this.btnStart_Mail.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // bgdWork
            // 
            this.bgdWork.WorkerReportsProgress = true;
            this.bgdWork.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgdWork_DoWork);
            this.bgdWork.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgdWork_ProgressChanged);
            this.bgdWork.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgdWork_RunWorkerCompleted);
            // 
            // txtApprove1
            // 
            this.txtApprove1.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtApprove1.Location = new System.Drawing.Point(70, 560);
            this.txtApprove1.MaxLength = 12;
            this.txtApprove1.Name = "txtApprove1";
            this.txtApprove1.Size = new System.Drawing.Size(102, 25);
            this.txtApprove1.TabIndex = 35;
            this.txtApprove1.Text = "張英世";
            this.txtApprove1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(20, 564);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 17);
            this.label5.TabIndex = 38;
            this.label5.Text = "核准：";
            // 
            // txtQA1
            // 
            this.txtQA1.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtQA1.Location = new System.Drawing.Point(404, 560);
            this.txtQA1.MaxLength = 12;
            this.txtQA1.Name = "txtQA1";
            this.txtQA1.Size = new System.Drawing.Size(102, 25);
            this.txtQA1.TabIndex = 37;
            this.txtQA1.Text = "葉仲宜";
            this.txtQA1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(333, 564);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 17);
            this.label6.TabIndex = 40;
            this.label6.Text = "QA審查：";
            // 
            // txtReview1
            // 
            this.txtReview1.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtReview1.Location = new System.Drawing.Point(70, 595);
            this.txtReview1.MaxLength = 12;
            this.txtReview1.Name = "txtReview1";
            this.txtReview1.Size = new System.Drawing.Size(102, 25);
            this.txtReview1.TabIndex = 39;
            this.txtReview1.Text = "黃俊豐";
            this.txtReview1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(20, 599);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 17);
            this.label7.TabIndex = 42;
            this.label7.Text = "審查：";
            // 
            // txtReview2
            // 
            this.txtReview2.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtReview2.Location = new System.Drawing.Point(178, 595);
            this.txtReview2.MaxLength = 12;
            this.txtReview2.Name = "txtReview2";
            this.txtReview2.Size = new System.Drawing.Size(102, 25);
            this.txtReview2.TabIndex = 40;
            this.txtReview2.Text = "王文祥";
            this.txtReview2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtReview3
            // 
            this.txtReview3.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtReview3.Location = new System.Drawing.Point(286, 595);
            this.txtReview3.MaxLength = 12;
            this.txtReview3.Name = "txtReview3";
            this.txtReview3.Size = new System.Drawing.Size(102, 25);
            this.txtReview3.TabIndex = 41;
            this.txtReview3.Text = "蕭孟維";
            this.txtReview3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtQA2
            // 
            this.txtQA2.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtQA2.Location = new System.Drawing.Point(512, 560);
            this.txtQA2.MaxLength = 12;
            this.txtQA2.Name = "txtQA2";
            this.txtQA2.Size = new System.Drawing.Size(102, 25);
            this.txtQA2.TabIndex = 38;
            this.txtQA2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtApprove2
            // 
            this.txtApprove2.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtApprove2.Location = new System.Drawing.Point(178, 560);
            this.txtApprove2.MaxLength = 12;
            this.txtApprove2.Name = "txtApprove2";
            this.txtApprove2.Size = new System.Drawing.Size(102, 25);
            this.txtApprove2.TabIndex = 36;
            this.txtApprove2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frmTDSCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 669);
            this.Controls.Add(this.txtApprove2);
            this.Controls.Add(this.txtQA2);
            this.Controls.Add(this.txtReview3);
            this.Controls.Add(this.txtReview2);
            this.Controls.Add(this.txtReview1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtQA1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtApprove1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnStart_Mail);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTDSName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pnlFiles);
            this.Controls.Add(this.lblStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmTDSCreator";
            this.Text = "TDS Creator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel pnlFiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.TextBox txtTDSName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Button btnStart_Mail;
        private System.Windows.Forms.Label lblStatus;
        private System.ComponentModel.BackgroundWorker bgdWork;
        private System.Windows.Forms.TextBox txtApprove1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtQA1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtReview1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtReview2;
        private System.Windows.Forms.TextBox txtReview3;
        private System.Windows.Forms.TextBox txtQA2;
        private System.Windows.Forms.TextBox txtApprove2;
    }
}

