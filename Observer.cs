namespace HelloWorld
{
	public enum EventType { Open, Save }

	// The base publisher class includes subscription managment
	// code and notification methods
	public abstract class IEventListener
	{
		// Receive update from subject
		public void HandleEvent(EventType eventType)
		{
			switch (eventType)
			{
				case EventType.Open:
					OnOpenEvent();
					break;
				case EventType.Save:
					OnSaveEvent();
					break;
				default:
					throw new ArgumentException("Error: This event is does not have any implementation. Make sure you provide an implementation for this event in your listener.");
			}
		}

		public virtual void OnOpenEvent() { }

		public virtual void OnSaveEvent() { }
	}

	public class EventManager
	{
		private readonly Dictionary<EventType, List<IEventListener>> _listeners = new();
		// Subscribes an event listener to the subject
		public void Subscribe(EventType eventType, IEventListener listener)
		{
			if (!_listeners.ContainsKey(eventType)) _listeners.Add(eventType, new());
			_listeners[eventType].Add(listener);
		}

		// Subscribes multiple event listeners to the subject
		public void Subscribe(EventType eventType, IEventListener[] listeners)
		{
			if (!_listeners.ContainsKey(eventType)) _listeners.Add(eventType, new());
			foreach (IEventListener listener in listeners) _listeners[eventType].Add(listener);
		}

		// Unsubscribes an event listener from the subject
		public void Unsubscribe(EventType eventType, IEventListener listener)
		{
			// Prefer a 'TryGetValue' call over a Dictionary indexer access guarded by a 'ContainsKey' check to avoid double lookup
			if (!_listeners.TryGetValue(eventType, out List<IEventListener>? value)) return;
			value.Remove(listener);
		}

		// Notify all event listeners about an event
		public void Notify(EventType eventType)
		{
			// Prefer a 'TryGetValue' call over a Dictionary indexer access guarded by a 'ContainsKey' check to avoid double lookup
			if (!_listeners.TryGetValue(eventType, out List<IEventListener>? value)) return;
			foreach (IEventListener listener in value) listener.HandleEvent(eventType);
		}
	}


	public class Subject(EventManager _eventManager)
	{
		public EventManager EventManager = _eventManager;

		public void OpenFile()
		{
			Console.WriteLine("Subject has opened file");
			EventManager.Notify(EventType.Open);
		}

		public void SaveFile()
		{
			Console.WriteLine("Subject has saved file");
			EventManager.Notify(EventType.Save);
		}
	}

	class LoggingListener : IEventListener
	{
		private readonly List<string> _openFileLogs = new();

		public override void OnOpenEvent()
		{
			_openFileLogs.Add(DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt"));
		}

		public void ViewLogs()
		{
			Console.WriteLine("[{0}]", string.Join(", ", _openFileLogs));
		}
	}

	class EmailListener : IEventListener
	{
		private readonly string? _email;

		public EmailListener(string email)
		{
			_email = email;
		}

		public override void OnOpenEvent()
		{
			Console.WriteLine($"To: {_email} -> \"Someone has opened the file\"");
		}

		public override void OnSaveEvent()
		{
			Console.WriteLine($"To: {_email} -> \"Someone has saved the file\"");
		}
	}
}