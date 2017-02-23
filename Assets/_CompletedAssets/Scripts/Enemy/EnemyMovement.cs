using UnityEngine;
using System.Collections;


public class EnemyMovement : MonoBehaviour
{
    Transform player;               // Reference to the player's position.
    PlayerHealth playerHealth;      // Reference to the player's health.
    NavMeshAgent nav;               // Reference to the nav mesh agent.
	public float minDistance = 8f;
	bool playerSpotted;
	Animator anim;                      // Reference to the animator component.
	bool walking;
	Vector3 dest;
	float speed;


    void Awake ()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        nav = GetComponent <NavMeshAgent> ();
		playerSpotted = false;
		anim = GetComponent<Animator> ();
		walking = false;
		speed = nav.speed;
		Running (false);

    }


    void FixedUpdate ()
    {
        // If the player have health left...
		if (playerHealth.currentHealth > 0)
        {
			if (!anim.GetBool("IsEating")) {
				nav.speed = speed;
				bool playerInRange = Vector3.Distance (nav.transform.position, player.position) < minDistance;
				string tiptoe = player.GetComponent<PlayerMovement> ().tiptoe;
				bool playerTiptoe = player.GetComponent<PlayerMovement> ().state == tiptoe;

				if (!playerSpotted)
					playerSpotted = playerInRange && !playerTiptoe;

				if (playerSpotted && playerInRange) {
					// TODO: ADD BARKING SOUND
					Running (true);
					nav.SetDestination (player.position);

				} else if (playerSpotted && !playerInRange) { // reset
					playerSpotted = false;
					Running (false);
				}

				if (!anim.GetBool ("IsRunning")) {
					// TODO: STOP BARKING
					if (!walking) {
						dest = SpawnController.FindFreeLocation (2f);
						walking = true;
					}

					nav.SetDestination (dest);
					CasuallyWalking ();

					if (Vector3.Distance (dest, nav.transform.position) < 0.5f) {
						walking = false;
					}
				}

			} else {
				nav.speed = 0f;
			}
				
        } else {
            // ... disable the nav mesh agent.
            nav.enabled = false;
        }

    }

	void Running (bool running)
	{
		// Tell the animator whether or not the player is running.
		if (running)
			nav.speed = speed;
		else
			nav.speed = 1f;
		anim.SetBool ("IsRunning", running);
	}

	internal void CasuallyWalking ()
	{
		nav.speed = 1f;
		anim.SetBool ("IsWalking", true);
	}
		
}
