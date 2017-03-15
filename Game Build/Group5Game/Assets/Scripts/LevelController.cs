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

    public float roundTime;
    private bool checkScore = true;
    public bool startSpawn = false;
    public float scoreUpdate = 3.0f;

    public int currentPlayer;

    public GameController gameController;
    public float round = 0;
    public float totalsRounds = 10;

    // Use this for initialization
    void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        scorePlayerOne = 0;
        scorePlayerTwo = 0;
        scoreCubes=  GameObject.FindGameObjectsWithTag("Score");
        //currentPlayer = 1;

        StartCoroutine(spawnAfter(1.0f));
    }
	
	// Update is called once per frame
	void Update () {
        if (checkScore)
        {
           StartCoroutine(updateScore(scoreUpdate));
           checkScore = false;
        }

        levelCheck();
    }

    void levelCheck()
    {
        if (round >= totalsRounds)
        {
            gameController.scores(scorePlayerOne, scorePlayerTwo);
            gameController.endLevel();
        }

        if (scoreCubes.Length <= 0)
        {
            gameController.scores(scorePlayerOne, scorePlayerTwo);
            gameController.endLevel();
        }
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

    // Coroutine to spawn player after n (roundTime) seconds
    public IEnumerator spawnAfter(float waitSeconds)
    { 
        yield return new WaitForSeconds(waitSeconds);

        switch (currentPlayer)
        {
            case 1:
                //spawn player2;
                Instantiate(playerTwo, spawnLocation, playerTwo.transform.rotation);
                currentPlayer = playerTwo.GetComponent<PlayerScript>().player;
                break;
            case 2:
                //spawn player1;
                Instantiate(playerOne, spawnLocation, playerOne.transform.rotation);
                currentPlayer = playerOne.GetComponent<PlayerScript>().player;
                break;
            default:
                Debug.Log("Player Id Error");
                break;
        }
        updateScoreCubes();
        resetTag();
        round += 0.5f;
    }

    public void addScore(int player, int amount)
    {
        switch (player)
        {
            case 1:
                // player 1 score
                scorePlayerOne += amount;
                break;
            case 2:
                // player 2 score
                scorePlayerTwo += amount;
                break;
            default:
                Debug.Log("Player Id Error");
                break;
        }
        TextPlayerOne.text = scorePlayerOne.ToString();
        TextPlayerTwo.text = scorePlayerTwo.ToString();
    }

    // Coroutine to check score every n (scoreUpdate) seconds
    IEnumerator updateScore(float scoreUpdate)
    {
        /*scorePlayerTwo = 0;
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
        */
        TextPlayerOne.text = scorePlayerOne.ToString();
        TextPlayerTwo.text = scorePlayerTwo.ToString();
        Debug.Log("score updated");
        yield return new WaitForSeconds(scoreUpdate);
        checkScore = true;
    }
}
