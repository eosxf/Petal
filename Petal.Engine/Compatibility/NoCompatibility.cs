namespace Petal.Engine.Compatibility;

public class NoCompatibility : IPlatformCompatibility
{
	public string ContentPathPrefix => string.Empty;
	public string LocalPathPrefix => string.Empty;
}