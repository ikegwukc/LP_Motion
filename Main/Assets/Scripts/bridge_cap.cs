using UnityEngine;
using System.Collections;

public class bridge_cap : MonoBehaviour {


	public GameObject anchor;
	public GameObject bridge;
	public bool end;
	public float lastPos = 0;

	void Start () {
		transform.localScale = new Vector3 (vars.blockSize,vars.blockSize/2-.0001F,vars.blockSize);
	}

	void Update () {
		if (anchor) {
			if (vars.bridgeInput > 0) {
				if (end) {
					renderer.material.mainTextureOffset = new Vector2 ((-bridge.transform.localScale.x) / vars.blockSize + .5F, 0);
					transform.position = new Vector3 (anchor.transform.position.x + vars.blockSize / 2 + vars.gapLength * vars.blockSize, anchor.transform.position.y + bridge.GetComponent<bridge> ().raise, 0);
					transform.rotation = new Quaternion (0, 0, 0, 0);
					transform.Rotate (new Vector3 (-90 + (180 / Mathf.PI) * Mathf.Atan (bridge.GetComponent<bridge> ().raise / (vars.gapLength * vars.blockSize)), -90, 90));
				} else {
					transform.position = new Vector3 (anchor.transform.position.x + vars.blockSize / 2, anchor.transform.position.y, 0);
				}
				if (lastPos != Input.mousePosition.y) {
					particleSystem.emissionRate = Mathf.Abs(Input.mousePosition.y-lastPos)*10;
					lastPos = Input.mousePosition.y;
				} else {
					particleSystem.emissionRate = 0;
				}
			} else {
				transform.position = new Vector3 (vars.screenStart * 2 + transform.localScale.x, 0, 0);
			}
		
		}
	}
	
}
