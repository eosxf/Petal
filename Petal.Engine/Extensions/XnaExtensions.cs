using Microsoft.Xna.Framework;
using Petal.Engine.Math;

namespace Petal.Engine.Extensions;

public static class XnaExtensions
{
	/// <summary>
	/// The preferred way of getting whether or not the window is borderless by preserving compatibility between
	/// FNA and MonoGame.
	/// </summary>
	public static bool IsBorderless(this GameWindow self)
	{
#if XNA_IMPLEMENTATION_FNA
		return self.IsBorderlessEXT;
#elif XNA_IMPLEMENTATION_MG
		return self.IsBorderless;
#endif
	}

	/// <summary>
	/// The preferred way of setting whether or not the window is borderless by preserving compatibility between
	/// FNA and MonoGame.
	/// </summary>
	public static void SetBorderless(this GameWindow self, bool val)
	{
#if XNA_IMPLEMENTATION_FNA
		self.IsBorderlessEXT = val;
#elif XNA_IMPLEMENTATION_MG
		self.IsBorderless = val;
#endif
	}
}