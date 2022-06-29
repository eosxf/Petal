﻿using System;
using Microsoft.Xna.Framework;

namespace Petal.Engine.Math;

[Serializable]
public struct Vector2Int : IEquatable<Vector2Int>
{
	public int X;
	public int Y;

	public Vector2Int(int x, int y)
	{
		X = x;
		Y = y;
	}

	public Vector2Int(int val)
	{
		X = val;
		Y = val;
	}

	public Vector2Int(Vector2 vec)
	{
		X = (int) vec.X;
		Y = (int) vec.Y;
	}

	public Vector2Int(Rectangle rect)
	{
		X = rect.Width;
		Y = rect.Height;
	}
	
	public bool Equals(Vector2Int other)
	{
		return X == other.X && Y == other.Y;
	}

	public override bool Equals(object? obj)
	{
		return obj is Vector2Int other && Equals(other);
	}

	public override int GetHashCode() => X.GetHashCode() + Y.GetHashCode();

	public static explicit operator Vector2Int(Vector2 vec) => new((int)vec.X, (int)vec.Y);
	public static explicit operator Vector2(Vector2Int vec) => new(vec.X, vec.Y);
	public static explicit operator Vector2Int(Rectangle rect) => new(rect.Width, rect.Height);
	public static explicit operator Rectangle(Vector2Int vec) => new(0, 0, vec.X, vec.Y);
}