using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tictactoe : MonoBehaviour {

	private int[,] chessboard = new int[3, 3]; //the chessboard
	private int gameStatus = 0; //judge whether the game begins
	private int turn= 1; 
	private float x = Screen.width*0.5f-150;
	private float y = Screen.height*0.5f-150;
	private int Winner = 0;
	float height = Screen.height * 0.5f;
	float width = Screen.width * 0.5f;
	int bHeight = 100;
	int bWidth = 150;

	// Use this for initialization
	void Start () {
		Restart();
	}

	//initial
	void Restart() {
		gameStatus = 0;
		Winner = 0;
		turn = 1;
		for (int i = 0; i < 3; i++)
			for (int j = 0; j < 3; j++)
				chessboard[i,j] = 0;
	}





	void OnGUI () {
		GUIStyle textStyle = new GUIStyle ();//text
		GUIStyle textStyle_OX = new GUIStyle ();
		textStyle.normal.textColor = Color.red;
		textStyle.fontSize = 70;
		textStyle_OX.fontSize = 30;
		GUI.Label(new Rect(x-40,y-100,300,100), "Tic Tac Toe", textStyle);
		if (gameStatus == 0) {
			int result = 0;
			if (Winner == 0) {
				result = judge ();
				if (result != 0) {
					Winner = result;
					gameStatus = 1;
				}
			}
			for (int i = 0; i < 3; i++)
				for (int j = 0; j < 3; j++) {
					if (chessboard [i, j] == 1)
						GUI.Button (new Rect (x + i * 100, y + j * 100, 100, 100), "O");
					if (chessboard [i, j] == 2)
						GUI.Button (new Rect (x + i * 100, y + j * 100, 100, 100), "X");
					if (GUI.Button (new Rect (x + i * 100, y + j * 100, 100, 100), "")) {  
						if (result == 0) {  
							chessboard [i, j] = turn;  
							turn = (turn== 2) ? 1 : 2;
						}  
					}  
				}
		} 
		else {
			if (Winner == 1) {
				GUI.Label (new Rect (x, y, 100, 50), "Player1 wins!", textStyle);
			} else if (Winner == 2) {
				GUI.Label (new Rect (x, y, 100, 50), "Player2 wins!", textStyle);
			} else if (Winner == 3) {
				GUI.Label (new Rect (x, y, 100, 50), "Dogfall~", textStyle);
			}
			if (GUI.Button (new Rect (x+30, y+150, 120, 50), "Play again!"))
				Restart();
		}
	}

	//judge whether gameover 
	int judge () {
		for (int i = 0; i < 3; i++) {
			if (chessboard [0, i] != 0 && chessboard [0, i] == chessboard [1, i] && chessboard [1, i] == chessboard [2, i]){
				return chessboard [0, i];
			}
			if (chessboard [i, 0] != 0 && chessboard [i, 0] == chessboard [i, 1] && chessboard [i, 1] == chessboard [i, 2]){
				return chessboard [i, 0];
			}
		}
		if (chessboard [1, 1] != 0 && chessboard [0, 0] == chessboard [1, 1] && chessboard [1, 1] == chessboard [2, 2]){
			return chessboard [1, 1];
		}
		if (chessboard [1, 1] != 0 && chessboard [0, 2] == chessboard [1, 1] && chessboard [1, 1] == chessboard [2, 0]){
			return chessboard [1, 1];
		}
		for (int i = 0; i < 3; i++){
			for (int j = 0; j < 3; j++) {
				if (chessboard[i,j] == 0) {
					return 0;
				}
			}
		}
		return 3;
	}
}
