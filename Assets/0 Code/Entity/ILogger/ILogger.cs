using UnityEngine;
using System.Collections;

interface ILogger : IEntity {
	void Log(string message);
}
