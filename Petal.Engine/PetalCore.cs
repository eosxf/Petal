using System;
using System.Runtime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Petal.Engine.Compatibility;
using Petal.Engine.Content;
using Petal.Engine.Extensions;
using Petal.Engine.Graphics;

namespace Petal.Engine;

public abstract class PetalCore : Game
{
	public readonly GraphicsDeviceManager Graphics;
	public readonly IPlatformCompatibility PlatformCompatibility;
	public PetalContentManager Contents => (PetalContentManager) Content;

	private SpriteBatch _spriteBatch = null!;
	private Texture2D? _texture;

	protected PetalCore(PetalConfiguration config)
	{
		PlatformCompatibility = IPlatformCompatibility.FromPlatform(config.Platform);
		Graphics = new GraphicsDeviceManager(this);
		Content = new PetalContentManager(this);
		ApplyConfiguration(config);

		GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
	}

	protected override void Initialize()
	{
		_spriteBatch = new SpriteBatch(GraphicsDevice);
		base.Initialize();
	}

	public void ApplyConfiguration(PetalConfiguration config)
	{
		Window.Title = config.WindowTitle;
		IsMouseVisible = config.IsMouseVisible;
		
		switch (config.WindowType)
		{
			case WindowType.Windowed:
				Graphics.PreferredBackBufferWidth = config.WindowWidth;
				Graphics.PreferredBackBufferHeight = config.WindowHeight;
				Window.SetBorderless(false);
				Graphics.IsFullScreen = false;
				break;
			case WindowType.BorderlessWindowed:
				Graphics.PreferredBackBufferWidth = config.WindowWidth;
				Graphics.PreferredBackBufferHeight = config.WindowHeight;
				Graphics.IsFullScreen = false;
				Window.SetBorderless(true);
				break;
			case WindowType.Fullscreen:
				Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
				Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
				Window.SetBorderless(false);
				Graphics.IsFullScreen = true;
				break;
			default:
				break;
		}

		IsFixedTimeStep = config.VSync;
		TargetElapsedTime = TimeSpan.FromSeconds(1d / config.DesiredFramerate);
		
		Graphics.ApplyChanges();
	}

	protected override void Update(GameTime gameTime)
	{
		if(Keyboard.GetState().IsKeyDown(Keys.Escape))
			Exit();
	}

	protected override void Draw(GameTime gameTime)
	{
		Graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

		var rArgs = RendererArgs.New();
		rArgs.Graphics = Graphics;

		var targetResolution = new Vector2(1920, 1080);
		var actualResolution = GetScreenDimensions();

		rArgs.Matrix = Matrix.CreateScale(actualResolution.X / targetResolution.X, actualResolution.Y /
			targetResolution.Y, 1.0f);
		
		_spriteBatch.Begin(rArgs.SortMode, rArgs.BlendState, rArgs.SamplerState, rArgs.DepthStencilState, rArgs
		.Rasterizer, rArgs.DefaultEffect, rArgs.Matrix);
		_spriteBatch.Draw(Contents.Load<Texture2D>("guy"), new Rectangle(1, 1, 1918, 1078), Color.Red);
		_spriteBatch.End();
		base.Draw(gameTime);
	}
	
	public Rectangle GetScreenBounds()
	{
		return Graphics.GraphicsDevice.Viewport.Bounds;
	}

	public Vector2 GetScreenDimensions()
	{
		var bounds = GetScreenBounds();
		return new Vector2(bounds.Width, bounds.Height);
	}
}