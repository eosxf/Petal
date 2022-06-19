using System;
using System.Runtime;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Petal.Engine.Content;
using Petal.Engine.Extensions;
using Petal.Engine.Graphics;
using Petal.Engine.Math;

namespace Petal.Engine;

public abstract class PetalCore : Game
{
	public readonly GraphicsDeviceManager Graphics;

	private SpriteBatch _spriteBatch = null!;
	private Texture2D? _texture;

	private WindowType _windowType = WindowType.Windowed;

	protected PetalCore(PetalConfiguration config)
	{
		Graphics = new GraphicsDeviceManager(this);
		Content = new PetalExceptionContentManager(this);
		ApplyConfiguration(config);

		GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
	}

	protected override void Initialize()
	{
		_spriteBatch = new SpriteBatch(GraphicsDevice);
		_texture = Content.Load<Texture2D>("guy");
		base.Initialize();
	}

	public void ApplyConfiguration(PetalConfiguration config)
	{
		_windowType = config.WindowType;
		Window.Title = config.WindowTitle;
		IsMouseVisible = config.IsMouseVisible;

		var preferredWindowSize = new Vector2Int(config.WindowWidth, config.WindowHeight);

		Window.AllowUserResizing = config.IsWindowUserResizable;
		IsFixedTimeStep = config.VSync;
		TargetElapsedTime = TimeSpan.FromSeconds(1d / config.DesiredFramerate);
		
		ChangeWindowProperties(preferredWindowSize, _windowType);
	}

	protected override void Update(GameTime gameTime)
	{
		var keys = Keyboard.GetState();
		
		if(keys.IsKeyDown(Keys.Escape))
			Exit();

		if (keys.IsKeyDown(Keys.Space))
		{
			var windowTypes = Enum.GetValues<WindowType>();
			var windowType = windowTypes[new Random().Next(0, windowTypes.Length)];
			var config = RecreateConfiguration();
			ChangeWindowProperties(new Vector2Int(config.WindowWidth, config.WindowHeight), windowType);
		}
	}

	protected override void Draw(GameTime gameTime)
	{
		Graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

		var rArgs = RendererArgs.New();
		rArgs.Graphics = Graphics;

		var targetResolution = new Vector2(1920, 1080);
		var actualResolution = GetWindowDimensions();

		rArgs.Matrix = Matrix.CreateScale(actualResolution.X / targetResolution.X, actualResolution.Y /
			targetResolution.Y, 1.0f);
		
		_spriteBatch.Begin(rArgs.SortMode, rArgs.BlendState, rArgs.SamplerState, rArgs.DepthStencilState, rArgs
		.Rasterizer, rArgs.DefaultEffect, rArgs.Matrix);
		//_spriteBatch.Begin();
		_spriteBatch.Draw(_texture, new Rectangle(5, 5, 1914, 1074), Color.Red);
		_spriteBatch.End();
		base.Draw(gameTime);
	}
	
	public Rectangle GetWindowBounds()
	{
		return Graphics.GraphicsDevice.Viewport.Bounds;
	}

	public Vector2Int GetWindowSize()
	{
		return new Vector2Int(GetWindowBounds());
	}

	public Vector2Int GetScreenSize()
	{
		var displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
		return new Vector2Int(displayMode.Width, displayMode.Height);
	}

	public Rectangle GetScreenBounds()
	{
		var screenSize = GetScreenSize();
		return new Rectangle(0, 0, screenSize.X, screenSize.Y);
	}

	public Vector2 GetWindowDimensions()
	{
		var bounds = GetWindowBounds();
		return new Vector2(bounds.Width, bounds.Height);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void ChangeWindowProperties(Vector2Int size) => ChangeWindowProperties(size, _windowType);

	public void ChangeWindowProperties(Vector2Int size, WindowType? windowType)
	{
		_windowType = windowType ?? _windowType;
		size = GetPreferredWindowSize(size, _windowType);

		switch (_windowType)
		{
			case WindowType.Windowed:
				Graphics.PreferredBackBufferWidth = size.X;
				Graphics.PreferredBackBufferHeight = size.Y;
				Window.SetBorderless(false);
				Graphics.IsFullScreen = false;
				break;
			case WindowType.BorderlessFullscreen:
				Graphics.PreferredBackBufferWidth = GetScreenSize().X;
				Graphics.PreferredBackBufferHeight = GetScreenSize().Y;
				Graphics.IsFullScreen = false;
				Window.SetBorderless(true);
				break;
			case WindowType.Fullscreen:
				Window.SetBorderless(false);
				Graphics.ToggleFullScreen();
				break;
			default:
				throw new InvalidOperationException($"Window type {windowType} is not supported.");
		}
		
		Graphics.ApplyChanges();
	}

	public Vector2Int GetPreferredWindowSize(Vector2Int size, WindowType windowType)
	{
		var windowSize = size;
		var displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
		
		switch (_windowType)
		{
			case WindowType.Windowed:
				break;
			case WindowType.BorderlessFullscreen:
				windowSize = new Vector2Int(displayMode.Width, displayMode.Height);
				break;
			case WindowType.Fullscreen:
				windowSize = new Vector2Int(displayMode.Width, displayMode.Height);
				break;
			}

		return windowSize;
	}

	protected virtual PetalConfiguration RecreateConfiguration() => new();
}