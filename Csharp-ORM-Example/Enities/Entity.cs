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
    }
}
