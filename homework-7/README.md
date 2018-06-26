# Unity-3D-homework7
-----
- 视频地址: https://pan.baidu.com/s/1skIrvinREJ_x4_ZU89Ikxg
----

## 实验要求：简单粒子制作 ## 
- 按参考资源要求，制作一个粒子系统
- 用代码控制使之在不同场景下效果不一样

----
### 实现效果：

![](https://img3.doubanio.com/view/photo/l/public/p2526275160.webp)

![](https://img3.doubanio.com/view/photo/l/public/p2526275162.webp)

![](https://img3.doubanio.com/view/photo/l/public/p2526275163.webp)

![](https://img3.doubanio.com/view/photo/l/public/p2526275164.webp)

![](https://img3.doubanio.com/view/photo/l/public/p2526275165.webp)

----
### 实现过程：

属性设置：由于粒子系统的属性设置大部分可以在代码中设置，所以粒子的最终参数还是要以代码中设置为准。

![](https://img3.doubanio.com/view/photo/l/public/p2526275166.webp)

    public Camera camera;                                     //摄像机
    public ParticleSystem particleRing;                       //粒子系统
    private int particleNum = 50000;                          //粒子数目
    private ParticleSystem.Particle[] particles;
    private ParticleInfo[] particleDatas;                     //粒子数据

    private float minRadius = 7.0f;                           //最内层半径
    private float maxRadius = 10.0f;                          //最外层半径

    private bool isChanged = false;                            //是否改变
    private float speed = 0.1f;
    private float shringSpeed = 1f;

粒子属性类：

    public class ParticleInfo
    {
        public float angle;                                       // 粒子初始角度
        public float radius;                                      // 当前粒子半径
        public float beforeRadius;                                // 粒子收缩前半径
        public float shringRadius;                                // 粒子收缩后半径

        public ParticleInfo(float angle, float radius, float beforeRadius, float shringRadius)
        {
            this.angle = angle;
            this.radius = radius;
            this.beforeRadius = beforeRadius;
            this.shringRadius = shringRadius;
        }
    }

粒子效果变换按钮：（通过bool值ischange来判断粒子当前的状态）

    //change按钮
    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 100, 200, 60), "Change"))
        {
            if (isChanged)
                isChanged = false;
            else isChanged = true;
        }
    }

粒子变换后效果的实现：

    int oneFifth = particleNum / 5;
        //点击按钮后，修改粒子，分五层
        for (int i = 0; i < oneFifth*4; ++i)
        {
            if (isChanged)
            {
                if (particleDatas[i].radius > particleDatas[i].shringRadius)
                {
                    particleDatas[i].radius -= shringSpeed * (particleDatas[i].radius / particleDatas[i].shringRadius ) * Time.deltaTime;
                    particles[i].startSize = 0.1f;
                }
            }
            else
            {
                particles[i].startSize = 0.05f;
                if (particleDatas[i].radius < particleDatas[i].beforeRadius)
                {
                    particleDatas[i].radius += shringSpeed * (particleDatas[i].beforeRadius / particleDatas[i].radius) * Time.deltaTime;
                }
                else
                {
                    particleDatas[i].radius = particleDatas[i].beforeRadius;
                }
            }
        }
---