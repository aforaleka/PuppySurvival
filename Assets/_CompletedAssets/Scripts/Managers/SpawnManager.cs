using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public GameObject keyPrefab;
	public GameObject chickenPrefab;
	public int numChicken = 10;


    void Start()
    {
        //random generation of position of key where other objects are not colliding
		Instantiate(keyPrefab, SpawnController.FindFreeLocation(12f), Quaternion.identity);
		for (int i=0; i < numChicken; i++)
			Instantiate(chickenPrefab, SpawnController.FindFreeLocation(8f), Quaternion.Euler(-30, 0, 0));
    }
}
