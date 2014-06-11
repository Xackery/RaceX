using UnityEngine;
using System.Collections;

public interface IGameEntity : IEntity {
	void Initialize();
	void Begin();
	void Closing();
	void Shutdown();
}
