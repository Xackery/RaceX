using UnityEngine;
using System.Collections;

public interface ISerializerEntity : IEntity {
	void SerializeToPlayerPrefs(string PlayerPrefsKey, object ObjectInstance);
	
	T DeserializeFromPlayerPrefs<T>(string PlayerPrefsKey);
	
	string SerializeToString(object ObjectInstance);
	
	T DeserializeFromString<T>(string StringData);
}
