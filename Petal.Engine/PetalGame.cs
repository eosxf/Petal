using System;
using Microsoft.Xna.Framework;

namespace Petal.Engine;

internal sealed class PetalGame : Game
{
	private readonly PetalApplication _app;

	public PetalGame(PetalApplication app)
	{
		_app = app;
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	protected override bool BeginDraw()
	{
		return base.BeginDraw();
	}

	protected override void EndDraw()
	{
		base.EndDraw();
	}

	protected override void BeginRun()
	{
		base.BeginRun();
	}

	protected override void EndRun()
	{
		base.EndRun();
	}
	
	protected override void Initialize()
	{
		base.Initialize();
		Content.Dispose(); // Content cannot be set to null :(
		_app.Initialize();
	}

	protected override void Draw(GameTime gameTime)
	{
		_app.Draw(gameTime);
	}

	protected override void Update(GameTime gameTime)
	{
		_app.Update(gameTime);
	}

	protected override void OnExiting(object sender, EventArgs args)
	{
		_app.OnExiting();
		base.OnExiting(sender, args);
	}

	protected override void OnActivated(object sender, EventArgs args)
	{
		base.OnActivated(sender, args);
	}

	protected override void OnDeactivated(object sender, EventArgs args)
	{
		base.OnDeactivated(sender, args);
	}

	protected override bool ShowMissingRequirementMessage(Exception exception)
	{
		return base.ShowMissingRequirementMessage(exception);
	}
}