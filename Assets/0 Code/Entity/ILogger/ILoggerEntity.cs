using UnityEngine;
using System.Collections;

public interface ILoggerEntity : IEntity {
	void Log(string message);
}
