using UnityEngine;

namespace CompleteProject
{
    public class PlayerMovement : MonoBehaviour
    {
		public float walkSpeed = 2f;
		public float runSpeed = 6f;
		public float slowSpeed = 1.0f;
		float speed; // The speed that the player will move at.

        Vector3 movement;                   // The vector to store the direction of the player's movement.
        Animator anim;                      // Reference to the animator component.
        Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
        int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
        float camRayLength = 100f;          // The length of the ray from the camera into the scene.

        void Awake ()
        {
            // Create a layer mask for the floor layer.
            // floorMask = LayerMask.GetMask ("Floor");

            // Set up references.
            anim = GetComponent <Animator> ();
            playerRigidbody = GetComponent <Rigidbody> ();
			speed = walkSpeed;
        }


        void FixedUpdate ()
        {
            // Store the input axes.
            float v = Input.GetAxisRaw("Vertical");

			// Set the speed according to the keys currently pressed down
			SetSpeed ();

            // Move the player around the scene.
            Move ();

            // Turn the player to face the mouse cursor.
            Turning ();

            // Animate the player.
            Animating (v);
        }

		void SetSpeed ()
		{
			// Speed up to running speed
			if (Input.GetKey (KeyCode.LeftShift))
				speed = runSpeed;
			// Or slow down to tip-toeing
			else if (Input.GetKey (KeyCode.Q))
				speed = slowSpeed;
			
			if (Input.GetKeyUp (KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.Q))
				speed = walkSpeed;
		}

        void Move ()
        {
			// Go forward
			if (Input.GetKey(KeyCode.W)) {
				transform.position += transform.forward * Time.deltaTime * speed;
			}
			// Go backward
			else if(Input.GetKey(KeyCode.S)) {
				transform.position -= transform.forward * Time.deltaTime * speed;
			}
        }


        void Turning ()
        {
            // Create a ray from the mouse cursor on screen in the direction of the camera.
            Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

            // Create a RaycastHit variable to store information about what was hit by the ray.
            RaycastHit floorHit;

            // Perform the raycast and if it hits something on the floor layer...
            if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = floorHit.point - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotatation = Quaternion.LookRotation (playerToMouse);

                // Set the player's rotation to this new rotation.
				playerRigidbody.MoveRotation (Quaternion.Lerp(transform.rotation, newRotatation, 0.1f));
            }
        }




        void Animating (float v)
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            bool walking = v != 0f;

            // Tell the animator whether or not the player is walking.
            anim.SetBool ("IsWalking", walking);
        }
    }
}