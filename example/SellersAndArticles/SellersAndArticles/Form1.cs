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
            
            list.setLimit(3)
                .setOrder("Id", Order.DESC);

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

            list.setLimit(1); 
                
            Seller[] sellers = (Seller[]) list.fetch();
            
            sellers[0].Name = "zmeneneJmeno";
            sellers[0].save();
            richTextBox1.Text += sellers[0].Name + " " + sellers[0].Id + "\n";
            
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            SellersRepository sellRepo = SellersRepository.getInstance();
            ArticlesRepository artRepo = ArticlesRepository.getInstance();

            EntityList sellerList = sellRepo.getList();
            EntityList articleList = artRepo.getList();

            DataList dataRel = sellerList.toDataRelationIterator();

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
    }
}
