namespace Petal.Engine.Utilities.Coroutines;

public class WaitForSeconds
{
	public float WaitTime { get; private set; }

	public WaitForSeconds(float seconds)
	{
		WaitTime = seconds;
	}
}