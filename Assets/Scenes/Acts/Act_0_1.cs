using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Linq;

public class Act_0_1 : MonoBehaviour {

    int state = 0;
    bool gameOver = false;

    ShipFactory pShipFactory;

    NavigationController pNavigationController;

	// Use this for initialization
	void Start () {
        // caching
        pShipFactory = FindObjectOfType<ShipFactory>();
        pNavigationController = FindObjectOfType<NavigationController>();
        Assert.IsNotNull(pShipFactory);
        Assert.IsNotNull(pShipFactory);

    }
	
	// Update is called once per frame
	void Update () {
        if(EnemyClear() && !gameOver)
        {
            switch (state)
            {
                case 0:
                    Wave0();
                    break;
                case 1:
                    Wave1();
                    break;
                case 2:
                    Wave2();
                    break;
                default:
                    // no more waves mission complete
                    pNavigationController.LoadScene("GameWin");
                    break;
            }
            ++state;
        }
	}

    private bool EnemyClear()
    {
        EnemyShipData[] Enemies = GameObject.FindObjectsOfType<EnemyShipData>();
        if(Enemies.Length>0)
        {
            return false;
        }
        return true;
    }
    private void SetWaypoints(GameObject ship, string waypointName, int startPoint)
    {
        if(ship == null)
        {
            return;
        }
        EnemyShipData pData = ship.GetComponent<EnemyShipData>();
        GameObject pWaypointGroup = GameObject.Find(waypointName);
        List<Transform> waypoints = pWaypointGroup.transform.Cast<Transform>().ToList();
        pData.SetWayPoints(waypoints, startPoint);
    }
    private void Wave0()
    {
        GameObject pShip;
        pShip = pShipFactory.SpawnEnemyShip("enemyBlack1", new Vector2(5, 7));
        SetWaypoints(pShip, "Patrol (0)",0);
        pShip = pShipFactory.SpawnEnemyShip("enemyBlack1", new Vector2(6, 8)); pShip.GetComponent<EnemyShipData>().SetShotOffset(0.25f);
        SetWaypoints(pShip, "Patrol (0)", 0);
        pShip = pShipFactory.SpawnEnemyShip("enemyBlack1", new Vector2(7, 9)); pShip.GetComponent<EnemyShipData>().SetShotOffset(0.5f);
        SetWaypoints(pShip, "Patrol (0)", 0);

        pShip = pShipFactory.SpawnEnemyShip("enemyBlack1", new Vector2(-5, 5));
        SetWaypoints(pShip, "Patrol (1)",1);
        pShip = pShipFactory.SpawnEnemyShip("enemyBlack1", new Vector2(-6, 6)); pShip.GetComponent<EnemyShipData>().SetShotOffset(0.25f);
        SetWaypoints(pShip, "Patrol (1)", 1);
        pShip = pShipFactory.SpawnEnemyShip("enemyBlack1", new Vector2(-7, 7)); pShip.GetComponent<EnemyShipData>().SetShotOffset(0.5f);
        SetWaypoints(pShip, "Patrol (1)", 1);

        pShip = pShipFactory.SpawnEnemyShip("enemyBlack1", new Vector2(5, 3));
        SetWaypoints(pShip, "Patrol (2)",0);
        pShip = pShipFactory.SpawnEnemyShip("enemyBlack1", new Vector2(6, 4)); pShip.GetComponent<EnemyShipData>().SetShotOffset(0.25f);
        SetWaypoints(pShip, "Patrol (2)", 0);
        pShip = pShipFactory.SpawnEnemyShip("enemyBlack1", new Vector2(7, 5)); pShip.GetComponent<EnemyShipData>().SetShotOffset(0.5f);
        SetWaypoints(pShip, "Patrol (2)", 0);
    }
    private void Wave1()
    {
        GameObject pShip;
        pShip = pShipFactory.SpawnEnemyShip("enemyBlack1", new Vector2(-5, 7));
        SetWaypoints(pShip, "Patrol (0)", 1);
        pShip = pShipFactory.SpawnEnemyShip("enemyBlack1", new Vector2(-6, 8)); pShip.GetComponent<EnemyShipData>().SetShotOffset(0.25f);
        SetWaypoints(pShip, "Patrol (0)", 1);
        pShip = pShipFactory.SpawnEnemyShip("enemyBlack1", new Vector2(-7, 9)); pShip.GetComponent<EnemyShipData>().SetShotOffset(0.5f);
        SetWaypoints(pShip, "Patrol (0)", 1);

        pShip = pShipFactory.SpawnEnemyShip("enemyRed2", new Vector2(5, 5));
        SetWaypoints(pShip, "Patrol (4)", 0);
        pShip = pShipFactory.SpawnEnemyShip("enemyRed2", new Vector2(6, 6)); pShip.GetComponent<EnemyShipData>().SetShotOffset(0.25f);
        SetWaypoints(pShip, "Patrol (4)", 0);
        pShip = pShipFactory.SpawnEnemyShip("enemyRed2", new Vector2(7, 7)); pShip.GetComponent<EnemyShipData>().SetShotOffset(0.5f);
        SetWaypoints(pShip, "Patrol (4)", 0);

        pShip = pShipFactory.SpawnEnemyShip("enemyBlack1", new Vector2(-5, 3));
        SetWaypoints(pShip, "Patrol (2)", 1);
        pShip = pShipFactory.SpawnEnemyShip("enemyBlack1", new Vector2(-6, 4)); pShip.GetComponent<EnemyShipData>().SetShotOffset(0.25f);
        SetWaypoints(pShip, "Patrol (2)", 1);
        pShip = pShipFactory.SpawnEnemyShip("enemyBlack1", new Vector2(-7, 5)); pShip.GetComponent<EnemyShipData>().SetShotOffset(0.5f);
        SetWaypoints(pShip, "Patrol (2)", 1);
    }
    private void Wave2()
    {
        GameObject pShip;
        pShip = pShipFactory.SpawnEnemyShip("enemyRed2", new Vector2(-5, 7));
        SetWaypoints(pShip, "Patrol (3)", 1);
        pShip = pShipFactory.SpawnEnemyShip("enemyRed2", new Vector2(-6, 8)); pShip.GetComponent<EnemyShipData>().SetShotOffset(0.25f);
        SetWaypoints(pShip, "Patrol (3)", 1);
        pShip = pShipFactory.SpawnEnemyShip("enemyRed2", new Vector2(-7, 9)); pShip.GetComponent<EnemyShipData>().SetShotOffset(0.5f);
        SetWaypoints(pShip, "Patrol (3)", 1);

        pShip = pShipFactory.SpawnEnemyShip("enemyBlack1", new Vector2(5, 5));
        SetWaypoints(pShip, "Patrol (4)", 0);
        pShip = pShipFactory.SpawnEnemyShip("enemyBlack1", new Vector2(6, 6)); pShip.GetComponent<EnemyShipData>().SetShotOffset(0.25f);
        SetWaypoints(pShip, "Patrol (4)", 0);
        pShip = pShipFactory.SpawnEnemyShip("enemyBlack1", new Vector2(7, 7)); pShip.GetComponent<EnemyShipData>().SetShotOffset(0.5f);
        SetWaypoints(pShip, "Patrol (4)", 0);

        pShip = pShipFactory.SpawnEnemyShip("enemyRed2", new Vector2(-5, 3));
        SetWaypoints(pShip, "Patrol (5)", 1);
        pShip = pShipFactory.SpawnEnemyShip("enemyRed2", new Vector2(-6, 4)); pShip.GetComponent<EnemyShipData>().SetShotOffset(0.25f);
        SetWaypoints(pShip, "Patrol (5)", 1);
        pShip = pShipFactory.SpawnEnemyShip("enemyRed2", new Vector2(-7, 5)); pShip.GetComponent<EnemyShipData>().SetShotOffset(0.5f);
        SetWaypoints(pShip, "Patrol (5)", 1);
    }
}
