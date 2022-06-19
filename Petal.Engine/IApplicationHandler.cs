using Microsoft.Xna.Framework;

namespace Petal.Engine;

public interface IApplicationHandler
{
	public void OnInitialize(PetalApplication app);
	public void OnUpdate(GameTime gameTime);
	public void OnDraw(GameTime gameTime);
	public void OnExit();
}