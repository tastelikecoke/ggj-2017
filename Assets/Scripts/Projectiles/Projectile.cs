﻿using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour {

	public float speed = 2f;
	public Vector3 direction;
	public float sizeMultiplier;

	EnemyColor color;
	List<int> playerNumbers = new List<int>();

	bool isMarkedForDestruction = false;

	void Start() {
		direction.Normalize();
		transform.up = direction;
		UpdateScale();

		Destroy(gameObject, 10f);
	}

	void UpdateScale() {
		Vector3 s = transform.localScale;
		s.x *= sizeMultiplier;
		transform.localScale = s;
	}

	public void AddPlayerNumber(int playerNumber) {
		playerNumbers.Add(playerNumber);
		UpdatePlayerNumbers();
	}

	void UpdatePlayerNumbers() {
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

	void OnTriggerEnter2D(Collider2D c) {
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

			sizeMultiplier = ((sizeMultiplier * thisCount) + (p.sizeMultiplier * themCount)) / (thisCount + themCount);
			UpdateScale();

			p.isMarkedForDestruction = true;
			Destroy(p.gameObject);
		}

		Enemy e = c.gameObject.GetComponent<Enemy>();
		if (e != null) {
			Debug.Log("Enemy is a dude!");
			if (e.enemyColor == color) {
				e.Explode();
			}
			StartCoroutine(BeginDeathCR());
		}
	}

	public IEnumerator BeginDeathCR() {
		GetComponent<Collider2D>().enabled = false;
		SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
		for(int i = 0; i < 100; i++){
			for (int o = 0; o < renderers.Length; o++) {
				Color c = renderers[o].color;
				c.a *= 0.8f;
				renderers[o].color = c;
				yield return null;
			}
		}
		Destroy(gameObject);
	}

	void Update() {
		transform.position += direction * speed * Time.deltaTime;
	}
}
