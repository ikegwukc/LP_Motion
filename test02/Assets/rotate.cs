using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Quaternion kc = new Quaternion(0,0,0,0);
		transform.rotation = new Quaternion(0,0,0,0);
		transform.Rotate (new Vector3 (0, 0, Input.mousePosition.y));
	}
}