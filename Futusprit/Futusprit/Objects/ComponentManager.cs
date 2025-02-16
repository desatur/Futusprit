namespace Futusprit.Objects
{
    public class ComponentManager
    {
        private Dictionary<Type, Dictionary<uint, object>> components = [];

        public void AddComponent<T>(uint entity, T component)
        {
            if (!components.ContainsKey(typeof(T))) components[typeof(T)] = [];
            components[typeof(T)][entity] = component;
        }

        public void RemoveComponent<T>(uint entity)
        {
            if (components.ContainsKey(typeof(T))) components[typeof(T)].Remove(entity);
        }

        public bool HasComponent<T>(uint entity) => components.ContainsKey(typeof(T)) && components[typeof(T)].ContainsKey(entity);

        public T GetComponent<T>(uint entity) => (T)components[typeof(T)][entity];
    }
}
