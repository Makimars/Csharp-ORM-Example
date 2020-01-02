using System.Data.SqlClient;

namespace Csharp_ORM_Example
{
    public class ArticlesRepository : Repository
    {
        protected ArticlesRepository(string table_name, string primary_key) : base(table_name, primary_key)
        {
        }

        private static ArticlesRepository instance;
        public static ArticlesRepository getInstance()
        {
            if (ArticlesRepository.instance == null)
                ArticlesRepository.instance = new ArticlesRepository("articles","id");

            return ArticlesRepository.instance;
        }

        public Article createNewEntity()
        {
            string query = "INSERT INTO " + table_name +
                " (name)" +
                "VALUES ('')";

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.ExecuteNonQuery();
                }
                con.Close();
            }

            Article article = (Article)this.getList()
                .setOrder("Id", Order.DESC)
                .setLimit(1)
                .fetch()[0];

            return article;
        }

        internal override void updateEntity(Entity entity)
        {
            Article article = (Article)entity;

            string query = "UPDATE " + table_name + 
                " SET seller_id = " + article.Seller_id + ", cost = " + article.Cost + ", name = '" + article.Name +
                "' WHERE Id = " + entity.Id;

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        internal override Entity[] getEntities(string query)
        {
            Article[] articles;

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    articles = Article.fromReader(command.ExecuteReader());
                }
                con.Close();
            }

            return articles;
        }
    }
}
