using UnityEngine;
using System.Collections;

public class player_projectile : MonoBehaviour {

	public int frameBalance = 1;
	float direction;
	float speed = .18F;
	int lifeSpan = 0;
	
	void Start () {
		transform.position = new Vector3(-vars.screenStart/2,0,.0001F);
		direction = (Mathf.PI / 180) * (player_item.rotation + 45);//+Random.Range(-2.0F, 2.0F));
		transform.localScale = Vector3.zero;//new Vector3 (.2f, .2f, .2f);//Vector3.zero;
		transform.Translate (new Vector3(vars.blockSize/3,vars.blockSize/6,0));
	}
	
	void Update () {
		if (frameBalance > 0) {
			frameBalance -= 1;
			lifeSpan++;
			transform.Translate (new Vector3 (speed * Mathf.Cos (direction), speed * Mathf.Sin (direction), 0));
			if (lifeSpan > 200) {
				Destroy (gameObject);
			}
			foreach (var i in GameObject.FindGameObjectsWithTag("Enemy")) {
				hitEnemy (gameObject, i);
			}
			foreach (var i in GameObject.FindGameObjectsWithTag("EnemyTame")) {
				hitEnemy (gameObject, i);
			}
		}
	}

	void hitEnemy(GameObject projectileGO, GameObject enemyGO){

		if(enemyGO.GetComponent<enemy>()){
			switch (enemyGO.GetComponent<enemy>().type) {
			case (0):
				if(Mathf.Abs(projectileGO.transform.position.x-enemyGO.transform.position.x)<enemyGO.transform.localScale.x*10/2){
					if(Mathf.Abs(projectileGO.transform.position.y-enemyGO.transform.position.y)<enemyGO.transform.localScale.z*10/2){
						enemyGO.GetComponent<enemy> ().health-=7;
						vars.particles(projectileGO.transform.position.x,projectileGO.transform.position.y,10,5,6);
						Destroy (projectileGO);
					}
				}
				break;
			case (1):
				if(Mathf.Abs(projectileGO.transform.position.x-enemyGO.transform.position.x)<enemyGO.transform.localScale.x/2){
					if(Mathf.Abs(projectileGO.transform.position.y-enemyGO.transform.position.y)<enemyGO.transform.localScale.y/2){
						enemyGO.GetComponent<enemy> ().health-=7;
						vars.particles(projectileGO.transform.position.x,projectileGO.transform.position.y,10,5,6);
						Destroy (projectileGO);
					}
				}
				break;
			case (2):
				if(Mathf.Abs(projectileGO.transform.position.x-enemyGO.transform.position.x)<enemyGO.transform.localScale.x/2){
					if(Mathf.Abs(projectileGO.transform.position.y-enemyGO.transform.position.y)<enemyGO.transform.localScale.y/2){
						enemyGO.GetComponent<enemy> ().health-=7;
						vars.particles(projectileGO.transform.position.x,projectileGO.transform.position.y,10,5,6);
						Destroy (projectileGO);
					}
				}
				break;
			case (3):

				if(enemyGO.transform.childCount>1){
					if(Mathf.Abs(projectileGO.transform.position.x-(enemyGO.GetComponent<enemy>().anchor.transform.position.x+vars.blockSize))<vars.blockSize/2){
						if(Mathf.Abs(projectileGO.transform.position.y-(enemyGO.GetComponent<enemy>().anchor.transform.position.y+vars.levelHeight/2))<vars.levelHeight/2){
							Destroy(enemyGO.transform.GetChild(1).gameObject);
							Destroy(enemyGO.transform.GetChild(2).gameObject);
							vars.particles(projectileGO.transform.position.x,projectileGO.transform.position.y,20,10,6);
							vars.particles(enemyGO.transform.position.x+player.hSpeed*10,enemyGO.transform.position.y-vars.blockSize/2,10,40,7);
							Destroy(projectileGO);
						}
					}
				}

				break;
			}
		}

	}
	
}
