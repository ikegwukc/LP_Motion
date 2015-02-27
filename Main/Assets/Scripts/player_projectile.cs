using UnityEngine;
using System.Collections;

public class player_Projectile : MonoBehaviour {

	float direction;
	float speed = .1F;
	int lifeSpan = 0;

	void Start () {
		transform.position = new Vector3(-vars.screenStart/2,0,.0001F);
		direction = (Mathf.PI/180)*(player_item.rotation+45+Random.Range(-2.0F, 2.0F));
		transform.localScale = new Vector3 (0,0,0);
	}

	void Update () {
		lifeSpan++;
		transform.Translate (new Vector3(speed*Mathf.Cos (direction),speed*Mathf.Sin (direction),0));
		if (lifeSpan > 200) {
			Destroy (gameObject);
		}
		//if (Random.Range (0, 3) == 0) {
			foreach (var i in GameObject.FindGameObjectsWithTag("Enemy")) {
				if (Vector3.Distance (i.transform.position, transform.position) < vars.blockSize) {
					if (i.GetComponent<enemy> ().health > 0) {
						i.GetComponent<enemy> ().health--;
						Destroy (gameObject);
					}
				}
			}
		//}

	}

}
