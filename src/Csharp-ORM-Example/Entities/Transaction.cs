using System.Collections.Generic;
using System.Data.SqlClient;

namespace Csharp_ORM_Example
{
    public class Transaction : Entity
    {
        int? articleId;
        int amount;

        public Transaction(int id, int? articleId, int amount, Repository parentRepository) : base(id, parentRepository)
        {
            this.articleId = articleId;
            this.amount = amount;
        }

        public int Amount { get => amount; set => amount = value; }
        public int? ArticleId { get => articleId; set => articleId = value; }

        internal static Transaction[] fromReader(SqlDataReader reader)
        {
            List<Transaction> entities = new List<Transaction>();

            while (reader.Read())
            {
                int? articleId = null;
                if(!reader.IsDBNull(1))
                    articleId = reader.GetInt32(1);

                Transaction a = new Transaction(
                    reader.GetInt32(0),
                    articleId,
                    reader.GetInt32(2),
                    TransactionsRepository.getInstance()
                    );

                entities.Add(a);
            }

            return entities.ToArray();
        }

    }
}
