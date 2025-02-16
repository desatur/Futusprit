namespace Futusprit.Objects
{
    public class ECS
    {
        public static ECS Singleton { get; private set; }
        public ObjectManager ObjectManager { get; private set; }
        public ComponentManager ComponentManager { get; private set; }

        public ECS()
        {
            Singleton ??= this;
            ObjectManager = new ObjectManager();
            ComponentManager = new ComponentManager();
        }

        //public void Update()
        //{

        //}
    }
}
