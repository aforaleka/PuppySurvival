using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	bool hasKey;

	void Start () {
		hasKey = false;
	}
		
	void Update () {
		hasKey = KeyCount.count > 0;
	}

	internal void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (hasKey) {
				gameObject.GetComponent<BoxCollider> ().isTrigger = true;
				KeyCount.count -= 1;
				ScoreManager.score += 1000;
			}
		}
		else
		{
			Destroy (other.gameObject);
			gameObject.GetComponent<BoxCollider> ().isTrigger = false;
		}
	}
}
