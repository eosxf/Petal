using Microsoft.Xna.Framework;

namespace Petal.Engine.UI;

public interface IScene
{
	public void Enter();
	public void Update(GameTime gameTime);
	public void Draw(GameTime gameTime);
	public void Leave();
}