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
	float newPosition = Mathf.Infinity;

	public LineRenderer lineRenderer;
	public LineRenderer overlapRenderer;

	void Awake() {
		players.Add(this);
//		players.Sort((a, b) => a.playerNumber - b.playerNumber);

//		for (int i = 0; i < players.Count; i++) {
//			Debug.Log("palyer: " + players[i].playerNumber);
//		}
	}

	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetVertexCount(numSegments + 1);
		lineRenderer.SetColors(PLAYER_COLORS[playerNumber - 1], PLAYER_COLORS[playerNumber - 1]);
		lineRenderer.sortingOrder = playerNumber;

		SetPosition(0 + 90f * (playerNumber - 1));
	}

	void Update() {
		Vector2 v = InputManager.GetAnalogOfController(playerNumber);
		if (v.magnitude > 0f) {
			v.Normalize();
			newPosition = 90f - Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
		}
	}

	void LateUpdate() {
		SetPosition(newPosition);
	}

	void SetPosition(float position) {
		this.position = position;

		// Check for overlaps
		bool handleOverlap = false;
		float startAngle = position - arc / 2f;
		float endAngle = startAngle + arc; 
		for (int i = 0; i < players.Count; i++) {
			if (players[i] == this) {
				continue;
			}


		}

//		if (playerNumberToHandleOverlap == playerNumber) {
//			// Setup the overlap renderer
//		}

		Vector3 center = tower.transform.position;
		Vector3[] segmentPositions = new Vector3[numSegments + 1];

		float deltaAngle = arc / numSegments;
		Debug.Log(startAngle + " > " + deltaAngle);
		for (int i = 0; i <= numSegments; i++) {
			float angle = (startAngle + deltaAngle * i) * Mathf.Deg2Rad;
			segmentPositions[i] = center + new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * tower.radius;
		}
		lineRenderer.SetPositions(segmentPositions);
	}
}
