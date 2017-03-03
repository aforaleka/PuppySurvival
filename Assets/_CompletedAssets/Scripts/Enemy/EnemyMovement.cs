using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;               // Reference to the player's position.
    PlayerHealth playerHealth;      // Reference to the player's health.
	UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.
	public float minDistance = 8f;
	bool playerSpotted;
	Animator anim;                      // Reference to the animator component.
	bool walking;
	Vector3 dest;
	float speed;

	public AudioClip bark; 
	AudioSource enemyAudio;

    void Awake ()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
		nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
		playerSpotted = false;
		anim = GetComponent<Animator> ();
		walking = false;
		speed = nav.speed;
		Running (false);
		enemyAudio = GetComponent<AudioSource> ();

    }


    void FixedUpdate ()
    {
        // If the player have health left...
		if (playerHealth.currentHealth > 0)
        {
			if (!anim.GetBool("IsEating")) {
				nav.speed = speed;

				// running makes you more noticeable (louder)
				string running = player.GetComponent<PlayerMovement> ().run;
				bool playerInRange = player.GetComponent<PlayerMovement> ().state == running
					? Vector3.Distance (nav.transform.position, player.position) < (minDistance * 1.3f)
					: Vector3.Distance (nav.transform.position, player.position) < minDistance;

				// but if you're tiptoeing/ not moving you're fine
				string tiptoe = player.GetComponent<PlayerMovement> ().tiptoe;
				string idle = player.GetComponent<PlayerMovement> ().idle;
				bool playerTiptoe = 
					player.GetComponent<PlayerMovement> ().state == tiptoe ||
					player.GetComponent<PlayerMovement> ().state == idle;

				if (!playerSpotted)
					playerSpotted = playerInRange && !playerTiptoe;

				if (playerSpotted && playerInRange) {
					enemyAudio.clip = bark;
					enemyAudio.loop = true;
					enemyAudio.volume = 0.6f;
					if (!enemyAudio.isPlaying) enemyAudio.Play ();
					Running (true);
					nav.SetDestination (player.position);

				} else if (playerSpotted && !playerInRange) { // reset
					playerSpotted = false;
					Running (false);
				}

				if (!anim.GetBool ("IsRunning")) {
					enemyAudio.Stop();
					if (!walking) {
						dest = SpawnController.FindFreeLocationRoom1 (2f);
						walking = true;
					}

					nav.SetDestination (dest);
					CasuallyWalking ();

					if (Vector3.Distance (dest, nav.transform.position) < 2f) {
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
