using UnityEngine;
using System.Collections;

public class GameController : BaseComponent {
	private IGameEntity gameEntity;
	private ILogger gameLog;

	void Awake() {
		BaseComponentBegin();
		if (gameEntity == null) gameEntity = new RaceXStartup();
		gameEntity.Initialize();
		DontDestroyOnLoad(transform);
	}

	void OnApplicationQuit() {
		gameEntity.Closing();
	}

}
