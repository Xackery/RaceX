using UnityEngine;
using System.Collections;

public interface ISerializerEntity : IEntity {
	string SerializeToString(object ObjectInstance);
	
	T DeserializeFromString<T>(string StringData);
}
