using Petal.Engine;
using Petal.Engine.Sandbox;
using Petal.IO;

var config = new PetalConfiguration(new File("petal_engine.json"));
var app = new PetalApplication(config, new SandboxApplicationHandler());
app.Run();