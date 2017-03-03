using UnityEngine;
using System.Collections;

public class KeyCollision : MonoBehaviour {

	public AudioClip getItem;
	float clipDuration;
	AudioSource audio;
	Renderer keyRenderer;

	void Start () {
		audio = GetComponent <AudioSource> ();
		keyRenderer = GetComponent<MeshRenderer> ();
	}

	void Update () {
		transform.Rotate(Vector3.up, 3f);
	}

	internal void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player") {
			KeyCount.count += 1;
			audio.PlayOneShot (getItem, 1);
			keyRenderer.enabled = false;
			ScoreManager.score += 500;
			Destroy(gameObject, getItem.length);

		} else if (other.gameObject.tag != "Enemy") {
			Vector3 dest = SpawnController.FindFreeLocationRoom1 (5f);
			transform.position = dest;
		}
	}
}
