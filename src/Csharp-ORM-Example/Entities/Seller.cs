using System.Collections.Generic;
using System.Data.SqlClient;

namespace Csharp_ORM_Example
{
    public class Seller : Entity
    {
        private string name;

        public Seller(int id, Repository parent_repository) : base(id, parent_repository)
        {

        }

        protected Seller(int id, string name, Repository parentRepository) : base(id, parentRepository)
        {
            this.name = name;
        }

        public string Name { get => name; set => name = value; }

        /// <summary>
        /// Creates new object from given SqlDataReader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static Seller[] fromReader(SqlDataReader reader)
        {
            List<Seller> entities = new List<Seller>();

            while (reader.Read())
            {
                string name = null;
                if (!reader.IsDBNull(1))
                    name = reader.GetString(1);

                Seller a = new Seller(
                    reader.GetInt32(0),
                    name,
                    SellersRepository.getInstance()
                    );

                entities.Add(a);
            }

            return entities.ToArray();
        }
    }
}
