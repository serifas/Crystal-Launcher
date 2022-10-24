using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace UpdateTest
{
    public partial class Form1 : Form
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public Form1()
        {
            InitializeComponent();
            this.TransparencyKey = Color.Green;
            button1.ForeColor = Color.Transparent;
            button1.BackColor = Color.Transparent;

            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;


            button2.ForeColor = Color.Transparent;
            button2.BackColor = Color.Transparent;

            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button2.FlatAppearance.MouseDownBackColor = Color.Transparent;

            button3.ForeColor = Color.Transparent;
            button3.BackColor = Color.Transparent;

            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button3.FlatAppearance.MouseDownBackColor = Color.Transparent;



        }
        private void Form1_MouseDown(object sender,   System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.button_h;
        }
        void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.button_p;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            updater1.CheckForUpdates();

           
        }
        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.ForeColor = Color.Transparent;
            button1.BackColor = Color.Transparent;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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
        public void OnApplicationExit(object sender, EventArgs e)
        {
            string[] DataenFileEntries = Directory.GetFiles(@".");
            foreach (string fileName in DataenFileEntries)
            {
                if (fileName.Contains(".old"))
                {
                    File.Delete(fileName);
                }
            }
        }
        private void button4_Click(object sender, MouseEventArgs e)
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
    }
}
