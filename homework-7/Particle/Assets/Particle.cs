using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Particle : MonoBehaviour {
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
    public Camera camera;                                     //摄像机
    public ParticleSystem particleRing;                       //粒子系统

    private int particleNum = 50000;                          //粒子数目
    private ParticleSystem.Particle[] particles;              //粒子
    private ParticleInfo[] particleDatas;                     //粒子数据

    private float minRadius = 7.0f;                           //最内层半径
    private float maxRadius = 10.0f;                          //最外层半径

    private bool isChanged = false;                            //是否改变
    private int level = 5;
    private float speed = 0.1f;
    private float shringSpeed = 1f;


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

    // Use this for initialization
    void Start() {
        particleRing.maxParticles = particleNum;                //设置最大粒子数
        particles = new ParticleSystem.Particle[particleNum];   //新建粒子数组
        particleDatas = new ParticleInfo[particleNum];          //新建粒子数据数组
        particleRing.Emit(particleNum);                         //粒子系统发射粒子
        particleRing.GetParticles(particles);
        int oneFifth = particleNum / 5;
        //初始化粒子位置
        for (int i = 0; i < particleNum; ++i) {
          float middleRadius = (maxRadius + minRadius) / 2;
          float upperbound = Random.Range(middleRadius, maxRadius);
          float lowerbound = Random.Range(minRadius, middleRadius);
          float radius = Random.Range(lowerbound, upperbound);
          float angle = Random.Range(0.0f, 360.0f);
          int circle = i / oneFifth +1 ;
          particleDatas[i] = new ParticleInfo(angle, radius, radius, 1+circle);

        }
   }

   // Update is called once per frame
   void Update() {
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
        
        for (int i = 0; i < particleNum; ++i) {
            if (i % 2 == 0) particleDatas[i].angle += (i % level + 1) * speed;
            else  particleDatas[i].angle -= (i % level + 1) * speed;
          
            particleDatas[i].angle %= 360;
            float rad = particleDatas[i].angle / 180 * Mathf.PI;
            particles[i].position = new Vector3(particleDatas[i].radius * Mathf.Cos(rad), particleDatas[i].radius * Mathf.Sin(rad), 0);
        }
        particleRing.SetParticles(particles, particleNum);
   }
}
