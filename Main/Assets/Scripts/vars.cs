using UnityEngine;
using System.Collections;

public class vars : MonoBehaviour {

	public static float blockSize = 2;
	public static float screenStart = 15;
	public static float levelHeight = 6;
	public static float minSpeed = .05F;
	public static float maxSpeed = .4F;
	public static float maxJump = .15F;
	public static float acceleration = .00004F;

	public static float gravity = .01F;
	public static float phaseOutSpeed = .2F;
	public static int gapLength = 6;
	public static int seed = 25565;

	public static float secondsPastFrame = 0;
	public static float frameRate = 1F/50F;

	public static int health = 20;
	public static int score = 0;

}
