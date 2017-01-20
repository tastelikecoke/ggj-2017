//TODO:
//manage enemies
//manage input
//manage

using UnityEngine;
using System.Collections;
using MonsterLove.StateMachine;
using PubSub;

public enum GameStates {
	Start,
	Play,
	Win,
	Lose
}

public class GameMgr : MonoBehaviour {
	[SerializeField]
	Camera mainCamera;
	private static GameMgr _instance;
	private PubSubBroker _pubSubMgr = new PubSubBroker();

	[SerializeField]
	private int _hp;
	[SerializeField]
	private int _points;
	[SerializeField]
	private float _difficulty;

	void OnEnable() {
		if (_instance == null) {
			_instance = this;
			DontDestroyOnLoad(this);
		}
	}

	public PubSubBroker PubSubMgr {
		get {
			return _pubSubMgr;
		}
	}

	static public GameMgr Instance {
		get { return _instance; }
	}

	//start state
	void Start_Enter() {
		Debug.Log("Game start state");
	}

	//game state
	#region PLAY_STATE
	void Play_Enter() {
		Debug.Log("Game play state");
	}

	void Play_Update() {

	}

	void Play_Exit() {

	}
	#endregion

	//win state
	#region WIN_STATE
	void Win_Enter() {

	}

	void Win_Update() {

	}

	void Win_Exit() {

	}
	#endregion

	//lose state
	#region LOSE_STATE
	void Lose_Enter() {

	}

	void Lose_Update() {

	}

	void Lose_Exit() {
		
	}
	#endregion
}
