using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	static List<PlayerController> players = new List<PlayerController>();

	public int playerNumber;
	public int numSegments;
	public float arc; // in degrees
	public float arcRegenerationRate;
	public float arcDegenerationRate;
	public float arcGrowthDelay;
	public Tower tower;
	public GameObject projectilePrefab;

	public EnemyColor shootColor { get; protected set; }

	float position = Mathf.Infinity; // in degrees
	float arcMultiplier = 1f;
	bool canArcGrow = true;

	public float startAngle { get { return Mathfx.ConvertToSmallestAngle(position - (arc * arcMultiplier) / 2f); } }
	public float endAngle { get { return Mathfx.ConvertToSmallestAngle(startAngle + (arc * arcMultiplier)); } } 

	public LineRenderer lineRenderer;
	public LineRenderer overlapRenderer;
	public Sprite spriteToUse;

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

		shootColor = playerNumber == 1 ? EnemyColor.Red : EnemyColor.Blue;

		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetVertexCount(numSegments + 1);
		lineRenderer.sortingOrder = playerNumber * 2;
		UpdateColors();

		MaterialPropertyBlock m = new MaterialPropertyBlock();
		m.SetTexture("_MainTex", (Texture) spriteToUse.texture);
		lineRenderer.SetPropertyBlock(m);

		position = 0 + 90f * (playerNumber - 1);
	}

	void Update() {
		Vector2 v = InputManager.GetAnalogOfController(playerNumber);
		if (v.magnitude > 0f) {
			v.Normalize();
			position = 90f - Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
		}

		if (InputManager.GetFireButtonDown(playerNumber)) {
			Fire();
		}

		if (InputManager.GetSwitchButtonDown(playerNumber)) {
			SwitchColor();
		}

		if (canArcGrow) {
			arcMultiplier = Mathf.Clamp01(arcMultiplier + Time.deltaTime * arcRegenerationRate);
		}
	}

	void SwitchColor() {
		EnemyColor[] possibleColors = new[] { EnemyColor.Red, EnemyColor.Yellow, EnemyColor.Blue };
		for (int i = 0; i < possibleColors.Length; i++) {
			bool found = true;
			for (int o = 0; o < players.Count; o++) {
				if (players[o].shootColor == possibleColors[i]) {
					found = false;
					break;
				}
			}

			if (found) {
				shootColor = possibleColors[i];
				UpdateColors();
				break;
			}
		}
	}

	void Fire() {
//		Debug.Log("Firing");
		Vector3 v = new Vector3(Mathf.Sin(position * Mathf.Deg2Rad), Mathf.Cos(position * Mathf.Deg2Rad), 0f) * tower.radius;
		GameObject g = Instantiate(projectilePrefab, v, Quaternion.identity) as GameObject;
		Projectile p = g.GetComponent<Projectile>();
		p.sizeMultiplier = arcMultiplier;
		p.direction = v.normalized;
		p.AddPlayerNumber(this);
		g.SetActive(true);

		switch (shootColor) {
		case EnemyColor.Blue:
			AudioPlayer.GetInstance().PlayBlueSFX();
			break;
		case EnemyColor.Red:
			AudioPlayer.GetInstance().PlayRedSFX();
			break;
		case EnemyColor.Yellow:
			AudioPlayer.GetInstance().PlayYellowSFX();
			break;
		}

		canArcGrow = false;
		arcMultiplier = Mathf.Clamp01(arcMultiplier - arcDegenerationRate);
		LeanTween.cancel(gameObject);
		LeanTween.delayedCall(gameObject, arcGrowthDelay, () => canArcGrow = true);
	}

	void LateUpdate() {
		UpdatePosition();
	}

	void UpdateColors() {
		lineRenderer.SetColors(shootColor.GetColor(), shootColor.GetColor());
	}

	void UpdatePosition() {
		position = Mathfx.ConvertToSmallestAngle(position);

		// Check for overlaps
		bool handleOverlap = false;
		float targetStartAngle = startAngle;
		float targetEndAngle = endAngle;
		float overlapStart = 0f;
		float overlapEnd = 0f;

//		for (int i = 0; i < players.Count; i++) {
//			if (players[i] == this) {
//				continue;
//			}
//
//			float otherPlayerStartAngle = players[i].startAngle;
//			float otherPlayerEndAngle = players[i].endAngle;
//			if (otherPlayerStartAngle > otherPlayerEndAngle) {
//				otherPlayerEndAngle += 360f;
//			}
//
//			if (Mathfx.IsAngleBetween(targetStartAngle, targetEndAngle, otherPlayerStartAngle)) {
//				overlapStart = otherPlayerStartAngle;
//				overlapEnd = targetEndAngle;
//				targetEndAngle = otherPlayerStartAngle;
//				handleOverlap = true;
//			} 
//			if (Mathfx.IsAngleBetween(targetStartAngle, targetEndAngle, otherPlayerEndAngle)) {
//				targetStartAngle = otherPlayerEndAngle;
//			}
//		}

		Vector3 center = tower.transform.position;
		Vector3[] segmentPositions = new Vector3[numSegments + 1];

		if (targetStartAngle > targetEndAngle) {
			targetEndAngle += 360f;
		}

		float totalDelta = targetEndAngle - targetStartAngle;
		float deltaAngle = (targetEndAngle - targetStartAngle) / numSegments;
		for (int i = 0; i <= numSegments; i++) {
			float angle = (targetStartAngle + deltaAngle * i) * Mathf.Deg2Rad;
			segmentPositions[i] = center + new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * (tower.radius + (playerNumber - 1) * 0.2f);
		}
		lineRenderer.SetPositions(segmentPositions);


//		if (handleOverlap) {
//			overlapStart = Mathfx.ConvertToSmallestAngle(overlapStart);
//			overlapEnd = Mathfx.ConvertToSmallestAngle(overlapEnd);
//			if (overlapStart > overlapEnd) {
//				overlapEnd += 360f;
//			}
//
//			// Setup the overlap renderer
//			overlapRenderer.SetVertexCount(numSegments + 1);
//			Vector3[] overlapPositions = new Vector3[numSegments + 1];
//			float overlapDeltaAngle = (overlapEnd - overlapStart) / numSegments;
//			for (int i = 0; i <= numSegments; i++) {
//				float angle = (overlapStart + overlapDeltaAngle * i) * Mathf.Deg2Rad;
//				overlapPositions[i] = center + new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * (tower.radius + 0.4f);
//			}
//			overlapRenderer.SetPositions(overlapPositions);
//			overlapRenderer.SetColors(Color.black, Color.black);
//		} else {
//			overlapRenderer.SetVertexCount(0);
//		}
	}
}
