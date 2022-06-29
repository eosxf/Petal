using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Petal.Engine.Sandbox;

public sealed class SandboxApplicationHandler : IApplicationHandler
{
	private PetalApplication _app = null!;

	public void OnInitialize()
	{
		_app = PetalApplication.Instance;
		_app.SceneManager.ChangeScene(new SandboxScene());
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