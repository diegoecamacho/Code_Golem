using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour {
    [SerializeField] GameObject SpawnObjectPrefab;
    [SerializeField] Transform EndPosition;
    [SerializeField] float TimeBetweenSpawns;

    float timeElapsed = 0;
    bool isActive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= TimeBetweenSpawns)
        {
            //Instantiate();

        }
		
	}
}
