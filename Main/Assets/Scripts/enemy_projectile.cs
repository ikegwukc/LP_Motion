using UnityEngine;
using System.Collections;

public class enemy_projectile : MonoBehaviour {

	public int frameBalance = 1;
	float speed = .1F;

	void Start () {
		transform.localScale = new Vector3 (vars.blockSize/10,1,vars.blockSize/10);
		transform.Translate (new Vector3(0,vars.blockSize/4,0));
		vars.particles(transform.position.x,transform.position.y,10,50,8);
	}

	void Update () {
		if (frameBalance > 0) {
			frameBalance -= 1;
			transform.Translate (new Vector3 (-player.hSpeed + speed, 0, 0));
			vars.particles (transform.position.x, transform.position.y, 10, 5, 8);
			if (transform.position.x < player.x + vars.blockSize / 2) {
				if (vars.itemInput != -2) {
					player.hit ();
					if(Random.Range(0,5)==0){
						vars.particles (transform.position.x, transform.position.y, 10, 50, 8);
					}
				}
				Destroy (gameObject);
			}
		}
	}
}
