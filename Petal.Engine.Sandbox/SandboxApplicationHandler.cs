using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
	}

	public IEnumerator TestCoroutine()
	{
		Console.WriteLine("Hi");
		yield return new WaitForSeconds(5.0f);
		Console.WriteLine("Hi");
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