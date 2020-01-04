using System.Data.SqlClient;

namespace Csharp_ORM_Example
{
    public abstract class Entity
    {
        protected int id;
        protected Repository parentRepository;

        public Entity(int id, Repository parent_repository)
        {
            this.id = id;
            this.parentRepository = parent_repository;
        }

        public int Id { get => id; }

        public void save()
        {
            parentRepository.updateEntity(this);
        }

        internal static Entity[] fromReader<T>(SqlDataReader reader)
        {
            if(typeof(T) == typeof(Article))
                return Article.fromReader(reader);
            if (typeof(T) == typeof(Seller))
                return Seller.fromReader(reader);
            if (typeof(T) == typeof(Storage))
                return Storage.fromReader(reader);
            if (typeof(T) == typeof(Transaction))
                return Transaction.fromReader(reader);

            throw new System.Exception("Invalid type passed, type: " + typeof(T));
        }
    }
}
