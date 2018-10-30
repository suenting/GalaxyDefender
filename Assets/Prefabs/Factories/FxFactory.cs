using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxFactory : MonoBehaviour {

    [SerializeField] GameObject[] ExplosionList;


    private GameObject fxContainer;

    // Use this for initialization
    void Start () {
        fxContainer = GameObject.Find("FxContainer");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject SpawnExplosion(string name, Vector2 position)
    {
        foreach (GameObject obj in ExplosionList)
        {
            if (obj.name != name)
            {
                continue;
            }
            GameObject explosion = (GameObject)Instantiate(obj, position, Quaternion.identity);
            explosion.transform.parent = fxContainer.transform;
            return explosion;
        }
        return null;
    }
}
