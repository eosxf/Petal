﻿using System.Runtime.Serialization;

namespace Petal.Engine;

public enum WindowType
{
	[EnumMember(Value = "windowed")]
	Windowed,
	[EnumMember(Value = "borderlessFullscreen")]
	BorderlessFullscreen,
	[EnumMember(Value = "fullscreen")]
	Fullscreen
}