using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFactory : MonoBehaviour {

    [SerializeField] GameObject[] PrefabPlayerShipList;
    [SerializeField] GameObject[] PrefabEnemyShipList;

    private GameObject enemyContainer;

    private void Start()
    {
        enemyContainer = GameObject.Find("EnemyContainer");
    }

    public GameObject SpawnPlayerShip(string name, Transform transform)
    {
        foreach(GameObject obj in PrefabPlayerShipList)
        {
            if(obj.name != name)
            {
                continue;
            }
            GameObject ship = (GameObject)Instantiate(obj, transform.position,Quaternion.identity);
            Utils.SetFriendly(ship);
            ShipData pShipData = ship.GetComponent<ShipData>();
            Health pShipHP = ship.GetComponent<Health>();
            pShipHP.SetHP(pShipData.GetMaxHP());

            return ship;
        }
        return null;
    }
    public GameObject SpawnEnemyShip(string name, Vector2 position)
    {
        foreach (GameObject obj in PrefabEnemyShipList)
        {
            if (obj.name != name)
            {
                continue;
            }
            GameObject ship = (GameObject)Instantiate(obj, position, Quaternion.identity);
            ship.transform.parent = enemyContainer.transform;
            Utils.SetHostile(ship);
            EnemyShipData pShipData = ship.GetComponent<EnemyShipData>();
            Health pShipHP = ship.GetComponent<Health>();
            pShipHP.SetHP(pShipData.GetMaxHP());
            return ship;
        }
        return null;
    }
}
