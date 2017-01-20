using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour {

	public float speed = 2f;
	public Vector3 direction;
	Color color;
	List<int> playerNumbers = new List<int>();

	void Start() {
		direction.Normalize();
		transform.up = direction;
	}

	public void SetPlayerNumber(int playerNumber) {
		playerNumbers.Add(playerNumber);
		if (playerNumbers.Count == 1) {
			color = PlayerController.PLAYER_COLORS[playerNumbers[0] - 1];
		} else if (playerNumbers.Count == 2) {
			if (playerNumbers.Contains(1)) {
				if (playerNumbers.Contains(2)) {
					color = new Color(1f, 0.5f, 0f); // orange
				} else if (playerNumbers.Contains(3)) {
					color = new Color(1f, 0f, 1f); // violet
				}
			} else if (playerNumbers.Contains(2)) {
				color = new Color(0f, 1f, 0f); // green
			}
		}

		GetComponent<SpriteRenderer>().color = color;
	}

	void Update() {
		transform.position += direction * speed * Time.deltaTime;
	}


}
