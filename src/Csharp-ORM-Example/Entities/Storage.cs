using System.Collections.Generic;
using System.Data.SqlClient;

namespace Csharp_ORM_Example
{
    public class Storage : Entity
    {
        int? articleId;
        int amount;

        public Storage(int id, int? articleId, int amount, Repository parentRepository) : base(id, parentRepository)
        {
            this.articleId = articleId;
            this.amount = amount;
        }

        public int Amount { get => amount; set => amount = value; }
        public int? ArticleId { get => articleId; set => articleId = value; }

        internal static Storage[] fromReader(SqlDataReader reader)
        {
            List<Storage> entities = new List<Storage>();

            while (reader.Read())
            {
                int? articleId = null;
                if (!reader.IsDBNull(1))
                    articleId = reader.GetInt32(1);

                Storage a = new Storage(
                    reader.GetInt32(0),
                    articleId,
                    reader.GetInt32(2),
                    StorageRepository.getInstance()
                    );

                entities.Add(a);
            }

            return entities.ToArray();
        }

    }
}
