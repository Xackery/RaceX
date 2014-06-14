using UnityEngine;
using System.Collections;

public abstract class BaseGameData : BaseData {
	public float timePlayingTotal;
	public float timePlayingThisSession;
	public string playerName = "";
	public int gameScore;
}
