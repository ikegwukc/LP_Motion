using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {

	public int frameBalance = 1;
	public GameObject anchor;
	public int health = 0;
	public int lastHealth = 0;
	float size;
	public int fireStage = -200-Random.Range(0,200);
	public int stackPos;
	public int type;
	public int species;
	float width = 9f/10;
	float height = 1f/10;
	GameObject breakGO;
	float wiggle;
	float wiggleDelta;


	void Start () {

		if (health < 0) {
			health = 0;
		}

		anchor.GetComponent<block> ().isAnchor = true;
		transform.localScale = new Vector3 (1,1,1);
		transform.position = new Vector3(vars.screenStart*2,0,0);
		//tag = "Enemy";

		switch (type) {
		case (0):
			health = Random.Range(100,200);
			break;
		case (1):
			species = Random.Range(0,3);
			health = Random.Range(10,100);
			gameObject.transform.GetChild(0).gameObject.renderer.material.mainTexture = Resources.Load ("Slime0"+species, typeof(Texture2D)) as Texture;
			gameObject.transform.GetChild(1).gameObject.renderer.material.mainTexture = Resources.Load ("Slime0"+species, typeof(Texture2D)) as Texture;
			gameObject.transform.GetChild(2).gameObject.renderer.material.mainTexture = Resources.Load ("Slime1"+species, typeof(Texture2D)) as Texture;
			gameObject.transform.GetChild(3).gameObject.renderer.material.mainTexture = Resources.Load ("Slime0"+species, typeof(Texture2D)) as Texture;
			gameObject.transform.GetChild(4).gameObject.renderer.material.mainTexture = Resources.Load ("Slime0"+species, typeof(Texture2D)) as Texture;
			gameObject.transform.GetChild(5).gameObject.renderer.material.mainTexture = Resources.Load ("Slime0"+species, typeof(Texture2D)) as Texture;
			transform.Rotate(new Vector3(0,Random.Range(-15f,15f),0));
			wiggle = Random.Range(5f,15f);
			wiggleDelta = 0;
			break;
		case (2):
			species = vars.randCurve(16,(int) Mathf.Max(-vars.score+1000)/100);
				Random.Range(0,16);
			health = 30+species*5;
			gameObject.transform.GetChild(0).gameObject.renderer.material.mainTexture = Resources.Load ("wbSide"+species, typeof(Texture2D)) as Texture;
			gameObject.transform.GetChild(1).gameObject.renderer.material.mainTexture = Resources.Load ("wbEnd"+species, typeof(Texture2D)) as Texture;
			gameObject.transform.GetChild(2).gameObject.renderer.material.mainTexture = Resources.Load ("wbSide"+species, typeof(Texture2D)) as Texture;
			gameObject.transform.GetChild(3).gameObject.renderer.material.mainTexture = Resources.Load ("wbSide"+species, typeof(Texture2D)) as Texture;
			gameObject.transform.GetChild(4).gameObject.renderer.material.mainTexture = Resources.Load ("wbSide"+species, typeof(Texture2D)) as Texture;
			gameObject.transform.GetChild(5).gameObject.renderer.material.mainTexture = Resources.Load ("wbEnd"+species, typeof(Texture2D)) as Texture;

			transform.Rotate(new Vector3(Random.Range(0,4)*90,Random.Range(0,4)*90,Random.Range(0,4)*90));
			transform.localScale = new Vector3(vars.blockSize,vars.blockSize,vars.blockSize);
			break;
		case (3):
			species = Random.Range(0,3);
			health = 1;
			renderer.material.mainTexture = Resources.Load ("Slime0"+species, typeof(Texture2D)) as Texture;
			gameObject.transform.GetChild(0).gameObject.renderer.material.mainTexture = Resources.Load ("sb10", typeof(Texture2D)) as Texture;
			gameObject.transform.GetChild(1).gameObject.renderer.material.mainTexture = Resources.Load ("wbSide10", typeof(Texture2D)) as Texture;
			gameObject.transform.GetChild(2).gameObject.renderer.material.mainTexture = Resources.Load ("wbSide10", typeof(Texture2D)) as Texture;
			transform.localScale = new Vector3(vars.blockSize*9/10,vars.blockSize/10,vars.blockSize*9/10);
			wiggle = 0;
			wiggleDelta = 0;
			break;
		}
		lastHealth = health;
	}

	void Update () {
		if (frameBalance > 0) {
			frameBalance -= 1;
			switch (type) {
			case (0):
				transform.localScale = new Vector3 (vars.blockSize / 10 * 2, 1, vars.blockSize / 10 * 2);
				transform.position = new Vector3 (anchor.transform.position.x, anchor.transform.position.y + vars.blockSize / 2 + vars.blockSize, 0);
				if (health < 100) {
					fireStage = -100;
					renderer.material.mainTexture = Resources.Load ("Golem2", typeof(Texture2D)) as Texture;
				}
				if (fireStage > 0) {
					if (Mathf.Abs (transform.position.y - player.y) < vars.blockSize && transform.position.x > player.x) {
						if (fireStage > 50) {
							GameObject go = GameObject.Instantiate (Resources.Load ("EnemyProjectile")) as GameObject;
							go.transform.position = transform.position + new Vector3 (-vars.blockSize / 5, -vars.blockSize / 3, 0);
							fireStage = Random.Range (-300, -100);
						}
					} else {
						fireStage = 0;
					}
				}
				if (fireStage > 0) {
					if (breakGO) {
						breakGO.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z - .01f);
						breakGO.renderer.material.color = new Color (1, 1, 1, (float)fireStage / 50f);
					} else {
						breakGO = GameObject.Instantiate (Resources.Load ("Charge")) as GameObject;
						breakGO.transform.position = new Vector3 (vars.screenStart * 2, 0, 0);
						breakGO.transform.localScale = transform.localScale;
						breakGO.renderer.material.mainTexture = Resources.Load ("Golem1", typeof(Texture2D)) as Texture;
					}
				} else {
					if (breakGO) {
						Destroy (breakGO);
					}
				}
				break;
			case (1):
				if (wiggle != 0) {
					wiggleDelta += -wiggle / Mathf.Abs (wiggle) * .001f;
				}
				;
				wiggle += wiggleDelta;

				size = vars.blockSize / 4 + vars.blockSize * Mathf.Min (health / 100F, .75F);
				transform.localScale = new Vector3 (size + wiggle * vars.blockSize / 100, size - wiggle * vars.blockSize / 100 * 2, size / 2);
				transform.position = new Vector3 (anchor.transform.position.x, anchor.transform.position.y + vars.blockSize / 2 + transform.localScale.y / 2, 0);
				break;
			case (2):
				if (anchor.GetComponent<block> ().isAnchor) {
					transform.localScale = new Vector3 (vars.blockSize, vars.blockSize, vars.blockSize);
					transform.position = new Vector3 (anchor.transform.position.x, anchor.transform.position.y + vars.blockSize / 2 + vars.blockSize / 2 + vars.blockSize * stackPos, 0);
				} else {
					if (breakGO) {
						Destroy (breakGO);
					}
					Destroy (gameObject);
				}
				if (health < 30 + species * 5) {
					if (breakGO) {
						breakGO.transform.position = transform.position;
						breakGO.renderer.material.mainTexture = Resources.Load ("break" + Mathf.Abs (Mathf.Floor (health / ((30 + species * 5) / 10)) - 9), typeof(Texture2D)) as Texture;
					} else {
						breakGO = GameObject.Instantiate (Resources.Load ("Break")) as GameObject;
						breakGO.transform.position = new Vector3 (vars.screenStart * 2, 0, 0);
						breakGO.transform.localScale = new Vector3 (vars.blockSize * 1.01F, vars.blockSize * 1.01F, vars.blockSize * 1.01F);
					}
				}
				break;
			case (3):



				transform.localScale = new Vector3 (vars.blockSize * .5f, vars.blockSize * .5f, vars.blockSize * .5f);
				transform.position = new Vector3 (anchor.transform.position.x - player.hSpeed * 50 + vars.blockSize * width, anchor.transform.position.y + vars.levelHeight - vars.blockSize / 2 - vars.blockSize / 2 - wiggle, 0);
				if (gameObject.transform.childCount > 1) {
					if (Mathf.Abs ((anchor.transform.position.x + vars.blockSize / 2) - player.x) < vars.blockSize / 2) {
						if (Mathf.Abs ((anchor.transform.position.y + vars.blockSize) - player.y) < vars.blockSize / 2) {
							Destroy (gameObject.transform.GetChild (1).gameObject);
							Destroy (gameObject.transform.GetChild (2).gameObject);
							vars.particles (transform.position.x + player.hSpeed * 50, transform.position.y - vars.blockSize / 2, 10, 40, 7);
						}
					}
					gameObject.transform.GetChild (0).gameObject.transform.localScale = new Vector3 (width * 2, height * 2, width * 2);
					gameObject.transform.GetChild (0).gameObject.transform.position = new Vector3 (anchor.transform.position.x, anchor.transform.position.y + vars.blockSize / 2, 0);
					gameObject.transform.GetChild (1).gameObject.transform.localScale = new Vector3 (2 * .05f, 2 * .25f, 2 * .05f);
					gameObject.transform.GetChild (1).gameObject.transform.position = new Vector3 (anchor.transform.position.x - player.hSpeed * 50 + vars.blockSize * width, anchor.transform.position.y + vars.levelHeight - vars.blockSize / 2 - vars.blockSize / 8, 0);
					gameObject.transform.GetChild (2).gameObject.transform.localScale = new Vector3 (Mathf.Sqrt (Mathf.Pow (-player.hSpeed * 50 + vars.blockSize * width / 2, 2) + Mathf.Pow (vars.levelHeight - vars.blockSize, 2)), 2 * .05f, 2 * .05f);
					gameObject.transform.GetChild (2).gameObject.transform.position = new Vector3 (anchor.transform.position.x + vars.blockSize * width / 2 + (-player.hSpeed * 50 + vars.blockSize * width / 2) / 2, anchor.transform.position.y + vars.blockSize / 2 + (vars.levelHeight - vars.blockSize) / 2, 0);
					gameObject.transform.GetChild (2).gameObject.transform.rotation = new Quaternion (0, 0, 0, 0);
					gameObject.transform.GetChild (2).gameObject.transform.Rotate (new Vector3 (0, 0, (180 / Mathf.PI) * Mathf.Atan ((vars.levelHeight - vars.blockSize) / (-player.hSpeed * 50 + vars.blockSize * width / 2))));
				} else {
					gameObject.transform.GetChild (0).gameObject.transform.localScale = new Vector3 (width * 2, height * 2, width * 2);
					gameObject.transform.GetChild (0).gameObject.transform.position = new Vector3 (anchor.transform.position.x, anchor.transform.position.y + vars.blockSize / 2 - vars.blockSize / 30, 0);

					wiggleDelta += vars.gravity / 5;
					wiggle += wiggleDelta;
					if (wiggle < 1000) {
						if (wiggle > vars.levelHeight - vars.blockSize - vars.blockSize / 2 - vars.blockSize / 4) {
							if (Mathf.Abs (player.x - transform.position.x) < vars.blockSize*1.1f) {
								player.hit ();
							} else {
								if (Random.Range (0, 5) == 0) {
									GameObject go = GameObject.Instantiate (Resources.Load ("Item")) as GameObject;
									go.GetComponent<item> ().anchor = anchor;
									go.GetComponent<item> ().offX = transform.position.x - anchor.transform.position.x;
									go.GetComponent<item> ().type = species + 1;
								}
							}
							if (species != 1) {
								vars.particles (transform.position.x, anchor.transform.position.y + vars.blockSize, 10, 50, 1 + species);
							} else {
								vars.particles (transform.position.x, anchor.transform.position.y + vars.blockSize, 10, 500, 4);
								vars.particles (transform.position.x, anchor.transform.position.y + vars.blockSize, 10, 200, 5);
							}
							wiggle = 1000;
						}
					}

				}
				break;
			}
			
			if (anchor.transform.position.x + vars.blockSize < -vars.screenStart) {
				if(type == 3){
					if(transform.position.x + vars.blockSize < -vars.screenStart){
						Destroy (anchor);
						if (breakGO) {
							Destroy (breakGO);
						}
						Destroy (gameObject);
					}
				} else {
					Destroy (anchor);
					if (breakGO) {
						Destroy (breakGO);
					}
					Destroy (gameObject);
				}
			}

			if (health != lastHealth) {
				switch (type) {
				case 0:
					vars.particles (transform.position.x, transform.position.y, 5, 20, 0);
					break;
				case 1:
					vars.particles (transform.position.x, transform.position.y, 10, 10, 1 + species);
					break;
				case 2:
					vars.particles (transform.position.x, transform.position.y, 5, 20, 6);
					break;
				}
			}

			if (health <= 0) {
				anchor.GetComponent<block> ().isAnchor = false;
				if (breakGO) {
					Destroy (breakGO);
				}
				GameObject go = GameObject.Instantiate (Resources.Load ("Item")) as GameObject;
				go.GetComponent<item> ().anchor = anchor;
				go.GetComponent<item> ().type = -1;
				switch (type) {
				case 0:
					if (Random.Range (0, 3) == 0) {
						go.GetComponent<item> ().type = 0;
					}
					break;
				case 1:
					if (Random.Range (0, 3) == 0) {
						go.GetComponent<item> ().type = species + 1;
					}
					break;
				case 2:
					if (Random.Range (0, 15) == 0) {
						go.GetComponent<item> ().type = 0;
					}
					break;
				}
				Destroy (gameObject);
			}

			lastHealth = health;
		}
	}
}
