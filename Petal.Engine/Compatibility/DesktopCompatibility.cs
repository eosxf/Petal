using System;

namespace Petal.Engine.Compatibility;

public class DesktopCompatibility : IPlatformCompatibility
{
	public string ContentPathPrefix => string.Empty;
	public string LocalPathPrefix => string.Empty;
}