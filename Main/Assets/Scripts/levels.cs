using UnityEngine;
using System.Collections;

public class levels : MonoBehaviour {

	public static GameObject lastBlock;

	public static bool newBridge = false;
	public static float y = 0;
	public static Level[] level = new Level[4];
	public static int nextGap;
	public static int lastGap;
	public static int gap = vars.gapLength;

	void Start () {
		lastBlock = GameObject.Instantiate(Resources.Load("Block")) as GameObject;
		lastBlock.transform.position = new Vector3 (-vars.screenStart - vars.blockSize / 2,99,0);
		lastBlock.GetComponent<block>().levelIndex = 99;
		nextGap = 15;
		for (int i = 0; i < 3; i++) {
			level [i] = new Level ();
		}
	}

	void Update () {
		//put code in time adjusted area (player script)
	}

	public static void addBlocks (){

		fixLevels();

		while(lastBlock.transform.position.x < vars.screenStart + vars.blockSize / 2){

			if(levels.nextGap == 2){
				if(player.vSpeed != 0 || player.nearBridge || newBridge == true){
					levels.nextGap++;
				}

			}
			if(levels.nextGap > 0){
				levels.lastGap++;
				levels.nextGap--;

				for (int i = 0; i < 3; i++) {
					level [i].update (i);
				}
			} else {
				newBridge = true;
				level[2].active = true;
				level [2].update (2);
				if(levels.gap > 1){
					levels.gap--;
				} else {
					levels.lastGap = 0;
					levels.nextGap = Random.Range(1,1)+1;
					levels.gap = vars.gapLength;


					float tempRand = Random.Range(0.0F,100.0F);
					if(tempRand < 33){
						level[0].active = true;
						level[1].active = true;
					} else if(tempRand < 66){
						level[0].active = false;
						level[1].active = true;
					} else {
						level[0].active = true;
						level[1].active = false;
					}

				}
			}

			if(player.hSpeed < -vars.minSpeed-vars.acceleration){
				vars.score++;
			}
			lastBlock.transform.Translate( new Vector3(vars.blockSize-.01F,0,0));

		}
	}

	public static void fixLevels (){
		if (player.vSpeed == 0 && player.nearBridge == false) {
			int tempLevel = (int) Mathf.Floor((player.y-(-vars.levelHeight+player.h/2+vars.blockSize/2-levels.y))/vars.levelHeight);
			if(tempLevel == 2){
				levels.y -= vars.levelHeight;
				foreach (var i in GameObject.FindGameObjectsWithTag("Block")) {
					if(i.GetComponent<block>().levelIndex == 2){
						i.GetComponent<block>().levelIndex = 1;
					} else if(i.GetComponent<block>().levelIndex == 1){
						i.GetComponent<block>().levelIndex = 0;
					} else if(i.GetComponent<block>().levelIndex == 0){
						if(i.GetComponent<block>().isAnchor == false){
							Destroy(i);
						} else {
							i.GetComponent<block>().levelIndex = -1;
						}
					}
				}

				level[0].active = level[1].active;
				level[1].active = level[2].active;
				level[2].active = false;
			} else if(tempLevel == 0){
				levels.y += vars.levelHeight;
				foreach (var i in GameObject.FindGameObjectsWithTag("Block")) {
					if(i.GetComponent<block>().levelIndex == 0){
						i.GetComponent<block>().levelIndex = 1;
					} else if(i.GetComponent<block>().levelIndex == 1){
						i.GetComponent<block>().levelIndex = 2;
					} else if(i.GetComponent<block>().levelIndex == 2){
						if(i.GetComponent<block>().isAnchor == false){
							Destroy(i);
						} else {
							i.GetComponent<block>().levelIndex = 3;
						}
					}
				}
				
				level[2].active = level[1].active;
				level[1].active = level[0].active;
				level[0].active = false;
			}
		}
	}
}

public class Level{

	public Level() {}
	
	public bool active = true;
	
	public void update( int index){
		if (active) {
			GameObject go = GameObject.Instantiate (Resources.Load ("Block")) as GameObject;
			go.transform.position = new Vector3 (levels.lastBlock.transform.position.x, index, 0);
			go.GetComponent<block>().levelIndex = index;

			if(index == 1){
				if(levels.nextGap == 0){
					GameObject go2 = GameObject.Instantiate (Resources.Load ("Bridge")) as GameObject;
					go2.GetComponent<bridge>().anchor = go;
					go.GetComponent<block>().isAnchor = true;
				}
			}

			if(Random.Range(0,100)<=10){
				if(player.hSpeed < -vars.minSpeed-vars.acceleration){
					if(go.GetComponent<block>().isAnchor == false){
						GameObject go2 = GameObject.Instantiate (Resources.Load ("Enemy")) as GameObject;
						go2.GetComponent<enemy>().anchor = go;
						go.GetComponent<block>().isAnchor = true;
					}
				}
			}
		}
	}
}