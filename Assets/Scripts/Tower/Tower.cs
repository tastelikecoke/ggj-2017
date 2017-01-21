using UnityEngine;
using System;
using System.Collections;

public class Tower : MonoBehaviour {

	static Tower __instance;
	public static Tower GetInstance() { return __instance; }

	public float radius;

	int _lives = 10;
	public event Action eventOnLivesChange = () => {};
	public int lives { get { return _lives; } set { 
			_lives = value; 
		} }

	void Awake() {
		__instance = this;
	}

}
