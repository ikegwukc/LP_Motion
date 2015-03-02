using UnityEngine;
using System.Collections;
using Leap;

class hands_example : MonoBehaviour
{
	Controller controller; //  Creates a virtual controller object, we interact with leap motion this way.
	public GameObject cube_obj;

	// Use this for initialization
	void Start () {
		controller = new Controller ();
		controller.Config.Save ();
	}
	
	// Update is called once per frame
	void Update () {
		Frame frame = controller.Frame (); //Frames kind of like a list or array that contains all of the tracking data in time.
		Hand hand = frame.Hands.Frontmost; // Find the Hand most torward the front, you can change this.
		if (hand.IsRight) { // Just doing stuff with the Right Hand you certainly can change this to left or both, etc.

			//Debug.Log ("Pitch: "+ hand.Direction.Pitch); // Gives the rotational value of Hand as a float with respect to the x axis.
			//Debug.Log ("Yaw: " + hand.Direction.Yaw); // Gives the rotational value of Hand as a float with respect to the y axis.
			Debug.Log ("Roll: " + (-1*hand.Direction.Roll)); // Gives the rotational value of Hand as a float with respect to the z axis.

			cube_obj.transform.Rotate(0,0,hand.Direction.Roll);//Rotate game object with the rotation of the hand.
		}
	}
}
