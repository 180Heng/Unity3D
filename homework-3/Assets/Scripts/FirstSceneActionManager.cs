using UnityEngine;
using System.Collections;
using Com.Mygame;
using System.Collections.Generic;


public class FirstSceneActionManager : ActionManager {
	public void toggleBoat(BoatController boat) {
		MoveAction action = MoveAction.getAction (boat.getTarget (), boat.speed);
		this.addAction (boat.getBoat (), action, this);
	}

	public void moveCharacter(MyCharacterController character, Vector3 target) {
		Vector3 nowPos = character.getPos ();
		Vector3 tmpPos = nowPos;
		if (target.y > nowPos.y) {
			tmpPos.y = target.y;
		} else {
			tmpPos.x = target.x;
		}
		SSAction action1 = MoveAction.getAction(tmpPos, character.speed);
		SSAction action2 = MoveAction.getAction(target, character.speed);
		SSAction sequenceAction = SequenceAction.getAction(1, 0, new List<SSAction>{action1, action2});
		this.addAction(character.getInstance(), sequenceAction, this);
	}
}