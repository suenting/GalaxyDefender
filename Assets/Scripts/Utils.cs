using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    private const int cFriendlyLayerId = 8;
    private const int cHostileLayerId = 9;

    public static void SetFriendly(GameObject gameObject)
    {
        gameObject.layer = cFriendlyLayerId;
    }

    public static void SetHostile(GameObject gameObject)
    {
        gameObject.layer = cHostileLayerId;
    }
}
