using System;

namespace Csharp_ORM_Example
{
    public class EntityList
    {
        protected string sql_query;
        protected Repository repository;

        protected EntityList()
        {

        }
        internal EntityList(string table_name, Repository repository)
        {
            this.sql_query = "SELECT * FROM " + table_name;
            this.repository = repository;
        }

        public EntityList setFilter(string column, string value)
        {
            this.sql_query = "SELECT * FROM (" + this.sql_query + ") AS previousQuery WHERE " + column + " = " + value;
            
            return this;
        }

        public EntityList setOrder(string column, Order order)
        {
            string orderString;

            if (order == Order.ASC)
                orderString = "ASC";
            else if (order == Order.DESC)
                orderString = "DESC";
            else
                return this;

            this.sql_query = "SELECT * FROM (" + this.sql_query + ") AS previousQuerry ORDER BY " + column + " " + orderString;

            return this;
        }

        public EntityList setLimit(int limit)
        {
            this.sql_query = "SELECT TOP " + limit.ToString() + " * FROM (" + this.sql_query + ") AS previousQuery";
             
            return this;
        }

        public Entity[] fetch()
        {
            return this.repository.getEntities(this.sql_query);
        }

        public DataList toDataRelationIterator()
        {
            DataList iterator = new DataList(this.sql_query, repository);

            return iterator;
        }

        public string getQuery()
        {
            return this.sql_query;
        }

    }

    public enum Order
    {
        ASC,
        DESC
    }

}
