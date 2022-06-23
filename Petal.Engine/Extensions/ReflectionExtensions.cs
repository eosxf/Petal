using System;
using System.Reflection;

namespace Petal.Engine.Extensions;

public static class ReflectionExtensions
{
	public static FieldInfo? GetFieldRecursively(this Type type, string fieldName)
	{
		var searchType = type; // searchType is nullable, don't let the rhs trick you
		
		FieldInfo? fieldInfo = null;
		do
		{
			fieldInfo = searchType?.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			searchType = type.BaseType;
		} while (fieldInfo == null && searchType != null);
		
		return fieldInfo;
	}
}