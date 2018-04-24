# Unity-3D-homework5
-----
## 视频演示网站：

- HitTheDisk Adapter模式：https://pan.baidu.com/s/1ZzRuG50DAE1Z8uwe3J77Rw

-----
- 这次任务我们是在学习了物理运动之后，给我们的HitTheDisk游戏加上adapter模式，使它同时支持物理运动与运动学（变换）运动。其实也只是通过接口来实现两种运动模式的切换，也就是使用刚体和不使用刚体两种模式来相互切换。

- 我们把函数声明实现在接口*ActionModeManager*中，通过对下面*FirstSceneController*中参数的修改来达到**CCAction**（运动学）和**PhysicAction**（物理运动）两种模式之间的切换，具体游戏中实现的效果其实没有太大区别，但是在代码量上，明显新学的物理运动的代码量更少一些，通过对**Rigidbody**（刚体）的使用，我们实现具体的运动相对更为简单。

        public class FirstSceneController : MonoBehaviour {

            // Use this for initialization
            void Start() {
                FirstSceneControllerBase.GetFirstSceneControllerBase().scene = gameObject.AddComponent<PhysicActionManager>();
            }
        }

        public class FirstSceneController : MonoBehaviour {

            // Use this for initialization
            void Start() {
                FirstSceneControllerBase.GetFirstSceneControllerBase().scene = gameObject.AddComponent<CCActionManager>();
            }
        }