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
	public static float FPS;
	float animationStage;
	
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

			if(gui.type==1){
				hSpeed -= vars.acceleration;
				if(hSpeed < -vars.maxSpeed){
					hSpeed = -vars.maxSpeed;
				}

			}

			vSpeed += vars.gravity;
			if(vSpeed < -vars.maxJump){
				vSpeed = -vars.maxJump;
			}

			animationStage -= hSpeed*2;
			if((int)animationStage>=5){
				animationStage -= 5;
			}
			renderer.material.mainTexture = Resources.Load ("player"+ (int) animationStage, typeof(Texture2D)) as Texture;


			enemy[] aaa  = (enemy[]) FindObjectsOfType (typeof(enemy));
			for (int i=0;i<aaa.Length;i++) {
				aaa[i].frameBalance++;
			}
			background[] bbb = (background[]) FindObjectsOfType (typeof(background));
			for (int i=0;i<bbb.Length;i++) {
				bbb[i].frameBalance++;
			}
			vines[] ccc = (vines[]) FindObjectsOfType (typeof(vines));
			for (int i=0;i<ccc.Length;i++) {
				ccc[i].frameBalance++;
			}
			gui[] ddd = (gui[]) FindObjectsOfType (typeof(gui));
			for (int i=0;i<ddd.Length;i++) {
				ddd[i].frameBalance++;
			}
			block[] eee = (block[]) FindObjectsOfType (typeof(block));
			for (int i=0;i<eee.Length;i++) {
				eee[i].frameBalance++;
			}
			bridge[] fff = (bridge[]) FindObjectsOfType (typeof(bridge));
			for (int i=0;i<fff.Length;i++) {
				fff[i].frameBalance++;
			}
			bridge_cap[] ggg = (bridge_cap[]) FindObjectsOfType (typeof(bridge_cap));
			for (int i=0;i<ggg.Length;i++) {
				ggg[i].frameBalance++;
			}
			player_projectile[] hhh = (player_projectile[]) FindObjectsOfType (typeof(player_projectile));
			for (int i=0;i<hhh.Length;i++) {
				hhh[i].frameBalance++;
			}
			player_item[] iii = (player_item[]) FindObjectsOfType (typeof(player_item));
			for (int i=0;i<iii.Length;i++) {
				iii[i].frameBalance++;
			}
			enemy_projectile[] jjj = (enemy_projectile[]) FindObjectsOfType (typeof(enemy_projectile));
			for (int i=0;i<jjj.Length;i++) {
				jjj[i].frameBalance++;
			}
			item[] kkk = (item[]) FindObjectsOfType (typeof(item));
			for (int i=0;i<kkk.Length;i++) {
				kkk[i].frameBalance++;
			}
			dust[] lll = (dust[]) FindObjectsOfType (typeof(dust));
			for (int i=0;i<lll.Length;i++) {
				lll[i].frameBalance++;
			}

			//enemy.frameBalance += 1;

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
			foreach (var i in GameObject.FindGameObjectsWithTag("BridgeCap")) {
				ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[i.particleSystem.particleCount];
				i.particleSystem.GetParticles(_particles);
				for (int j = 0; j < _particles.Length; j++){
					_particles[j].position = new Vector3(_particles[j].position.x+player.hSpeed,_particles[j].position.y+player.vSpeed,_particles[j].position.z);
				}
				i.particleSystem.SetParticles(_particles, i.particleSystem.particleCount);
			}
			foreach (var i in GameObject.FindGameObjectsWithTag("Dust")) {
				if(i.GetComponent<dust>().still == false){
					i.transform.Translate (new Vector3 (player.hSpeed, player.vSpeed, 0));
				}
				i.GetComponent<dust>().lifeTime++;
			}
						
			ParticleSystem.Particle[] _particles2 = new ParticleSystem.Particle[player_item.playerMagic.particleSystem.particleCount];
			player_item.playerMagic.particleSystem.GetParticles(_particles2);
			for (int j = 0; j < _particles2.Length; j++){
				_particles2[j].position = new Vector3(_particles2[j].position.x,_particles2[j].position.y+player.vSpeed,_particles2[j].position.z);
			}
			player_item.playerMagic.particleSystem.SetParticles(_particles2, player_item.playerMagic.particleSystem.particleCount);

			foreach (var i in GameObject.FindGameObjectsWithTag("Bullet")) {
				i.transform.Translate(new Vector3(0,player.vSpeed,0));
			}

			levels.y += vSpeed;
			foreach (var i in GameObject.FindGameObjectsWithTag("Enemy")) {
				if (Mathf.Abs (x - i.transform.position.x) < vars.blockSize/4) {
					if(Mathf.Abs(y - i.transform.position.y) < vars.blockSize) {
						hit ();
					}
				}
				if(i.GetComponent<enemy>()){
					i.GetComponent<enemy>().fireStage++;
				}
			}

			foreach (var i in GameObject.FindGameObjectsWithTag("EnemyTame")) {
				if(i.GetComponent<enemy>()){
					i.GetComponent<enemy>().fireStage++;
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
			if(vars.health<=0){
				vars.health = 0;
				//endGame();
				gui.type = 0;
				gui.species = 0;
				vars.itemInput = -1;
			}
		}
		lastHit = 0;
	}
	/*
	public static void endGame (){
		//The Game Is Over
	}
	*/
}
