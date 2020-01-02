using System.Data.SqlClient;

namespace Csharp_ORM_Example
{
    public class SellersRepository : Repository
    {
        protected SellersRepository(string table_name, string primary_key) : base(table_name, primary_key)
        {
        }

        private static SellersRepository instance;
        public static SellersRepository getInstance()
        {
            if (SellersRepository.instance == null)
                SellersRepository.instance = new SellersRepository("sellers", "id");

            return SellersRepository.instance;
        }

        public Seller createNewEntity()
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

            Seller seller = (Seller)this.getList()
                .setOrder("Id", Order.DESC)
                .setLimit(1)
                .fetch()[0];

            return seller;
        }

        public override void updateEntity(Entity entity)
        {
            Seller seller = (Seller)entity;

            string query = "UPDATE " + table_name +
                " SET name = '" + seller.Name +
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

        public override Entity[] getEntities(string query)
        {
            Seller[] sellers;

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    sellers = Seller.fromReader(command.ExecuteReader());
                }
                con.Close();
            }

            return sellers;
        }
    }
}
