using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class KeyCount : MonoBehaviour {
	public static int count;
	public int prevCount;
	public Text countText;

	public float flashSpeed = 0.5f;
	public Color flashColourGreen;

	public float timer;
	public float timeInBetween = 0.2f;

	void Start () {
		prevCount = 0;
		count = 0;
		timer = timeInBetween;
		SetCountText();
	}
		
	void FixedUpdate () {
		SetCountText();
		timer += Time.fixedDeltaTime;

		if (timer >= timeInBetween)
		{
			// ... transition the colour back to clear.
			countText.color =  Color.Lerp(countText.color, new Color(1f, 1f, 1f, 1), flashSpeed * Time.fixedDeltaTime);
		}

		// If the player has just been damaged...
		if (prevCount < count)
		{
			// ... set the colour of the damageImage to the flash colour.
			countText.color = flashColourGreen;
			timer = 0;
		}

		prevCount = count;
	}

	void SetCountText()
	{
		countText.text = "Key: " + count.ToString();
	}
}
