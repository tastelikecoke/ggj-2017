using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public static readonly Color[] PLAYER_COLORS = { Color.red, Color.yellow, Color.blue } ;
	static List<PlayerController> players = new List<PlayerController>();

	public int playerNumber;
	public int numSegments;
	public float arc; // in degrees
	public Tower tower;

	float position = Mathf.Infinity; // in degrees

	public float startAngle { get { return position - arc / 2f; } }
	public float endAngle { get { return startAngle + arc; } } 

	public LineRenderer lineRenderer;
	public LineRenderer overlapRenderer;

	void Awake() {
		players.Add(this);
		players.Sort((a, b) => a.playerNumber - b.playerNumber);
	}

	void Start () {
		// TEST
//		Debug.Log(Mathfx.IsAngleBetween(150, 210, 185)); // true
//		Debug.Log(Mathfx.IsAngleBetween(150, 210, 220)); // false
//		Debug.Log(Mathfx.IsAngleBetween(150, 210, 145)); // false
//		Debug.Log(Mathfx.IsAngleBetween(-90, 90, 0)); // true
//		Debug.Log(Mathfx.IsAngleBetween(-90, 90, 50)); // true
//		Debug.Log(Mathfx.IsAngleBetween(-90, 90, -30)); // true
//		Debug.Log(Mathfx.IsAngleBetween(-90, 90, 100)); // false
//		Debug.Log(Mathfx.IsAngleBetween(-90, 90, -100)); // false
//		Debug.Log(Mathfx.IsAngleBetween(170, 10, 180)); // true

		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetVertexCount(numSegments + 1);
		lineRenderer.SetColors(PLAYER_COLORS[playerNumber - 1], PLAYER_COLORS[playerNumber - 1]);
		lineRenderer.sortingOrder = playerNumber;

		position = 0 + 90f * (playerNumber - 1);
	}

	void Update() {
		Vector2 v = InputManager.GetAnalogOfController(playerNumber);
		if (v.magnitude > 0f) {
			v.Normalize();
			position = 90f - Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
		}
	}

	void LateUpdate() {
		UpdatePosition();
	}

	void UpdatePosition() {

		// Check for overlaps
		bool handleOverlap = false;
		float targetStartAngle = startAngle;
		float targetEndAngle = endAngle;
		float overlapStart = 0f;
		float overlapEnd = 0f;
		for (int i = 0; i < players.Count; i++) {
			if (players[i] == this) {
				continue;
			}

			if (playerNumber == 2) {
				Debug.Log(">>>" + targetStartAngle + " > " + targetEndAngle + " > " + players[i].startAngle + " > " + players[i].endAngle);
			}
			if (Mathfx.IsAngleBetween(targetStartAngle, targetEndAngle, players[i].startAngle)) {
//				if (playerNumber == 2) {
//					Debug.Log("Start: " + targetStartAngle + " > " + targetEndAngle + " > " + players[i].startAngle);
//				}
				overlapStart = players[i].startAngle;
				overlapEnd = targetEndAngle;
				targetEndAngle = players[i].startAngle;
				handleOverlap |= playerNumber > players[i].playerNumber;
//				if (playerNumber == 2) {
//					Debug.Log("Start: " + targetStartAngle + " > " + targetEndAngle);
//				}
			} 
			if (Mathfx.IsAngleBetween(targetStartAngle, targetEndAngle, players[i].endAngle)) {
				if (playerNumber == 2) {
					Debug.Log("End: " + targetStartAngle + " > " + targetEndAngle + " > " + players[i].endAngle);
				}

//				if (Mathfx.ConvertAngle(

				overlapStart = targetStartAngle;
				overlapEnd = players[i].endAngle;
				targetStartAngle = players[i].endAngle;
				handleOverlap |= playerNumber > players[i].playerNumber;

				if (playerNumber == 2) {
					Debug.Log("End: " + targetStartAngle + " > " + targetEndAngle + " > " + players[i].endAngle);
				}
			}
		}
//		Debug.Log("HandleOverlap: " + playerNumber + " > " + handleOverlap);

//		targetStartAngle = Mathfx.ConvertAngle(targetStartAngle);
//		targetEndAngle = Mathfx.ConvertAngle(targetEndAngle);
//		overlapStart = Mathfx.ConvertAngle(overlapStart);
//		overlapEnd = Mathfx.ConvertAngle(overlapEnd);

		Vector3 center = tower.transform.position;
		Vector3[] segmentPositions = new Vector3[numSegments + 1];

		if (playerNumber == 2) {
			Debug.Log(targetStartAngle + " > " + targetEndAngle + " > " + (targetEndAngle - targetStartAngle));
		}
		float deltaAngle = (targetEndAngle - targetStartAngle) / numSegments;
		for (int i = 0; i <= numSegments; i++) {
			float angle = (targetStartAngle + deltaAngle * i) * Mathf.Deg2Rad;
			segmentPositions[i] = center + new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * tower.radius;
		}
		lineRenderer.SetPositions(segmentPositions);


		if (handleOverlap) {
			// Setup the overlap renderer
			overlapRenderer.SetVertexCount(numSegments + 1);
			Vector3[] overlapPositions = new Vector3[numSegments + 1];
//			if (playerNumber == 2) {
//				Debug.Log(overlapStart + " > " + overlapEnd + " > " + (overlapEnd - overlapStart));
//			}
			float overlapDeltaAngle = (overlapEnd - overlapStart) / numSegments;
			for (int i = 0; i <= numSegments; i++) {
				float angle = (overlapStart + overlapDeltaAngle * i) * Mathf.Deg2Rad;
				overlapPositions[i] = center + new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * tower.radius;
			}
			overlapRenderer.SetPositions(overlapPositions);
			overlapRenderer.SetColors(Color.black, Color.black);
		} else {
			overlapRenderer.SetVertexCount(0);
		}
	}
}
