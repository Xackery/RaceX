using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
//Helpful references: http://stackoverflow.com/questions/2347642/deserialize-from-string-instead-textreader
//Helpful references: http://unitynoobs.blogspot.com/2011/02/xml-loading-data-from-xml-file.html#more
//Helpful references: http://wiki.unity3d.com/index.php?title=Saving_and_Loading_Data:_XmlSerializer

public class XMLSerializerEntity : ISerializerEntity {

	public string SerializeToString(this object objectInstance) {
		var serializer = new XmlSerializer(objectInstance.GetType());
		var stringBuilder = new System.Text.StringBuilder();
		
		using (TextWriter writer = new StringWriter(stringBuilder)) {
			serializer.Serialize(writer, objectInstance);
		}
		
		return stringBuilder.ToString();
	}

	public T DeserializeFromString<T>(string data) {
		if (data.Length < 1) return default(T); //Return default type (null) to request if no string contents
		return (T)DeserializeFromString(data, typeof(T)); //call overload with proper structure
	}

	public object DeserializeFromString(string data, System.Type type) {
		if (data.Length < 1) return null;
		var tmpSerializer = new XmlSerializer(type);
		object tmpResult;
		
		using (TextReader tmpReader = new StringReader(data))
		{
			tmpResult = tmpSerializer.Deserialize(tmpReader);
		}
		
		return tmpResult;
	}

}
