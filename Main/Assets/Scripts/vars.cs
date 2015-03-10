using UnityEngine;
using System.Collections;

public class vars : MonoBehaviour {

	public static float blockSize = 2;
	public static float screenStart = 15;
	public static float levelHeight = 6;
	public static float minSpeed = .05F;
	public static float maxSpeed = .4F;
	public static float maxJump = .15F;
	public static float acceleration = .00002F;

	public static float gravity = .01F;
	public static float phaseOutSpeed = .2F;
	public static int gapLength = 6;
	public static int seed = 25565;

	public static float secondsPastFrame = 0;
	public static float frameRate = 1F/50F;

	public static int health = 20;
	public static int score = 0;

	public static float bridgeInput = -1;
	public static float itemInput = -1;


	public static float backWidth = 10;
	public static float backHeight = 5;


	public static int randCurve (int n, int c){
		
		return (int) Mathf.Min (Mathf.Floor(Mathf.Pow(Random.Range(0F,n+1F),c+1)/Mathf.Pow(n+1,c)),n);

	}

	public static void particles(float x, float y, int time, float rate, int type){
		GameObject go = GameObject.Instantiate (Resources.Load ("Dust")) as GameObject;
		go.transform.position = new Vector3(x,y+player.hSpeed,0);
		go.GetComponent<dust>().type = type;
		go.GetComponent<dust>().lifeSpan = time;
		go.particleSystem.emissionRate = rate;
	}

}