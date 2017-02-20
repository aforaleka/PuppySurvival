using UnityEngine;
using System.Collections;

public class keyCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up, 3f);
	}

	internal void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player")
		{
			Destroy(gameObject);
			KeyCount.count += 1;
			ScoreManager.score += 100;
		}
	}
}
