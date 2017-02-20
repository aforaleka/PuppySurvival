using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class EnemyMovement : MonoBehaviour
    {
        Transform player;               // Reference to the player's position.
        PlayerHealth playerHealth;      // Reference to the player's health.
        EnemyHealth enemyHealth;        // Reference to this enemy's health.
        NavMeshAgent nav;               // Reference to the nav mesh agent.
		public float minDistance = 8f;
		bool playerSpotted;
		Animator anim;                      // Reference to the animator component.


        void Awake ()
        {
            // Set up the references.
            player = GameObject.FindGameObjectWithTag ("Player").transform;
            playerHealth = player.GetComponent <PlayerHealth> ();
            enemyHealth = GetComponent <EnemyHealth> ();
            nav = GetComponent <NavMeshAgent> ();
			playerSpotted = false;
			anim = GetComponent<Animator> ();
        }


        void FixedUpdate ()
        {
            // If the player have health left...
			if (playerHealth.currentHealth > 0 && enemyHealth.currentHealth > 0)
            {
				bool playerInRange = Vector3.Distance (nav.transform.position, player.position) < minDistance;
				string tiptoe = player.GetComponent<PlayerMovement> ().tiptoe;
				bool playerTiptoe = player.GetComponent<PlayerMovement> ().state == tiptoe;

				if (!playerSpotted)
					// If player has not been spotted yet, check if in range and loud
					playerSpotted = playerInRange && !playerTiptoe;

				if (playerSpotted && playerInRange) {
					// once spotted, keep chasing until player out of range, disregard tiptoe
					// ... set the destination of the nav mesh agent to the player.
					nav.SetDestination (player.position);

				} else if (playerSpotted && !playerInRange) {
					// player escaped, reset spot so next time player could tiptoe without getting noticed.
					playerSpotted = false;
				}

				Animating (playerSpotted && playerInRange);
					
            } else {
                // ... disable the nav mesh agent.
                nav.enabled = false;
            }


        }

		void Animating (bool running)
		{
			// Tell the animator whether or not the player is walking.
			anim.SetBool ("IsRunning", running);
		}
			
    }

}