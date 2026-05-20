using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TSVFile
{
    public partial class frmTSVFile : Form
    {
        /// <summary>
        /// 單字清單
        /// </summary>
        private readonly WordCollection _WordList = new WordCollection();

        /// <summary>
        /// 關於視窗
        /// </summary>
        private readonly frmAbout about = new frmAbout();

        public frmTSVFile()
        {
            InitializeComponent();
        }

        private void frmTSVFile_Load(object sender, EventArgs e)
        {
            tsslMessage.Text = "請開啟檔案";
        }

        private void tsmiOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "TSV files (*.tsv)|*.tsv|Text files (*.txt)|*.txt|All files (*.*)|*.*";
                ofd.Title = "開啟檔案";
                ofd.InitialDirectory = Application.StartupPath;

                DialogResult dr = ofd.ShowDialog(this);
                if (dr != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    // 讀取檔案並且將每一行的資料放入字串陣列。
                    // WordCards.txt 是 Unicode 文字，StreamReader 會依 BOM 自動判斷實際編碼。
                    string[] lines = File.ReadAllLines(ofd.FileName, Encoding.UTF8);

                    // 將字串陣列的資料載入到 WordCollection 物件中。
                    _WordList.LoadFromStringArray(lines);

                    // 將 WordCollection 物件中的資料載入到 ListView 中。
                    UpdateListView();
                    tsslMessage.Text = $"{_WordList.Count} 筆單字載入完成：{Path.GetFileName(ofd.FileName)}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("讀取檔案時發生錯誤：" + ex.Message,
                                    "錯誤",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    tsslMessage.Text = "檔案讀取失敗";
                }
            }
        }

        /// <summary>
        /// 更新 ListView 的內容。
        /// </summary>
        private void UpdateListView()
        {
            lvwWord.BeginUpdate();
            lvwWord.Items.Clear();

            foreach (WordItem item in _WordList)
            {
                ListViewItem lvi = new ListViewItem(item.Word);
                lvi.SubItems.Add(item.Phonogram);
                lvi.SubItems.Add(item.SoundPath);
                lvi.SubItems.Add(item.Explain);
                lvwWord.Items.Add(lvi);
            }

            lvwWord.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            EnsureMinimumColumnWidth();
            lvwWord.EndUpdate();
        }

        /// <summary>
        /// 避免內容太短時欄寬變得過窄。
        /// </summary>
        private void EnsureMinimumColumnWidth()
        {
            int[] minimumWidths = { 120, 120, 220, 480 };
            for (int i = 0; i < lvwWord.Columns.Count && i < minimumWidths.Length; i++)
            {
                if (lvwWord.Columns[i].Width < minimumWidths[i])
                {
                    lvwWord.Columns[i].Width = minimumWidths[i];
                }
            }
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            about.ShowDialog(this);
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmTSVFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("確定要離開嗎?",
                                             "離開",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
