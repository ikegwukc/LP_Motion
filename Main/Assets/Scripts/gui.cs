using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class gui : MonoBehaviour {

	void Start () {

	}

	void Update () {
		foreach (Text texts in GetComponentsInChildren<Text>()) {
			if(texts.gameObject.name == "Score"){
				texts.text = "Score: "+vars.score;
			} else if(texts.gameObject.name == "Health"){

				texts.text = "Health: "+vars.health;
			}
		}
	}
}
