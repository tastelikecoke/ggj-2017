using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	[System.NonSerialized]
	public Vector3 movement;
	[System.NonSerialized]
	public int color;

	public void Init(Vector3 movement, int color) {
		this.movement = movement;
		this.color = color;

		SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
		renderer.color = Color.red;
	}
	public void Update() {
		transform.position += movement * Time.deltaTime;
		if(transform.position.magnitude <= 1f) {
			Explode();
		}
	}
	public void Explode() {
		Destroy(gameObject);
	}
}
