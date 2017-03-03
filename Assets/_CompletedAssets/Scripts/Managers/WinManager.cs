using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinManager : MonoBehaviour
{
	Animator anim;                          // Reference to the animator component.
	internal static bool win;

	void Awake ()
	{
		// Set up the reference.
		anim = GetComponent <Animator> ();
		win = false;
	}

	void Update ()
	{
		if (win)
			anim.SetBool ("Win2", true);
	}
}