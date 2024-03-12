using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace ObjectEncoding;

public static class ObjectEncoder
{
	public static string EncodeObject<T>(
		T obj, bool includePrivateMembers = true, JsonSerializerOptions? options = null
	)
	{
		byte[] bytes;
		if (includePrivateMembers)
		{
			bytes = JsonSerializer.SerializeToUtf8Bytes(obj, InteralProperiesOptions);
		}
		else
		{
			bytes = JsonSerializer.SerializeToUtf8Bytes(obj, options);
		}
		return Convert.ToHexString(bytes);
	}

	public static string EncodeObject(
		object obj, bool includePrivateMembers = true, JsonSerializerOptions? options = null
	)
	{
		byte[] bytes;
		if (includePrivateMembers)
		{
			bytes = JsonSerializer.SerializeToUtf8Bytes(obj, obj.GetType(), InteralProperiesOptions);
		}
		else
		{
			bytes = JsonSerializer.SerializeToUtf8Bytes(obj, obj.GetType(), options);
		}
		return Convert.ToHexString(bytes);
	}

	public static T? DecodeObject<T>(
		string hexString, bool includePrivateMembers = true, JsonSerializerOptions? options = null
	)
	{
		byte[] bytes = Convert.FromHexString(hexString);
		if (includePrivateMembers)
		{
			return JsonSerializer.Deserialize<T>(bytes, InteralProperiesOptions);
		}
		else
		{
			return JsonSerializer.Deserialize<T>(bytes, options);
		}
	}

	public static object? DecodeObject(
		string hexString, Type typeInfo,
		bool includePrivateMembers = true, JsonSerializerOptions? options = null
	)
	{
		byte[] bytes = Convert.FromHexString(hexString);
		if (includePrivateMembers)
		{
			return JsonSerializer.Deserialize(bytes, typeInfo, InteralProperiesOptions);
		}
		else
		{
			return JsonSerializer.Deserialize(bytes, typeInfo, options);
		}
	}

	public static void AddInternalPropertiesModifier(JsonTypeInfo jsonTypeInfo)
	{
		if (jsonTypeInfo.Kind != JsonTypeInfoKind.Object)
			return;

		foreach (PropertyInfo property in jsonTypeInfo.Type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic))
		{
			JsonPropertyInfo jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(property.PropertyType, property.Name);
			jsonPropertyInfo.Get = property.GetValue;
			jsonPropertyInfo.Set = property.SetValue;

			jsonTypeInfo.Properties.Add(jsonPropertyInfo);
		}
	}

	public static JsonSerializerOptions InteralProperiesOptions { get; } = new()
	{
		TypeInfoResolver = new DefaultJsonTypeInfoResolver
		{
			Modifiers = { AddInternalPropertiesModifier }
		}
	};
}