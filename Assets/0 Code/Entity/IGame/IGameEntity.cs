using UnityEngine;
using System.Collections;

public interface IGameEntity : IEntity {
	void Begin();
	void SetPlayerName(string name);
	string GetPlayerName();
	void Closing();
	void Shutdown();
}
