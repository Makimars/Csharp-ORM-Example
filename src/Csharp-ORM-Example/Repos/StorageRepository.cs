using System.Data.SqlClient;

namespace Csharp_ORM_Example
{
    public class StorageRepository : Repository
    {

        protected StorageRepository(string table_name, string primary_key) : base(table_name, primary_key)
        {
        }

        private static StorageRepository instance;

        public static StorageRepository getInstance()
        {
            if (StorageRepository.instance == null)
                StorageRepository.instance = new StorageRepository("storage", "id");

            return StorageRepository.instance;
        }

        public Storage createNewEntity()
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

            Storage storage = (Storage)this.getList()
                .setOrder(primary_key, Order.DESC)
                .setLimit(1)
                .fetch()[0];

            return storage;
        }

        internal override void updateEntity(Entity entity)
        {
            if (entity.GetType() != typeof(Storage))
                throw new System.Exception("Cannot update type " + entity.GetType());

            Storage storage = (Storage)entity;

            string query = "UPDATE " + table_name +
                " SET amount = " + storage.Amount +
                ", article_id = " + storage.ArticleId +
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
            return getEntities<Storage>(query);
        }
    }
}
