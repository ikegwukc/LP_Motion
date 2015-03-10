using UnityEngine;
using System.Collections;

public class background : MonoBehaviour {

	public int frameBalance = 1;

	void Start () {

		randomizeBackground(gameObject);
		switch ((int) transform.position.z) {
		case 15:
			if (transform.position.x == 0) {
				GameObject.Instantiate (gameObject,new Vector3(transform.position.x+transform.localScale.x*12,transform.position.y,transform.position.z), transform.rotation);
			}
			break;
		case 16:
			if (transform.position.x == 0) {
				GameObject.Instantiate (gameObject,new Vector3(transform.position.x+transform.localScale.x*10,transform.position.y,transform.position.z), transform.rotation);
			}
			break;
		case 17:
			if (transform.position.x == 0) {
				for(int i = 0; i < 3; i++){
					GameObject.Instantiate (gameObject,new Vector3((vars.backWidth / 2 + transform.localScale.x / 2)*10+Random.Range(0,300),Random.Range(0,vars.backHeight/3*10),transform.position.z), transform.rotation);
				}
			}
			break;
		case 18:
			if (transform.position.x == 0) {
				GameObject.Instantiate (gameObject,new Vector3(transform.position.x+transform.localScale.x*12,transform.position.y,transform.position.z), transform.rotation);
			}
			break;
		case 19:
			if (transform.position.x == 0) {
				GameObject.Instantiate (gameObject,new Vector3(transform.position.x+transform.localScale.x*10,transform.position.y,transform.position.z), transform.rotation);
			}
			break;
		}

	}

	void Update () {
		if (frameBalance > 0) {
			frameBalance -= 1;
			switch ((int)transform.position.z) {
			case 15:
				transform.Translate (new Vector3 (-player.hSpeed / 2, 0, Mathf.Max (-player.vSpeed / 10, 0)));
				break;
			case 16:
				transform.Translate (new Vector3 (-player.hSpeed / 3, 0, 0));
				break;
			case 17:
				transform.Translate (new Vector3 (-player.hSpeed / 4, 0, 0));
				break;
			case 18:
				transform.Translate (new Vector3 (-player.hSpeed / 7, 0, 0));
				break;
			case 19:
				transform.Translate (new Vector3 (-player.hSpeed / 10, 0, 0));
				break;
			}

			if (transform.position.x < -(vars.backWidth / 2 + transform.localScale.x / 2) * 10) {
				randomizeBackground (gameObject);
				switch ((int)transform.position.z) {
				case 15:
					transform.position = new Vector3 (transform.position.x + transform.localScale.x * 24 + Random.Range (0, 50), -8, 15 + Random.Range (0f, .5f));
					break;
				case 16:
					transform.position = new Vector3 (transform.position.x + transform.localScale.x * 20 + player.hSpeed, transform.position.y, transform.position.z);
					break;
				case 17:
					transform.localScale = new Vector3 (Random.Range (1f, 2f), 1, 1);
					transform.position = new Vector3 ((vars.backWidth / 2 + transform.localScale.x / 2) * 10 + Random.Range (0, 50), Random.Range (0, vars.backHeight / 3 * 10), transform.position.z);
					break;
				case 18:
					transform.position = new Vector3 (transform.position.x + transform.localScale.x * 24 + Random.Range (0, 50), -8, transform.position.z);
					break;
				case 19:
					transform.position = new Vector3 (transform.position.x + transform.localScale.x * 20 + player.hSpeed, transform.position.y, transform.position.z);
					break;
				}
			}
		}
	}

	void randomizeBackground(GameObject go){
		switch ((int) go.transform.position.z) {
		case 15:
			break;
		case 16:
			break;
		case 17:
			go.renderer.material.mainTexture = Resources.Load ("Cloud"+Random.Range (0,2), typeof(Texture2D)) as Texture;
			go.renderer.material.mainTextureScale = new Vector2 ((Random.Range(0,1)-.5f)*2, 1);
			break;
		case 18:
			go.renderer.material.mainTexture = Resources.Load ("Near Mountains"+Random.Range (0,2), typeof(Texture2D)) as Texture;
			go.renderer.material.mainTextureScale = new Vector2 ((Random.Range(0,1)-.5f)*2, 1);
			break;
		case 19:
			go.renderer.material.mainTexture = Resources.Load ("Far Mountains"+Random.Range (0,3), typeof(Texture2D)) as Texture;
			go.renderer.material.mainTextureScale = new Vector2 ((Random.Range(0,1)-.5f)*2, 1);
			break;
		}
	}
}
