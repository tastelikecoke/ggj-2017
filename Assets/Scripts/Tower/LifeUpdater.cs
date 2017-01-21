using UnityEngine;
using System.Collections;

public class LifeUpdater : MonoBehaviour {

	public GameObject[] lives;

	void Start() {
		Tower.GetInstance().eventOnLivesChange += OnUpdateLives;
	}

	void OnUpdateLives() {
		int livesLeft = Tower.GetInstance().lives;
		for (int i = 0; i < lives.Length; i++) {
			lives[i].SetActive(i < livesLeft);
		}
	}

}
