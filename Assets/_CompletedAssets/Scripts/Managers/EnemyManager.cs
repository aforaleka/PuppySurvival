using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
    public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject enemy;                // The enemy prefab to be spawned.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	public Transform[] scareSpawnPoints;	// An array of jump scares spawn points
	public float minDistanceScare = 4f;

	List<Transform> listScarePoints;
	List<Transform> traversed;
	GameObject player;

    void Start ()
    {
		player = GameObject.FindGameObjectWithTag ("Player");

		listScarePoints = new List<Transform> ();
		traversed = new List<Transform> ();
		for (int i = 0; i < scareSpawnPoints.Length; i++)
			listScarePoints.Add (scareSpawnPoints [i]);

		Spawn ();
    }

	void FixedUpdate ()
	{
		foreach (Transform scare in listScarePoints) {
			if (Vector3.Distance (player.transform.position, scare.position) < minDistanceScare) {
				Instantiate (enemy, scare.position, Quaternion.identity);
				traversed.Add (scare);
			}
		}
		listScarePoints.RemoveAll(x => traversed.Contains(x));
	}


    void Spawn ()
    {
        // If the player has no health left...
		if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

		for (int i=0; i < spawnPoints.Length; i++)
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        	Instantiate (enemy, spawnPoints[i].position, spawnPoints[i].rotation);

    }
}