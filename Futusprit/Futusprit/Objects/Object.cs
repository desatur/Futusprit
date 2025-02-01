namespace Futusprit.Objects
{
    public abstract class Object
    {
        public uint ID { get; private set; }
        public virtual string Name { get; protected set; } = "Object";
        public virtual Transform Transform { get; protected set; } = new();

        private readonly ObjectManager _objectManager;
        private readonly ComponentManager _componentManager;

        protected Object(ObjectManager objectManager, ComponentManager componentManager)
        {
            _objectManager = objectManager;
            _componentManager = componentManager;
            ID = _objectManager.Create();
        }

        public void AddComponent<T>(T component) => _componentManager.AddComponent(ID, component);

        public void RemoveComponent<T>() => _componentManager.RemoveComponent<T>(ID);

        public bool HasComponent<T>() => _componentManager.HasComponent<T>(ID);

        public T GetComponent<T>() => _componentManager.GetComponent<T>(ID);

        public virtual void Destroy()
        {
            _objectManager.Destroy(ID);
        }
    }
}
