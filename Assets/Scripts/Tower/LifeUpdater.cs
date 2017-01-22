using UnityEngine;
using System.Collections;

public class LifeUpdater : MonoBehaviour {

	public GameObject[] lives;

	public void OnStart() {
		if (!Tower.GetInstance()) {
			Debug.Log("tower is null, ignore for now");
			return;
		}

		Debug.Log("Starting LifeUpdater");
		Tower.GetInstance().eventOnLivesChange += OnUpdateLives;
	}

	public void OnStop() {
		if (!Tower.GetInstance()) {
			Debug.Log("tower is null, ignore for now");
			return;
		}

		Debug.Log("Stopping LifeUpdater");
		Tower.GetInstance().eventOnLivesChange -= OnUpdateLives;
	}

	void OnUpdateLives() {
		int livesLeft = Tower.GetInstance().lives;
		for (int i = 0; i < lives.Length; i++) {
			lives[i].SetActive(i < livesLeft);
		}
	}

}
