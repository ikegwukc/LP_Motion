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
	public static int spawnBlocks = -1;

	void Start () {
		lastBlock = GameObject.Instantiate(Resources.Load("Block")) as GameObject;
		lastBlock.transform.position = new Vector3 (-vars.screenStart - vars.blockSize / 2,99,0);
		lastBlock.GetComponent<block>().levelIndex = 99;
		nextGap = 15;
		for (int i = 0; i < 3; i++) {
			level [i] = new Level ();
		}
	}

	public static void addBlocks (){

		fixLevels();

		while(lastBlock.transform.position.x < vars.screenStart + vars.blockSize / 2){

			if(levels.nextGap == 2){
				if(Mathf.Round(player.vSpeed*10000) != 0 || player.nearBridge || newBridge == true){
					levels.nextGap++;
				}

				if(gui.type != 1){
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
					levels.nextGap = Random.Range(1,50)+1;
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

			if(player.hSpeed < -vars.minSpeed-vars.acceleration*10){
				if(gui.type == 1){
					vars.score++;
				}
			}
			lastBlock.transform.Translate( new Vector3(vars.blockSize-.01F,0,0));

		}
	}

	public static void fixLevels (){
		if (Mathf.Round(player.vSpeed*10000) == 0 && player.nearBridge == false) {
			int tempLevel = (int) Mathf.Floor((player.y-(-vars.levelHeight+player.h/2+vars.blockSize/2-levels.y))/vars.levelHeight)+1;
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
	public int canSpawn = 0;
	
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

			if(levels.lastGap>1 && levels.lastGap>5 && levels.level[index].canSpawn<vars.score){
				if(Random.Range(0f,6+vars.score/100)>=5 || levels.spawnBlocks>=1){
					if(vars.score>10 && gui.type == 1){
						if(go.GetComponent<block>().isAnchor == false){
							//go.GetComponent<block>().isAnchor = true;
							int rand = Random.Range(0,100);
							if(levels.spawnBlocks>=1){
								rand = -1;
							}
							if(rand > 0 && rand <= 10){
								GameObject go2 = GameObject.Instantiate (Resources.Load ("Golem")) as GameObject;
								go2.GetComponent<enemy>().anchor = go;
								go2.GetComponent<enemy>().type = 0;
								levels.level[index].canSpawn = vars.score+1;
							} else if(rand > 10 && rand <= 20) {
								GameObject go2 = GameObject.Instantiate (Resources.Load ("Slime")) as GameObject;
								go2.GetComponent<enemy>().anchor = go;
								go2.GetComponent<enemy>().type = 1;
								levels.level[index].canSpawn = vars.score+1;
							} else if(rand > 20 && rand <= 30 || levels.spawnBlocks>=1) {
								rand = Random.Range(1,3);
								for(int i = 0; i < rand; i++){
									GameObject go2 = GameObject.Instantiate (Resources.Load ("Cube")) as GameObject;
									go2.GetComponent<enemy>().anchor = go;
									go2.GetComponent<enemy>().stackPos = i;
									go2.GetComponent<enemy>().type = 2;
									levels.level[index].canSpawn = vars.score;
								}
								if(levels.spawnBlocks<1){
									levels.spawnBlocks = Random.Range(2,5);
								} else {
									levels.spawnBlocks -= 1;
								}
							} else if(rand > 30 && rand <= 100) {
								if(index > 0 && levels.nextGap>2+(int) Mathf.Ceil((Mathf.Abs (player.hSpeed)*10)/vars.blockSize)){
									if(levels.level[index-1].active){
										GameObject go2 = GameObject.Instantiate (Resources.Load ("Plate")) as GameObject;
										go2.GetComponent<enemy>().anchor = go;
										go2.GetComponent<enemy>().type = 3;
										levels.level[index].canSpawn = vars.score+2+(int) Mathf.Ceil((Mathf.Abs (player.hSpeed)*10)/vars.blockSize);
									}
								}
							}
						}
					}
				}
			}

			if(go.GetComponent<block>().isAnchor == false){
				if(Random.Range(0,10)==0){
					go.GetComponent<block>().isAnchor = true;

					GameObject go2 = GameObject.Instantiate (Resources.Load ("Vines")) as GameObject;
					go2.GetComponent<vines>().anchor = go;
					go.GetComponent<block>().isAnchor = false;
				}
			}
			
		}
	}
	/*
	bool blockAtPoint(float a, float b, float c){
		foreach (var i in GameObject.FindGameObjectsWithTag("Block")) {
			if(Mathf.Abs(a-i.transform.position.x)<vars.blockSize/2){
				if(Mathf.Abs(b-i.transform.position.y)<vars.blockSize/2){
					if(Mathf.Abs(c-i.transform.position.z)<vars.blockSize/2){
						Debug.LogError("can place");
						return true;
					}
				}
			}
		}
		Debug.LogError("nope");
		return false;
	}
	*/
}