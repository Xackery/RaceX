using UnityEngine;
using System.Collections;

public interface IGameEntity : IEntity {
	void Begin();
	void SetPlayerName(string name);
	string GetPlayerName();
	BaseGameData GetGameData();
	void SetGameData(BaseGameData data);

	int GetGameScore();
	void SetGameScore(int value);
	void SubtractGameScore(int value);
	void AddGameScore(int value);

	void Closing();
	void Shutdown();
}
