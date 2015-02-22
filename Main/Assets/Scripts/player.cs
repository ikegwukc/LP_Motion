using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
	
	public static float hSpeed ;
	public static float vSpeed;
	public static float x;
	public static float y;
	public static float w;
	public static float h;
	public static bool onBridge = false;
	public static bool nearBridge = false;
	public static int lastHit = 0;
	
	void Start () {
		hSpeed = -vars.minSpeed;
		vSpeed = 0;
		x = -vars.screenStart/2;
		y = 0;
		w = vars.blockSize;
		h = vars.blockSize;
		transform.localScale = new Vector3(w/10, 1, h/10);
		transform.position = new Vector3(x, y, 0);
		GameObject.Instantiate(Resources.Load("PlayerItem"));
	}
	
	void Update () {

		vars.secondsPastFrame += Time.deltaTime;

		if (vars.secondsPastFrame > vars.frameRate*30) {
			Debug.LogWarning("Warning! Cant Keep Up! Lower the frame rate! - AJ");
		}

		if (vars.secondsPastFrame >= vars.frameRate) {
			vars.secondsPastFrame -= vars.frameRate;

			hSpeed -= vars.acceleration;
			if(hSpeed < -vars.maxSpeed){
				hSpeed = -vars.maxSpeed;
			}
			vSpeed += vars.gravity;
			if(vSpeed < -vars.maxJump){
				vSpeed = -vars.maxJump;
			}

			onBridge = false;
			nearBridge = false;
			foreach (var i in GameObject.FindGameObjectsWithTag("Bridge")) {
				if (Mathf.Abs (x - (i.GetComponent<bridge>().anchor.transform.position.x+vars.blockSize/2+vars.gapLength*vars.blockSize/2)) < (vars.gapLength*vars.blockSize/2)) {
					float hitY = (x-(i.GetComponent<bridge>().anchor.transform.position.x+vars.blockSize/2))*i.GetComponent<bridge>().raise/(vars.gapLength*vars.blockSize);
					float spaceY = ((vars.blockSize)/Mathf.Cos(Mathf.Atan((i.GetComponent<bridge>().raise)/(vars.gapLength*vars.blockSize))))/2+h/2;
					float goalY = i.GetComponent<bridge>().anchor.transform.position.y+hitY+spaceY;

					if(Mathf.Abs(y-goalY)<vars.blockSize){
						if(vars.bridgeInput > 0){
							if(y<goalY){
								vSpeed = y-goalY;
							}
							onBridge = true;
						}
					}
					nearBridge = true;
					levels.newBridge = false;
				}

			}

			if(onBridge == false){
				foreach (var i in GameObject.FindGameObjectsWithTag("Block")) {
					if (Mathf.Abs (player.x - (i.transform.position.x + player.hSpeed)) < (player.w / 2 + vars.blockSize / 2)) {
						if (Mathf.Abs (player.y - (i.transform.position.y + player.vSpeed)) < (player.h / 2 + vars.blockSize / 2)) {
							vSpeed = 0;

							if(i.transform.position.y + vars.blockSize/2 + h/2 > y+vars.gravity){
								if((i.transform.position.y + vars.blockSize/2 + h/2)-y < vars.blockSize){
									vSpeed = -vars.phaseOutSpeed;
								} else {
									vSpeed = vars.phaseOutSpeed+vars.gravity;
								}
							} else {
								vSpeed = y-(i.transform.position.y + vars.blockSize/2 + h/2);
							}
						}
					}
				}
			}


			foreach (var i in GameObject.FindGameObjectsWithTag("Block")) {
				//transform.Translate (new Vector3 (player.hSpeed, player.vSpeed, 0));
				i.transform.Translate (new Vector3 (player.hSpeed, 0, 0));//player.vSpeed
			}
			foreach (var i in GameObject.FindGameObjectsWithTag("Bridge")) {
				//transform.Translate (new Vector3 (player.hSpeed, player.vSpeed, 0));
				i.transform.Translate (new Vector3 (player.hSpeed, 0, 0));//player.vSpeed
			}

			levels.y += vSpeed;
			foreach (var i in GameObject.FindGameObjectsWithTag("Enemy")) {
				if (Mathf.Abs (x - i.transform.position.x) < vars.blockSize/4) {
					if(Mathf.Abs(y - i.transform.position.y) < vars.blockSize) {
						hit ();
					}
				}
			}
			if(player.vSpeed > .25F){
				hit ();
			}
			if(lastHit < 100){
				lastHit++;
			}
			levels.addBlocks ();



		}
	}

	public static void hit (){

		if (lastHit > 1) {
			vars.health--;
			if(vars.health<0){
				endGame();
			}
		}
		lastHit = 0;
	}

	public static void endGame (){
		//The Game Is Over
		vars.health = -999;
		vars.acceleration = 0;
		hSpeed = 0;
	}
}
