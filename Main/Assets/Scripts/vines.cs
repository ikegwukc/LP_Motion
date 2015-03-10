using UnityEngine;
using System.Collections;

public class vines : MonoBehaviour {

	public int frameBalance = 1;
	public GameObject anchor;

	void Start () {
		transform.localScale = new Vector3 (.1f,1,.1f);
		transform.position = new Vector3(vars.screenStart*2,0,0);
		renderer.material.mainTextureOffset = new Vector2(0,Random.Range(0f,vars.blockSize));
		if (Random.Range (0, 1) == 0) {
			renderer.material.mainTextureScale = new Vector2 (1, vars.levelHeight * 2 / vars.blockSize);
		} else {
			renderer.material.mainTextureScale = new Vector2 (-1, vars.levelHeight * 2 / vars.blockSize);
		}
		if (anchor.GetComponent<block> ().isAnchor) {
			Destroy(gameObject);
		} else {
			anchor.GetComponent<block> ().isAnchor = true;
		}


	}

	void Update () {
		if (frameBalance > 0) {
			frameBalance -= 1;
			transform.localScale = new Vector3 (vars.blockSize / 10, 1, vars.levelHeight / 5);

			if (anchor) {
				transform.position = new Vector3 (anchor.transform.position.x, anchor.transform.position.y - vars.levelHeight, vars.blockSize / 2);
			} else {
				Destroy (gameObject);
			}

			if (transform.position.x < -vars.screenStart - transform.localScale.x / 2) {
				anchor.GetComponent<block>().isAnchor = false;
				Destroy (gameObject);

			}
		}
	}
}
