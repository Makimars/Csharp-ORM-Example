using System.Collections.Generic;
using System.Data.SqlClient;

namespace Csharp_ORM_Example
{
    public class Article : Entity
    {

        int seller_id;
        int cost;
        string name;

        public Article(int id, Repository parent_repository) : base(id, parent_repository)
        {
        }

        protected Article(int id, int Seller_id, int cost, string name, Repository parentRepository) : base (id, parentRepository)
        {
            this.seller_id = Seller_id;
            this.cost = cost;
            this.name = name;
        }

        public int Seller_id { get => seller_id; set => seller_id = value; }
        public int Cost { get => cost; set => cost = value; }
        public string Name { get => name; set => name = value; }

        public static Article[] fromReader(SqlDataReader reader)
        {
            List<Article> entities = new List<Article>();

            while (reader.Read())
            {
                string name = null;
                if (!reader.IsDBNull(3))
                    name = reader.GetString(3);

                Article a = new Article(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    name,
                    ArticlesRepository.getInstance()
                    );

                entities.Add(a);
            }

            return entities.ToArray();
        }
    }
}
