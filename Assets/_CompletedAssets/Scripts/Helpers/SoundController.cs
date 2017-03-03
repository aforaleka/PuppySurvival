using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	public static AudioClip getItem;
	static AudioSource audio;

	void Start () {
		audio = GetComponent <AudioSource> ();
	}

	void Update () {
	}

	internal static void playKeyGetAudio()
	{
		audio.PlayOneShot (getItem, 1);
	}
}
