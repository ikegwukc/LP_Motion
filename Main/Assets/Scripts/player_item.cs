using UnityEngine;
using System.Collections;

public class player_item : MonoBehaviour {

	public int frameBalance = 1;
	Quaternion startRot;
	public static GameObject playerMagic;
	Quaternion magicStartRot;
	public static float rotation;

	void Start () {
		transform.position = new Vector3(vars.screenStart*2,0,-.0001F);
		startRot = transform.rotation;

		playerMagic = GameObject.Instantiate(Resources.Load("PlayerMagic")) as GameObject;
		magicStartRot = playerMagic.transform.rotation;
		playerMagic.transform.position = new Vector3(player.x+vars.blockSize/3,player.y+vars.blockSize/6,0);
	}

	void Update () {
		if (frameBalance > 0) {
			frameBalance -= 1;
			rotation = 95*vars.itemInput-35-90+45;//90 * Mathf.Min (Mathf.Max ((Input.mousePosition.y) / (Screen.height), 0), 1) - 45 - 22.5F;
			if (vars.itemInput > 0) {
				transform.position = new Vector3 (-vars.screenStart / 2, 0, -.0001F);
				transform.rotation = startRot;
				renderer.material.mainTexture = Resources.Load ("PItem0", typeof(Texture2D)) as Texture;
				transform.localScale = new Vector3 (vars.blockSize / 20, 1, vars.blockSize / 20);
				transform.RotateAround (new Vector3 (-vars.screenStart / 2, 0, 0), new Vector3 (0, 0, 1), rotation);

				shoot ();
			} else if (vars.itemInput == -2) {
				dontShoot ();
				transform.position = new Vector3 (-vars.screenStart / 2, 0, -.0001F);
				transform.rotation = startRot;
				renderer.material.mainTexture = Resources.Load ("PItem1", typeof(Texture2D)) as Texture;
				transform.localScale = new Vector3 (vars.blockSize / 5, 1, vars.blockSize / 5);
				vars.particles(player.x+vars.blockSize*5/12,player.y+vars.blockSize/3,15,6,9);

			} else {
				dontShoot ();
				transform.position = new Vector3 (vars.screenStart * 2 + transform.localScale.x, 0, 0);
			}
		}
	}

	void shoot(){
		if (Random.Range (0, 5) == 0) {
			GameObject.Instantiate (Resources.Load ("PlayerProjectile"));
		}

		playerMagic.particleSystem.emissionRate = 100;

		playerMagic.transform.rotation = magicStartRot;
		playerMagic.transform.Rotate (new Vector3 (-rotation-45,0,0));
	}

	void dontShoot(){
		playerMagic.particleSystem.emissionRate = 0;
	}
}
