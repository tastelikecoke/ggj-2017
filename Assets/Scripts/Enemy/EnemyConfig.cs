using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public enum RaidType {
	Enemy,
	Delay,
	Enemies,

}

[System.Serializable]
public class Raid {
	public RaidType type;
	public int color;
	public float time;
	public Raid(RaidType type, int color, float time){
		this.type = type;
		this.color = color;
		this.time = time;
	}
}

public class EnemyConfig : MonoBehaviour {
	
	private static EnemyConfig _instance;
	
	public List<Raid> raids;
	public GameObject enemyPrefab;
	public List<IEnumerator> activeRaids;

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
		IEnumerator yellowRaid = RaidCR(new Raid(RaidType.Enemy, 0, 1f));
		StartCoroutine(yellowRaid);

		IEnumerator redRaid = RaidCR(new Raid(RaidType.Enemy, 0, 1.5f));
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
			enemyScript.Init(randomPosition * -2f, Random.Range(0, 2) == 0 ? EnemyColor.Red : EnemyColor.Yellow);
			yield return new WaitForSeconds(raid.time);
		}
	}
}
