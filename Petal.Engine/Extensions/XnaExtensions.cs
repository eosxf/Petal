using Microsoft.Xna.Framework;

namespace Petal.Engine.Extensions;

public static class XnaExtensions
{
	public static bool IsBorderless(this GameWindow self)
	{
#if XNA_IMPLEMENTATION_FNA
		return self.IsBorderlessEXT;
#elif XNA_IMPLEMENTATION_MG
		return self.IsBorderless;
#endif
	}

	public static void SetBorderless(this GameWindow self, bool val)
	{
#if XNA_IMPLEMENTATION_FNA
		self.IsBorderlessEXT = val;
#elif XNA_IMPLEMENTATION_MG
		self.IsBorderless = val;
#endif
	}
}