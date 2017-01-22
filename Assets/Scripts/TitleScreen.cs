using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	public Animator animator;
	void Update() {
		if(InputManager.GetFireButtonDown(1)) {
			animator.SetTrigger("Next");
		}
	}

	public void Next() {
		Application.LoadLevel("Main");
	}
}
