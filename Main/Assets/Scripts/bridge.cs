using UnityEngine;
using System.Collections;

public class bridge : MonoBehaviour {

	public GameObject anchor;
	public GameObject cap1;
	public GameObject cap2;
	public float raise;

	void Start () {
		tag = "Bridge";
		anchor.GetComponent<block>().isAnchor = true;
		transform.position = new Vector3(vars.screenStart+transform.localScale.x,0,0);

		cap1 = GameObject.Instantiate(Resources.Load("BridgeCap")) as GameObject;
		cap1.transform.position = new Vector3 (-vars.screenStart - vars.blockSize / 2,0,0);
		cap1.GetComponent<bridge_cap>().anchor = anchor;
		cap1.GetComponent<bridge_cap>().bridge = gameObject;
		cap1.GetComponent<bridge_cap>().end = true;

		cap2 = GameObject.Instantiate(Resources.Load("BridgeCap")) as GameObject;
		cap2.transform.position = new Vector3 (-vars.screenStart - vars.blockSize / 2,0,0);
		cap2.GetComponent<bridge_cap>().anchor = anchor;
		cap2.GetComponent<bridge_cap>().end = false;
	}

	void Update () {
		renderer.material.mainTextureScale = new Vector2(transform.localScale.x/vars.blockSize,renderer.material.mainTextureScale.y);
		renderer.material.mainTextureOffset = new Vector2(-transform.localScale.x/vars.blockSize,0);
		if (Input.GetMouseButton (0)) {
			raise = vars.levelHeight * Mathf.Min (Mathf.Max ((Input.mousePosition.y - Screen.height / 4) / (Screen.height / 2), 0), 1);

			transform.rotation = new Quaternion(0,0,0,0);
			transform.localScale = new Vector3(Mathf.Sqrt( Mathf.Pow(vars.gapLength*vars.blockSize,2) + Mathf.Pow(raise,2)),vars.blockSize,vars.blockSize-.00001F);
			transform.position = new Vector3(anchor.transform.position.x+vars.blockSize/2+transform.localScale.x/2,anchor.transform.position.y,0);
			transform.RotateAround( new Vector3(anchor.transform.position.x+vars.blockSize/2,anchor.transform.position.y,0), new Vector3(0,0,1), (180/Mathf.PI)*Mathf.Atan (raise/(vars.gapLength*vars.blockSize)));
		} else {
			transform.position = new Vector3(vars.screenStart+transform.localScale.x,0,0);
		}

		if (anchor.transform.position.x + vars.blockSize + vars.blockSize * vars.gapLength < -vars.screenStart) {
			Destroy (anchor);
			Destroy (cap1);
			Destroy (cap2);
			Destroy (gameObject);
		}
	}
}
