using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JsonFx.Json;

public class JSONSerializer : ISerializer {
	private static JSONSerializer jsonManagerInstance;
	
	public JSONSerializer() {
		if (jsonManagerInstance == null) return;
		jsonManagerInstance = new JSONSerializer();
	}
	
	public void SerializeToPlayerPrefs(string PlayerPrefsKey, object ObjectInstance) {
		if (ObjectInstance == null) return;
		if (PlayerPrefsKey.Length < 1) return;
		
		string stringData = SerializeToString(ObjectInstance);
		PlayerPrefs.SetString(PlayerPrefsKey, stringData);
	}
	
	public T DeserializeFromPlayerPrefs<T>(string PlayerPrefsKey)
	{
		if (PlayerPrefsKey.Length < 1) return default(T); //Return default type (null) to request if no string contents
		string playerPrefsValue = PlayerPrefs.GetString(PlayerPrefsKey);
		if (playerPrefsValue.Length < 1) return default(T);
		return DeserializeFromString<T>(playerPrefsValue);
	}
	
	public string SerializeToString(object ObjectInstance)
	{
		return JsonWriter.Serialize(ObjectInstance);
	}
	
	public T DeserializeFromString<T>(string StringData)
	{
		if (StringData.Length < 1) return default(T);
		return JsonReader.Deserialize<T>(StringData);
	}
}
