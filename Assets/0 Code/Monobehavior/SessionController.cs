using UnityEngine;
using System.Collections;

public class SessionController : MonoBehaviour {
	private ISessionEntity sessionEntity;
	private static SessionController instance;

	public static SessionController GetInstance() {
		return instance;
	}

	void Awake () {
		if (instance != null) {
			Destroy(gameObject);
			return;
		}
		instance = this;
		sessionEntity = new RaceXSessionEntity();
		DontDestroyOnLoad(transform);
	}


}