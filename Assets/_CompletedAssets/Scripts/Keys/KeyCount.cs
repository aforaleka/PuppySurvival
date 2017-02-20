using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class KeyCount : MonoBehaviour {
    internal static int count;
    public Text countText;

	void Start () {
        count = 0;
        SetCountText();
    }

	void Update () {
        SetCountText();
	}

    void SetCountText()
    {
        countText.text = "Keys: " + count.ToString();
    }
}
