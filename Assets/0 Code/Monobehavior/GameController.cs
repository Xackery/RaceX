using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	private IGameEntity gameEntity;
	private ILoggerEntity logEntity;

	private static GameController instance;

	public static GameController GetInstance() {
		return instance;
	}

	void Awake() {
		if (instance != null) {
			Destroy(gameObject);
			return;
		}
		instance = this;
		gameEntity = new RaceXEntity();
		DontDestroyOnLoad(transform);
	}



	void OnApplicationQuit() {
		gameEntity.Closing();
	}

}
