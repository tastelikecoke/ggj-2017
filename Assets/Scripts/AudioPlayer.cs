using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour {

	static AudioPlayer __instance; 
	public static AudioPlayer GetInstance() { return __instance; } 

	public AudioClip ambientSFX;
	public AudioClip bgm;

	public AudioClip redSFX;
	public AudioClip blueSFX;
	public AudioClip yellowSFX;

	public AudioClip explodeSFX;
	public AudioClip switchSFX;

	AudioSource audioSource;

	void Awake() {
		__instance = this;
	}

	void Start() {
		audioSource = gameObject.AddComponent<AudioSource>();

		GameObject g = new GameObject("bgmPlayer");
		AudioSource s = g.AddComponent<AudioSource>();
		s.clip = bgm;
		s.loop = true;
		s.Play();

		g = new GameObject("windSFXLoop");
		s = g.AddComponent<AudioSource>();
		s.clip = ambientSFX;
		s.loop = true;
		s.Play();
	}

	public void PlayRedSFX() {
		audioSource.PlayOneShot(redSFX);
	}

	public void PlayBlueSFX() {
		audioSource.PlayOneShot(blueSFX);
	}

	public void PlayYellowSFX() {
		audioSource.PlayOneShot(yellowSFX);
	}

	public void PlaySwitchSFX() {
		audioSource.PlayOneShot(switchSFX);
	}

	public void PlayExplodeSFX() {
		audioSource.PlayOneShot(explodeSFX);
	}
}
