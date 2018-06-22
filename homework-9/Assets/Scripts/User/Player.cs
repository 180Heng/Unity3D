using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Tank
{
    public delegate void destroy();
    public static event destroy destroyEvent;

    void Start()
    {
        setHp(500f);//设置初始生血量为500
    }
    void Update()
    {
        if (getHp() <= 0)
        {
            //血量为0，游戏结束
            this.gameObject.SetActive(false);
            if (destroyEvent != null)
            {
                destroyEvent();
            }
        }
    }
}