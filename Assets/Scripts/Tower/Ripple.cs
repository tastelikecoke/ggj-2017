using UnityEngine;
using System.Collections;

public class Ripple : MonoBehaviour {

	public RippleTimer timer;
	int pulseCount = 0;
	bool isScaling = true;

	void Start() {
		timer = RippleTimer.GetInstance();
//		timer.eventOnPulse += OnPulse;

		LeanTween.scale(gameObject, Vector3.one * Tower.GetInstance().radius, timer.secondsPerBeat).setEase(LeanTweenType.easeOutQuad).setOnComplete(() => isScaling = true);
	}

	void Update() {
		if (isScaling) {
			transform.localScale = Vector3.one * (transform.localScale.x + 8 * Time.deltaTime);
		}
	}

//	void OnDestroy() {
//		timer.eventOnPulse -= OnPulse;
//	}

//	void OnPulse() {
//		pulseCount++;
//		Vector3 scale = transform.localScale;
//		LeanTween.scale(gameObject, Vector3.one * pulseCount * 3, timer.secondsPerBeat).setEase(LeanTweenType.easeOutExpo);
//		LeanTween.scale(gameObject, Vector3.one * pulseCount * 3, timer.secondsPerBeat).setEase(LeanTweenType.easeInOutQuad);
//	}

}
