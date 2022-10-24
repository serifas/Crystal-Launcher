using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalUpdater.Data;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;

namespace CrystalUpdater.UI
{
    public partial class frmUpdater : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        int index = 0;
        public UpdateSaveFile localInfoFile;
        private String baseUrl;

        public frmUpdater(UpdateSaveFile file, String baseUrl)
        {
            InitializeComponent();

            localInfoFile = file;

            

            this.baseUrl = baseUrl;

            foreach (UpdateFileInfo fileInfo in file.UpdateFileCollection)
            {
                if (!Directory.Exists(@"." + fileInfo.Path))
                {
                    Directory.CreateDirectory(@"." + fileInfo.Path);
                }
                string FilePath = @"." + fileInfo.Path + fileInfo.Name;
                ListViewItem lvItem = new ListViewItem(new String[] { fileInfo.Name, "Waiting...", fileInfo.Path, fileInfo.Description });
                lvItems.Items.Add(lvItem);

                if (File.Exists(FilePath))
                {                    
                    FileInfo fi = new FileInfo(FilePath);
                    if (fi.Length == int.Parse(fileInfo.Description) && fi.Name.Contains(".exe") != true)
                    {
                        var item = lvItems.FindItemWithText(fi.Name);
                        lvItems.Items.Remove(item);
                    }
                    
                    
                }
              
                  
                

            }
            
               
              
        }
        private void frmUpdater_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (btnInstall.Text == "Finish")
            {
                Application.Restart();
                foreach (var file in localInfoFile.UpdateFileCollection)
                {
                    string[] DataenFileEntries = Directory.GetFiles(@"." +file.Path);
                    foreach (string fileName in DataenFileEntries)
                    {
                        if (fileName.Contains(".old"))
                        {
                            File.Delete(fileName);
                        }
                    }

                }
            }
            else
            {
                btnInstall.Enabled = false;
                DownloadFile();
            }
        }

        private void SetStatus(String p)
        {
            lblStatus.Text = String.Format("Status: {0}", p);
        }

        private void DownloadFile()
        {
            WebClient downloadClient = new WebClient();

            downloadClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(downloadClient_DownloadProgressChanged);
            downloadClient.DownloadFileCompleted += new AsyncCompletedEventHandler(downloadClient_DownloadFileCompleted);

            ListViewItem currItem = lvItems.Items[index];

            String name = currItem.SubItems[0].Text;
            String path = currItem.SubItems[2].Text;
            SetStatus("Downloading " + name + "...");

            currItem.SubItems[1].Text = "Downloading...";

            String local = String.Format(@"{0}", name);
            String online = String.Format("{0}{1}", baseUrl, name);

            if (File.Exists(local))
            {
                if (File.Exists(local + ".old"))
                    File.Delete(local + ".old");

                File.Move(local, local + ".old");
            }
            downloadClient.DownloadFileAsync(new Uri(online), @".\"+path + name);
        }

        void downloadClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            ListViewItem currItem = lvItems.Items[index];
            currItem.SubItems[1].Text = "Downloaded";

            panel3.Width = 0;
            index += 1;
            
            if (lvItems.Items.Count - 1 >= index)
            {
                DownloadFile();
            }
            else
            {
                SetStatus("Finished!");
                btnInstall.Text = "Finish";
                btnInstall.Enabled = true;
            }
        }

        void downloadClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            panel3.Width = e.ProgressPercentage * 5;
        }

        private void pbDownload_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] DataenFileEntries = Directory.GetFiles(@".");
            foreach (string fileName in DataenFileEntries)
            {
                if (fileName.Contains(".old"))
                {
                    File.Delete(fileName);
                }
            }
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void frmUpdater_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        private void lvItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
