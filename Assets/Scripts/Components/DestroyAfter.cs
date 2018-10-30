using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour {

    [SerializeField] float timeToLiveSeconds = 0;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, timeToLiveSeconds);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
