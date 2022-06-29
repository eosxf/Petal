using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Petal.Engine.UI;

namespace Petal.Engine.Sandbox;

public class SandboxScene : Scene
{
	private SpriteBatch _spriteBatch = null!;
	private ContentManager _content = null!;
	
	protected override void OnEnter()
	{
		_spriteBatch = new SpriteBatch(PetalApplication.Instance.Graphics.GraphicsDevice);
		_content = new ContentManager(PetalApplication.Instance.Services);
		_content.Load<Texture2D>("guy");
	}

	protected override void OnDraw(GameTime gameTime)
	{
		_spriteBatch.Begin();
		_spriteBatch.Draw(_content.Load<Texture2D>("guy"), new Rectangle(100, 100, 400, 75), Color.White);
		_spriteBatch.End();
	}
}