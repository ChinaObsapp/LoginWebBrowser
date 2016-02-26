using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace LoginBrowser
{
    public partial class BrowserForm : Form
    {
        [DllImport("shell32.dll")]
        public static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

        public BrowserForm()
        {
            InitializeComponent();
        } 
        private void goButton_Click(object sender, EventArgs e)
        {
            String url = URLTextbox.Text.Trim();
            if (String.IsNullOrEmpty(url))
            {
                MessageBox.Show("请输入网址或者选择平台");
                return;
            }
            webBrowser1.Navigate(url);
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            URLTextbox.Text = webBrowser1.Url.ToString();
        }

        private void URLTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                goButton_Click(sender, e);
            }
        }

        private void clearCacheButton_Click(object sender, EventArgs e)
        {
            try
            {
               ShellExecute(IntPtr.Zero, "open", "rundll32.exe", " InetCpl.cpl,ClearMyTracksByProcess 255", "", 0); 
            }
            catch (Exception e1)
            {
                MessageBox.Show("清理失败:" + e1.StackTrace);
            }
        }

        private void platformComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (platformComboBox.SelectedIndex)
            {
                case 0:
                    URLTextbox.Text = "http://www.douyutv.com/directory/all";
                    return;
                case 2:
                    URLTextbox.Text = "http://www.panda.tv/all";
                    return;
                case 1:
                    URLTextbox.Text = "http://longzhu.com/channels/all";
                    return;
            }
        }
    }
}
