using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	[System.NonSerialized]
	public Vector3 movement;
	[System.NonSerialized]
	public EnemyColor enemyColor;
	public SpriteRenderer whole;
	public SpriteRenderer eyes;

	public void Init(Vector3 movement, EnemyColor enemyColor) {
		this.movement = movement;
		this.enemyColor = enemyColor;
		eyes.color = enemyColor.GetColor();
	}
	public void Update() {
		transform.position += movement * Time.deltaTime;
		Vector3 back = new Vector3(0f, 1f, 0f);

		if(movement.x >= 0) {
			whole.transform.localScale = new Vector3(Mathf.Abs(whole.transform.localScale.x), whole.transform.localScale.y, whole.transform.localScale.z);
		} else {
			whole.transform.localScale = new Vector3(-Mathf.Abs(whole.transform.localScale.x), whole.transform.localScale.y, whole.transform.localScale.z);
		}

		if(Mathf.Abs(Vector3.Angle(back, movement)) < 45f) {
			Animator animator = GetComponent<Animator>();
			animator.SetTrigger("Back");
		}

		if (transform.position.magnitude <= Tower.GetInstance().radius) {
			OnGetToCenter();
		}
	}

	void OnGetToCenter() {
		Tower.GetInstance().lives--;
		Explode();
	}
	public void Explode() {
		StartCoroutine(BeginDeathCR());
	}

	IEnumerator BeginDeathCR(){
		movement = Vector3.zero;
		GetComponent<Collider2D>().enabled = false;
		Animator animator = GetComponent<Animator>();
		animator.SetTrigger("Death");

		yield return new WaitForSeconds(1f);
		yield return null;
		Destroy(gameObject);
	}
}
