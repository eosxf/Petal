using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace Petal.Engine.Content;

public sealed class PetalContentManager : ContentManager
{
	private readonly PetalCore _petal;
	
	public PetalContentManager(PetalCore petal) : base(petal.Services, string.Empty)
	{
		_petal = petal;
	}
}