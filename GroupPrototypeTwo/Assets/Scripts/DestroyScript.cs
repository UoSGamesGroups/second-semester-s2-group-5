using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour {

    public GameObject textPrefab;
    public int scoreGain;

    private GameObject lvlcontrol;
    private bool running = false;


    // Use this for initialization
    void Start () {
        lvlcontrol = GameObject.FindGameObjectWithTag("LevelController");
    }
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.y < -8.5f && !running)
        {
            StartCoroutine(destroyAfter(0.0f));
            running = true;
        }
    }

    IEnumerator destroyAfter(float waitSeconds)
    {
        GameObject textSpawn = Instantiate(textPrefab, new Vector3(transform.position.x, -8f, transform.position.z), Quaternion.Euler(0.0f, 0.0f, Random.Range(-25.0f, 25.0f)));
        textSpawn.GetComponent<TextMesh>().text = "+" + scoreGain;
        textSpawn.GetComponent<TextMesh>().color = GetComponent<Renderer>().material.color;

        lvlcontrol.GetComponent<LevelController>().addScore(getPlayerId(), scoreGain);

        yield return new WaitForSeconds(waitSeconds);
        Destroy(this.gameObject);
    }

    int getPlayerId()
    {
        if (GetComponent<CollisionScript>() != null)
        {
            return GetComponent<CollisionScript>().hit;
        }
        else
        {
            StartCoroutine(lvlcontrol.GetComponent<LevelController>().spawnAfter(0.0f));
            return GetComponent<PlayerScript>().player;
        }
    }
}