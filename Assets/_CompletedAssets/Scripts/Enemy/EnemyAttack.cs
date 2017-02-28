using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 5f;     // The time in seconds between each attack.
    public int attackDamage = 50;               // The amount of health taken away per attack.

    Animator anim;                              // Reference to the animator component.
    GameObject player;                          // Reference to the player GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health.
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;                                // Timer for counting up to the next attack.
	internal static bool isEating;

	NavMeshAgent nav;               // Reference to the nav mesh agent.

	public AudioClip eating; 
	public AudioClip attack; 
	AudioSource enemyAudio;

    void Awake ()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        anim = GetComponent <Animator> ();
		isEating = false;
		anim.SetBool ("IsEating", false);
		nav = GetComponent <NavMeshAgent> ();
		enemyAudio = GetComponent<AudioSource> ();
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        	playerInRange = true;
    }

    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
            playerInRange = false;
    }


    void Update ()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

		if (timer >= timeBetweenAttacks) {
			if (nav.enabled) nav.Resume ();
			isEating = false;
			anim.SetBool ("IsEating", false);

			if (playerInRange) {
				Attack (isEating);
			}
		}
			
		if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }
    }


	void Attack (bool isEating)
    {
		if (!isEating) {
			timer = 0f;
			if (BaitCount.count > 0) {
				enemyAudio.clip = eating;
				enemyAudio.volume = 0.5f;
				enemyAudio.Play ();
				BaitCount.count -= 1;
				isEating = true;
				anim.SetBool ("IsWalking", false);
				anim.SetBool ("IsRunning", false);
				anim.SetBool ("IsEating", true);
				nav.Stop ();
			} else if (playerHealth.currentHealth > 0) {
				enemyAudio.clip = attack;
				enemyAudio.PlayOneShot (attack, 1);
				playerHealth.TakeDamage (attackDamage);
				nav.Stop ();
			}
		}
    }
		
}
