using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Petal.Common;
using Petal.Engine.Content;
using Petal.Engine.Extensions;
using Petal.Engine.Graphics;
using Petal.Engine.Math;
using Petal.IO;

namespace Petal.Engine;

public abstract class PetalCore : Game
{
	public readonly GraphicsDeviceManager Graphics;
	public PetalContentManager Contents => (PetalContentManager) Content;

	private SpriteBatch _spriteBatch = null!;
	private Texture2D? _texture;

	private WindowType _windowType = WindowType.Windowed;

	protected PetalCore(PetalConfiguration config)
	{
		Graphics = new GraphicsDeviceManager(this);
		Content = new PetalContentManager(this);
		ApplyConfiguration(config);

		GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
	}

	protected override void Initialize()
	{
		_spriteBatch = new SpriteBatch(GraphicsDevice);
		_texture = Contents.Load<Texture2D>("guy");
		base.Initialize();
	}

	public void ApplyConfiguration(PetalConfiguration config)
	{
		_windowType = config.WindowType;
		Window.Title = config.WindowTitle;
		IsMouseVisible = config.IsMouseVisible;

		var windowSize = new Vector2Int(config.WindowWidth, config.WindowHeight);

		switch (_windowType)
		{
			case WindowType.Windowed:
				break;
			case WindowType.BorderlessFullscreen:
			{
				var displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
				windowSize = new Vector2Int(displayMode.Width, displayMode.Height);
				break;
			}
			case WindowType.Fullscreen:
			{
				var displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
				windowSize = new Vector2Int(displayMode.Width, displayMode.Height);
				break;
			}
			default:
				throw new InvalidOperationException($"Config window type {config.WindowType} is not supported.");
		}

		Window.AllowUserResizing = config.IsWindowUserResizable;
		IsFixedTimeStep = config.VSync;
		TargetElapsedTime = TimeSpan.FromSeconds(1d / config.DesiredFramerate);
		
		ChangeWindowProperties(windowSize, _windowType);
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

	public Vector2Int GetScreenSize()
	{
		var displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
		return new Vector2Int(displayMode.Width, displayMode.Height);
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

		switch (windowType)
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
				throw new InvalidOperationException($"Config window type {windowType} is not supported.");
		}
		
		Graphics.ApplyChanges();
	}

	protected virtual PetalConfiguration RecreateConfiguration() => new();
}