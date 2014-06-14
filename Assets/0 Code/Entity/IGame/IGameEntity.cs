using UnityEngine;
using System.Collections;

public interface IGameEntity : IEntity {
	void Begin();
	void SetPlayerName(string name);
	string GetPlayerName();
	BaseGameData GetGameData();
	void SetGameData(BaseGameData data);
	void Closing();
	void Shutdown();
}
