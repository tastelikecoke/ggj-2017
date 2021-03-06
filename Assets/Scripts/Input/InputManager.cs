﻿using UnityEngine;
using System.Collections;

public static class InputManager {

//	void Update() {
//		Vector2 c1Analog = new Vector2(Input.GetAxis("C1AnalogX"), Input.GetAxis("C1AnalogY"));
//		Debug.Log("C1: " + c1Analog + " " + Input.GetButton("C1A"));
//
//		Vector2 c2Analog = new Vector2(Input.GetAxis("C2AnalogX"), Input.GetAxis("C2AnalogY"));
//		Debug.Log("C2: " + c2Analog + " " + Input.GetButton("C2A"));
//
//		Vector2 c3Analog = new Vector2(Input.GetAxis("C3AnalogX"), Input.GetAxis("C3AnalogY"));
//		Debug.Log("C3: " + c3Analog + " " + Input.GetButton("C3A"));
//	}

	public static Vector2 GetAnalogOfController(int playerNumber) {
		if (playerNumber < 1 || playerNumber > 4) {
			Debug.LogError("Analog of player " + playerNumber + " does not exist.");
			return Vector2.zero;
		}

		// Debug
		if (Input.GetMouseButtonDown(playerNumber - 1)) {
			return (Vector2) Input.mousePosition - new Vector2(Screen.width / 2f, Screen.height / 2f);
		}

		return new Vector2(Input.GetAxis("C" + playerNumber + "AnalogX"), Input.GetAxis("C" + playerNumber + "AnalogY"));
	}

	public static bool GetSwitchButtonDown(int playerNumber) {
		if (playerNumber < 1 || playerNumber > 4) {
			Debug.LogError("Fuck you half.");
			return false;
		}

		if (Input.GetKeyDown(KeyCode.Alpha1) && playerNumber == 1) {
			return true;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2) && playerNumber == 2) {
			return true;
		}

		return Input.GetButtonDown("C" + playerNumber + "B");
	}

	public static bool GetFireButtonDown(int playerNumber) {
		if (playerNumber < 1 || playerNumber > 4) {
			Debug.LogError("Fuck you once.");
			return false;
		}

		if (Input.GetMouseButtonDown(playerNumber - 1)) {
			return true;
		}

		return Input.GetButtonDown("C" + playerNumber + "A");
	}

	public static bool GetFireButtonUp(int playerNumber) {
		if (playerNumber < 1 || playerNumber > 4) {
			Debug.LogError("Fuck you twice.");
			return false;
		}
		return Input.GetButtonUp("C" + playerNumber + "A");
	}

	public static bool GetFireButton(int playerNumber) {
		if (playerNumber < 1 || playerNumber > 4) {
			Debug.LogError("Fuck you thrice.");
			return false;
		}
		return Input.GetButton("C" + playerNumber + "A");
	}
}
