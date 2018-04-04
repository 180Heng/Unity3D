using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;

public class ClickGUI : MonoBehaviour {
	UserAction action;
	MyCharacterController characterController;

	public void setController(MyCharacterController characterCtrl) {
		characterController = characterCtrl;
	}

	void Start() {
		action = Director.getInstance ().currentSceneController as UserAction;
	}

	void OnMouseDown() {
		if (gameObject.name == "boat") {
			action.moveBoat ();
		} else {
			action.characterIsClicked (characterController);
		}
	}
	public Texture2D img; 

	void OnGUI(){
		string a = "";
		GUIStyle b = new GUIStyle(); 
		b.normal.background = img;
		GUI.Label(new Rect(0, 0, 1370, 780), a, b);
	}

}


