using System.Runtime.Intrinsics.Arm;
using HelloWorld;

/// <summary>
///	This method implements and tests the Observer Pattern.
/// </summary>
void ObserverPattern()
{
	var EventManager = new EventManager();

	var subject = new Subject(EventManager);
	var loggingListener = new LoggingListener();
	var emailListener = new EmailListener("gabadelemoni@outlook.com");

	subject.EventManager.Subscribe(EventType.Open, [loggingListener, emailListener]);
	subject.EventManager.Subscribe(EventType.Save, [loggingListener]);

	subject.OpenFile();
	subject.SaveFile();
	loggingListener.ViewLogs();
}

/// <summary>
///	This method implements and tests the State Pattern.
/// </summary>
void StatePattern()
{
	var trafficLight = new TrafficLight(new GreenLight());
	int i = 0;
	while (i < 5)
	{
		trafficLight.ChangeLight();
		i++;
	}
	trafficLight.GetStateHistory();
}

/// <summary>
///	This method implements and tests the Builder Pattern.
/// </summary>
void BuilderPattern()
{
	var builder = new ConcreteBuilder();
	builder.BuildPartA();
	builder.BuildPartC();
	Console.WriteLine(builder.GetProduct().ListParts());
}

/// <summary>
///	This method implements and tests the Singleton Pattern.
/// </summary>
void SingletonPattern()
{
	Singleton singleton1 = Singleton.GetInstance();
	Singleton singleton2 = Singleton.GetInstance();

	if (singleton1 == singleton2) Console.WriteLine("Singleton works, both vairables contain the same instance.");
	else Console.WriteLine("Singleton failed, variables contain different instances.");
}

StatePattern();