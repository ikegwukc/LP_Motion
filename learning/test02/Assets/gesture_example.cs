using UnityEngine;
using System.Collections;
using Leap;

class gesture_example : MonoBehaviour
{
	Controller controller;
	public GameObject cube_obj;
	public bool spin;
	// Use this for initialization
	void Start () {
		SampleListener listener = new SampleListener ();
		controller = new Controller ();
		controller.EnableGesture (Gesture.GestureType.TYPESWIPE);
		controller.Config.SetFloat ("Gesture.Swipe.MinLength", 200.0f);
		controller.Config.SetFloat ("Gesture.Swipe.MinVelocity", 750f);
		controller.Config.Save ();
	}
	
	// Update is called once per frame
	void Update () {
		Frame frame = controller.Frame ();
		GestureList gestures = frame.Gestures ();
		Hand hand = frame.Hands.Frontmost;
		if (hand.IsRight) {
			for(int i=0; i<gestures.Count;i++){
				Gesture gesture =gestures[i];
				spin=false;
				if(gesture.Type == Gesture.GestureType.TYPESWIPE){
					SwipeGesture Swipe = new SwipeGesture(gesture);
					Vector swipeDirection = Swipe.Direction;
					if(swipeDirection.x<0){
						Debug.Log("Left");
						spin=true;
					}
				}
			}
		}
		if(spin==true){
			cube_obj.transform.Rotate(Vector3.up, 45 * Time.deltaTime);
		}
	}
}