using System.Data.SqlClient;

namespace Csharp_ORM_Example
{
    public class TransactionsRepository : Repository
    {
        protected TransactionsRepository(string tableName, string primaryKey) : base(tableName, primaryKey)
        {
        }

        private static TransactionsRepository instance;

        public static TransactionsRepository getInstance()
        {
            if (TransactionsRepository.instance == null)
                TransactionsRepository.instance = new TransactionsRepository("transactions", "Id");

            return TransactionsRepository.instance;
        }

        public Transaction createNewEntity()
        {
            string query = "INSERT INTO " + table_name +
                " (amount)" +
                "VALUES (0)";

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.ExecuteNonQuery();
                }
                con.Close();
            }

            Transaction transaction = (Transaction)this.getList()
                .setOrder(primary_key, Order.DESC)
                .setLimit(1)
                .fetch()[0];

            return transaction;
        }

        internal override void updateEntity(Entity entity)
        {
            if (entity.GetType() != typeof(Transaction))
                throw new System.Exception("Cannot update type " + entity.GetType());

            Transaction transaction = (Transaction)entity;

            string query = "UPDATE " + table_name +
                " SET amount = " + transaction.Amount +
                ", article_id = " + transaction.ArticleId +
                " WHERE " + this.primary_key + " = " + entity.Id;

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
            return getEntities<Transaction>(query);
        }
    }
}
