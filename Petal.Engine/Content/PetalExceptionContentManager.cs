using System;
using Microsoft.Xna.Framework.Content;

namespace Petal.Engine.Content;

/// <summary>
/// Content manager which only exists to throw exceptions for bastards who use Game.Content
/// </summary>
public sealed class PetalExceptionContentManager : ContentManager
{
	private PetalCore _petal;
	
	public PetalExceptionContentManager(PetalCore petal) : base(petal.Services, "No really, don't.")
	{
		_petal = petal;
	}

	public override T Load<T>(string assetName)
	{
		throw new NotImplementedException($"Don't use [{_petal.GetType()}].Content!");
	}
}