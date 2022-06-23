using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Petal.Engine.UI;

public class Scene
{
	private bool _initialized = false;
	private readonly Dictionary<string, SceneLayer> _layers = new();

	public Scene()
	{
		
	}

	internal void Initialize()
	{
		if (_initialized) return;
		_initialized = true;
	}

	public void Update(GameTime gameTime)
	{
		
	}

	public void Draw(GameTime gameTime)
	{
		
	}

	public SceneLayer GetLayer(string layerTag) => _layers[layerTag];

	public void AddLayer(SceneLayer layer) => _layers.Add(layer.Tag, layer);
}