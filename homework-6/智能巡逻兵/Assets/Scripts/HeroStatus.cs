using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Patrols;

//----------------------------------
// 此脚本加在主人公car上
//----------------------------------

public class HeroStatus : MonoBehaviour {
    public int standOnArea = -1;
	public delegate void GameOverAction();
	void Start () {
		
	}
	
	void Update () {
        
	}
	private void OnGUI() {
		modifyStandOnArea();
		float posY = this.gameObject.transform.position.y;
		if (posY <= -3.0) {
			GUIStyle fontStyle = new GUIStyle();  
			fontStyle.fontSize = 40 ;  
			fontStyle.normal.background = null;    //设置背景填充  
			fontStyle.normal.textColor= new Color(1,0,0);   //设置字体颜色  
			GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, Screen.width / 2,  Screen.height / 2), "Oh! You Lose Yourself",fontStyle);
		}
	}



    //检测所在区域
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
