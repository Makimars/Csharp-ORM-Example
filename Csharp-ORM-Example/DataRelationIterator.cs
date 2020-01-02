using System.Data;
using System.Data.SqlClient;

namespace Csharp_ORM_Example
{
    public class DataRelationIterator : EntityList
    {
        internal DataRelationIterator(string sqlQuery, Repository repository)
        {
            this.sql_query = sqlQuery;
            this.repository = repository;
        }

        public void groupBy(string selectString, string groupByColumns)
        {
            this.sql_query = "SELECT (" + selectString + ") FROM (" + this.sql_query + ") GROUP BY (" + groupByColumns + ");";
        }

        public void joinWith(EntityList iterator, string columnOnThis, string columnOnJoined)
        {
            this.sql_query = "SELECT * FROM (" + this.sql_query + ") AS primaryRelation JOIN (" + iterator.getQuery() + ") AS secondaryRelation ON primaryRelation." 
                + columnOnThis + "=secondaryRelation." + columnOnJoined;
        }

        public new DataTable fetch()
        {
            DataTable dt = new DataTable();
            using (var con = new SqlConnection(Repository.connString))
            {
                con.Open();

                using (SqlCommand command = new SqlCommand(this.sql_query, con))
                {
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        dt.Load(dr);
                    }
                }

                con.Close();
            }

            return dt;
        }
    }
}
