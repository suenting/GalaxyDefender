using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipData : MonoBehaviour {

    public enum MovementPattern
    {
        MP_PATROL,
        MP_LOOP,
        MP_ONEWAY
    }

    [SerializeField] float speed = 1f;
    [SerializeField] float maxHP = 100f;
    [SerializeField] float rawDamage = 100f;
    [SerializeField] float shotCooldown = 1f;
    [SerializeField] float shotOffset = 0f;
    [SerializeField] string basicAttack;
    [SerializeField] MovementPattern movementPattern;
    [SerializeField] string deathFx = "explosion1";

    List<Transform> waypoints;
    int currentDestination = 0;
    bool patrolFwd = true;

    public float GetSpeed() { return speed; }
    public float GetMaxHP() { return maxHP; }
    public float GetRawDamage() { return rawDamage; }
    public float GetShotCooldown() { return shotCooldown; }
    public string GetBasicAttack() { return basicAttack; }
    public string GetDeathFx() { return deathFx; }
    public MovementPattern GetMovementPattern() { return movementPattern; }

    // references
    BulletFactory pBulletFactory;

    // Use this for initialization
    void Start () {
        pBulletFactory = FindObjectOfType<BulletFactory>();

        Damage dmg = GetComponent<Damage>();
        dmg.SetDamage(rawDamage);

        Invoke("StartFire", shotOffset);
    }

	// Update is called once per frame (consider refactoring into 'EnemyShipAI')
	void Update () {
        Movement();
	}

    public void SetShotOffset(float shotOffset)
    {
        this.shotOffset = shotOffset;
    }

    public void SetWayPoints(List<Transform> waypoints, int startPoint = 0)
    {
        this.waypoints = waypoints;
        currentDestination = startPoint;
    }
    private void Movement()
    {
        if(waypoints.Count==0)
        {
            return;
        }
        switch(GetMovementPattern())
        {
            case MovementPattern.MP_PATROL:
                MovementPatrol();
                break;
        }
    }
    private void MovementPatrol()
    {
        float relativeSpeed = GetSpeed() * Time.deltaTime;
        Vector3 targetPosition = waypoints[currentDestination].transform.position;
        if (transform.position == targetPosition)
        {
            if(patrolFwd)
            {
                ++currentDestination;
                if(currentDestination>=waypoints.Count)
                {
                    --currentDestination;
                    patrolFwd = false;
                }
            }
            else if(!patrolFwd)
            {
                --currentDestination;
                if (currentDestination < 0)
                {
                    ++currentDestination;
                    patrolFwd = true;
                }
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, relativeSpeed);
    }

    private void StartFire()
    {
        StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            if ("" == GetBasicAttack())
            {
                yield return new WaitForSeconds(GetShotCooldown());
            }
            pBulletFactory.FireWeapon(GetBasicAttack(), gameObject.transform, GetRawDamage(), false);
            yield return new WaitForSeconds(GetShotCooldown());
        }
    }
}
