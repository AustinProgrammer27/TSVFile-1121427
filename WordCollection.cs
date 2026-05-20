using System.Collections.ObjectModel;

namespace TSVFile
{
    /// <summary>
    /// 自訂單字集合類別，用來集中管理 WordItem 物件。
    /// </summary>
    public class WordCollection : Collection<WordItem>
    {
        /// <summary>
        /// 從字串陣列載入資料。
        /// </summary>
        /// <param name="lines">輸入的單字字串陣列</param>
        public void LoadFromStringArray(string[] lines)
        {
            Clear();

            foreach (string line in lines)
            {
                WordItem item = new WordItem(line);

                if (!string.IsNullOrWhiteSpace(item.Word))
                {
                    Add(item);
                }
            }
        }
    }
}
