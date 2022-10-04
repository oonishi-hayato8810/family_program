using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHousekeepingBook
{
    public partial class ItemForm : Form
    {
        /// <summary>
        /// 追加ボタンで呼び出される登録画面の処理
        /// </summary>
        /// <param name="dsCategory"></param>
     　 public ItemForm(CategoryDataSet dsCategory)
        {
            InitializeComponent();
            categoryDataSet.Merge(dsCategory);
        }

        /// <summary>
        /// データを登録画面の各コントロールにセットする
        /// </summary>
        /// <param name="dsCategory"></param>
        /// <param name="nowDate"></param>
        /// <param name="category"></param>
        /// <param name="item"></param>
        /// <param name="money"></param>
        /// <param name="remarks"></param>
        public ItemForm(CategoryDataSet dsCategory, // 最初から書かれておらず自分で()の中を追記した   第一引数、分類一覧(型付きデータセット型)
                        DateTime nowDate,           // 第二引数、日付(日付型)
                        string category,            // 第三引数、分類(文字列型)
                        string item,                // 第四引数、品名(文字列型)
                        int money,                  // 第五引数、金額(整数型)
                        string remarks)             // 第六引数、備考(文字列型)
            
        {
            InitializeComponent();  // 初期化処理
            categoryDataSet.Merge(dsCategory); //データセットは「=」では代入できないので、Merge()メソッドを利用している
            monCalendar.SetDate(nowDate);
            cmbCategory.Text = category;
            txtItem.Text = item;
            mtxtMoney.Text = money.ToString();
            txtRemarks.Text = remarks;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtltem_TextChanged(object sender, EventArgs e)
        {

        }

        private void mtxtMoney_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtRemarks_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {

        }
    }
}
