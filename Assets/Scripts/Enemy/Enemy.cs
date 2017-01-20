using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	[System.NonSerialized]
	public Vector3 movement;
	[System.NonSerialized]
	public int color;
	public SpriteRenderer eyes;

	public void Init(Vector3 movement, int color) {
		this.movement = movement;
		this.color = color;
		eyes.color = Color.red;
	}
	public void Update() {
		transform.position += movement * Time.deltaTime;
		Vector3 back = new Vector3(0f, 1f, 0f);
		

		if(movement.x >= 0) {
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y);
		}
		else {
			transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
		}

		if(Mathf.Abs(Vector3.Angle(back, movement)) < 45f) {
			Animator animator = GetComponent<Animator>();
			animator.SetTrigger("Back");
		}

		if(transform.position.magnitude <= 1f) {
			Explode();
		}
	}
	public void Explode() {
		Destroy(gameObject);
	}
}
