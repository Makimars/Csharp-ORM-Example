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

            richTextBox1.Text = "";

            ArticlesRepository artRepo = ArticlesRepository.getInstance();

            EntityList iterator = artRepo.getList();
            /*
            iterator.setLimit(3)
                .setOrder("Id", Order.DESC);*/

            Article[] entities = (Article[])iterator.fetch();

            foreach (Article ar in entities)
            {
                richTextBox1.Text += ar.Name + " " + ar.Id + "\n";
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";

            SellersRepository sellRepo = SellersRepository.getInstance();

            EntityList sellIterator = sellRepo.getList();
            sellIterator.setLimit(1); 
                
            Seller[] sellers = (Seller[])sellIterator.fetch();
            
            foreach (Seller ar in sellers)
            {
                ar.Name = "zmena";
                ar.save();
                richTextBox1.Text += ar.Name + " " + ar.Id + "\n";
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";

            SellersRepository sellRepo = SellersRepository.getInstance();
            ArticlesRepository artRepo = ArticlesRepository.getInstance();

            EntityList sellIterator = sellRepo.getList();
            EntityList artIterator = artRepo.getList();

            DataRelationIterator dataRel = sellIterator.toDataRelationIterator();

            dataRel.joinWith(artIterator, "Id", "seller_id");

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
                DataRow row = rows[i];

                var items = row.ItemArray;

                for (int a = 0; a < items.Length; a++)
                {
                    richTextBox1.Text += items[a].ToString() + " ";
                }

                richTextBox1.Text += "\n";
            }

        }
    }
}
