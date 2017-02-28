using UnityEngine;
using System.Collections;

public class BaitCollision : MonoBehaviour {

	public AudioClip getItem;
	float clipDuration;
	AudioSource audio;
	Renderer baitRenderer;

	void Start () {
		transform.Translate (0, 0.5f, 0);
		audio = GetComponent <AudioSource> ();
		baitRenderer = GetComponent<MeshRenderer> ();
	}
		
	void Update () {
		transform.Rotate(Vector3.forward, 3f);
	}

	internal void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			audio.PlayOneShot (getItem, 1);
			baitRenderer.enabled = false;
			Destroy (gameObject, getItem.length);
			ScoreManager.score += 50;
			BaitCount.count += 1;
		} else if (other.gameObject.tag != "Enemy") {
			Vector3 dest = SpawnController.WhichRoom (transform.position) == 1
				? SpawnController.FindFreeLocationRoom1 (5f)
				: SpawnController.FindFreeLocationRoom2 (5f);
			transform.position = dest;
		}
	}
}
