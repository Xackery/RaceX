using UnityEngine;
using System.Collections;

public class RaceXEntity : IGameEntity {
	public BaseGameData gameData;

	public RaceXEntity() {
		gameData = new RaceXData();
	}

	public void Begin() {

	}

	public void SetPlayerName(string name) {
		if (name.Length > 127) throw new System.ArgumentException("Name length must be less than 127 characters");
		gameData.playerName = name;
	}

	public string GetPlayerName() {
		return gameData.playerName;
	}

	public void Closing() {
		Debug.Log ("Closing");
	}

	public void Shutdown() {
	}
}
