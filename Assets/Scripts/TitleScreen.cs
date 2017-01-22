using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	public Animator animator;
	void Update() {
		if(InputManager.GetFireButtonDown(1) || InputManager.GetFireButtonDown(2)) {
			animator.SetTrigger("Next");
		}
	}

	public void Next() {
		Application.LoadLevel("Main");
	}
}
