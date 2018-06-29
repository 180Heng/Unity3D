using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Patrols;
using UnityEngine.Networking;

public class HeroStatus : NetworkBehaviour
{
    private int State;//角色状态
    private int oldState = 0;//前一次角色的状态
    private int UP = 0;//角色状态向前
    private int RIGHT = 1;//角色状态向右
    private int DOWN = 2;//角色状态向后
    private int LEFT = 3;//角色状态向左
    public int standOnArea = -1;
    public delegate void GameOverAction();
	void Start () {
		
	}
    void Update () {
        if (!isLocalPlayer)
            return;
        Camera.main.transform.position = new Vector3(this.gameObject.transform.position.x/4, 13.0f, this.gameObject.transform.position.z/3);
        if (Input.GetKey("w"))
        {
            setState(UP);
        }
        else if (Input.GetKey("s"))
        {
            setState(DOWN);
        }

        if (Input.GetKey("a"))
        {
            setState(LEFT);
        }
        else if (Input.GetKey("d"))
        {
            setState(RIGHT);
        }
    }
    void setState(int currState)
    {
        Vector3 transformValue = new Vector3();//定义平移向量
        int rotateValue = (currState - State) * 90;
        switch (currState)
        {
            case 0:
                transformValue = Vector3.forward * Time.deltaTime * 10;
                break;
            case 1:
                transformValue = Vector3.right * Time.deltaTime * 10;
                break;
            case 2:
                transformValue = Vector3.back * Time.deltaTime * 10;
                break;
            case 3://角色状态向左时，角色不断向左缓慢移动
                transformValue = Vector3.left * Time.deltaTime * 10;
                break;
        }
        transform.Rotate(Vector3.up, rotateValue);//旋转角色
        transform.Translate(transformValue, Space.World);//平移角色
        oldState = State;//赋值，方便下一次计算
        State = currState;//赋值，方便下一次计算
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
