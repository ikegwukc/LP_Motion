using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {

	public GameObject anchor;

	void Start () {
		transform.localScale = new Vector3 (vars.blockSize/10,1,vars.blockSize/10);
		transform.position = new Vector3(vars.screenStart+vars.blockSize,0,0);
	}

	void Update () {
		transform.position = new Vector3 (anchor.transform.position.x,anchor.transform.position.y+vars.blockSize,0);



		if (anchor.transform.position.x + vars.blockSize < -vars.screenStart) {
			Destroy (anchor);
			Destroy (gameObject);
		}
	}
}
