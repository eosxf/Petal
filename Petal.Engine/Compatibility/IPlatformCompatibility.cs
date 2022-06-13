using System;

namespace Petal.Engine.Compatibility;

/// <summary>
/// These methods are just guesses as to compatibility issues for different platforms (ie. desktop, consoles, mobile)
/// These may just be wrong/useless.
/// Platform compatibility likely won't be implemented until shown that it needs to be implemented, which makes me
/// wonder why I bothered to implement it in the first place.
/// </summary>
public interface IPlatformCompatibility
{
	public string ContentPathPrefix { get; }
	public string LocalPathPrefix { get; }

	public static IPlatformCompatibility FromPlatform(Platforms platform)
	{
		/*switch (platform)
		{
			case Platforms.Windows: return new DesktopCompatibility();
			case Platforms.Mac: return new DesktopCompatibility();
			case Platforms.Linux: return new DesktopCompatibility();
			
			// obviously we need something other than desktop compatibilities
			case Platforms.Android: return new DesktopCompatibility();
			case Platforms.Osx: return new DesktopCompatibility();
			
			case Platforms.Playstation4: return new DesktopCompatibility();
			case Platforms.Playstation5: return new DesktopCompatibility();
			case Platforms.NintendoSwitch: return new DesktopCompatibility();
			case Platforms.XBoxOne: return new DesktopCompatibility();
			default: throw new InvalidOperationException($"Platform '{platform}' not detected/supported.");
		}*/

		return new NoCompatibility();
	}
}