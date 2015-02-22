using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class gui : MonoBehaviour {

	void Start () {

	}

	void Update () {
		if (Input.mousePosition.x > 0) {
			if (Input.mousePosition.x < Screen.width / 3) {
				vars.bridgeInput = Mathf.Min (Mathf.Max ((Input.mousePosition.y - Screen.height / 4) / (Screen.height / 2), 0.0001F), 1);
				vars.itemInput = -1;
			} else if (Input.mousePosition.x < Screen.width / 3 * 2) {
				if(Input.mousePosition.y<Screen.height/3){
					vars.itemInput = -2;
				} else {
					vars.itemInput = -1;
				}
				vars.bridgeInput = -1;
			} else if (Input.mousePosition.x < Screen.width) {
				vars.itemInput = Mathf.Min (Mathf.Max (Input.mousePosition.y / Screen.height, 0.0001F), 1);
				vars.bridgeInput = -1;
			}
		}

		foreach (Text texts in GetComponentsInChildren<Text>()) {
			if(texts.gameObject.name == "Score"){
				texts.text = "Score: "+vars.score;
			} else if(texts.gameObject.name == "Health"){

				texts.text = "Health: "+vars.health;
			}
		}
	}
}
