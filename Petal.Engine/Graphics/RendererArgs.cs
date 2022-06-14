using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Petal.Engine.Graphics;

public struct RendererArgs
{
	public static RendererArgs New()
	{
		var args = new RendererArgs
		{
			Rasterizer = new RasterizerState
			{
				CullMode = CullMode.CullCounterClockwiseFace,
				DepthBias = 0.0f,
				FillMode = FillMode.Solid,
				MultiSampleAntiAlias = false,
				ScissorTestEnable = true,
				SlopeScaleDepthBias = 0.0f
			},
			SortMode = SpriteSortMode.Deferred,
			BlendState = BlendState.AlphaBlend,
			SamplerState = SamplerState.PointClamp,
			DepthStencilState = DepthStencilState.None,
			DefaultEffect = null,
			Matrix = Matrix.Identity
		};

		return args;
	}
	
	public static RendererArgs New(GraphicsDeviceManager graphics)
	{
		var args = new RendererArgs
		{
			Graphics = graphics,
			Rasterizer = new RasterizerState
			{
				CullMode = CullMode.CullCounterClockwiseFace,
				DepthBias = 0.0f,
				FillMode = FillMode.Solid,
				MultiSampleAntiAlias = false,
				ScissorTestEnable = true,
				SlopeScaleDepthBias = 0.0f
			},
			SortMode = SpriteSortMode.Deferred,
			BlendState = BlendState.AlphaBlend,
			SamplerState = SamplerState.PointClamp,
			DepthStencilState = DepthStencilState.None,
			DefaultEffect = null,
			Matrix = Matrix.Identity
		};

		return args;
	}
	
	public GraphicsDeviceManager Graphics { get; set; }

	public RasterizerState Rasterizer { get; set; }
		
	public SpriteSortMode SortMode { get; set; }
		
	public BlendState BlendState { get; set; }
		
	public SamplerState SamplerState { get; set; }
		
	public DepthStencilState DepthStencilState { get; set; }
		
	public Effect? DefaultEffect { get; set; }
		
	public Matrix Matrix { get; set; }
}