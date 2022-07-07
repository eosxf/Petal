using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Petal.Engine.Math;
using Petal.Engine.Utilities.Coroutines;

namespace Petal.Engine.Sandbox;

public sealed class SandboxApplicationHandler : IApplicationHandler
{
	private PetalApplication _app = null!;

	public void OnInitialize()
	{
		_app = PetalApplication.Instance;
		_app.SceneManager.ChangeScene(new SandboxScene());

		_app.CoroutineManager.StartCoroutine(TestCoroutine());
		_app.CoroutineManager.StartCoroutine(ChangeWindow(WindowType.Windowed, 2.5f, new Vector2Int(960, 540)));
		_app.CoroutineManager.StartCoroutine(ChangeWindow(WindowType.Borderless, 5.0f, new Vector2Int(1280, 700)));
		_app.CoroutineManager.StartCoroutine(ChangeWindow(WindowType.BorderlessFullscreen, 7.5f));
		_app.CoroutineManager.StartCoroutine(ChangeWindow(WindowType.Fullscreen, 10.0f));
		_app.CoroutineManager.StartCoroutine(StopGame(12.5f));
	}

	public IEnumerator TestCoroutine()
	{
		Console.WriteLine("Hi");
		yield return new WaitForSeconds(2.5f);
		Console.WriteLine("Hi");
	}

	public IEnumerator StopGame(float waitSeconds)
	{
		yield return new WaitForSeconds(waitSeconds);
		_app.RequestExit();
	}

	public IEnumerator ChangeWindow(WindowType windowType, float waitSeconds, Vector2Int? size = null)
	{
		yield return new WaitForSeconds(waitSeconds);
		size ??= new Vector2Int(960, 540);
		_app.ChangeWindowProperties(size.Value, windowType);
	}

	public void OnUpdate(GameTime gameTime)
	{
		//Console.WriteLine("Update!");
	}

	public void OnDraw(GameTime gameTime)
	{
		//Console.WriteLine("Draw!");
	}

	public void OnExit()
	{
		Console.WriteLine("Exit!");
	}
}