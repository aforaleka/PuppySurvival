using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	bool hasKey;
	Animator anim;

	void Start () {
		hasKey = false;
		anim = GetComponent <Animator> ();
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
				anim.SetBool ("open", true);
				KeyCount.count -= 1;
				ScoreManager.score += 2000;
				WinManager.win = true;
			}
		}
	}


}
