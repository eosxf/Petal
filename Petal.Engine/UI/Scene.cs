using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Petal.Engine.UI;

public class Scene
{
	public Color BackgroundColor { get; set; } = Color.CornflowerBlue;
	
	private bool _initialized = false;
	private readonly Dictionary<string, SceneLayer> _layers = new();

	public Scene()
	{
		
	}

	public void Enter()
	{
		if (_initialized) return;
		OnEnter();
		_initialized = true;
	}

	protected virtual void OnEnter()
	{
		
	}

	public void Update(GameTime gameTime)
	{
		OnUpdate(gameTime);
	}

	protected virtual void OnUpdate(GameTime gameTime)
	{
		
	}

	public void Draw(GameTime gameTime)
	{
		PetalApplication.Instance.Graphics.GraphicsDevice.Clear(BackgroundColor);
		OnDraw(gameTime);
	}
	
	protected virtual void OnDraw(GameTime gameTime)
	{
		
	}

	public void Leave()
	{
		
	}

	public SceneLayer GetLayer(string layerTag) => _layers[layerTag];

	public void AddLayer(SceneLayer layer) => _layers.Add(layer.Tag, layer);
}