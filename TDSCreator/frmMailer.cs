using System;
using System.Drawing.Imaging;
using System.Net.Mail;
using System.Windows.Forms;

namespace TDSCreator
{
    public partial class frmMailer : Form
    {

        #region | Properties |
        public string MailAddress { get; set; }
        public string FileName { get; set; }
        #endregion

        public frmMailer()
        {
            InitializeComponent();
            txtMailAddress.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //Check mail address
            if (!IsValid(txtMailAddress.Text))
            {
                MessageBox.Show("郵件地址錯誤，請更正。", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Check file name
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtFileName.Text, @"^[\w\-. ]+$"))
            {
                MessageBox.Show("檔案名稱錯誤，請更正。", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MailAddress = txtMailAddress.Text;
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
