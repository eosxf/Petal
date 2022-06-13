using System.Runtime.Serialization;

namespace Petal.Engine.Compatibility;

public enum Platforms
{
	[EnumMember(Value = "none")]
	None,
	[EnumMember(Value = "windows")]
	Windows,
	[EnumMember(Value = "mac")]
	Mac,
	[EnumMember(Value = "linux")]
	Linux,
	[EnumMember(Value = "android")]
	Android,
	[EnumMember(Value = "osx")]
	Osx,
	[EnumMember(Value = "playstation4")]
	Playstation4,
	[EnumMember(Value = "playstation5")]
	Playstation5,
	[EnumMember(Value = "xboxOne")]
	XBoxOne,
	[EnumMember(Value = "nintendoSwitch")]
	NintendoSwitch
}