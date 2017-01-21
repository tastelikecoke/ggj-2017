using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RippleTimer : MonoBehaviour {

	static RippleTimer __instance;
	public static RippleTimer GetInstance() { return __instance; }

	public float bpm;
	public float secondsPerBeat { get { return 60f / bpm; } }
	public GameObject prefab;

//	List<GameObject> ripples = new List<GameObject>();
	float lastBeatTime;

	public event System.Action eventOnPulse = () => {};

	void Awake() {
		__instance = this;
	}

	void Start() {
		lastBeatTime = Time.time;
	}

	void Update() {
		float t = lastBeatTime + secondsPerBeat;
		if (t < Time.time) {
			Pulse(Time.time - t);
		}
	}

	void Pulse(float timeOffset) {
		lastBeatTime = Time.time - timeOffset;

		GameObject g = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
		g.transform.SetParent(transform);
		g.SetActive(true);
//		ripples.Add(g);

		eventOnPulse();
	}
}
