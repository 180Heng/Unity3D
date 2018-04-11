# 牧师与魔鬼 动作分离版
-----

- 为何要建立动作管理器？

    V1.0的牧师与魔鬼中，我们把物体的动作，包括牧师的上船下船，船的移动，统一封装在GenGameObjects里面，这样操作的话，当我们游戏对象增加起来的时候，代码量就会大大增加，代码出错时的维护成本也会大大增加。所以我们就建立动作管理器来解决这个问题。

-----
- 具体实现：

    * 接口：

            public interface ActionCallback {
                void actionDone (SSAction source);
            }


    * 抽象的动作类：

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




    * 重点-动作管理器：

            public class SSActionManager : MonoBehaviour, ActionCallback {
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



    * 基础动作的实现：

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


     * 组合动作的实现：

            public class CCSequenceAction : SSAction, ActionCallback {
                public List<SSAction> sequence;
                public int repeat = 1; // 1->only do it for once, -1->repeat forever
                public int currentActionIndex = 0;

                public static CCSequenceAction getAction(int repeat, int currentActionIndex, List<SSAction> sequence) {
                    CCSequenceAction action = ScriptableObject.CreateInstance<CCSequenceAction>();
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



