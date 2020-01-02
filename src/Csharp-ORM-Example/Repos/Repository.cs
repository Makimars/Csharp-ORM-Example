using System.Data.SqlClient;

namespace Csharp_ORM_Example
{
    public abstract class Repository
    {
        internal static string connString;

        public static void setConnString(string connString)
        {
            Repository.connString = connString;
        }

        protected string table_name;
        protected string primary_key;

        protected Repository(string table_name, string primary_key)
        {
            this.table_name = table_name;
            this.primary_key = primary_key;
        }

        public EntityList getList()
        {
            EntityList list = new EntityList(this.table_name, this);

            return list;
        }

        public Entity getEntityById(int id)
        {
            EntityList list = new EntityList(this.table_name, this);
            list.setFilter("Id", id.ToString());

            return list.fetch()[0];
        }

        internal abstract void updateEntity(Entity entity);

        internal abstract Entity[] getEntities(string query);

    }
}
