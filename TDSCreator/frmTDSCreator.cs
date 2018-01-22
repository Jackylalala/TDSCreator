using System;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Linq;
using System.Runtime.InteropServices;

namespace TDSCreator
{
    public partial class frmTDSCreator : Form
    {
        #region | Field |
        private static BaseFont fontMsjh;
        private static BaseFont fontMsjhBd;
        private static BaseFont fontCalibri;
        private static BaseFont fontCalibriBd;
        private static string TdsName="";
        private static int version = 0;
        private static string approver = "";
        private static string QAer = "";
        private static string reviewer = "";
        private static string author="";
        private static DateTime date = new DateTime();
        private Label[] lblTitle = new Label[16];
        private ListBox[] lstFileName = new ListBox[16];
        private Label[] lblCount = new Label[16];
        private Button[] btnClearFile = new Button[16];
        private Button[] btnAddFile = new Button[16];
        private static string[] TDS_ITEM_LIST = new string[] { "版序修訂紀錄", "產品開發流程", "原料物性暨品質分析資料", "原料安全資料表", "原料品質標準表", "產品物性資料", "產品安全資料表", "產品品質標準表", "產品應用暨市場需求分析", "競品與開發產品分析比較", "產品配方表", "製程參數", "製程暨Mass Balance流程圖", "最適批次操作結果", "配方暨製程最適化相關研究資料", "中間品管取樣分析表" };
        private static List<TdsItem> tdsFile = new List<TdsItem>();
        private static bool validFile;
        private List<TdsItem> dragDropFiles = new List<TdsItem>();
        private static string[] allowedFileExt = new string[] { ".pdf", ".docx", ".doc", ".xls", ".xlsx", "ppt", "pptx" };
        #endregion

        #region | Events |
        public frmTDSCreator()
        {
            InitializeComponent();
            //define fonts
            //check ttc file
            bool isTTC = File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Fonts) + @"\msjh.ttc");
            try
            {
                fontMsjh = BaseFont.CreateFont(Environment.GetFolderPath(Environment.SpecialFolder.Fonts) + @"\msjh." + (isTTC ? "ttc,1" : "ttf"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                fontMsjhBd = BaseFont.CreateFont(Environment.GetFolderPath(Environment.SpecialFolder.Fonts) + @"\msjhbd." + (isTTC ? "ttc,1" : "ttf"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                fontCalibri = BaseFont.CreateFont(Environment.GetFolderPath(Environment.SpecialFolder.Fonts) + @"\calibri.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                fontCalibriBd = BaseFont.CreateFont(Environment.GetFolderPath(Environment.SpecialFolder.Fonts) + @"\calibrib.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            }
            catch (Exception)
            {
                MessageBox.Show("Please confirm that you already install the following font: 微軟正黑體, Calibri");
            }
            //create control
            for (int i = 0; i < lblTitle.Length; i++)
            {
                lblTitle[i] = new Label();
                lblTitle[i].Left = 10;
                lblTitle[i].Top = 12 + i * 31;
                lblTitle[i].Width = 220;
                lblTitle[i].AutoSize = false;
                lblTitle[i].Font = new System.Drawing.Font("微軟正黑體", 10);
                lblTitle[i].Text = TDS_ITEM_LIST[i];
                pnlFiles.Controls.Add(lblTitle[i]);
                lstFileName[i] = new ListBox();
                lstFileName[i].Left = 235;
                lstFileName[i].Top = 12 + i * 31;
                lstFileName[i].Width = 250;
                lstFileName[i].Height = 25;
                lstFileName[i].Font = new System.Drawing.Font("微軟正黑體", 10);
                lstFileName[i].SelectionMode = SelectionMode.One;
                lstFileName[i].Tag = i.ToString();
                lstFileName[i].AllowDrop = true;
                lstFileName[i].DragEnter += lstFileName_DragEnter;
                lstFileName[i].DragDrop += lstFileName_DragDrop;
                lstFileName[i].KeyDown += lstFileName_KeyDown;
                lstFileName[i].DoubleClick += lstFileName_DoubleClick;
                pnlFiles.Controls.Add(lstFileName[i]);
                lblCount[i] = new Label();
                lblCount[i].Left = 490;
                lblCount[i].Top = 12 + i * 31;
                lblCount[i].Width = 40;
                lblCount[i].AutoSize = false;
                lblCount[i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                lblCount[i].Font = new System.Drawing.Font("微軟正黑體", 10);
                lblCount[i].Text = "0";
                pnlFiles.Controls.Add(lblCount[i]);
                btnClearFile[i] = new Button();
                btnClearFile[i].Left = 535;
                btnClearFile[i].Top = 12 + i * 31;
                btnClearFile[i].Width = 50;
                btnClearFile[i].Font = new System.Drawing.Font("Calibri", 10);
                btnClearFile[i].Text = "Clear";
                btnClearFile[i].Tag = i.ToString();
                btnClearFile[i].Click += btnClearFile_Click;
                pnlFiles.Controls.Add(btnClearFile[i]);
                btnAddFile[i] = new Button();
                btnAddFile[i].Left = 590;
                btnAddFile[i].Top = 12 + i * 31;
                btnAddFile[i].Width = 26;
                btnAddFile[i].Font = new System.Drawing.Font("Calibri", 10);
                btnAddFile[i].Text = "+";
                btnAddFile[i].Tag = i.ToString();
                btnAddFile[i].Click += SelectFile;
                pnlFiles.Controls.Add(btnAddFile[i]);
            }
            pnlFiles.Visible = true;
        }

        private void lstFileName_DoubleClick(object sender, EventArgs e)
        {
            int index = int.Parse(((ListBox)sender).Tag.ToString());
            if (lstFileName[index].SelectedItem != null)
                System.Diagnostics.Process.Start(((TdsItem)lstFileName[index].SelectedItem).FileName);
        }

        private void btnClearFile_Click(object sender, EventArgs e)
        {
            int index = int.Parse(((Button)sender).Tag.ToString());
            foreach (TdsItem item in lstFileName[index].Items)
                tdsFile.Remove(item);
            lstFileName[index].Items.Clear();
            lblCount[index].Text = lstFileName[index].Items.Count.ToString();
        }

        private void lstFileName_KeyDown(object sender, KeyEventArgs e)
        {
            int index = int.Parse(((ListBox)sender).Tag.ToString());
            if (e.KeyCode == Keys.Delete && lstFileName[index].SelectedItem != null)
            {
                tdsFile.Remove((TdsItem)lstFileName[index].SelectedItem);
                lstFileName[index].Items.Remove(lstFileName[index].SelectedItem);
            }
            lblCount[index].Text = lstFileName[index].Items.Count.ToString();
        }

        private void txtVersion_KeyPress(object sender, KeyPressEventArgs e)
        {
            //only allow integer (no decimal point)
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-') && (e.KeyChar != 8))
                e.Handled = true;
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                e.Handled = true;
            // only allow sign symbol at first char
            if ((e.KeyChar == '-') && ((sender as TextBox).Text.IndexOf('-') > -1))
                e.Handled = true;
            if ((e.KeyChar == '-') && !((sender as TextBox).Text.IndexOf('-') > -1) && ((sender as TextBox).SelectionStart != 0))
                e.Handled = true;
        }

        private void lstFileName_DragDrop(object sender, DragEventArgs e)
        {
            int index = int.Parse(((ListBox)sender).Tag.ToString());
            if (validFile)
            {
                foreach (TdsItem item in dragDropFiles)
                {
                    //if not find in list, add it
                    if (!tdsFile.Any(item.Equals))
                    {
                        tdsFile.Add(item);
                        lstFileName[index].Items.Add(item);
                    }
                }
            }
            lblCount[index].Text = lstFileName[index].Items.Count.ToString();
        }

        private void lstFileName_DragEnter(object sender, DragEventArgs e)
        {
            int index = int.Parse(((ListBox)sender).Tag.ToString());
            e.Effect = DragDropEffects.None;
            validFile = true;
            dragDropFiles.Clear();
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach(string filename in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    string ext = Path.GetExtension(filename).ToLower();
                    if (allowedFileExt.Any(ext.Contains))
                        dragDropFiles.Add(new TdsItem(index, filename, ext));
                    else
                        validFile = false;
                }
            }
            if (validFile)
                e.Effect = DragDropEffects.Copy;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string fileName = "";
            string mailAddress = "";
            bool toFile;
            if (((Button)sender).Tag.ToString().Equals("file")) //to file
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Save as file";
                sfd.Filter = "PDF file(*.pdf)|*.pdf";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    toFile = true;
                    fileName = sfd.FileName;
                }
                else
                    return;
            }
            else //via mail
            {
                frmMailer frmMailer = new frmMailer();
                if (frmMailer.ShowDialog() == DialogResult.OK)
                {
                    toFile = false;
                    fileName = frmMailer.FileName;
                    mailAddress = frmMailer.MailAddress;
                }
                else
                    return;
            }
            //version, author, TDSName, date
            int.TryParse(txtVersion.Text, out version);
            approver = txtApprove1.Text + (txtApprove2.Text.Equals("") ? "" : "\v" + txtApprove2.Text);
            QAer = txtQA1.Text + (txtQA2.Text.Equals("") ? "" : "\v" + txtQA2.Text);
            reviewer = txtReview1.Text + (txtReview2.Text.Equals("") ? "" : "\v" + txtReview2.Text) + (txtReview3.Text.Equals("") ? "" : "\v" + txtReview3.Text);
            author = txtAuthor.Text;
            TdsName = txtTDSName.Text;
            date = dtpDate.Value;
            //sort
            tdsFile.Sort();
            bgdWork.RunWorkerAsync(new object[] { fileName, mailAddress, toFile });
        }

        private void bgdWork_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            bgdWork.ReportProgress(0, ""); //initiate
            string fileName = (e.Argument as object[])[0].ToString(); ;
            string mailAddress = (e.Argument as object[])[1].ToString(); ;
            bool toFile = bool.Parse((e.Argument as object[])[2].ToString());
            try
            {
                //convert files
                for (int i = 0; i < tdsFile.Count; i++)
                {
                    bgdWork.ReportProgress(99, "Handling file " + TDS_ITEM_LIST[tdsFile[i].TdsIndex] + "...");
                    MemoryStream ms = new MemoryStream();
                    if (File.Exists(tdsFile[i].FileName))
                    {
                        try
                        {
                            //variable for office
                            object srcFile = tdsFile[i].FileName;
                            object dstFile = Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
                            //convert *.doc and *.xls
                            switch (tdsFile[i].FileType)
                            {
                                case ".doc":
                                case ".docx":
                                    // Create a new Microsoft Word application object
                                    Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                                    //open file
                                    Microsoft.Office.Interop.Word.Document doc = word.Documents.Open(ref srcFile);
                                    //save to pdf
                                    object fileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;
                                    doc.SaveAs2(ref dstFile, ref fileFormat);
                                    //close file
                                    object saveChanges = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
                                    doc.Close(ref saveChanges);
                                    Marshal.FinalReleaseComObject(doc);
                                    doc = null;
                                    word.Quit();
                                    Marshal.FinalReleaseComObject(word);
                                    word = null;
                                    //copy pdf to memorystream
                                    using (FileStream fs = new FileStream(dstFile.ToString(), FileMode.Open, FileAccess.Read))
                                        fs.CopyTo(ms);
                                    File.Delete(dstFile.ToString());
                                    break;
                                case ".xls":
                                case ".xlsx":
                                    // Create a new Microsoft Excel application object
                                    Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                                    //open file
                                    Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Open(srcFile.ToString());
                                    //save to pdf
                                    workbook.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF, dstFile);
                                    //close file
                                    workbook.Close(false);
                                    Marshal.FinalReleaseComObject(workbook);
                                    workbook = null;
                                    excel.Quit();
                                    Marshal.FinalReleaseComObject(excel);
                                    excel = null;
                                    //copy pdf to memorystream
                                    using (FileStream fs = new FileStream(dstFile.ToString(), FileMode.Open, FileAccess.Read))
                                        fs.CopyTo(ms);
                                    File.Delete(dstFile.ToString());
                                    break;
                                case ".ppt":
                                case ".pptx":
                                    // Create a new Microsoft Word application object
                                    Microsoft.Office.Interop.PowerPoint.Application powerpoint = new Microsoft.Office.Interop.PowerPoint.Application();
                                    //open file
                                    Microsoft.Office.Interop.PowerPoint.Presentation presentation = powerpoint.Presentations.Open(srcFile.ToString(), Microsoft.Office.Core.MsoTriState.msoTrue, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoFalse);
                                    //save to pdf
                                    presentation.ExportAsFixedFormat(dstFile.ToString(), Microsoft.Office.Interop.PowerPoint.PpFixedFormatType.ppFixedFormatTypePDF);
                                    //close file
                                    presentation.Close();
                                    Marshal.FinalReleaseComObject(presentation);
                                    presentation = null;
                                    powerpoint.Quit();
                                    Marshal.FinalReleaseComObject(powerpoint);
                                    powerpoint = null;
                                    //copy pdf to memorystream
                                    using (FileStream fs = new FileStream(dstFile.ToString(), FileMode.Open, FileAccess.Read))
                                        fs.CopyTo(ms);
                                    File.Delete(dstFile.ToString());
                                    break;
                                case ".pdf":
                                    using (FileStream fs = new FileStream(tdsFile[i].FileName, FileMode.Open, FileAccess.Read))
                                        fs.CopyTo(ms);
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.StackTrace + "\n" + ex.Message);
                        }
                        finally
                        {
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                        }
                    }
                    tdsFile[i].Data = ms;
                }
                //create streams
                bgdWork.ReportProgress(99, "Merging file...");
                MemoryStream msContent = MergeSinglePdfs(tdsFile);
                bgdWork.ReportProgress(99, "Creating TOC...");
                MemoryStream msTOC = CreateTOC();
                List<MemoryStream> streams = new List<MemoryStream>();
                streams.Add(msTOC);
                streams.Add(msContent);
                MemoryStream finalStream = new MemoryStream();
                using (Document doc = new Document())
                {
                    using (PdfCopy writer = new PdfCopy(doc, finalStream))
                    {
                        List<Dictionary<string, object>> outline = new List<Dictionary<string, object>>();
                        int pageCounter = -1;
                        writer.CloseStream = false;
                        foreach (MemoryStream ms in streams)
                        {
                            doc.Open();
                            ms.Position = 0; //reset ms position to zero
                            IList<Dictionary<string, object>> tempBookmarks;
                            //merge pdf and add header/footer
                            using (PdfReader reader = new PdfReader(ms))
                            {
                                //handle exist bookmarks
                                tempBookmarks = SimpleBookmark.GetBookmark(reader);
                                SimpleBookmark.ShiftPageNumbers(tempBookmarks, pageCounter + 1, null);
                                if (tempBookmarks != null)
                                    outline.AddRange(tempBookmarks);
                                for (int i = 1; i <= reader.NumberOfPages; i++)
                                {
                                    bgdWork.ReportProgress(99, "Adding page " + pageCounter);
                                    PdfImportedPage page = writer.GetImportedPage(reader, i);
                                    PdfCopy.PageStamp stamper = writer.CreatePageStamp(page);
                                    float width = reader.GetPageSize(i).Width;
                                    float height = reader.GetPageSize(i).Height;
                                    //detect rotation
                                    if (reader.GetPageRotation(i) == 90 || reader.GetPageRotation(i) == 270)
                                    {
                                        float temp = width;
                                        width = height;
                                        height = temp;
                                    }
                                    if (pageCounter > 0)
                                    {
                                        //add header
                                        Image logo = Image.GetInstance(
                                            new System.Drawing.Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("TDSCreator.Resources.logo.jpg")),
                                            System.Drawing.Imaging.ImageFormat.Jpeg);
                                        logo.ScaleAbsoluteWidth(width * 0.3024f);
                                        logo.ScaleAbsoluteHeight(height * 0.0343f);
                                        logo.SetAbsolutePosition(width * 0.0095f, height * 0.9590f);
                                        logo.Alignment = Element.ALIGN_LEFT;
                                        PdfContentByte cb = stamper.GetOverContent();
                                        cb.AddImage(logo);
                                        Paragraph p = new Paragraph();
                                        /*
                                        p.Add(new Phrase("文件編號：", new Font(fontMsjh, 8)));
                                        p.Add(new Phrase("76-PC-08-01", new Font(fontCalibri, 8)));
                                        ColumnText.ShowTextAligned(
                                            stamper.GetOverContent(),
                                            Element.ALIGN_RIGHT,
                                            p,
                                            width * 0.9405f, height * 0.9590f,
                                            0);
                                        */
                                        //add footer
                                        ColumnText.ShowTextAligned(
                                            stamper.GetOverContent(),
                                            Element.ALIGN_RIGHT,
                                            new Phrase(
                                                "以上內文資訊具有機密性，僅限於東聯化學有限公司內部使用。",
                                                new Font(fontMsjh, 9)),
                                            width * 0.9905f, height * 0.0067f + 8f,
                                            0);
                                        ColumnText.ShowTextAligned(
                                            stamper.GetOverContent(),
                                            Element.ALIGN_RIGHT,
                                            new Phrase(
                                                "The above containing confidential information for OUCC internal use only.",
                                                new Font(fontCalibri, 9)),
                                            width * 0.9905f, height * 0.0067f,
                                            0);
                                        //add page number
                                        ColumnText.ShowTextAligned(
                                            stamper.GetOverContent(),
                                            Element.ALIGN_CENTER,
                                            new Phrase(pageCounter.ToString(), new Font(fontCalibriBd, 10)),
                                            width * 0.5f, height * 0.0296f,
                                            0);
                                        //commit
                                        stamper.AlterContents();
                                    }
                                    pageCounter++;
                                    //add page
                                    writer.AddPage(page);
                                }
                                //merge Acro field
                                PRAcroForm form = reader.AcroForm;
                                if (form != null)
                                    writer.AddDocument(reader);
                                reader.Close();
                            }
                            ms.Dispose();
                        }
                        writer.Outlines = outline;
                        doc.Close();
                    }
                }
                if (toFile)
                {
                    bgdWork.ReportProgress(99, "Saving file...");
                    //write to file
                    using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite))
                    {
                        byte[] bytes; //byte array for combined stream
                        bytes = finalStream.ToArray();
                        fs.Write(bytes, 0, bytes.Length);
                        MessageBox.Show("Save to file success");
                    }
                }
                else
                {
                    bgdWork.ReportProgress(99, "Sending mail...");
                    finalStream.Position = 0; //reset
                    Attachment attach = new Attachment(finalStream, fileName);
                    attach.ContentDisposition.FileName = fileName + ".pdf";
                    MailMessage myMail = new MailMessage();
                    myMail.Subject = fileName + " - " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    myMail.From = new MailAddress("jackylalala.report@gmail.com", "TDS Mailer");
                    myMail.To.Add(mailAddress);
                    myMail.SubjectEncoding = Encoding.UTF8;
                    myMail.IsBodyHtml = true;
                    myMail.BodyEncoding = Encoding.UTF8;
                    myMail.Body = fileName + " - " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    myMail.Attachments.Add(attach);
                    try
                    {
                        using (SmtpClient mySmtp = new SmtpClient())
                        {
                            mySmtp.Port = 587;
                            mySmtp.Credentials = new NetworkCredential("jackylalala.report@gmail.com", "7QVT6-t2738");
                            mySmtp.Host = "smtp.gmail.com";
                            mySmtp.EnableSsl = true;
                            mySmtp.Send(myMail);
                        }
                        MessageBox.Show("Sending mail success");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Sending mail fail");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\n" + ex.Message);
            }
        }

        private void bgdWork_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage==0) //initiate
            {
                btnStart.Enabled = false;
                btnStart_Mail.Enabled = false;
                pnlFiles.Visible = false;
                lblStatus.Text = "Start merging";
                lblStatus.Visible = true;
            }
            lblStatus.Text = e.UserState.ToString();
            Application.DoEvents();
        }

        private void bgdWork_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            btnStart.Enabled = true;
            btnStart_Mail.Enabled = true;
            pnlFiles.Visible = true;
            lblStatus.Visible = false;
        }
        #endregion

        #region | Methods |
        private static MemoryStream MergeSinglePdfs(List<TdsItem> tdsFile)
        {
            MemoryStream ms = new MemoryStream();
            using (Document doc = new Document()) //creation of a document-object
            {
                using (PdfCopy writer = new PdfCopy(doc, ms)) //create a writer that listens to the document
                {
                    writer.CloseStream = false; //avoid writer to close stream(IMPORTANT!!)
                    writer.SetMergeFields();
                    //outline
                    IList<Dictionary<string, object>> outlines = new List<Dictionary<string, object>>();
                    int page = 1;
                    //open the document
                    doc.Open();
                    int flag = -1; //avoid to add the same bookmark
                    foreach (TdsItem item in tdsFile)
                    {
                        if (item.Data.Length > 0)
                        {
                            item.Data.Position = 0; //reset
                            PdfReader reader = new PdfReader(item.Data);
                            if (!reader.IsOpenedWithFullPermissions)
                                MessageBox.Show("Please check the permission of file " + item.SafeFileName);
                            writer.AddDocument(reader);
                            item.Page = reader.NumberOfPages;
                            //add bookmark
                            if (item.TdsIndex != flag)
                            {
                                Dictionary<string, object> bookmark = new Dictionary<string, object>();
                                bookmark.Add("Title", TDS_ITEM_LIST[item.TdsIndex]);
                                bookmark.Add("Action", "GoTo");
                                bookmark.Add("Page", string.Format("{0:D} FitH 846", page));
                                outlines.Add(bookmark);
                                flag = item.TdsIndex;
                            }
                            page += item.Page;
                        }
                    }
                    writer.Outlines = outlines;
                    doc.Close();
                }
            }
            return ms;
        }

        public static MemoryStream CreateTOC()
        {
            MemoryStream ms = new MemoryStream();
            string tempDoc = Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
            string tempPdf = Path.GetTempPath() + Guid.NewGuid().ToString() + ".docx";
            //get template
            try
            {
                using (MemoryStream ms2 = new MemoryStream())
                {
                    Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("TDSCreator.Resources.template.docx");
                    s.CopyTo(ms2);
                    byte[] bytes = ms2.ToArray();
                    using (FileStream fs = new FileStream(tempDoc, FileMode.Create, FileAccess.ReadWrite))
                        fs.Write(bytes, 0, bytes.Length);
                }
                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = word.Documents.Open(tempDoc);
                doc.Activate();
                FindAndReplace(word, "(ver)", "V" + version.ToString());
                FindAndReplace(word, "(tdsName)", TdsName);
                FindAndReplace(word, "(approve)", approver);
                FindAndReplace(word, "(QA)", QAer);
                FindAndReplace(word, "(review)", reviewer);
                FindAndReplace(word, "(author)", author);
                FindAndReplace(word, "(yyyy年MM月dd日)", date.ToString("yyyy年MM月dd日"));
                //create TOC
                int counter = 0;
                for (int i = 0; i < tdsFile.Count; i++)
                {
                    //replace will ONLY PERFORM ONCE!!
                    FindAndReplace(word, "item" + tdsFile[i].TdsIndex, string.Format("{0:00}", (counter + 1).ToString()));
                    counter += tdsFile[i].Page;
                }
                //claer page tag
                for (int i = 0; i < TDS_ITEM_LIST.Length; i++)
                    FindAndReplace(word, "item" + i, "");
                //save to pdf
                object dstFile = tempPdf;
                object fileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;
                doc.SaveAs2(ref dstFile, ref fileFormat);
                object saveChanges = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
                doc.Close(ref saveChanges);
                doc = null;
                word.Quit();
                word = null;
                //convert to stream
                using (FileStream fs = new FileStream(tempPdf, FileMode.Open, FileAccess.Read))
                    fs.CopyTo(ms);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.StackTrace + "\n" + ex.Message);
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(tempPdf);
                File.Delete(tempDoc);
            }
            return ms;
        }

        private static void FindAndReplace(Microsoft.Office.Interop.Word.Application doc, object findText, object replaceWithText)
        {
            //options
            object matchCase = false;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;
            //execute find and replace
            doc.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace,
                ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
        }

        private void SelectFile(object sender, EventArgs e)
        {
            int index = int.Parse(((Button)sender).Tag.ToString());
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Choose the file for " + lblTitle[index].Text;
            ofd.Filter = "All supported file(*.pdf;*.doc;*.docx;*.xls;*.xlsx;*.ppt;*.pptx)|*.pdf;*.doc;*.docx;*.xls;*.xlsx;*.ppt;*.pptx";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in ofd.FileNames)
                {
                    TdsItem temp = new TdsItem(index, file);
                    //if not find in list, add it
                    if (!tdsFile.Any(temp.Equals))
                    {
                        tdsFile.Add(temp);
                        lstFileName[index].Items.Add(temp);
                    }
                }
                lblCount[index].Text = lstFileName[index].Items.Count.ToString();
            }
        }

        #endregion
    }
}
