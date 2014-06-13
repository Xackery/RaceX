using UnityEngine;
using System.Collections;

interface ILoggerEntity : IEntity {
	void Log(string message);
}
