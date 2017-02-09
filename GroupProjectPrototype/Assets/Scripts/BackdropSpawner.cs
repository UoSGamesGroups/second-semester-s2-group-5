using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackdropSpawner : MonoBehaviour
{

    public GameObject backdropPrefab;
    public Vector3 location;
    public float waitTime;
    bool canSpawn;

    void Start()
    {
        canSpawn = true;
        spawnPlat();
    }

    // Update is called once per frame
    void Update()
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
        Instantiate(backdropPrefab, location, backdropPrefab.transform.rotation);
    }
}
