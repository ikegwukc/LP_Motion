using UnityEngine;
using System.Collections;
using Leap;

/*public class Sample : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}*/

class Sample : MonoBehaviour
{
	public static void Main ()
	{
		SampleListener listener = new SampleListener ();
		Controller controller = new Controller ();
		controller.AddListener (listener);
		
		// Keep this process running until Enter is pressed
		Debug.Log ("Press Enter to quit...");
		Debug.Log ("");
		
		controller.RemoveListener (listener);
		controller.Dispose ();
	}
}

class SampleListener : Listener
{
	private Object thisLock = new Object ();
	
	private void SafeWriteLine (string line)
	{
		lock (thisLock) {
			Debug.Log ("");
			//Console.WriteLine (line);
		}
	}
	
	public override void OnConnect (Controller controller)
	{
		SafeWriteLine ("Connected");
	}
	
	
	public override void OnFrame (Controller controller)
	{
		SafeWriteLine ("Frame available");
	}
}