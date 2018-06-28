using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Patrols;


public class HeroStatus : MonoBehaviour {
    public int standOnArea = -1;
    public GameObject player;
    public delegate void GameOverAction();
	void Start () {
		
	}
	
	void Update () {
        Camera.main.transform.position = new Vector3(player.transform.position.x/4, 13.0f, player.transform.position.z/3);
    }
	private void OnGUI() {
		modifyStandOnArea();
		float posY = this.gameObject.transform.position.y;
		if (posY <= -3.0) {
			GUIStyle fontStyle = new GUIStyle();  
			fontStyle.fontSize = 50 ;  
			fontStyle.normal.background = null;    //设置背景填充  
			fontStyle.normal.textColor= new Color(1,0,0);   //设置字体颜色  
			GUI.Label(new Rect(Screen.width / 3, Screen.height / 2 +100, Screen.width / 2,  Screen.height / 2), "Oh! You Lose Yourself",fontStyle);
		}
	}




    void modifyStandOnArea() {
        float posX = this.gameObject.transform.position.x;
        float posZ = this.gameObject.transform.position.z;
        if (posZ >= FenchLocation.FenchHori) {
            if (posX < FenchLocation.FenchVertLeft)
                standOnArea = 0;
            else if (posX > FenchLocation.FenchVertRight)
                standOnArea = 2;
            else
                standOnArea = 1;
        }
        else {
            if (posX < FenchLocation.FenchVertLeft)
                standOnArea = 3;
            else if (posX > FenchLocation.FenchVertRight)
                standOnArea = 5;
            else
                standOnArea = 4;
        }
    }
}
