using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PingSpawner : MonoBehaviour {

	public GameObject prefab;
	public float pingInterval;
	public float scale;
	public float duration;
	float nextPingTime;

	List<GameObject> pings = new List<GameObject>();

	void Start() {
		nextPingTime = Time.time;	 
	}

	void Update () {
		if (Time.time > nextPingTime) {
			Ping();
		}
	}

	void Ping() {
		nextPingTime = Time.time + pingInterval;

		GameObject g = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
		g.transform.localScale = Vector3.zero;
		g.transform.SetParent(transform);
		g.SetActive(true);

		LeanTween.scale(g, Vector3.one * scale, duration).setEase(LeanTweenType.easeOutSine);
		LeanTween.value(1f, 0f, duration).setOnUpdate((f) => {
			if (g != null) {
				Color c = g.GetComponent<SpriteRenderer>().color;
				c.a = f;
				g.GetComponent<SpriteRenderer>().color = c;
			}
		}).setOnComplete(() => Destroy (g));

		pings.Add(g);
	}

	void OnDestroy() {
		for (int i = 0; i < pings.Count; i++) {
			if (pings[i] != null) {
				LeanTween.cancel(pings[i]);
			}
		}
	}
}
