using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData : MonoBehaviour {

    [SerializeField] float speed = 1f;
    [SerializeField] float maxHP = 100f;
    [SerializeField] float rawDamage = 100f;
    [SerializeField] float basicShotCooldown = 1f;
    [SerializeField] string basicAttack;
    [SerializeField] string deathFx = "explosion1";

    public float GetSpeed() { return speed; }
    public float GetMaxHP() { return maxHP; }
    public float GetRawDamage() { return rawDamage; }
    public float GetBasicShotCooldown() { return basicShotCooldown; }
    public string GetBasicAttack() { return basicAttack; }
    public string GetDeathFx() { return deathFx; }

}
