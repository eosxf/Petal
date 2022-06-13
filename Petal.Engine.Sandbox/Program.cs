using System;
using Petal.Engine;
using Petal.Engine.Sandbox;
using Petal.IO;

var config = new PetalConfiguration(new File("petal_engine.json"));
Console.WriteLine(config);

using var game = new SandboxGame(config);
game.Run();