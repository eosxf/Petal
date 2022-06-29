using System.Collections;

namespace Petal.Engine.Utilities.Coroutines;

public class Coroutine : ICoroutine
{
	public IEnumerator? Enumerator;
	public float WaitTimer;
	public Coroutine? WaitForCoroutine;
	public bool IsDone;
	
	public void Stop()
	{
		IsDone = true;
	}

	public void PrepareForReuse()
	{
		IsDone = false;
	}
}