namespace HelloWorld
{

    public class Singleton
    {
        private static Singleton _instance = null!;
        private static readonly object Padlock = new();

        // Properties of the Singleton class that contains data and information that will be shared
        private readonly string _sharedData = "...some data";
        private readonly Queue<string> _queue = new();

        // Private constructor
        private Singleton()
        {
        }

        // Public method to create and/or return the new Singleton class. It return the Singleton class if it already exists. 
        // If it doesn;t exist yet, it calls the private constructor within class to create the Singleton instance.
        public static Singleton GetInstance()
        {
            // Make it thread safe
            lock (Padlock) return _instance ??= new Singleton();
        }

        // Public methods to provided communication between the Singleton and other classes.
        // This methods may also access or edit any of the Singleton's properties
        // All code to access or mutate the property ust be wrapped around a lock for thread safety
        // Below we put the code for enqueuing documents to the Singleton queue within a lock for the property _queue
        // If the property is never mutated, then a lock may not be necessary but add it anyways.
        public void AddToQueue(IEnumerable<string> documents)
        {
            ArgumentNullException.ThrowIfNull(documents);
            // Make it thread safe
            lock (_queue) foreach (var document in documents) _queue.Enqueue(document);
        }

        public Queue<string> ViewQueue()
        {
            return _queue;
        }

        public string GetSharedData()
        {
            return _sharedData;
        }
    }
}