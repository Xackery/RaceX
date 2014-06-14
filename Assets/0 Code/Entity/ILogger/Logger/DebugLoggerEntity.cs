using UnityEngine;
using System.Collections;

public class DebugLoggerEntity : ILoggerEntity {
	public void Log(string message) {
		Debug.Log (message);
	}
}
