using Microsoft.Xna.Framework;

namespace Petal.Engine.UI;

public sealed class SceneManager
{
	private Scene? _currentScene;
	private SceneTransition? _currentTransition;

	public SceneManager()
	{
		
	}

	public void ChangeScene(Scene newScene)
	{
		_currentScene = newScene;
		_currentScene.Enter();
	}

	public void ChangeScene(Scene newScene, SceneTransition transition)
	{
		_currentScene = newScene;
		_currentScene.Enter();
	}

	public void Update(GameTime gameTime)
	{
		if (_currentScene is null) return;
		_currentScene.Update(gameTime);
	}

	public void Draw(GameTime gameTime)
	{
		if (_currentScene is null) return;
		_currentScene.Draw(gameTime);
	}
}