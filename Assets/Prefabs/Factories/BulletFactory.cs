using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BulletFactory : MonoBehaviour {

    [SerializeField] GameObject[] BulletList;

    const float bulletSpeed = 7f;

    private GameObject enemyBullets;
    private GameObject playerBullets;

    private PlayerController playerController;

    // Use this for initialization
    void Start () {
        enemyBullets = GameObject.Find("EnemyBullets");
        playerBullets = GameObject.Find("PlayerBullets");
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FireWeapon(string name, Transform origin, float rawDamage, bool isPlayer)
    {
        switch(name)
        {
            case "laserblue01":
            case "laserred06":
                FireLaserBasic(origin, rawDamage, isPlayer, name);
                break;
            case "laserred10":
                FireLaserAtPlayer(origin, rawDamage, name);
                break;
        }
    }

    private GameObject FindBulletByName(string name)
    {
        foreach (GameObject obj in BulletList)
        {
            if(string.Compare(obj.name,name,true)==0)
            {
                return obj;
            }
        }
        return null;
    }

    // a simple basic attack no damage scaling
    private void FireLaserBasic(Transform origin, float rawDamage, bool isPlayer, string name)
    {
        
        GameObject bulletPreab= FindBulletByName(name);
        Assert.IsNotNull(bulletPreab);
        GameObject bullet = Instantiate(bulletPreab, origin.position, Quaternion.identity);
        Damage dmg = bullet.GetComponent<Damage>();
        dmg.SetDamage(rawDamage);
        if(isPlayer)
        {
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
            Utils.SetFriendly(bullet);
            bullet.transform.parent = playerBullets.transform;
        }
        else
        {
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1*bulletSpeed);
            bullet.transform.Rotate(180, 0, 0);
            Utils.SetHostile(bullet);
            bullet.transform.parent = enemyBullets.transform;
        }
    }

    private void FireLaserAtPlayer(Transform origin, float rawDamage, string name)
    {
        GameObject bulletPreab = FindBulletByName(name);
        Assert.IsNotNull(bulletPreab);
        GameObject bullet = Instantiate(bulletPreab, origin.position, Quaternion.identity);
        Damage dmg = bullet.GetComponent<Damage>();
        dmg.SetDamage(rawDamage);

        Vector2 playerPosition = playerController.GetPlayerPosition();
        Vector2 direction = (playerPosition-(Vector2)origin.position);
        direction.Normalize();
        direction = direction * bulletSpeed;
        bullet.GetComponent<Rigidbody2D>().velocity = direction;

        Utils.SetHostile(bullet);
        bullet.transform.parent = enemyBullets.transform;
    }

}
