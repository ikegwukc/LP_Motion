using UnityEngine;
using System.Collections;

public class player_item : MonoBehaviour {

	Quaternion startRot;
	GameObject playerMagic;
	Quaternion magicStartRot;
	public static float rotation;

	void Start () {
		transform.localScale = new Vector3 (vars.blockSize/20,1,vars.blockSize/20);
		transform.position = new Vector3(-vars.screenStart/2,0,.0001F);
		startRot = transform.rotation;

		playerMagic = GameObject.Instantiate(Resources.Load("PlayerMagic")) as GameObject;
		magicStartRot = playerMagic.transform.rotation;
		playerMagic.transform.position = transform.position;
	}

	void Update () {
		rotation = 90 * Mathf.Min (Mathf.Max ((Input.mousePosition.y) / (Screen.height), 0), 1) - 45 - 22.5F;
		if (vars.itemInput > 0) {
			transform.position = new Vector3 (-vars.screenStart / 2, 0, .0001F);
			transform.rotation = startRot;
			transform.RotateAround (new Vector3 (-vars.screenStart / 2, 0, 0), new Vector3 (0, 0, 1), rotation);
			shoot ();
		} else if (vars.itemInput == -2) {
			dontShoot();
			transform.position = new Vector3 (-vars.screenStart / 2, 0, .0001F);
			transform.rotation = startRot;
			transform.RotateAround (new Vector3 (-vars.screenStart / 2, 0, 0), new Vector3 (0, 0, 1), rotation);
		} else {
			dontShoot();
			transform.position = new Vector3 (vars.screenStart * 2 + transform.localScale.x, 0, 0);
		}
	}

	void shoot(){
		//if (Random.Range (0, 10) == 0) {
			GameObject.Instantiate (Resources.Load ("PlayerProjectile"));
		//}
		playerMagic.particleSystem.emissionRate = 100;

		playerMagic.transform.rotation = magicStartRot;
		playerMagic.transform.Rotate (new Vector3 (-rotation-45,0,0));
	}

	void dontShoot(){
		playerMagic.particleSystem.emissionRate = 0;
	}
}
