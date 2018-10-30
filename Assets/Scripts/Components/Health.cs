using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    float maxHp = 100;
    float hp = 100;


    

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetHP(float hp)
    {
        this.maxHp = hp;
        this.hp = hp;
    }

    public float GetHpPercentage()
    {
        return hp / maxHp;
    }

    public void Die()
    {
        // display deathFx if present
        string deathFx = "";
        var pShipData = gameObject.GetComponent<ShipData>();
        var pEnemyShipData = gameObject.GetComponent<EnemyShipData>();
        if (pShipData)
        {
            deathFx = pShipData.GetDeathFx();
        }
        if (pEnemyShipData)
        {
            deathFx = pEnemyShipData.GetDeathFx();
        }
        if ("" != deathFx)
        {
            FxFactory FxFactory = FindObjectOfType<FxFactory>();
            FxFactory.SpawnExplosion(deathFx, gameObject.transform.position);
        }

        if (pShipData)
        {
            // send message to player controller that you've died
            PlayerController playerController = FindObjectOfType<PlayerController>();
            playerController.KillCurrentShip();
        }
        if (pEnemyShipData)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage dmg = collision.gameObject.GetComponent<Damage>();
        if (!dmg)
        {
            return;
        }

        hp -= dmg.GetDamage();
        dmg.Hit();
        if (hp <= 0)
        {
            Die();
        }
    }
}
