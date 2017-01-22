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

	public Tower tower;
	public EnemyConfig enemySpawner;
	public LifeUpdater lifeUpdater;

	public UnityEngine.UI.Text textTimer;
	public UnityEngine.UI.Text textBestTimer;

	private float _timer = 0f;
	private float _bestTimer;

	StateMachine<GameStates> fsm;

	void Awake() {
		fsm = StateMachine<GameStates>.Initialize(this, GameStates.Start);
	}

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

	void StartGameUnits() {
		if (tower.gameObject.activeInHierarchy) return;

		tower.gameObject.SetActive(true);
		enemySpawner.gameObject.SetActive(true);
		lifeUpdater.OnStart();
	}
	void StopGameUnits() {
		if (!tower.gameObject.activeInHierarchy) return;

		lifeUpdater.OnStop();
		tower.gameObject.SetActive(false);
		enemySpawner.gameObject.SetActive(false);
	}

	//start state
	void Start_Enter() {
		Debug.Log("Game start state");
		StopGameUnits();
		_timer = 0f;
		_bestTimer = PlayerPrefs.GetFloat("BestTime", 0f);
		int minutes = (int)(_bestTimer/60);
		int seconds = (int)(_bestTimer - minutes*(60));
		string textMinutes = minutes > 9 ? ""+minutes : "0"+minutes;
		string textSeconds = seconds > 9 ? ""+seconds : "0"+seconds;
		textBestTimer.text = "Best Time: " + textMinutes + ":" + textSeconds;
	}

	void Start_Update() {
		fsm.ChangeState(GameStates.Play,StateTransition.Overwrite);
	}

	//game state
	#region PLAY_STATE
	void Play_Enter() {
		Debug.Log("Game play state");
		StartGameUnits();
		_timer = 0f;
		textTimer.text = "00:00";
	}

	void Play_Update() {
		if (tower.lives <= 0) {
			fsm.ChangeState(GameStates.Lose);
		}
		_timer += Time.deltaTime;
		int minutes = (int)(_timer/60);
		int seconds = (int)(_timer - minutes*(60));
		string textMinutes = minutes > 9 ? ""+minutes : "0"+minutes;
		string textSeconds = seconds > 9 ? ""+seconds : "0"+seconds;
		textTimer.text = textMinutes + ":" + textSeconds;

		if (_timer > _bestTimer) {
			_bestTimer = _timer;
			PlayerPrefs.SetFloat("BestTime", _bestTimer);
			int b_minutes = (int)(_bestTimer/60);
			int b_seconds = (int)(_bestTimer - minutes*(60));
			string b_textMinutes = minutes > 9 ? ""+minutes : "0"+minutes;
			string b_textSeconds = seconds > 9 ? ""+seconds : "0"+seconds;
			textBestTimer.text = "Best Time: " + b_textMinutes + ":" + b_textSeconds;
		}
	}

	void Play_Exit() {
	}
	#endregion

	//lose state
	#region LOSE_STATE
	void Lose_Enter() {
		//StopGameUnits();
		Debug.Log("you lost!");
		//fade out to the title screen
		FadeOutOverlay.Instance.FadeOut(1.5f, () => {
			//StopGameUnits();
			UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
		}); //Application.LoadLevel("Start"));
	}

	void Lose_Update() {
	}

	void Lose_Exit() {
		
	}
	#endregion
}
