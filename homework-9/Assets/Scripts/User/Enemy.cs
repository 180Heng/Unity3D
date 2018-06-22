using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Tank
{
    public delegate void recycle(GameObject Tank);
    public static event recycle recycleEvent;
    private Vector3 target;
    private bool gameover;

    void Start()
    {
        setHp(100f);
    }

    void Update()
    {
        gameover = GameDirector.getInstance().currentSceneController.isGameOver();
        if (!gameover)
        {
            target = GameDirector.getInstance().currentSceneController.getPlayerPos();
            if (getHp() <= 0 && recycleEvent != null)
            {
                recycleEvent(this.gameObject);
            }
            else
            {
                NavMeshAgent agent = GetComponent<NavMeshAgent>();
                agent.SetDestination(target);
            }
        }
        else
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.velocity = Vector3.zero;
            agent.ResetPath();
        }
    }
}