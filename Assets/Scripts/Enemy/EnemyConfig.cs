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
}

public class EnemyConfig : MonoBehaviour {
	
	private static EnemyConfig _instance;
	
	public List<Raid> raids;
	public GameObject enemyPrefab;

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
		StartCoroutine(RaidCR());
	}
	public void Start() {
		InitRaids();
	}

	public IEnumerator RaidCR() {
		for(int idx = 0; idx < raids.Count; idx++) {
			if(raids[idx].type == RaidType.Delay){
				yield return new WaitForSeconds(raids[idx].time);
			}
			if(raids[idx].type == RaidType.Enemy){
				GameObject enemy = Instantiate(enemyPrefab);
				enemy.transform.SetParent(transform, true);
				Vector2 randomPosition = Random.insideUnitCircle;

				enemy.transform.position = (Vector3)randomPosition * 10f;
				Enemy enemyScript = enemy.GetComponent<Enemy>();
				enemyScript.Init(randomPosition * -2f, 0);
			}
		}
		yield return null;
	}
}
