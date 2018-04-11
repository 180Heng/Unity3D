using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;



public interface ActionCallback {
	void actionDone (SSAction source);
}


public class SSAction : ScriptableObject {
	public bool enable = true;
	public bool destroy = false;

	public GameObject gameObject;
	public Transform transform;
	public ActionCallback callback;

	public virtual void Start()
	{
		throw new System.NotImplementedException();
	}

	public virtual void Update()
	{
		throw new System.NotImplementedException();
	}
}



public class MoveAction : SSAction {
	public Vector3 target;
	public float speed;

	private MoveAction(){
	}

	public static MoveAction getAction(Vector3 target, float speed) {
		MoveAction action = ScriptableObject.CreateInstance<MoveAction> ();
		action.target = target;
		action.speed = speed;
		return action;
	}

	// Use this for initialization
	public override void Start () {

	}

	// Update is called once per frame
	public override void Update () {
		transform.position = Vector3.MoveTowards(transform.position, target, speed*Time.deltaTime);
		if (transform.transform.position == target) {
			destroy = true;
			callback.actionDone (this);
		}
	}
}




public class SequenceAction : SSAction, ActionCallback {
	public List<SSAction> sequence;
	public int repeat = 1; // 1->only do it for once, -1->repeat forever
	public int currentActionIndex = 0;

	public static SequenceAction getAction(int repeat, int currentActionIndex, List<SSAction> sequence) {
		SequenceAction action = ScriptableObject.CreateInstance<SequenceAction>();
		action.sequence = sequence;
		action.repeat = repeat;
		action.currentActionIndex = currentActionIndex;
		return action;
	}

	public override void Update() {
		if (sequence.Count == 0)return;
		if (currentActionIndex < sequence.Count) {
			sequence[currentActionIndex].Update();
		}
	}

	public void actionDone(SSAction source) {
		source.destroy = false;
		this.currentActionIndex++;
		if (this.currentActionIndex >= sequence.Count) {
			this.currentActionIndex = 0;
			if (repeat > 0) repeat--;
			if (repeat == 0) {
				this.destroy = true;
				this.callback.actionDone(this);
			}
		}
	}

	public override void Start() {
		foreach(SSAction action in sequence) {
			action.gameObject = this.gameObject;
			action.transform = this.transform;
			action.callback = this;
			action.Start();
		}
	}

	void OnDestroy() {
		foreach(SSAction action in sequence) {
			DestroyObject(action);
		}
	}
}





public class ActionManager : MonoBehaviour, ActionCallback {
	private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
	private List<SSAction> waitingToAdd = new List<SSAction>();
	private List<int> watingToDelete = new List<int>();

	protected void Update() {
		foreach(SSAction ac in waitingToAdd) {
			actions[ac.GetInstanceID()] = ac;
		}
		waitingToAdd.Clear();

		foreach(KeyValuePair<int, SSAction> kv in actions) {
			SSAction ac = kv.Value;
			if (ac.destroy) {
				watingToDelete.Add(ac.GetInstanceID());
			} else if (ac.enable) {
				ac.Update();
			}
		}

		foreach(int key in watingToDelete) {
			SSAction ac = actions[key];
			actions.Remove(key);
			DestroyObject(ac);
		}
		watingToDelete.Clear();
	}

	public void addAction(GameObject gameObject, SSAction action, ActionCallback callback) {
		action.gameObject = gameObject;
		action.transform = gameObject.transform;
		action.callback = callback;
		waitingToAdd.Add(action);
		action.Start();
	}

	public void actionDone(SSAction source) {

	}
}





