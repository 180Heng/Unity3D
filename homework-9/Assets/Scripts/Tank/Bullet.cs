using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private tankType type;
    void OnCollisionEnter(Collision other)
    {
        Factory mF = Singleton<Factory>.Instance;
        ParticleSystem explosion = mF.getPs();
        explosion.transform.position = transform.position;
        explosion.Play();
        if (this.gameObject.activeSelf)
        {
            mF.recycleBullet(this.gameObject);
        }

    }

    public void setTankType(tankType type)
    {
        this.type = type;
    }
}