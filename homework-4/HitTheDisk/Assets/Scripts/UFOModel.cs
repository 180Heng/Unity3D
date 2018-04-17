using System;
using UnityEngine;
using UnityEngine.UI;

public class UFOModel {
    public Color UFOColor;
    public Vector3 startPos;
    public Vector3 startDirection;
    public float UFOSpeed;

    public void Reset(int round) {
        UFOSpeed = 0.1f;
		if (round  == 2|| round  == 5) {
			UFOColor = Color.yellow;
			startPos = new Vector3(-2f, 3f, -10f);
            startDirection = new Vector3(3f, 8f, 5f);
        } else {
			UFOColor = Color.blue;
            startPos = new Vector3(2f, 3f, -10f);
            startDirection = new Vector3(-3f, 8f, 5f);
        }
        for (int i = 1; i < round; i++) {
            UFOSpeed *= 1.1f;
        }
    }
}