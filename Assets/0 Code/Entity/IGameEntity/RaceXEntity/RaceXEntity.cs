using UnityEngine;
using System.Collections;

public class RaceXStartup : IGameEntity {
	private bool isInitialized;

	public void Initialize() {
		if (isInitialized) return;
		Debug.Log ("Starting");
		isInitialized = true;
	}

	public void Begin() {
	}

	public void Closing() {
		Debug.Log ("Closing");
	}

	public void Shutdown() {
	}
}
