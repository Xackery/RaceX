using UnityEngine;
using System.Collections;

public class DebugLogger : ILogger {
	public void Log(string message) {
		Debug.Log (message);
	}
}
