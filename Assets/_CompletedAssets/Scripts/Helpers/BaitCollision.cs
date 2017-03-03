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
		
	void FixedUpdate () {
		transform.Rotate(Vector3.forward * Time.fixedDeltaTime, 3f);
	}

	internal void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			BaitCount.count += 1;
			audio.PlayOneShot (getItem, 1);
			baitRenderer.enabled = false;
			ScoreManager.score += 50;
			Destroy (gameObject, getItem.length);

		} else if (other.gameObject.tag != "Enemy") {
			Vector3 dest = SpawnController.FindFreeLocationRoom1 (5f);
			transform.position = dest;
		}
	}
}
