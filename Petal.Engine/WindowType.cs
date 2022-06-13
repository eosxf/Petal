using System.Runtime.Serialization;

namespace Petal.Engine;

public enum WindowType
{
	[EnumMember(Value = "windowed")]
	Windowed,
	[EnumMember(Value = "borderlessWindowed")]
	BorderlessWindowed,
	[EnumMember(Value = "fullscreen")]
	Fullscreen
}