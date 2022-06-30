using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Petal.Engine.Utilities.Coroutines;

public sealed class CoroutineManager
{
	private bool _isInUpdate;

	private readonly List<Coroutine> _unblockedCoroutines = new();
	private readonly List<Coroutine> _shouldRunNextFrame = new();

	public void ClearAllCoroutines()
	{
		_unblockedCoroutines.Clear();
		_shouldRunNextFrame.Clear();
	}

	public ICoroutine? StartCoroutine(IEnumerator enumerator)
	{
		var coroutine = new Coroutine();
		coroutine.PrepareForReuse();

		coroutine.Enumerator = enumerator;
		var shouldContinueCoroutine = TickCoroutine(coroutine);

		if (!shouldContinueCoroutine)
			return null;
		
		if(_isInUpdate)
			_shouldRunNextFrame.Add(coroutine);
		else
			_unblockedCoroutines.Add(coroutine);

		return coroutine;
	}

	public void Update(GameTime gameTime)
	{
		_isInUpdate = true;

		foreach (var coroutine in _unblockedCoroutines)
		{
			if (coroutine.IsDone)
				continue;

			if (coroutine.WaitForCoroutine != null)
			{
				if (coroutine.WaitForCoroutine.IsDone)
					coroutine.WaitForCoroutine = null;
				else
				{
					_shouldRunNextFrame.Add(coroutine);
					continue;
				}
			}

			if (coroutine.WaitTimer > 0)
			{
				coroutine.WaitTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
				_shouldRunNextFrame.Add(coroutine);
				continue;
			}
			
			if(TickCoroutine(coroutine))
				_shouldRunNextFrame.Add(coroutine);
		}
		
		_unblockedCoroutines.Clear();
		_unblockedCoroutines.AddRange(_shouldRunNextFrame);
		_shouldRunNextFrame.Clear();

		_isInUpdate = false;
	}

	private bool TickCoroutine(Coroutine coroutine)
	{
		if (!coroutine.Enumerator?.MoveNext() ?? coroutine.IsDone)
		{
			return false;
		}

		if (coroutine.Enumerator?.Current == null)
		{
			return true;
		}

		if (coroutine.Enumerator?.Current is WaitForSeconds waitForSeconds)
		{
			coroutine.WaitTimer = waitForSeconds.WaitTime;
			return true;
		}

		if (coroutine.Enumerator?.Current is IEnumerator enumerator)
		{
			coroutine.WaitForCoroutine = StartCoroutine(enumerator) as Coroutine;
			return true;
		}

		if (coroutine.Enumerator?.Current is Coroutine coroutineCurrent)
		{
			coroutine.WaitForCoroutine = coroutine.Enumerator?.Current as Coroutine;
			return true;
		}
		
		return true;
	}
}