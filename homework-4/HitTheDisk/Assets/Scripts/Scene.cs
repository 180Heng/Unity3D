using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour {
    public int Round { get; set; }
    public int UFONum { get; private set; }
	public int count;
    private UFOModel ufoModel = new UFOModel();
    
    List<GameObject> inUseUFOs;

	public void Reset(int round,int count) {
        Round = round;
        UFONum = round;
        ufoModel.Reset(round,count);
    }

    public void SendUFO(List<GameObject> usingUFOs) {
		count++;
        inUseUFOs = usingUFOs;
		Reset(Round,count);
        for (int i = 0; i < usingUFOs.Count; i++) {
            usingUFOs[i].GetComponent<Renderer>().material.color = ufoModel.UFOColor;

            var startPos = ufoModel.startPos;
            usingUFOs[i].transform.position = new Vector3(startPos.x, startPos.y + i, startPos.z);

            Rigidbody rigibody;
            rigibody = usingUFOs[i].GetComponent<Rigidbody>();
            rigibody.WakeUp();
            rigibody.useGravity = true;
            rigibody.AddForce(ufoModel.startDirection * Random.Range(ufoModel.UFOSpeed * 5, ufoModel.UFOSpeed * 8) / 5, 
                ForceMode.Impulse);
			FirstSceneControllerBase.GetFirstSceneControllerBase ().Count ();
            FirstSceneControllerBase.GetFirstSceneControllerBase().SetSceneStatus(SceneStatus.Shooting);
        }
    }

    public void DestroyUFO(GameObject UFO) {
        UFO.GetComponent<Rigidbody>().Sleep();
        UFO.GetComponent<Rigidbody>().useGravity = false;
        UFO.transform.position = new Vector3(0f, -99f, 0f);
    }

    public void SceneUpdate() {
        Round++;
        Reset(Round,count);
    }

    private void Start() {
        Round = 1;
		count = 0;
        Reset(Round,count);
    }
		
    private void Update() {
        if (inUseUFOs != null) {
            for (int i = 0; i < inUseUFOs.Count; i++) {
                if (inUseUFOs[i].transform.position.y <= 1f) {
                    FirstSceneControllerBase.GetFirstSceneControllerBase().DestroyUFO(inUseUFOs[i]);
                    FirstSceneControllerBase.GetFirstSceneControllerBase().SubScore();
                }
            }
            if (inUseUFOs.Count == 0) {
                FirstSceneControllerBase.GetFirstSceneControllerBase().SetSceneStatus(SceneStatus.Waiting);
            }
        }
    }
}