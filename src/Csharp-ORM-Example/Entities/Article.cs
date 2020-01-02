using System.Collections.Generic;
using System.Data.SqlClient;

namespace Csharp_ORM_Example
{
    public class Article : Entity
    {
        int? seller_id;
        int? cost;
        string name;

        public Article(int id, Repository parent_repository) : base(id, parent_repository)
        {
        }

        protected Article(int id, int? Seller_id, int? Cost, string name, Repository parentRepository) : base (id, parentRepository)
        {
            this.Seller_id = Seller_id;
            this.Cost = Cost;
            this.name = name;
        }

        public string Name { get => name; set => name = value; }
        public int? Seller_id { get => seller_id; set => seller_id = value; }
        public int? Cost { get => cost; set => cost = value; }

        /// <summary>
        /// Creates new object from given SqlDataReader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static Article[] fromReader(SqlDataReader reader)
        {
            List<Article> entities = new List<Article>();

            while (reader.Read())
            {
                int? sellerId = null;
                if (!reader.IsDBNull(1))
                    sellerId = reader.GetInt32(1);

                int? cost = null;
                if (!reader.IsDBNull(2))
                    cost = reader.GetInt32(2);

                string name = null;
                if (!reader.IsDBNull(3))
                    name = reader.GetString(3);

                Article a = new Article(
                    reader.GetInt32(0),
                    sellerId,
                    cost,
                    name,
                    ArticlesRepository.getInstance()
                    );

                entities.Add(a);
            }

            return entities.ToArray();
        }
    }
}
