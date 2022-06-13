using System;
using System.IO;
using System.Text;

namespace Petal.IO;

public class File : IFile
{
	private readonly FileInfo _info;

	public File(string filePath, FsType fsType = FsType.Local)
	{
		_info = new FileInfo(filePath);
		Type = fsType;
	}

	public FsType Type { get; }
	public FileAccess Access { get; set; } = FileAccess.ReadWrite;
	public FileShare Share { get; set; } = FileShare.ReadWrite;
	public Encoding Encoding { get; set; } = Encoding.UTF8;

	public bool Exists => _info.Exists;

	public IDirectory? Directory => (_info.Directory != null) ? new Directory(_info.Directory, Type) : null;

	public long Length => _info.Length;

	public string Name => _info.Name;

	public string? DirectoryName => Directory?.Name;

	public bool IsReadOnly => _info.IsReadOnly;

	public FileAttributes Attributes => _info.Attributes;

	public string Extension => _info.Extension;

	public DateTime CreationTime => _info.CreationTime;

	public string FullName => _info.FullName;

	public string FullNameWithoutExtension => FullName.Substring(0, FullName.LastIndexOf(Extension, StringComparison.InvariantCulture));

	public DateTime CreationTimeUtc => _info.CreationTimeUtc;

	public DateTime LastAccessTime => _info.LastAccessTime;

	public DateTime LastWriteTime => _info.LastWriteTime;

	public DateTime LastAccessTimeUtc => _info.LastAccessTimeUtc;

	public DateTime LastWriteTimeUtc => _info.LastWriteTimeUtc;

	public void Create()
	{
		if (Exists) return;
		Directory?.Create();
		_info.Create().Close();
	}

	public void Delete()
	{
		_info.Delete();
	}

	public Stream Open(FileMode mode = FileMode.Open)
	{
		return _info.Open(mode, Access, Share);
	}

	public bool WriteString(string text, FileMode fileMode = FileMode.Truncate)
	{
		if (Type == FsType.Internal) return false;

		if(fileMode != FileMode.CreateNew)
			EnsureCreated();
		
		using var writer = new StreamWriter(Open(fileMode), Encoding);
		writer.Write(text);
		writer.Flush();
		return true;
	}

	public bool WriteBytes(byte[] buffer, FileMode fileMode = FileMode.Truncate)
		=> WriteBytes(buffer, 0, buffer.Length, fileMode);

	public bool WriteBytes(byte[] buffer, int index, int count, FileMode fileMode = FileMode.Truncate)
	{
		if (Type == FsType.Internal) return false;
		
		if(fileMode != FileMode.CreateNew)
			EnsureCreated();
		
		using var writer = new BinaryWriter(Open(fileMode), Encoding);
		writer.Write(buffer, index, count);
		writer.Flush();
		return true;
	}

	public string ReadString(FileMode fileMode = FileMode.Open)
	{
		if (Type == FsType.Internal) return string.Empty;
		
		if (!Exists) return string.Empty;
		using var reader = new StreamReader(Open(fileMode), Encoding);
		return reader.ReadToEnd();
	}

	public byte[]? ReadBytes(FileMode fileMode = FileMode.Open)
	{
		if (Type == FsType.Internal) return null;
		
		using var stream = Open(fileMode);
		using var ms = new MemoryStream();

		stream.CopyTo(ms);
		return ms.ToArray();
	}

	public void CopyTo(IFile dest, bool overwrite = true)
	{
		// don't want to copy to a directory that doesn't/can't exist
		if (dest.Directory == null) return;
		
		dest.Directory.Create();
		_info.CopyTo(dest.FullName, overwrite);
	}

	public void MoveTo(IFile dest, bool overwrite = true)
	{
		_info.MoveTo(dest.FullName, overwrite);
	}

	public IFile CopyTo(IDirectory dest, string name, bool overwrite = true)
	{
		var file = new File(dest.FullName + '/' + name, dest.Type);
		CopyTo(file, overwrite);
		return file;
	}

	public IFile MoveTo(IDirectory dest, string name, bool overwrite = true)
	{
		var file = new File(dest.FullName + '/' + name, dest.Type);
		MoveTo(file, overwrite);
		return file;
	}

	public override string ToString() => FullName;

	public override bool Equals(object? obj)
	{
		// todo maybe check File instead of IFile
		if (obj is IFile val)
			return Type == val.Type && FullName.Equals(val.FullName);

		return false;
	}

	public override int GetHashCode()
	{
		unchecked
		{
			int hash = 97;
			hash = hash * 43 + _info.GetHashCode();
			hash = hash * 43 + FullName.GetHashCode();
			return hash;
		}
	}

	private void EnsureCreated() => Create();
}