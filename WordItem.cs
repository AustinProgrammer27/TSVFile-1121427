using System;
using System.Linq;

namespace TSVFile
{
    /// <summary>
    /// 代表 WordCards.txt 中的一筆 TSV 單字資料。
    /// 欄位順序：Word、Phonogram、SoundPath、Explain。
    /// </summary>
    public class WordItem
    {
        public string Word { get; set; } = string.Empty;
        public string Phonogram { get; set; } = string.Empty;
        public string SoundPath { get; set; } = string.Empty;
        public string Explain { get; set; } = string.Empty;

        public WordItem()
        {
        }

        /// <summary>
        /// 建構子：將單行 TSV 字串轉換成 WordItem。
        /// </summary>
        /// <param name="str">單行的單字資料</param>
        public WordItem(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return;
            }

            string[] strLists = str.Split('\t');
            if (strLists.Length >= 3)
            {
                Word = strLists[0].Trim();
                Phonogram = strLists[1].Trim();
                SoundPath = strLists[2].Trim();
                Explain = string.Join(Environment.NewLine, strLists.Skip(3)).Trim();
            }
            else if (strLists.Length > 0)
            {
                Word = strLists[0].Trim();
            }
        }

        /// <summary>
        /// 覆寫 ToString() 可將物件自動轉換為單字字串。
        /// </summary>
        public override string ToString()
        {
            return Word;
        }
    }
}
