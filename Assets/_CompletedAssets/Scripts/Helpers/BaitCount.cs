using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BaitCount : MonoBehaviour {
	public static int count;
	public int prevCount;
	public Text countText;

	public float flashSpeed = 0.5f;
	public Color flashColourRed;
	public Color flashColourGreen;

	public float timer;
	public float timeInBetween = 0.2f;

	void Start () {
		prevCount = 0;
		count = 0;
		timer = timeInBetween;
		SetCountText();
	}

	// Update is called once per frame
	void FixedUpdate () {
		SetCountText();
		timer += Time.fixedDeltaTime;

		// If the player has just been damaged...
		if (prevCount < count) {
			// ... set the colour of the damageImage to the flash colour.
			countText.color = flashColourGreen;
			timer = 0;
		} else if (prevCount > count) {
			countText.color = flashColourRed;
			timer = 0;
		} else if (timer >= timeInBetween) {
			countText.color = Color.Lerp (countText.color, Color.white, flashSpeed * Time.fixedDeltaTime);
		}

//		if (timer >= timeInBetween)
//		{
//			// ... transition the colour back to clear.
//			countText.color =  Color.Lerp(countText.color, Color.white, flashSpeed * Time.fixedDeltaTime);
//		}

		prevCount = count;
	}

	void SetCountText()
	{
		countText.text = "Bait: " + count.ToString();
	}
}
