using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class PlatformerSpawner : MonoBehaviour {

    public GameObject platformPrefab;
    public Vector3 location;
    public float waitTime;
    bool canSpawn;

    void Start()
    {
        //location = new Vector3(-7.0f, 20.0f, 0.0f);
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
        spawnPos.x = Random.Range(location.x, location.x+5.00f);
        Instantiate(platformPrefab, spawnPos, transform.rotation);
    }
}
