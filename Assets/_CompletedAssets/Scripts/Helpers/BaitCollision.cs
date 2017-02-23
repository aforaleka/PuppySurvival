using UnityEngine;
using System.Collections;

public class BaitCollision : MonoBehaviour {

	void Start () {
		transform.Translate (0, 0.5f, 0);
	}
		
	void Update () {
		transform.Rotate(Vector3.forward, 3f);
	}

	internal void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Player") {
			Destroy (gameObject);
			ScoreManager.score += 50;
			BaitCount.count += 1;
		} else if (other.gameObject.tag != "Enemy") {
			transform.position = SpawnController.FindFreeLocation (5f);
		}
	}
}
