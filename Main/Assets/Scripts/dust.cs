using UnityEngine;
using System.Collections;

public class dust : MonoBehaviour {

	public int frameBalance = 1;
	public int lifeSpan;
	public int lifeTime = 0;
	public int type = -1;
	public bool still = false;

	void Start () {
		//Types:
		//0 = Rock
		//1 = Slime
		//2 = Magma
		//3 = Ice
		//4 = Explosion fire
		//5 = Explosion smoke
		//6 = Magic
		//7 = Rope
		//8 = projectile
		switch (type) {
		case 0:
			particleSystem.startColor = new Color(.4f,.4f,.4f);
			transform.localScale = new Vector3(2,2,2);
			break;
		case 1:
			particleSystem.startColor = new Color(.52f,.78f,.45f,.7f);
			break;
		case 2:
			particleSystem.startColor = new Color(.22f,.11f,.06f,.9f);
			break;
		case 3:
			particleSystem.startColor = new Color(.42f,.63f,1f,.5f);
			break;
		case 4:
			particleSystem.startColor = new Color(1,.2f,0,.5f);
			particleSystem.gravityModifier = 0;
			break;
		case 5:
			particleSystem.startColor = new Color(1,1,1,.5f);
			particleSystem.gravityModifier *= -.5f;
			break;
		case 6:
			particleSystem.startColor = new Color(.83f,.23f,1,.5f);
			particleSystem.gravityModifier *= .5f;
			break;
		case 7:
			particleSystem.startColor = new Color(.44f,.35f,.21f);
			particleSystem.startSize = particleSystem.startSize/2;
			transform.localScale = new Vector3(-player.hSpeed*50,3,.1f);
			break;
		case 8:
			particleSystem.startColor = new Color(1,1,.7f,.5f);
			particleSystem.gravityModifier *= .5f;
			break;
		case 9:
			particleSystem.startColor = new Color(.83f,.23f,1,.5f);
			particleSystem.gravityModifier *= .05f;
			particleSystem.startSize = particleSystem.startSize/4;
			still = true;
			transform.localScale = new Vector3(vars.blockSize/3,vars.blockSize*1.3f,.5f);
			particleSystem.startSpeed *= .05f;
			particleSystem.startLifetime = .8f;
			break;
		case 10:
			particleSystem.startColor = new Color(.83f,.23f,1,.5f);
			particleSystem.gravityModifier *= .5f;
			transform.rotation = GameObject.FindGameObjectWithTag("Bridge").transform.rotation;
			transform.localScale = GameObject.FindGameObjectWithTag("Bridge").transform.localScale*1.5f;
			particleSystem.startLifetime = 1;
			break;
		}
	}

	void Update () {
		if (frameBalance > 0) {
			frameBalance -= 1;
			if (lifeTime >= lifeSpan) {
				particleSystem.emissionRate = 0;
				if (particleSystem.particleCount < 1) {
					Destroy (gameObject);
				}
			}
		}
	}
}
