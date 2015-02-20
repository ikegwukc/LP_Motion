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
	}

	void Update () {


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
