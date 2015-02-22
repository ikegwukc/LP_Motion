using UnityEngine;
using System.Collections;

public class block : MonoBehaviour {

	public int levelIndex;
	public bool isAnchor = false;

	bool thisGood;
	bool nextGood;


	void Start () {
		//levelIndex = (int) Mathf.Round(transform.position.y);
		transform.localScale = new Vector3(vars.blockSize, vars.blockSize, vars.blockSize);
		tag = "Block";

		if (Random.Range (0, 5) <= 0) {
			renderer.material.mainTexture = Resources.Load ("blocktxt2", typeof(Texture2D)) as Texture;
		}
		if (Random.Range (0, 5) <= 0) {
			renderer.material.mainTexture = Resources.Load ("blocktxt3", typeof(Texture2D)) as Texture;
		}
		if (Random.Range (0, 2) <= 0) {
			renderer.material.mainTextureScale = new Vector2 (-1, 1);
		}

	}

	void Update () {



		//renderer.material.SetTexture("_BumpMap", player.TEXTURE);
		//translate

		//transform.position = 

		transform.position = new Vector3(transform.position.x, -levelIndex*vars.levelHeight+vars.levelHeight-player.h/2-vars.blockSize/2-vars.gravity*30+levels.y, 0);
		if(transform.position.x < -vars.screenStart-transform.localScale.x/2){
			if(isAnchor == false){
				Destroy(gameObject);
			}
		}
	}
}
