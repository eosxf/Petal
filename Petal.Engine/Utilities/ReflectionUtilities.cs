using System;
using System.Reflection;

namespace Petal.Engine.Utilities;

public static class ReflectionUtilities
{
	public static FieldInfo GetFieldInfo(object targetObject, string fieldName) => GetFieldInfo(targetObject.GetType(), fieldName);

	public static FieldInfo GetFieldInfo(Type type, string fieldName)
	{
		Type? searchType = type;
		
		FieldInfo? fieldInfo = null;
		do
		{
			fieldInfo = searchType?.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			searchType = type.BaseType;
		} while (fieldInfo == null && searchType != null);

		// if we get here fieldInfo has to not be null, not sure why static analysis doesn't realize it
		return fieldInfo!;
	}

	
}