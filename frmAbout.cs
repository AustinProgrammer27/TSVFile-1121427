using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace TSVFile
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
            LoadAssemblyInfo();
            LoadLogo();
        }

        private void LoadAssemblyInfo()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly.Location);

            lblProductName.Text = string.IsNullOrWhiteSpace(info.ProductName)
                ? "TSV資料檔讀取程式"
                : info.ProductName;
            lblVersion.Text = "版本 " + (string.IsNullOrWhiteSpace(info.ProductVersion) ? "1.0.0.0" : info.ProductVersion);
            lblCopyright.Text = string.IsNullOrWhiteSpace(info.LegalCopyright)
                ? "Copyright © 2026"
                : info.LegalCopyright;
            lblDescription.Text = "讀取 TSV / TXT 格式的 WordCards 資料，並以 ListView 顯示單字、音標、音檔路徑與解釋。";
        }

        private void LoadLogo()
        {
            string path = Path.Combine(Application.StartupPath, "WordCards_Logo.png");
            if (File.Exists(path))
            {
                picLogo.Image = Image.FromFile(path);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
