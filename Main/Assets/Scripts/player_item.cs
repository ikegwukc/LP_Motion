using UnityEngine;
using System.Collections;

public class player_item : MonoBehaviour {

	Quaternion startRot;

	void Start () {
		transform.localScale = new Vector3 (vars.blockSize/20,1,vars.blockSize/20);
		transform.position = new Vector3(-vars.screenStart/2,0,.0001F);
		startRot = transform.rotation;
	}

	void Update () {
		if (vars.itemInput > 0) {
			transform.position = new Vector3 (-vars.screenStart / 2, 0, .0001F);
			transform.rotation = startRot;
			transform.RotateAround (new Vector3 (-vars.screenStart / 2, 0, 0), new Vector3 (0, 0, 1), 90 * Mathf.Min (Mathf.Max ((Input.mousePosition.y) / (Screen.height), 0), 1) - 45);
		} else {
			transform.position = new Vector3(vars.screenStart*2+transform.localScale.x,0,0);
		}
	}
}
