using UnityEngine;
using System.Collections;

public class RaceXEntity : IGameEntity {
	public BaseGameData gameData;

	public RaceXEntity() {
		gameData = new RaceXData();
	}

	public void Begin() {

	}

	public BaseGameData GetGameData() {
		return gameData;
	}

	public void SetGameData(BaseGameData data) {
		gameData = data;
	}

	public void SetPlayerName(string name) {
		if (name.Length > 127) throw new System.ArgumentException("Name length must be less than 127 characters");
		gameData.playerName = name;
	}

	public string GetPlayerName() {
		return gameData.playerName;
	}

	public int GetGameScore() {
		return gameData.gameScore;
	}

	public void AddGameScore(int value) {
		if (value < 0) throw new System.ArgumentException("Can only add values larger than 0");
		gameData.gameScore += value;
	}

	public void SubtractGameScore(int value) {
		if (value < 0) throw new System.ArgumentException("Can only subtract values larger than 0");
		gameData.gameScore -= value;
	}

	public void SetGameScore(int value) {
		gameData.gameScore = value;
	}

	public void Closing() {
		Debug.Log ("Closing");
	}

	public void Shutdown() {
	}
}
