namespace Futusprit.Objects
{
    public class ObjectManager
    {
        private uint nextEntityId = 0;
        private HashSet<uint> activeEntities = [];

        public int Count
        {
            get
            {
                return activeEntities.Count;
            }
        }

        public uint Create()
        {
            uint entity = nextEntityId++;
            activeEntities.Add(entity);
            return entity;
        }

        public void Destroy(uint entity)
        {
            activeEntities.Remove(entity);
        }

        public bool IsObjectActive(uint entity) => activeEntities.Contains(entity);
    }
}
