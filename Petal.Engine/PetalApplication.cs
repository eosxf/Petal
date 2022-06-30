using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Petal.Engine.Extensions;
using Petal.Engine.Math;
using Petal.Engine.UI;
using Petal.Engine.Utilities.Coroutines;

namespace Petal.Engine;

public sealed class PetalApplication
{
	private static PetalApplication? _instance = null;

	public static PetalApplication Instance
	{
		get
		{
			if (_instance == null)
				throw new InvalidOperationException("No Petal application has been created yet.");
			return _instance;
		}
	}
	
	private readonly PetalGame _game;
	private readonly GraphicsDeviceManager _graphics;
	private readonly IApplicationHandler _applicationHandler;
	
	private bool _initialized = false;
	private WindowType _windowType = WindowType.Windowed;

	public GraphicsDeviceManager Graphics => _graphics;
	public WindowType WindowType => _windowType;

	//private GameTime _gameTime = new();

	private SceneManager _sceneManager;

	public SceneManager SceneManager => _sceneManager;

	public GameServiceContainer Services => _game.Services;

	private CoroutineManager _coroutineManager = new();

	public CoroutineManager CoroutineManager => _coroutineManager;

	public PetalApplication(PetalConfiguration config, IApplicationHandler applicationHandler)
	{
		if (_instance != null)
			throw new InvalidOperationException("A Petal application already exists, exit the previous application" +
			                                    "before creating a new one.");
		_instance = this;
		
		_game = new PetalGame(this);
		_graphics = new GraphicsDeviceManager(_game);
		_applicationHandler = applicationHandler;
		_sceneManager = new SceneManager();

		ApplyConfiguration(config);
	}

	// maybe make public
	private void ApplyConfiguration(PetalConfiguration config)
	{
		_windowType = config.WindowType;
		_game.Window.Title = config.WindowTitle;
		_game.IsMouseVisible = config.IsMouseVisible;

		var preferredWindowSize = new Vector2Int(config.WindowWidth, config.WindowHeight);

		_game.Window.AllowUserResizing = config.IsWindowUserResizable;
		_game.IsFixedTimeStep = config.VSync;
		_game.TargetElapsedTime = TimeSpan.FromSeconds(1d / config.DesiredFramerate);
		
		ChangeWindowProperties(preferredWindowSize, _windowType);
	}

	public void Run()
	{
		_game.Run();
	}

	public void RequestExit()
	{
		_game.Exit();
	}

	internal void Initialize()
	{
		if (_initialized)
			throw new InvalidOperationException("Petal application is already initialized, this is likely a bug.");
		
		_initialized = true;
		_applicationHandler.OnInitialize();
	}

	public void Update(GameTime gameTime)
	{
		if (_sceneManager.IsRunningScene())
		{
			_coroutineManager.Update(gameTime);
		}
		
		_sceneManager.Update(gameTime);
		_applicationHandler.OnUpdate(gameTime);
	}

	public void Draw(GameTime gameTime)
	{
		_sceneManager.Draw(gameTime);
		_applicationHandler.OnDraw(gameTime);
	}
	
	internal void OnExiting()
	{
		_applicationHandler.OnExit();
		_game.Dispose();
		_instance = null;
	}
	
	public void ChangeWindowProperties(Vector2Int size, WindowType? windowType = null)
	{
		_windowType = windowType ?? _windowType;
		size = GetPreferredWindowSize(size, _windowType);
		Graphics.PreferredBackBufferWidth = size.X;
		Graphics.PreferredBackBufferHeight = size.Y;

		switch (_windowType)
		{
			case WindowType.Windowed:
				_game.Window.SetBorderless(false);
				Graphics.IsFullScreen = false;
				break;
			case WindowType.Borderless:
				Graphics.IsFullScreen = false;
				_game.Window.SetBorderless(true);
				break;
			case WindowType.BorderlessFullscreen:
				Graphics.IsFullScreen = false; // this isn't destined to cause confusion
				_game.Window.SetBorderless(true);
				break;
			case WindowType.Fullscreen:
				_game.Window.SetBorderless(false);
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
			case WindowType.Borderless:
				break;
			case WindowType.BorderlessFullscreen:
				windowSize = new Vector2Int(displayMode.Width, displayMode.Height);
				break;
			case WindowType.Fullscreen:
				windowSize = new Vector2Int(displayMode.Width, displayMode.Height);
				break;
			default:
				throw new InvalidOperationException($"Window type {windowType} is not supported.");
		}

		return windowSize;
	}
	
	public Rectangle GetWindowBounds() => Graphics.GraphicsDevice.Viewport.Bounds;

	public Vector2Int GetWindowSize() => new(GetWindowBounds());

	public Rectangle GetScreenBounds() => (Rectangle) GetScreenSize();

	public Vector2Int GetWindowDimensions() => (Vector2Int) GetWindowBounds();
	
	public Vector2Int GetScreenSize()
	{
		var displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
		return new Vector2Int(displayMode.Width, displayMode.Height);
	}
}