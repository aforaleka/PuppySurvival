using UnityEngine;

namespace CompleteProject
{
    public class PlayerMovement : MonoBehaviour
    {
		public float walkSpeed = 2f;
		public float runSpeed = 6f;
		public float slowSpeed = 1f;
		public float deadzone = 5f;
		float speed; // The speed that the player will move at.

		Vector3 new_position;
        Vector3 movement;                   // The vector to store the direction of the player's movement.
        Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
        float camRayLength = 100f;          // The length of the ray from the camera into the scene.

		internal string state;
		internal string idle = "idle";
		internal string walk = "walk";
		internal string run = "run";
		internal string tiptoe = "tiptoe";

        void Awake ()
        {
            // Set up references.
            playerRigidbody = GetComponent <Rigidbody> ();
			speed = walkSpeed;
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
			} else if (Input.GetKey (KeyCode.Q)) {
				speed = slowSpeed;
				state = tiptoe;
			} else {
				speed = walkSpeed;
				state = walk;
			}
			
		}

        void Move ()
        {
			Vector3 delta = Vector3.zero;

			if (Input.GetKey(KeyCode.W)) { 
				delta += transform.forward;
			}  else if (Input.GetKey(KeyCode.S)) {
				delta -= transform.forward;
			}

			if (Input.GetKey(KeyCode.D)) {
				delta += transform.right;
			} else if (Input.GetKey(KeyCode.A)) {
				delta -= transform.right;
			}

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
                // Set the player's rotation to this new rotation.
				if (Quaternion.Angle(transform.rotation, newRotatation) > deadzone)
					playerRigidbody.MoveRotation (Quaternion.Lerp(transform.rotation, newRotatation, speed/100f));
            }
        }
    }
}