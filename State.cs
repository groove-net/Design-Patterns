namespace HelloWorld;


public interface ITrafficLightState
{
	public void OnInit();
	public void ChangeLight(TrafficLight trafficLight);
	public string ToString();
}

public class RedLight : ITrafficLightState
{
	public void OnInit()
	{
		Console.WriteLine("Red light - Stop!");
	}
	public void ChangeLight(TrafficLight trafficLight)
	{
		Thread.Sleep(3000);
		trafficLight.SetState(new GreenLight());
	}
	public override string ToString()
	{
		return "Red Light";
	}
}

public class YellowLight : ITrafficLightState
{
	public void OnInit()
	{
		Console.WriteLine("Yellow light - Wait!");
	}
	public void ChangeLight(TrafficLight trafficLight)
	{
		Thread.Sleep(1000);

		// If Yellow Light is the Initial State
		if (trafficLight._stateHistory.Count == 1)
		{
			trafficLight.SetState(new GreenLight());
			return;
		}

		// Get previous state
		trafficLight._stateHistory.Pop();
		var previousState = trafficLight._stateHistory.Peek().GetType();
		trafficLight._stateHistory.Push(this);

		// Change state based on previous state
		if (previousState == typeof(RedLight))
			trafficLight.SetState(new GreenLight());
		else if (previousState == typeof(GreenLight))
			trafficLight.SetState(new RedLight());
	}
	public override string ToString()
	{
		return "Yellow Light";
	}
}

public class GreenLight : ITrafficLightState
{
	public void OnInit()
	{
		Console.WriteLine("Green light - Go!");
	}
	public void ChangeLight(TrafficLight trafficLight)
	{
		Thread.Sleep(6000);
		trafficLight.SetState(new YellowLight());
	}
	public override string ToString()
	{
		return "Green Light";
	}
}

public class TrafficLight
{
	private ITrafficLightState _state = null!;
	public Stack<ITrafficLightState> _stateHistory = new();
	public TrafficLight(ITrafficLightState initialState)
	{
		SetState(initialState);
	}
	public void SetState(ITrafficLightState state)
	{
		_state = state;
		_stateHistory.Push(state);
		_state.OnInit();
	}
	public ITrafficLightState GetState()
	{
		return _state;
	}
	public void ChangeLight()
	{
		_state.ChangeLight(this);
	}
	public void GetStateHistory()
	{
		Console.Write("State History: ");
		Console.WriteLine("[{0}]", string.Join(", ", _stateHistory.Reverse()));
	}
}