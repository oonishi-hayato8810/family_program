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
    public partial class Form1 : Form
    {
        /// <summary>
        /// Form1の処理
        /// </summary>
        public Form1()
        {
            InitializeComponent();
           
        }

        /// <summary>
        /// 画面表示時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            categoryDataSet1.CategoryDataTable.AddCategoryDataTableRow("給料", "入金");　//型付きデータセット(categoryDataSet1)のデータテーブル荷「給料」という分類データを準備する処理を行う
            categoryDataSet1.CategoryDataTable.AddCategoryDataTableRow("食費", "出金");
            categoryDataSet1.CategoryDataTable.AddCategoryDataTableRow("雑費", "出金");
            categoryDataSet1.CategoryDataTable.AddCategoryDataTableRow("住居", "出金");
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        /// <summary>
        /// 追加ボタンのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddData();
        }

        /// <summary>
        /// AddData()のサブルーチン
        /// 登録画面で登録ボタンをクリックしたときに、入力されたデータを型付きデータセットに格納する
        /// </summary>
        private void AddData()
        {
            ItemForm frmItem = new ItemForm(categoryDataSet1); //登録フォームを作成
            DialogResult drRet = frmItem.ShowDialog(); //登録画面のフォームをモーダル画面で表示
            if (drRet == DialogResult.OK) //登録画面が[登録]ボタンでとじられたことを確認する
            {
                moneyDataSet.moneyDataTable.AddmoneyDataTableRow(
                    frmItem.monCalendar.SelectionRange.Start,
                    frmItem.cmbCategory.Text,
                    frmItem.txtItem.Text,
                    int.Parse(frmItem.mtxtMoney.Text),
                    frmItem.txtRemarks.Text);
            }
        }
        /// <summary>
        /// 終了ボタンのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 編集EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 編集メニューの追加イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 追加AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddData();
        }
        /// <summary>
        /// ファイルメニューの終了イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// ファイルへ出力する処理
        /// </summary>
        private void SaveData()
        {
            string path = "MoneyData.csv"; //ファイル名
            string strData = ""; //1行分のデータ
            System.IO.StreamWriter sw = new System.IO.StreamWriter(
                                    path,
                                    false,
                                    System.Text.Encoding.Default); // ファイルを作成するためのクラスのインスタンスを作成する

            foreach (MoneyDataSet.moneyDataTableRow drMoney in moneyDataSet.moneyDataTable)// レコードの数だけループさせる
            {
                strData = drMoney.日付.ToShortDateString() + "," // 1行分の値を保持する変数に現在行のデータを代入する
                        + drMoney.分類 + ","
                        + drMoney.品名 + ","
                        + drMoney.金額.ToString() + ","
                        + drMoney.備考;
                sw.WriteLine(strData);


            }
            sw.Close();
        }

        /// <summary>
        /// 生成された保存処理のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        /// <summary>
        /// 生成されたフォームを閉じる処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData();
        }

        /// <summary>
        /// CSVファイルがなくなるまで読み込む処理を繰り返す
        /// </summary>
        private void LoadData()
        {
            string path = "MoneyData.csv";
            string delimStr = ",";
            char[] delimiter = delimStr.ToCharArray();
            string[] strData;
            string strLine;
            bool fileExists = System.IO.File.Exists(path);
            if (fileExists)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(
                                                 path,
                                                 System.Text.Encoding.Default);
                while (sr.Peek() >= 0)  //「読み取り可能な文字があるか」を調べてくれるメソッド
                                        // 「-1」が返ってくるとファイルにデータがないことを表す
                {
                    strLine = sr.ReadLine();
                    strData = strLine.Split(delimiter);
                    moneyDataSet.moneyDataTable.AddmoneyDataTableRow(
                               DateTime.Parse(strData[0]),
                               strData[1],
                               strData[2],
                               int.Parse(strData[3]),
                               strData[4]);
                }
                sr.Close();
            }
        }

        /// <summary>
        /// データを追加・変更する処理
        /// </summary>
        private void UpdateData()
        {
                int nowRow = dgv.CurrentRow.Index;
                DateTime oldDate = DateTime.Parse(dgv.Rows[nowRow].Cells[0].Value.ToString());
                string oldCategory = dgv.Rows[nowRow].Cells[1].Value.ToString();
                string oldItem = dgv.Rows[nowRow].Cells[2].Value.ToString();
                int oldMoney = int.Parse(dgv.Rows[nowRow].Cells[3].Value.ToString());
                string oldRemarks = dgv.Rows[nowRow].Cells[4].Value.ToString();
                ItemForm frmItem = new ItemForm(categoryDataSet1,
                                                oldDate,
                                                oldCategory,
                                                oldItem,
                                                oldMoney,
                                                oldRemarks);
                DialogResult drRet = frmItem.ShowDialog();
                if (drRet == DialogResult.OK)
                {
                    dgv.Rows[nowRow].Cells[0].Value = frmItem.monCalendar.SelectionRange.Start;
                    dgv.Rows[nowRow].Cells[1].Value = frmItem.cmbCategory.Text;
                    dgv.Rows[nowRow].Cells[2].Value = frmItem.txtItem.Text;
                    dgv.Rows[nowRow].Cells[3].Value = int.Parse(frmItem.mtxtMoney.Text);
                    dgv.Rows[nowRow].Cells[4].Value = frmItem.txtRemarks.Text;

                }
        }
        

        
        
        /// <summary>
        /// UpdateData()サブルーチンを呼び出すコード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonChange_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        /// <summary>
        /// 削除処理
        /// </summary>
        private void DeleteData()
        {
            int nowRow = dgv.CurrentRow.Index;
            dgv.Rows.RemoveAt(nowRow); // 現在行を削除
        }

        /// <summary>
        /// DeleteData()サブルーチンを呼び出すコード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// データを編集する処理
        /// </summary>
        private void CalcSummary()
        {
            string expression;
            summaryDataSet.SumDataTable.Clear();
            foreach (MoneyDataSet.moneyDataTableRow drMoney in moneyDataSet.moneyDataTable)
            {
                expression = "日付= '" + drMoney.日付.ToShortDateString() + "'";
                SummaryDataSet.SumDataTableRow[] curDR = (SummaryDataSet.SumDataTableRow[])
                summaryDataSet.SumDataTable.Select(expression);
                if (curDR.Length == 0)
                {
                    CategoryDataSet.CategoryDataTableRow[] selectedDataRow;
                    selectedDataRow = (CategoryDataSet.CategoryDataTableRow[])
                        categoryDataSet1.CategoryDataTable.Select("分類='" + drMoney.分類 + "'");

                    if (selectedDataRow[0].入出金分類 == "入金")
                    {
                        summaryDataSet.SumDataTable.AddSumDataTableRow(drMoney.日付, drMoney.金額, 0);
                    }
                    else if (selectedDataRow[0].入出金分類 == "出金")
                    {
                        summaryDataSet.SumDataTable.AddSumDataTableRow(drMoney.日付, drMoney.金額, 0);
                    }
                }
                else
                { 
                
                    CategoryDataSet.CategoryDataTableRow[] selectedDataRow;
                    selectedDataRow = (CategoryDataSet.CategoryDataTableRow[])
                            categoryDataSet1.CategoryDataTable.Select("分類='" + drMoney.分類 + "'");

                    if (selectedDataRow[0].入出金分類 == "入金")
                    {
                        curDR[0].入金合計 += drMoney.金額;
                    }
                    else if (selectedDataRow[0].入出金分類 == "出金")
                    {
                        curDR[0].出金合計 += drMoney.金額;
                    }
                }
            }
        }

        /// <summary>
        /// CalcSummary()サブルーチンを呼び出すコード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSummary();
        }

        /// <summary>
        /// 一覧表示タブを集計表示タブの画面を切り替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 一覧表示LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabList);
        }

        private void 集計表示SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabSummary);
        }
    }
}
