using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SceneController : MonoBehaviour, IUserAction
{
    public GameObject player;
    private bool gameOver = false;

    private int enemyConut = 5;
    private Factory mF;


    void Awake()
    {
        GameDirector director = GameDirector.getInstance();
        director.currentSceneController = this;
        mF = Singleton<Factory>.Instance;
        player = mF.getPlayer();
    }
    void Start()
    {
        for (int i = 0; i < enemyConut; i++)
        {
            mF.getTank();
        }
        Player.destroyEvent += setGameOver;
    }

    void Update()
    {
        Camera.main.transform.position = new Vector3(player.transform.position.x, 100, player.transform.position.z);
    }

    public Vector3 getPlayerPos()
    {
        return player.transform.position;
    }

    public bool isGameOver()
    {
        return gameOver;
    }
    public void setGameOver()
    {
        gameOver = true;
    }

    public void moveForward()
    {
        player.GetComponent<Rigidbody>().velocity = player.transform.forward * 20;
    }
    public void moveBackWard()
    {
        player.GetComponent<Rigidbody>().velocity = player.transform.forward * -20;
    }
    public void turn(float offsetX)
    {
        float x = player.transform.localEulerAngles.y + offsetX * 5;
        float y = player.transform.localEulerAngles.x;
        player.transform.localEulerAngles = new Vector3(y, x, 0);
    }

    public void shoot()
    {
        GameObject bullet = mF.getBullet(tankType.Player);
        bullet.transform.position = new Vector3(player.transform.position.x, 8.5f, player.transform.position.z) + player.transform.forward * 1.5f;
        bullet.transform.forward = player.transform.forward;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bullet.transform.forward * 20, ForceMode.Impulse);
    }

}