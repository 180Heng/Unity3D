using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;



public class GameStatusText : NetworkBehaviour
{

    public int score = 0;
    public int textType;  //0为score，1为gameover

	void Start () {
        distinguishText();
	}
	
	void Update () {
		
	}

    void distinguishText() {
        if (gameObject.name.Contains("Score"))
            textType = 0;
        else
            textType = 1;
    }

    void OnEnable() {
        GameEventManager.myGameScoreAction += gameScore;
        GameEventManager.myGameOverAction += gameOver;
    }

    void OnDisable() {
        GameEventManager.myGameScoreAction -= gameScore;
        GameEventManager.myGameOverAction -= gameOver;
    }

    void gameScore() {
        if (textType == 0) {
            score++;
            this.gameObject.GetComponent<Text>().text = "Score: " + score;
        }
    } 

    void gameOver() {
        if (textType == 1 )
            this.gameObject.GetComponent<Text>().text = "You lose";
    }
}
