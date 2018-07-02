using System;
using System.Drawing.Imaging;
using System.Net.Mail;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

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

        public frmMailer(string filename)
        {
            InitializeComponent();
            //read mail list
            try
            {
                string mailList = string.Empty;
                using (StreamReader sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("TDSCreator.Resources.mailList.txt"), Encoding.UTF8))
                    mailList = sr.ReadToEnd();
                foreach (string item in mailList.Split(';'))
                    cboMailAddress.Items.Add(new MailItem(item, item.Substring(item.IndexOf('<') + 1, item.IndexOf('>') - item.IndexOf('<') - 1)));
            }
            catch (Exception)
            { }
            cboMailAddress.SelectedIndex = 0;
            //initiate
            txtFileName.Text = filename;
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

        private void btnReadMailAddress_Click(object sender, EventArgs e)
        {
            Enabled = false;
            cboMailAddress.Items.Clear();
            MessageBox.Show("將嘗試讀取Outlook中通訊錄，需要一段時間", "Alert");
            Microsoft.Office.Interop.Outlook.Application outlook = new Microsoft.Office.Interop.Outlook.Application();
            Microsoft.Office.Interop.Outlook.AddressList addressList = outlook.Session.GetGlobalAddressList();
            for (int i = 1; i <= addressList.AddressEntries.Count - 1; i++)
            {
                Microsoft.Office.Interop.Outlook.AddressEntry addrEntry = addressList.AddressEntries[i];
                if (addrEntry.AddressEntryUserType == Microsoft.Office.Interop.Outlook.OlAddressEntryUserType.olExchangeUserAddressEntry)
                {
                    Microsoft.Office.Interop.Outlook.ExchangeUser exchUser = addrEntry.GetExchangeUser();
                    cboMailAddress.Items.Add(new MailItem(exchUser.Name + " <" + exchUser.PrimarySmtpAddress + ">", exchUser.PrimarySmtpAddress));
                }
            }
            if (cboMailAddress.Items.Count > 0)
            {
                cboMailAddress.SelectedIndex = 0;
                txtMailAddress.Visible = false;
                cboMailAddress.Visible = true;
                MessageBox.Show("讀取成功", "Alert");
            }
            else
            {
                txtMailAddress.Visible = true;
                cboMailAddress.Visible = false;
                MessageBox.Show("讀取失敗，請手動輸入郵件地址", "Alert");
            }
            Enabled = true;
        }
    }
}
