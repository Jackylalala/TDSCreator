using System;
using System.Drawing.Imaging;
using System.Net.Mail;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace TDSCreator
{
    public partial class frmMailer : Form
    {

        #region | Properties |
        public string MailAddress { get; set; }
        public string FileName { get; set; }
        private class MailItem
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public MailItem(string name, string address)
            {
                Name = name;
                Address = address;
            }
            public override string ToString()
            {
                return Name;
            }
        }
        #endregion

        public frmMailer()
        {
            InitializeComponent();
            //read mail list
            try
            {
                string mailListFile = Application.StartupPath + @"\mailList.txt";
                string mailList = string.Empty;
                if (File.Exists(mailListFile))
                {
                    using (FileStream fs = new FileStream(mailListFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                            mailList = sr.ReadToEnd();
                    }
                }
                foreach (string item in mailList.Split(';'))
                {
                    MailItem tmp = new MailItem(item, item.Substring(item.IndexOf('<') + 1, item.IndexOf('>') - item.IndexOf('<') - 1));
                    cboMailAddress.Items.Add(tmp);
                }
            }
            catch (Exception)
            { }
            cboMailAddress.SelectedIndex = 0;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //Check mail address
            if (!IsValid(cboMailAddress.Text))
            {
                MessageBox.Show("郵件地址錯誤，請更正。", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Check file name
            if (txtFileName.Text.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
            {
                MessageBox.Show("檔案名稱錯誤，請更正。", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MailAddress = ((MailItem)cboMailAddress.SelectedItem).Address;
            FileName = txtFileName.Text;
            DialogResult = DialogResult.OK;
        }

        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
