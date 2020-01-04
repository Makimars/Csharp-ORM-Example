using System;
using System.Data;
using System.Windows.Forms;
using Csharp_ORM_Example;

namespace SellersAndArticles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Repository.setConnString(Properties.Settings.Default.connString);
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            ArticlesRepository artRepo = ArticlesRepository.getInstance();

            EntityList list = artRepo.getList();

            Article[] entities = (Article[]) list.fetch();

            foreach (Article ar in entities)
            {
                richTextBox1.Text += ar.Name + " " + ar.Id + "\n";
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            SellersRepository sellRepo = SellersRepository.getInstance();

            EntityList list = sellRepo.getList();

            Seller[] sellers = (Seller[]) list.fetch();
            
            foreach(Seller s in sellers)
            {
                richTextBox1.Text += s.Name + " " + s.Id + "\n";
            }
            
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            SellersRepository sellRepo = SellersRepository.getInstance();
            ArticlesRepository artRepo = ArticlesRepository.getInstance();

            EntityList sellerList = sellRepo.getList();
            EntityList articleList = artRepo.getList();

            DataList dataRel = sellerList.toDataList();

            dataRel.joinWith(articleList, "Id", "seller_id");

            DataTable dt = dataRel.fetch();

            DataRowCollection rows = dt.Rows;
            DataColumnCollection columns = dt.Columns;

            for (int i = 0; i < columns.Count; i++)
            {
                DataColumn column = columns[i];

                richTextBox1.Text += column.ToString() + " ";
            }

            richTextBox1.Text += "\n";

            for (int i = 0; i < rows.Count; i++)
            {
                object[] items = rows[i].ItemArray;

                for (int a = 0; a < items.Length; a++)
                {
                    richTextBox1.Text += items[a].ToString() + " ";
                }

                richTextBox1.Text += "\n";
            }

        }
        private void Button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            StorageRepository storageRepo = StorageRepository.getInstance();

            EntityList list = storageRepo.getList();

            Storage[] storage = (Storage[])list.fetch();

            foreach (Storage s in storage)
            {
                richTextBox1.Text += s.Id + " " + s.ArticleId + " " + s.Amount + "\n";
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            TransactionsRepository tranRepo = TransactionsRepository.getInstance();

            EntityList list = tranRepo.getList();

            Transaction[] transactions = (Transaction[])list.fetch();

            foreach (Transaction t in transactions)
            {
                richTextBox1.Text += t.Id + " " + t.ArticleId + " " + t.Amount + "\n";
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            SellersRepository sellRepo = SellersRepository.getInstance();
            ArticlesRepository artRepo = ArticlesRepository.getInstance();

            Seller s = (Seller)sellRepo.getEntityById(1);
            richTextBox1.Text = s.Name;
        }
    }
}
