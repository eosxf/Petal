namespace Petal.Engine.UI;

public sealed class SceneLayer
{
	public string Tag { get; private set; }
	
	public SceneNode RootNode { get; } // may add public set

	public SceneLayer(string tag, SceneNode rootNode)
	{
		Tag = tag;
		RootNode = rootNode;
	}
}