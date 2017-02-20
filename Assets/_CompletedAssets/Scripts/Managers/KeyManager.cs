using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{

    public float xPosition = 5f;
    public float yPosition = 0f;
    public float zPosition = 5f;

    public string countText; 

	public GameObject keyPrefab;

    void Start()
    {
        //random generation of position of key where other objects are not colliding
		Instantiate(keyPrefab, SpawnController.FindFreeLocation(10f), Quaternion.identity);
    }
    

}
