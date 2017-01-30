using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class PlatformerSpawner : MonoBehaviour {

    public GameObject platformPrefab;
    Vector3 location;
    public float waitTime;
    bool canSpawn;

    void Start()
    {
        location = new Vector3(-7.0f, 20.0f, 0.0f);
        canSpawn = true;
        spawnPlat();
    }

	// Update is called once per frame
	void Update ()
    {
        if (canSpawn)
        {
            StartCoroutine(waitSeconds(waitTime));
        }
    }

    IEnumerator waitSeconds(float seconds)
    {
        canSpawn = false;
        yield return new WaitForSeconds(seconds);
        spawnPlat();
        canSpawn = true;
    }

    void spawnPlat()
    {
        Vector3 spawnPos = location;
        spawnPos.x = Random.Range(-7.25f, 3.25f);
        Instantiate(platformPrefab, spawnPos, transform.rotation);
    }
}
