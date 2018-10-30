using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    // list of all playable ships
    [SerializeField] GameObject[] PrefabShipList;
    [SerializeField] Slider HpSlider;

    // cached variables
    Vector2 minBounds;
    Vector2 maxBounds;
    ShipFactory pShipFactory;
    BulletFactory pBulletFactory;

    // gameplay variables
    List<GameObject> mShipList = new List<GameObject>();
    int mCurrentShipIdx = 0;

    NavigationController pNavigationController;

    // Use this for initialization
    void Start () {
        // caching
        pShipFactory = FindObjectOfType<ShipFactory>();
        pBulletFactory = FindObjectOfType<BulletFactory>();
        pNavigationController = FindObjectOfType<NavigationController>();
        Assert.IsNotNull(pShipFactory);
        Assert.IsNotNull(pBulletFactory);
        Assert.IsNotNull(pNavigationController);

        // startup
        InitializeBounds();
        CreatePlayerShips();

        StartCoroutine(Fire());
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        UpdateHpUI();
    }

    public Vector2 GetPlayerPosition()
    {
        return mShipList[mCurrentShipIdx].transform.position;
    }

    public void KillCurrentShip()
    {
        Destroy(mShipList[mCurrentShipIdx]);
        mShipList.RemoveAt(mCurrentShipIdx);
        mCurrentShipIdx = 0;

        // end game as lose (only 1 ship)
        pNavigationController.LoadScene("GameLose");
    }

    private void UpdateHpUI()
    {
        
        if (mShipList.Count <= 0)
        {
            HpSlider.value = 0;
            return;
        }

        Health hp = mShipList[mCurrentShipIdx].GetComponent<Health>();
        HpSlider.value = hp.GetHpPercentage();
    }

    private void CreatePlayerShips()
    {
        GameObject ship = pShipFactory.SpawnPlayerShip("Ship1_blue", transform);
        mShipList.Add(ship);
    }

    private void InitializeBounds()
    {
        Camera gameCamera = Camera.main;
        minBounds = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)) + new Vector3(0.5f, 0.5f, 0);
        maxBounds = gameCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)) - new Vector3(0.5f, 5.5f, 0);
    }

    private void Move()
    {
        if(mShipList.Count<=0)
        {
            return;
        }
        Vector3 deltaPosition = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        deltaPosition.Normalize();
        deltaPosition *= Time.deltaTime;
        
        Vector3 newPosition = mShipList[mCurrentShipIdx].transform.position + deltaPosition * mShipList[mCurrentShipIdx].GetComponent<ShipData>().GetSpeed();
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);


        mShipList[mCurrentShipIdx].transform.position = new Vector2(newPosition.x, newPosition.y);
    }
    private IEnumerator Fire()
    {
        while (true)
        {
            if (mShipList.Count <= 0)
            {
                break;
            }
            ShipData pShipData = mShipList[mCurrentShipIdx].GetComponent<ShipData>();
            if(""==pShipData.GetBasicAttack())
            {
                yield return new WaitForSeconds(pShipData.GetBasicShotCooldown());
            }
            pBulletFactory.FireWeapon(pShipData.GetBasicAttack(), mShipList[mCurrentShipIdx].transform, pShipData.GetRawDamage(), true);
            yield return new WaitForSeconds(pShipData.GetBasicShotCooldown());
        }
    }
}
