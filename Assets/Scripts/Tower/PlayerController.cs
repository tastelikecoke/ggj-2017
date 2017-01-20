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
	public GameObject projectilePrefab;

	float position = Mathf.Infinity; // in degrees

	public float startAngle { get { return Mathfx.ConvertAngle(position - arc / 2f); } }
	public float endAngle { get { return Mathfx.ConvertAngle(startAngle + arc); } } 

	public LineRenderer lineRenderer;
	public LineRenderer overlapRenderer;

	void Awake() {
		players.Add(this);
		players.Sort((a, b) => a.playerNumber - b.playerNumber);
	}

	void Start () {
		// TEST
//		Debug.Log(Mathfx.IsAngleBetween(169, 230, -169)); // true
//		Debug.Log(Mathfx.IsAngleBetween(169, 230, 191)); // true
//		Debug.Log(Mathfx.IsAngleBetween(169, -130, 191)); // true
//		Debug.Log(Mathfx.IsAngleBetween(150, -150, -176)); // true
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
//			position = 90f - Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
			position = 90f - Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
		}

		if (InputManager.GetButtonDown(playerNumber)) {
			Fire();
		}
	}

	public void Fire() {
		Debug.Log("Firing");
		Vector3 v = new Vector3(Mathf.Sin(position * Mathf.Deg2Rad), Mathf.Cos(position * Mathf.Deg2Rad), 0f) * tower.radius;
		GameObject g = Instantiate(projectilePrefab, v, Quaternion.identity) as GameObject;
		Projectile p = g.GetComponent<Projectile>();
		p.direction = v.normalized;
		p.SetPlayerNumber(playerNumber);
		g.SetActive(true);
	}

	void LateUpdate() {
		UpdatePosition();
	}

	void UpdatePosition() {
		position = Mathfx.ConvertAngle(position);

		// Check for overlaps
		bool handleOverlap = false;
		float targetStartAngle = startAngle;
		float targetEndAngle = endAngle;
		float overlapStart = 0f;
		float overlapEnd = 0f;

//		if (targetStartAngle > targetEndAngle) {
//			targetEndAngle += 360f;
//		}

		for (int i = 0; i < players.Count; i++) {
			if (players[i] == this) {
				continue;
			}

			float otherPlayerStartAngle = players[i].startAngle;
			float otherPlayerEndAngle = players[i].endAngle;
			if (otherPlayerStartAngle > otherPlayerEndAngle) {
				otherPlayerEndAngle += 360f;
			}

//			Debug.Log(">>> " + playerNumber + " > "  + targetStartAngle + " > " + targetEndAngle + " > " + players[i].playerNumber + " >> " + otherPlayerStartAngle + " > " + otherPlayerEndAngle);
			if (Mathfx.IsAngleBetween(targetStartAngle, targetEndAngle, otherPlayerStartAngle)) {
				overlapStart = otherPlayerStartAngle;
				overlapEnd = targetEndAngle;
				targetEndAngle = otherPlayerStartAngle;
				handleOverlap = true;
//				handleOverlap |= playerNumber > players[i].playerNumber;
				Debug.Log(playerNumber + " >> Start: " + targetStartAngle + " > " + targetEndAngle + " > " + players[i].playerNumber + " > " + players[i].startAngle);
			} 
			if (Mathfx.IsAngleBetween(targetStartAngle, targetEndAngle, otherPlayerEndAngle)) {
//				overlapStart = targetStartAngle;
//				overlapEnd = otherPlayerEndAngle;
				targetStartAngle = otherPlayerEndAngle;
//				handleOverlap |= playerNumber > players[i].playerNumber;
				Debug.Log(playerNumber + " >> End: " + targetStartAngle + " > " + targetEndAngle + " > " + players[i].playerNumber + " > " + players[i].startAngle);
			}
		}
//		Debug.Log("HandleOverlap: " + playerNumber + " > " + handleOverlap);

		Vector3 center = tower.transform.position;
		Vector3[] segmentPositions = new Vector3[numSegments + 1];

		if (targetStartAngle > targetEndAngle) {
			targetEndAngle += 360f;
		}
//		Debug.Log(playerNumber + ">> " + targetStartAngle + " > " + targetEndAngle + " > " + (targetEndAngle - targetStartAngle));

		float totalDelta = targetEndAngle - targetStartAngle;
		float deltaAngle = (targetEndAngle - targetStartAngle) / numSegments;
		for (int i = 0; i <= numSegments; i++) {
			float angle = (targetStartAngle + deltaAngle * i) * Mathf.Deg2Rad;
			segmentPositions[i] = center + new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * (tower.radius + (playerNumber - 1) * 0.2f);
		}
		lineRenderer.SetPositions(segmentPositions);


		if (handleOverlap) {
//			Debug.
			overlapStart = Mathfx.ConvertAngle(overlapStart);
			overlapEnd = Mathfx.ConvertAngle(overlapEnd);
			if (overlapStart > overlapEnd) {
				overlapEnd += 360f;
			}

			// Setup the overlap renderer
			overlapRenderer.SetVertexCount(numSegments + 1);
			Vector3[] overlapPositions = new Vector3[numSegments + 1];
			float overlapDeltaAngle = (overlapEnd - overlapStart) / numSegments;
			for (int i = 0; i <= numSegments; i++) {
				float angle = (overlapStart + overlapDeltaAngle * i) * Mathf.Deg2Rad;
				overlapPositions[i] = center + new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * (tower.radius + 0.4f);
			}
			overlapRenderer.SetPositions(overlapPositions);
			overlapRenderer.SetColors(Color.black, Color.black);
		} else {
			overlapRenderer.SetVertexCount(0);
		}
	}
}
