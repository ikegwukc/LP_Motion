using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Leap;

public class gui : MonoBehaviour {

	public int frameBalance = 1;
	float lastFrame;
	Controller controller = new Controller();
	Leap.Gesture gesture = new Leap.Gesture();
	SwipeGesture sGesture = new SwipeGesture ();
	public static int type = 0;
	public static int species = 0;
	int options = 0;
	string[] option = new string[10];
	float rotation;
	float loading = 0;
	bool stopLoad;
	int selected = 0;
	int slide = 0;
	int newSlide = 0;
	float[] handX = new float[10];
	int swipes = 0;

	void Start(){
		controller.EnableGesture(Leap.Gesture.GestureType.TYPE_SWIPE);
		//sGesture.Direction.x;
		for (int i = 0; i < handX.Length; i++) {
			handX[i] = -999;
		}
	}

	void Update () {

		//Debug.LogError(controller.Frame().Hands[0].Direction.x*(180/Mathf.PI));
		if (frameBalance > 0) {
			frameBalance -= 1;

			switch (type) {
			case 0:

				if(controller.IsConnected){
					if(controller.Frame().Hands.Count>0){
						rotation = controller.Frame().Hands[0].PalmNormal.Roll*(180/Mathf.PI)-90;
					} else {
						loading = 0-.01f;
					}
				} else {
					rotation = (360/Mathf.PI/2)*Mathf.Atan((Input.mousePosition.y-UnityEngine.Screen.height/2)/(Input.mousePosition.x-UnityEngine.Screen.width/2))+90;
					if(Input.mousePosition.x>=UnityEngine.Screen.width/2){
						rotation += 180;
					}
					loading = 0-.01f;
				}
				rotation = fixAngle(rotation);
				switch (species) {
				case 0:
					options = 3;
					option[0] = "Play";
					option[1] = "Help";
					option[2] = "Exit";
					if(Input.GetMouseButtonDown(0)){
						loading = 1;
					}
					break;
				}
				//transform.GetChild(2).GetComponent<UnityEngine.UI.Image>().overrideSprite =  Resources.Load("sb1", typeof(Texture2D)) as Sprite;
				foreach (UnityEngine.UI.Image texts in GetComponentsInChildren<UnityEngine.UI.Image>()) {
					for(int i = 0; i < options; i++){
						if(texts.gameObject.name == "Line"+i.ToString()){
							texts.sprite = Resources.Load<Sprite>("guiClear");
						}
					}
				}
				stopLoad = true;
				foreach (UnityEngine.UI.Image texts in GetComponentsInChildren<UnityEngine.UI.Image>()) {
					if (texts.gameObject.name == "MouseOverlay") {
						texts.sprite = Resources.Load<Sprite>("guiCircle");
						texts.gameObject.transform.localScale = new Vector3((float)UnityEngine.Screen.height/UnityEngine.Screen.width,1,1);
					} else if (texts.gameObject.name == "Arrow") {
						texts.sprite = Resources.Load<Sprite>("guiArrow");
						texts.gameObject.transform.localScale = new Vector3((float)UnityEngine.Screen.height/UnityEngine.Screen.width,1,1);
						texts.gameObject.transform.rotation = new Quaternion(0,0,0,0);
						texts.gameObject.transform.Rotate(new Vector3(0,0,rotation));
					} else if (texts.gameObject.name == "Middle") {
						texts.sprite = Resources.Load<Sprite>("guiMiddle");
						texts.gameObject.transform.localScale = new Vector3((float)UnityEngine.Screen.height/UnityEngine.Screen.width,1,1);
					} else if (texts.gameObject.name == "Load") {
						texts.sprite = Resources.Load<Sprite>("guiLoad");
						texts.gameObject.transform.localScale = new Vector3((float)UnityEngine.Screen.height/UnityEngine.Screen.width*loading,1*loading,1);
					} else {
						for(int i = 0; i < options; i++){
							if(texts.gameObject.name == "Line"+i.ToString()){
								texts.canvasRenderer.SetColor(new Color(1,1,1,1));
								texts.sprite = Resources.Load<Sprite>("guiLine");
								texts.gameObject.transform.rotation = new Quaternion(0,0,0,0);
								texts.gameObject.transform.Rotate(new Vector3(0,0,360/options*i));
								//if(i==1){
									//Debug.LogError(rotation);
								if(Mathf.Abs((360/options*i+360/options/2)-rotation)<(360/options)/4){
									stopLoad = false;
									selected = i;
								}
								//}
								texts.gameObject.transform.localScale = new Vector3((float)UnityEngine.Screen.height/UnityEngine.Screen.width,1,1);
							}
						}
					}
				}
				if(stopLoad){
					loading = 0;
				} else {
					loading += .01f;
				}
				foreach (UnityEngine.UI.Text texts in GetComponentsInChildren<UnityEngine.UI.Text>()) {
					for(int i = 0; i < options; i++){
						if(texts.gameObject.name == "Text"+i.ToString()){
							float rot = ((-i)*(360/options)+90-(360/options)/2)*(Mathf.PI/180);
							texts.gameObject.transform.position = new Vector3(UnityEngine.Screen.width/2+Mathf.Cos(rot)*UnityEngine.Screen.height*7/24,UnityEngine.Screen.height/2+Mathf.Sin(rot)*UnityEngine.Screen.height*7/24,0);
							texts.text = option[i];
							texts.gameObject.transform.localScale = new Vector3(1.7f,1.7f,1.7f);
						}
					}
				}
				if(loading>=1){

					switch(species){
					case 0:
						//selected = (int) rotation/(360/options);
						switch(selected){
						case 0:
							Application.Quit();
							break;
						case 1:
							type = 2;
							species = 0;
							slide = -1;
							newSlide = 0;
							loading = 0;
							break;
						case 2:
							type = 1;
							species = 0;
							vars.health = 20;
							player.hSpeed = -vars.minSpeed;
							vars.score = 0;
							break;
						}
						break;
					}
				}
				break;
			case 1:

				if(controller.Frame().Hands.Count>0){
					rotation = controller.Frame().Hands[0].PalmNormal.Roll*(180/Mathf.PI);
				} else {
					rotation = 115;
				}

				foreach (Text texts in GetComponentsInChildren<Text>()) {
					for(int i = 0; i < options; i++){
						if(texts.gameObject.name == "Text"+i.ToString()){
							texts.text = "";
						}
					}
				}

				foreach (UnityEngine.UI.Image texts in GetComponentsInChildren<UnityEngine.UI.Image>()) {
					texts.sprite = Resources.Load<Sprite>("guiClear");
					if (texts.gameObject.name == "MouseOverlay") {
						if(controller.IsConnected == false){
							texts.sprite = Resources.Load<Sprite>("guiMouse");
							texts.gameObject.transform.localScale = new Vector3(1,1,1);
						}
					}
				}

				if(controller.IsConnected == false){
					if (Input.mousePosition.x > 0) {
						if (Input.mousePosition.x < UnityEngine.Screen.width / 3) {
							vars.bridgeInput = Mathf.Min (Mathf.Max ((Input.mousePosition.y - UnityEngine.Screen.height / 4) / (UnityEngine.Screen.height / 2), 0.0001F), 1);
							vars.itemInput = -1;
						} else if (Input.mousePosition.x < UnityEngine.Screen.width / 3 * 2) {
							if (Input.mousePosition.y < UnityEngine.Screen.height / 3) {
								vars.itemInput = -2;
							} else {
								vars.itemInput = -1;
							}
							vars.bridgeInput = -1;
						} else if (Input.mousePosition.x < UnityEngine.Screen.width) {
							vars.itemInput = Mathf.Min (Mathf.Max (Input.mousePosition.y / UnityEngine.Screen.height, 0.0001F), 1);
							vars.bridgeInput = -1;
						}
					}
				} else {
					if(controller.Frame().Hands.Count>0){

						if( -60 < rotation && rotation < 90 ){
							vars.itemInput = Mathf.Min (Mathf.Max (((float) rotation + 35)/95, 0.0001F), 1);
							vars.bridgeInput = -1;
						} else if( -120 < rotation && rotation < -60 ) {
							vars.itemInput = -2;
							vars.bridgeInput = -1;
						} else if( -180 < rotation && rotation < -120 ){
							vars.bridgeInput = Mathf.Min (Mathf.Max ((rotation+169)/23, 0.0001F), 1);
							vars.itemInput = -1;
						} else if( 140 < rotation && rotation < 180 ){
							vars.bridgeInput = .0001f;
							vars.itemInput = -1;
						}
					} else {
						vars.bridgeInput = -1;
						vars.itemInput = -1;
					}
				}

				break;
			
			case 2:

				foreach (Text texts in GetComponentsInChildren<Text>()) {
					for(int i = 0; i < options; i++){
						if(texts.gameObject.name == "Text"+i.ToString()){
							texts.text = "";
						}
					}
				}
				
				foreach (UnityEngine.UI.Image texts in GetComponentsInChildren<UnityEngine.UI.Image>()) {
					texts.sprite = Resources.Load<Sprite>("guiClear");
					if (texts.gameObject.name == "MouseOverlay") {
						if(slide >= 0){
							texts.sprite = Resources.Load<Sprite>("Slide"+(slide+1).ToString());
							texts.gameObject.transform.localScale = new Vector3(1,1,1);
						} else {
							texts.sprite = Resources.Load<Sprite>("guiClear");
							texts.gameObject.transform.localScale = new Vector3(1,1,1);
						}
					}
					if (texts.gameObject.name == "Line0") {
						if(newSlide >= 0){
							texts.sprite = Resources.Load<Sprite>("Slide"+(newSlide+1).ToString());
							texts.gameObject.transform.localScale = new Vector3(1,1,1);
						} else {
							texts.sprite = Resources.Load<Sprite>("guiClear");
							texts.gameObject.transform.localScale = new Vector3(1,1,1);
						}
					}
				}

				if(slide == newSlide){
					loading = 0;
					if(controller.IsConnected == false){

						if(Input.GetMouseButtonDown(0)){

							newSlide++;
							if(newSlide>=13){
								newSlide = -1;
								//type = 0;
								//species = 0;
							}
							
						}

						if(Input.GetMouseButtonDown(1)){

							newSlide--;
							if(newSlide<0){
								newSlide = -1;
								//type = 0;
								//species = 0;
							}
							
						}
					} else {

						if(controller.Frame().Gestures().Count > 0) {

							gesture = controller.Frame().Gestures()[0];
							if(gesture.Type == Gesture.GestureType.TYPE_SWIPE) {
								sGesture = new SwipeGesture(gesture);
								if(sGesture.State == Gesture.GestureState.STATE_START){
									if(sGesture.Direction.x>0){

										//newSlide--;
										if(newSlide<0){
											//newSlide = -1;
											//type = 0;
											//species = 0;
										}
									} else {
										//newSlide++;
										if(newSlide>=13){
											//newSlide = -1;
											//type = 0;
											//species = 0;
										}
									}
								}
							}
						}

						if(controller.Frame().Hands.Count>0){
							for (int i = 0; i < handX.Length-1; i++) {
								handX[i] = handX[i+1];
								//Debug.Log (i.ToString()+":"+handX[i]);
							}
							handX[handX.Length-1] = controller.Frame().Hands[0].PalmPosition.x;

							//Debug.Log (handX[99]-handX[0]);

							//swipes -= 2;
							//if(swipes < 0){
								//swipes = 0;
							//}
							if(handX[0]>-999 && handX[handX.Length-1]>-999){
								if(handX[handX.Length-1]-handX[0]>300){
									Debug.Log ("0");
									swipes+=6;


									if(swipes > 5){
										newSlide--;
										if(newSlide<0){
											newSlide = -1;
										}
										swipes = 0;
									}

									for (int i = 0; i < handX.Length; i++) {
										handX[i] = -999;
									}
								} else if(handX[handX.Length-1]-handX[0]<-100) {
									Debug.Log ("1");

									swipes-=6;

									if(swipes < -5){
										newSlide++;
										if(newSlide>=13){
											newSlide = -1;
										}
										swipes = 0;
									}

									for (int i = 0; i < handX.Length; i++) {
										handX[i] = -999;
									}
								}
							}
						} else {
							for (int i = 0; i < handX.Length; i++) {
								handX[i] = -999;
							}
							swipes = 0;
						}


						//sGesture = new SwipeGesture();
						//if(swipeGesture.Direction.x>0){
						//	Debug.Log("Swipe");
						//}
						//Debug.Log(sGesture.Direction.x);

						//for(int g = 0; g < controller.Frame().Gestures().Count; g++){
							//Debug.Log(controller.Frame().Hands[0].);
						//}
						
					}
				} else {
					if(loading < 1){
						loading += .05f;
					} else {
						loading = 1;
						slide = newSlide;
						if(slide == -1){
							type = 0;
							species = 0;
						}
					}
				}

				foreach (UnityEngine.UI.Image texts in GetComponentsInChildren<UnityEngine.UI.Image>()) {
					if (texts.gameObject.name == "Line0") {
						texts.canvasRenderer.SetColor(new Color(1,1,1,loading));
					}
				}

				break;
			}

			foreach (Text texts in GetComponentsInChildren<Text>()) {
				if (texts.gameObject.name == "Score") {
					texts.text = "Score: " + vars.score;
				} else if (texts.gameObject.name == "Health") {

					texts.text = "Health: " + vars.health;
				}
			}
		}
	}

	float fixAngle(float a){
		if (a >= 360) {
			return fixAngle (a - 360);
		} else if (a < 0) {
			return fixAngle (a + 360);
		} else {
			return a;
		}
	}
}
