using System;
using Microsoft.Xna.Framework.Content;
using Petal.Common;

namespace Petal.Engine.Content;

public sealed class PetalContentManager : ContentManager
{
	private readonly PetalCore _petal;
	
	public PetalContentManager(PetalCore petal) : base(petal.Services, string.Empty)
	{
		_petal = petal;
	}
}