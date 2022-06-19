using System;
using Microsoft.Xna.Framework;
using Petal.Engine.Math;

namespace Petal.Engine.Sandbox;

public sealed class SandboxApplicationHandler : IApplicationHandler
{
	private PetalApplication _app = null!;

	public void OnInitialize(PetalApplication app)
	{
		_app = app;
	}

	public void OnUpdate(GameTime gameTime)
	{
		Console.WriteLine("Update!");
	}

	public void OnDraw(GameTime gameTime)
	{
		Console.WriteLine("Draw!");
	}

	public void OnExit()
	{
		Console.WriteLine("Exit!");
	}
}