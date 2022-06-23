namespace Petal.Engine.UI;

public sealed class SceneLayer
{
	public string Tag { get; private set; }

	public SceneLayer(string tag)
	{
		Tag = tag;
	}
}