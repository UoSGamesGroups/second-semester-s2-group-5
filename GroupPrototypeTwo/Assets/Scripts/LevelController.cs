using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

    public GameObject playerOne;
    public GameObject playerTwo;
    public Vector3 spawnLocation;

    public GameObject[] scoreCubes;

    public int scorePlayerOne;
    public int scorePlayerTwo;

    public Text TextPlayerOne;
    public Text TextPlayerTwo;

    // Use this for initialization
    void Start () {
        scorePlayerOne = 0;
        scorePlayerTwo = 0;
        scoreCubes=  GameObject.FindGameObjectsWithTag("Score");
	}
	
	// Update is called once per frame
	void Update () {
        scoreCubes = GameObject.FindGameObjectsWithTag("Score");
        getScore(); 
    }

    public void SpawnPlayer(int prevPlayer)
    {
        switch (prevPlayer)
        {
            case 1:
                //spawn player2;
                Instantiate(playerTwo, spawnLocation, playerTwo.transform.rotation);
                break;
            case 2:
                //spawn player1;
                Instantiate(playerOne, spawnLocation, playerOne.transform.rotation);
                break;
            default:
                Debug.Log("Player Id Error");
                break;           
        }
        updateScoreCubes();
        resetTag();
    }

    void updateScoreCubes()
    {
        scoreCubes = GameObject.FindGameObjectsWithTag("Score");
    }

    void resetTag()
    {
        for(int i = 0; i < scoreCubes.Length; i++)
        {
            scoreCubes[i].GetComponent<CollisionScript>().changed = false;
        }
    }

    void getScore()
    {
        scorePlayerTwo = 0;
        scorePlayerOne = 0;

        for (int i = 0; i < scoreCubes.Length; i++)
        {
            if(scoreCubes[i].GetComponent<CollisionScript>().hit == 1)
            {
                scorePlayerOne++;
            } else if(scoreCubes[i].GetComponent<CollisionScript>().hit == 2)
            {
                scorePlayerTwo++;
            }
        }
        TextPlayerOne.text = scorePlayerOne.ToString();
        TextPlayerTwo.text = scorePlayerTwo.ToString();
    }
}
