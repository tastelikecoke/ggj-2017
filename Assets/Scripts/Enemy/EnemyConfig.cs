using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public enum RaidType {
	Enemy,
	Nothing
}

[System.Serializable]
public class Raid {
	public RaidType type;
	public EnemySpawn color;

	public float startDelay;
	public float interval;
	public float killTime;
	public int numToSpawn;
	public float moveSpeed;
}

public class EnemyConfig : MonoBehaviour {
	
	static EnemyConfig _instance;
	public static EnemyConfig GetInstance() { return _instance; }

	public int numLanes;
	public float spawnRadius;

	[Tooltip("How much you add to the multiplier per second (ie. 1f / 30 seconds = 2x multiplier after the first 30 seconds)")]
	public float difficultyIncreaseRate = 0.033333f;

	float difficultyMultiplier = 1f;

	public GameObject enemyPrefab;
	public List<IEnumerator> activeRaids;

	public Raid[] raids;

	void OnEnable() {
		if (_instance == null) {
			_instance = this;
//			DontDestroyOnLoad(this);
		}
	}
	
	static public EnemyConfig Instance {
		get { return _instance; }
	}

	public void InitRaids() {
		for (int i = 0; i < raids.Length; i++) {
			StartCoroutine(RaidCR(raids[i]));
		}
	}

	public void Start() {
		InitRaids();
	}

	void Update() {
		difficultyMultiplier += Time.deltaTime * difficultyIncreaseRate;
	}

	public IEnumerator RaidCR(Raid raid) {
		yield return new WaitForSeconds(raid.startDelay);

		float startTime = Time.time;
		while(true) {
			for (int i = 0; i < raid.numToSpawn; i++) {
				GameObject enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity) as GameObject;
				enemy.transform.SetParent(transform, true);

				int laneToSpawn = Random.Range(0, numLanes);
				float laneRadians = ((float) laneToSpawn / numLanes) * Mathfx.TAU;
				Vector2 laneDirection = new Vector2(Mathf.Cos(laneRadians), Mathf.Sin(laneRadians));
				enemy.transform.position = (Vector3) laneDirection * spawnRadius;

				Enemy enemyScript = enemy.GetComponent<Enemy>();
				enemyScript.Init(laneDirection * -1f * raid.moveSpeed * difficultyMultiplier, raid.color.GetEnemyColor());
			}

			yield return new WaitForSeconds(raid.interval / difficultyMultiplier);
			if (startTime + raid.killTime > Time.time) {
				yield break;
			}
		}
	}
}
