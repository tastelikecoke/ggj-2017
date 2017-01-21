using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour {

	public float speed = 2f;
	public Vector3 direction;

	Color color;
	List<int> playerNumbers = new List<int>();

	bool isMarkedForDestruction = false;

	void Start() {
		direction.Normalize();
		transform.up = direction;
	}

	public void AddPlayerNumber(int playerNumber) {
		playerNumbers.Add(playerNumber);
		UpdatePlayerNumbers();
	}

	public void UpdatePlayerNumbers() {
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

	void OnCollisionEnter2D(Collision2D c) {
		Projectile p = c.gameObject.GetComponent<Projectile>();
		if (p != null && !isMarkedForDestruction) {
			int thisCount = playerNumbers.Count;
			int themCount = p.playerNumbers.Count;

			Vector3 newPosition = transform.position * thisCount + p.transform.position * themCount;
			transform.position = newPosition / (thisCount + themCount);

			Vector3 newDirection = direction * thisCount + p.direction * themCount;
			direction = newDirection / (thisCount + themCount);

			playerNumbers = playerNumbers.Union(p.playerNumbers).ToList();
			transform.up = direction;
			UpdatePlayerNumbers();

			p.isMarkedForDestruction = true;
			Destroy(p.gameObject);
		}
	}

	void Update() {
		transform.position += direction * speed * Time.deltaTime;
	}
}
