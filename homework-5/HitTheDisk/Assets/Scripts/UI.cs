using System;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    GameObject scoreText;
    GameObject gameStatuText;
	private float totalTime=10;
	private float intervalTime=1;
	public Text CountDownText;

    IScore score = FirstSceneControllerBase.GetFirstSceneControllerBase() as IScore;
    IQueryStatus gameStatu = FirstSceneControllerBase.GetFirstSceneControllerBase() as IQueryStatus;

    // Use this for initialization
    void Start() {
        scoreText = GameObject.Find("Score");
        gameStatuText = GameObject.Find("GameStatu");
		CountDownText.text = string.Format ("{0:D2:{1:D2}}",(int)totalTime/60,(int)totalTime%60);
    }

    // Update is called once per frame
    void Update() {
        string score = Convert.ToString(this.score.GetScore());
        if (gameStatu.QueryGameStatus() == GameStatus.Lose) {
			gameStatuText.GetComponent<Text>().text = "Game Over! \n You final score:"+score;
        } else if (gameStatu.QueryGameStatus() == GameStatus.Win) {
            gameStatuText.GetComponent<Text>().text = "You Win!";
        }
        scoreText.GetComponent<Text>().text = "按空格键发射飞碟.\n" +
            "Score: " + score;

		if (totalTime > 0) 
		{
			intervalTime -= Time.deltaTime;
			if(intervalTime<=0)
			{
				intervalTime += 1;
				totalTime--;
				CountDownText.text = string.Format ("{0:D2:{1:D2}}",(int)totalTime/60,(int)totalTime%60);
			}
		}
		
    }
}
