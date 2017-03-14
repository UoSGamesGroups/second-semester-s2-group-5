using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour {

    public GameObject textPrefab;
    public int scoreGain;

    private GameObject lvlcontrol;
    private bool running = false;
    private Color color;

    private int hit;

    // Use this for initialization
    void Start () {
        lvlcontrol = GameObject.FindGameObjectWithTag("LevelController");

        if (GetComponent<CollisionScript>() != null)
        {
            hit = GetComponent<CollisionScript>().hit;
        } else if(GetComponent<PlayerScript>() != null){
            hit = GetComponent<PlayerScript>().player;
        }
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
        hit = getPlayerId();
        setColor();
        lvlcontrol.GetComponent<LevelController>().addScore(hit, scoreGain);

        GameObject textSpawn = Instantiate(textPrefab, new Vector3(transform.position.x, -8f, transform.position.z), Quaternion.Euler(0.0f, 0.0f, Random.Range(-25.0f, 25.0f)));
        textSpawn.GetComponent<TextMesh>().text = "+" + scoreGain;
        textSpawn.GetComponent<TextMesh>().color = color;

        yield return new WaitForSeconds(waitSeconds);
        Destroy(this.gameObject);
    }

    void setColor()
    {
        switch (hit)
        {
            case 1:
                color = new Color32(245,193,125,255);
                break;
            case 2:
                color = new Color32(146,99,206,255);
                break;
            default:
                color = Color.white;
                break;
        }
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