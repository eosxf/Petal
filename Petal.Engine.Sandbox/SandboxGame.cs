﻿using Petal.IO;

namespace Petal.Engine.Sandbox;

public class SandboxGame : PetalCore
{
	public SandboxGame(PetalConfiguration config) : base(config)
	{
		
	}

	protected override PetalConfiguration RecreateConfiguration()
		=> new (new File("petal_engine.json"));
}