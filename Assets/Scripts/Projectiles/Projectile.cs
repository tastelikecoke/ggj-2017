using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour {

	public float speed = 2f;
	public Vector3 direction;

	EnemyColor color;
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
			color = EnemyColorExtensions.GetEnemyColorFromPlayerNumber(playerNumbers[0]);
		} else if (playerNumbers.Count == 2) {
			if (playerNumbers.Contains(1)) {
				if (playerNumbers.Contains(2)) {
					color = EnemyColor.Orange;
				} else if (playerNumbers.Contains(3)) {
					color = EnemyColor.Violet;
				}
			} else if (playerNumbers.Contains(2)) {
				color = EnemyColor.Green;
			}
		}

		GetComponent<SpriteRenderer>().color = color.GetColor();
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

		Enemy e = c.gameObject.GetComponent<Enemy>();
		if (e != null) {
			if (e.enemyColor == color) {
				e.Explode();
			}
			Destroy(gameObject);
		}
	}

	void Update() {
		transform.position += direction * speed * Time.deltaTime;
	}
}
