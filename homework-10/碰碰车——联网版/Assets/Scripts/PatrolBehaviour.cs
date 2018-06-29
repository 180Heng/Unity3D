using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Patrols;
using UnityEngine.Networking;

public class PatrolBehaviour : NetworkBehaviour
{
    private IAddAction addAction;
    private IGameStatusOp gameStatusOp;

    public int ownIndex;
    public bool isCatching;    //是否感知到hero

    private float CATCH_RADIUS = 1.0f;

    void Start () {
        addAction = SceneController.getInstance() as IAddAction;
        gameStatusOp = SceneController.getInstance() as IGameStatusOp;
        ownIndex = getOwnIndex();
        isCatching = false;
    }
	
	void Update () {
        CmdcheckNearByHero();
	}

    int getOwnIndex() {
        string name = this.gameObject.name;
        char cindex = name[name.Length - 1];
        int result = cindex - '0';
        return result;
    }

    void CmdcheckNearByHero() {
        if (gameStatusOp.getHeroStandOnArea() == ownIndex) {   
            if (!isCatching) {
                isCatching = true;
                addAction.addDirectMovement(this.gameObject);
            }
        }
        else {
            if (isCatching) {    //刚才为捕捉状态，但此时hero已经走出所属区域
                gameStatusOp.heroEscapeAndScore();
                isCatching = false;
                addAction.addRandomMovement(this.gameObject, false);
            }
        }
    }
    
    void OnCollisionStay(Collision e) {
        if (e.gameObject.name.Contains("Hero")) {
            gameStatusOp.patrolHitHeroAndGameover();
            Debug.Log("Game Over!");
        }
    }
}
