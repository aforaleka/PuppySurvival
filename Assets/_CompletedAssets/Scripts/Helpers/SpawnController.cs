using UnityEngine;

/// <summary>
/// Just a simple component to keep track of the bounds of the arena
/// and propose locations to respawn objects
/// </summary>
public class SpawnController : MonoBehaviour
{
	// Screen bounds
	public static float XMin;
	public static float XMax;
	public static float ZMin;
	public static float ZMax;

	public static float X2Min;
	public static float X2Max;
	public static float Z2Min;
	public static float Z2Max;

	internal static int floorMask;

	// Fill in within editor with walls defining the boundaries of the arena
	public GameObject LeftWall;
	public GameObject RightWall;
	public GameObject TopWall;
	public GameObject BottomWall;

	public GameObject LeftWall2;
	public GameObject RightWall2;
	public GameObject TopWall2;
	public GameObject BottomWall2;

	// Note the bounds of the arena.
	internal void Start ()
	{
		floorMask = LayerMask.GetMask ("Floor");
		XMin = LeftWall.transform.position.x;
		XMax = RightWall.transform.position.x;
		ZMin = BottomWall.transform.position.z;
		ZMax = TopWall.transform.position.z;
	}

	/// <summary>
	/// Return a random location within the arena that is at least radius units from any other objects.
	/// </summary>
	/// <param name="radius">Safety distance for placement</param>
	/// <returns>Free location</returns>
	public static Vector3 FindFreeLocationRoom1(float radius)
	{
		Vector3 point = new Vector3 (Random.Range (XMin, XMax), 0, Random.Range (ZMin, ZMax));
		while (Physics.OverlapSphere (point, radius, floorMask).Length > 0) {
			point = new Vector3 (Random.Range (XMin, XMax), 0, Random.Range (ZMin, ZMax));
		}
		return point;
	}
}
