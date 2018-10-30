using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

    float mDamage=100;
    bool mDieOnHit=true;

    public void SetDamage(float damage)
    {
        this.mDamage = damage;
    }

    public void Hit()
    {
        Health health = gameObject.GetComponent<Health>();
        if(health)
        {
            health.Die();
            return;
        }
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return mDamage;
    }
}
