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
	public float time;

	public Raid(RaidType type, EnemyColor color, float time){
		this.type = type;
		this.color = color;
		this.time = time;
	}
}

public class EnemyConfig : MonoBehaviour {
	
	private static EnemyConfig _instance;

	public GameObject enemyPrefab;
	public List<IEnumerator> activeRaids;

	public Raid redRaid1;
	public Raid yellowRaid1;

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
		IEnumerator yellowRaid = RaidCR(redRaid1);
		StartCoroutine(yellowRaid);

		IEnumerator redRaid = RaidCR(yellowRaid1);
		StartCoroutine(redRaid);

	}
	public void Start() {
		InitRaids();
	}

	public IEnumerator RaidCR(Raid raid) {
		while(true) {
			GameObject enemy = Instantiate(enemyPrefab);
			enemy.transform.SetParent(transform, true);

			Vector2 randomPosition = Random.insideUnitCircle.normalized;
			enemy.transform.position = (Vector3)randomPosition * 10f;

			Enemy enemyScript = enemy.GetComponent<Enemy>();
			enemyScript.Init(randomPosition * -2f, raid.color);
			yield return new WaitForSeconds(raid.time);
		}
	}
}
