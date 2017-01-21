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
	public EnemyColor color;

	public float startDelay;
	public float interval;
	public float killTime;

	public Raid(RaidType type, EnemyColor color, float interval){
		this.type = type;
		this.color = color;
		this.interval = interval;
	}
}

public class EnemyConfig : MonoBehaviour {
	
	private static EnemyConfig _instance;

	public GameObject enemyPrefab;
	public List<IEnumerator> activeRaids;

	public Raid[] raids;

	void OnEnable() {
		if (_instance == null) {
			_instance = this;
			DontDestroyOnLoad(this);
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

	public IEnumerator RaidCR(Raid raid) {
		yield return new WaitForSeconds(raid.startDelay);

		float startTime = Time.time;
		while(true) {
			GameObject enemy = Instantiate(enemyPrefab);
			enemy.transform.SetParent(transform, true);

			Vector2 randomPosition = Random.insideUnitCircle.normalized;
			enemy.transform.position = (Vector3)randomPosition * 10f;

			Enemy enemyScript = enemy.GetComponent<Enemy>();
			enemyScript.Init(randomPosition * -1f, raid.color);
			yield return new WaitForSeconds(raid.interval);
			if (startTime + raid.killTime > Time.time) {
				yield break;
			}
		}
	}
}
