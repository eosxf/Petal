using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Petal.Engine.Compatibility;
using Petal.IO;

namespace Petal.Engine;

public class PetalConfiguration
{
	public static JsonSerializerOptions PetalConfigurationJsonSettings() => new JsonSerializerOptions
	{
		WriteIndented = true,
		IncludeFields = true,
		Converters =
		{
			new JsonStringEnumConverter()
		}
	};

	public readonly string WindowTitle;
	public readonly int WindowWidth;
	public readonly int WindowHeight;
	public readonly WindowType WindowType;
	public readonly bool IsWindowUserResizable;
	public readonly bool IsMouseVisible;
	public readonly Platforms Platform;
	public readonly bool VSync;
	public readonly int DesiredFramerate;

	public PetalConfiguration()
		: this(new SerialView())
	{
		
	}
	
	public PetalConfiguration(IFile file) 
		: this(JsonSerializer.Deserialize<SerialView>(file.ReadString(), PetalConfigurationJsonSettings())!)
	{
		
	}

	public PetalConfiguration(SerialView serial) // todo use !! in C# 11
	{
		// we're using null forgiving so serial can be null! <-- heh
		if (serial == null) throw new ArgumentNullException(nameof(serial));
		
		WindowTitle = serial.WindowTitle;
		WindowWidth = serial.WindowWidth;
		WindowHeight = serial.WindowHeight;
		WindowType = serial.WindowType;
		IsWindowUserResizable = serial.IsWindowUserResizable;
		IsMouseVisible = serial.IsMouseVisible;
		Platform = serial.Platform;
		VSync = serial.VSync;
		DesiredFramerate = serial.DesiredFramerate;
	}

	[Serializable]
	public class SerialView
	{
		[JsonPropertyName("windowTitle")]
		public string WindowTitle = "Petal";
		[JsonPropertyName("windowWidth")]
		public int WindowWidth = 960;
		[JsonPropertyName("windowHeight")]
		public int WindowHeight = 540;
		[JsonPropertyName("windowType")]
		public WindowType WindowType = WindowType.Windowed;
		[JsonPropertyName("isMouseVisible")]
		public bool IsMouseVisible = true;
		[JsonPropertyName("platform")]
		public Platforms Platform = Platforms.Windows;
		[JsonPropertyName("vSync")]
		public bool VSync = false;
		[JsonPropertyName("desiredFramerate")]
		public int DesiredFramerate = 60;
		[JsonPropertyName("isWindowUserResizable")]
		public bool IsWindowUserResizable;
		
		[JsonConstructor]
		public SerialView()
		{
			
		}
	}

	public override string ToString()
	{
		return $"PetalConfiguration[" +
		       $"WindowTitle: {WindowTitle}, WindowWidth: {WindowWidth}, WindowHeight: " +
		       $"{WindowHeight}, WindowType: {WindowType}, IsWindowResizable: " +
		       $"{IsWindowUserResizable}, IsMouseVisible: {IsMouseVisible}]";
	}
}