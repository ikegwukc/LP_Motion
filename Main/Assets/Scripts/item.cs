using UnityEngine;
using System.Collections;

public class item : MonoBehaviour {

	public int frameBalance = 1;
	public GameObject anchor;
	float hSpeed;
	float vSpeed;
	public float offX=0;
	float offY=0;
	public int type;
	
	void Start () {
		hSpeed = Random.Range (-vars.blockSize/80,vars.blockSize/80);
		vSpeed = Random.Range (0,vars.blockSize/20);
		transform.position = new Vector3(vars.screenStart*2,0,0);
		if (type >= 0) {
			renderer.material.mainTexture = Resources.Load ("Item" + type, typeof(Texture2D)) as Texture;
		} else {
			Destroy(gameObject);
		}
	}
	
	void Update () {
		if (frameBalance > 0) {
			frameBalance -= 1;
			transform.localScale = new Vector3 (vars.blockSize / 10 / 3, 1, vars.blockSize / 10 / 3);
		
			if (anchor) {
				transform.position = new Vector3 (anchor.transform.position.x + offX, anchor.transform.position.y + offY + vars.blockSize, -.01f);

				vSpeed -= vars.gravity / 5;
				if (Mathf.Abs (transform.position.y - anchor.transform.position.y) < vars.blockSize / 2 + vars.blockSize / 3 / 2) {
					vSpeed = 0;
					hSpeed = 0;
					//offY = vars.blockSize/2-transform.localScale.y*10/2;
					//transform.position = new Vector3 (transform.position.x, anchor.transform.position.y+offY - vars.blockSize, -.01f);
				}
				offX += hSpeed;
				offY += vSpeed;

				if (Mathf.Abs (transform.position.x - player.x) < vars.blockSize / 2) {
					if (Mathf.Abs (transform.position.y - player.y) < vars.blockSize / 2) {
						if (type == 0) {
							vars.health++;
						} else {
							player.hSpeed = (-vars.minSpeed + player.hSpeed * 2) / 3;
						}
						Destroy (gameObject);
					}
				}

			} else {
				Destroy (gameObject);
			}
		}
	}
}
