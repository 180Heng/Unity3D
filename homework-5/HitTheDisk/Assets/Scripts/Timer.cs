using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Timer : MonoBehaviour {


	private float totalTime=30;

	private float x = Screen.width*0.5f+250;
	private float y = Screen.height*0.5f-190;





	void OnGUI () {
		GUIStyle textStyle = new GUIStyle ();
		textStyle.fontSize = 40;
		if (totalTime >= 0) {
			GUI.Label (new Rect (x + 10, y - 100, -100, 100), "倒计时:" + (int)totalTime + "s", textStyle);

		}
		else if(totalTime>=-3)
			GUI.Label (new Rect (x + 10, y - 100, -100, 100), "倒计时:0s", textStyle);
		else if(totalTime<-3) 
			FirstSceneControllerBase.GetFirstSceneControllerBase().SetGameStatus(GameStatus.Lose);
		totalTime-=Time.deltaTime;
	}


}

