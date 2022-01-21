namespace Common.DesignPatterns.Creational.Singleton
{
    public abstract class Singleton<TInstance>
        where TInstance : notnull, new()
    {
        protected static TInstance? Instance
        
        {
            get;
            private set;
        }

        // TODO Relax type constraints to force consumer to have protected constructor?
        public static TInstance GetInstance()
        {
            if (Instance is null)
            {
                Instance = new TInstance();
            }

            return Instance;
        }
    }
}
