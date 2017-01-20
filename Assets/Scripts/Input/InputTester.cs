using UnityEngine;
using System.Collections;

public class InputTester : MonoBehaviour {

	void Update() {
		for (int i = 1; i <= 4; i++) {
			if (InputManager.GetAnalogOfController(i) != Vector2.zero) {
				Debug.Log("Player " + i + " moving!");
			}
			if (InputManager.GetButton(i)) {
				Debug.Log("Player " + i + " thing!");
			}
		}
	}

}
