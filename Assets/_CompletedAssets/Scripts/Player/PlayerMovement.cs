using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float walkSpeed = 3f;
	public float runSpeed = 6f;
	public float slowSpeed = 1f;
	float speed; // The speed that the player will move at.

	Vector3 new_position;
    Vector3 movement;                   // The vector to store the direction of the player's movement.
    internal Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.

	internal string state;
	internal string idle = "idle";
	internal string walk = "walk";
	internal string run = "run";
	internal string tiptoe = "tiptoe";

	AudioSource audio;
	public AudioClip walkforward;
	public AudioClip walkbackward;
	public AudioClip runforward;

    void Awake ()
    {
        // Set up references.
        playerRigidbody = GetComponent <Rigidbody> ();
		speed = walkSpeed;
		audio = GetComponent<AudioSource> ();
    }


    void FixedUpdate ()
    {
        // Store the input axes.
        float v = Input.GetAxisRaw("Vertical");
		state = v == 0f ? idle : walk;

		// Set the speed according to the keys currently pressed down
		SetSpeed ();

        // Move the player around the scene.
        Move ();

        // Turn the player to face the mouse cursor.
        Turning ();
    }

	void SetSpeed ()
	{
		if (Input.GetKey (KeyCode.LeftShift)) {
			speed = runSpeed;
			state = run;
			audio.clip = runforward;
			audio.volume = 0.4f;
		} else if (Input.GetKey (KeyCode.Q)) {
			speed = slowSpeed;
			state = tiptoe;
			audio.volume = 0.1f;
			audio.clip = walkforward;
		} else {
			speed = walkSpeed;
			state = walk;
			audio.volume = 0.2f;
			audio.clip = walkbackward;
		}
		
	}

    void Move ()
    {
		Vector3 delta = Vector3.zero;

		if (Input.GetKey(KeyCode.W)) { 
			delta += transform.forward;

			if (!audio.isPlaying)
				audio.Play ();
		}  else if (Input.GetKey(KeyCode.S)) {
			delta -= transform.forward;

			if (!audio.isPlaying)
				audio.Play ();
		}

		if (Input.GetKey(KeyCode.D)) {
			delta += transform.right;
			if (!audio.isPlaying)
				audio.Play ();
		} else if (Input.GetKey(KeyCode.A)) {
			delta -= transform.right;
			if (!audio.isPlaying)
				audio.Play ();
		}

		if (!Input.anyKey)
			audio.Stop ();
		
		playerRigidbody.MovePosition(playerRigidbody.position + delta * Time.deltaTime * speed);
    }


    void Turning ()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast
		if(Physics.Raycast (camRay, out floorHit, camRayLength))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
			playerToMouse.y = 0.0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotatation = Quaternion.LookRotation (playerToMouse);
			float angle = Quaternion.Angle (transform.rotation, newRotatation);

            // Set the player's rotation to this new rotation.
			playerRigidbody.MoveRotation (Quaternion.Lerp(transform.rotation, newRotatation, angle/20 * speed/80f));
        }
    }
}