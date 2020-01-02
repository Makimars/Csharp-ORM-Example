using System.Data;
using System.Data.SqlClient;

namespace Csharp_ORM_Example
{
    public class DataRelationIterator : EntityList
    {
        public DataRelationIterator(string table_name, Repository repository) : base(table_name, repository) { }

        public EntityList groupBy(string selectString, string groupByColumns)
        {
            this.sql_query = "SELECT (" + selectString + ") FROM (" + this.sql_query + ") GROUP BY (" + groupByColumns + ");";

            return this;
        }

        public new DataTable fetch()
        {
            DataTable dt = new DataTable();
            using (var con = new SqlConnection(Repository.connString))
            {
                using (var command = new SqlCommand(""))
                {
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        dt.Load(dr);
                    }
                }
            }

            return dt;
        }
    }
}
