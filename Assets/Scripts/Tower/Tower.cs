using UnityEngine;
using System;
using System.Collections;
using PubSub;

public class Tower : MonoBehaviour {

	static Tower __instance;
	public static Tower GetInstance() { return __instance; }

	public float radius;

	public int MAX_LIVES = 10;
	int _lives = 10;
	public event Action eventOnLivesChange = () => {};

	public int lives {
		get { return _lives; }
		set { 
			if (_lives != value) {
				_lives = value; 
				eventOnLivesChange();
//				Debug.Log("On lives change");
			}
		}
	}

	public void ResetLives() {
		lives = MAX_LIVES;
	}

	void Awake() {
		__instance = this;
	}

}
