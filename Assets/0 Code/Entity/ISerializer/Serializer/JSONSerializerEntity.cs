using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JsonFx.Json;

public class JSONSerializerEntity : ISerializerEntity {
	
	public string SerializeToString(object objectInstance)
	{
		return JsonWriter.Serialize(objectInstance);
	}
	
	public T DeserializeFromString<T>(string stringData)
	{
		if (stringData.Length < 1) return default(T);
		return JsonReader.Deserialize<T>(stringData);
	}
}
