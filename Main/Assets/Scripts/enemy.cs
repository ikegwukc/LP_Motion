using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {

	public GameObject anchor;
	public int health = Random.Range(10,30);
	float size = 0;

	void Start () {
		transform.localScale = new Vector3 (vars.blockSize/10,1,vars.blockSize/10);
		transform.position = new Vector3(vars.screenStart+vars.blockSize,0,0);
	}

	void Update () {
		size = vars.blockSize/2+vars.blockSize*Mathf.Min(health/20F,1.5F);
		transform.localScale = new Vector3 (size/10,1,size/10);
		transform.position = new Vector3 (anchor.transform.position.x,anchor.transform.position.y+vars.blockSize/2+size/2,0);



		if (anchor.transform.position.x + vars.blockSize < -vars.screenStart) {
			Destroy (anchor);
			Destroy (gameObject);
		}
		if (health <= 0) {
			anchor.GetComponent<block>().isAnchor = false;
			Destroy (gameObject);
		}
	}
}
