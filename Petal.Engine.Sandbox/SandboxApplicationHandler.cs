using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Petal.Engine.Sandbox;

public sealed class SandboxApplicationHandler : IApplicationHandler
{
	private PetalApplication _app = null!;
	private SpriteBatch _spriteBatch = null!;

	public void OnInitialize(PetalApplication app)
	{
		_app = app;
		_spriteBatch = new SpriteBatch(_app.Graphics.GraphicsDevice);
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