using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundEndScript : MonoBehaviour {

    private GameController gameController;

    public GameObject playerOneScoreText;
    public GameObject playerTwoScoreText;
    public GameObject playerText;
    public GameObject headerObject;
    public GameObject nextLevelButton;

    private Text playerOneText;
    private Text playerTwoText;
    private Text playerWinText;

    private Text winText;

    private bool canUpdate;
    private int iterator;
    private float updateTime;

    //put in for speed over memory
    private int maxScore;

    // Use this for initialization
    void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        headerObject.GetComponent<Text>().text = "Round " + (gameController.currentLevel+1) + " Over!";
        playerOneText = playerOneScoreText.GetComponent<Text>();
        playerTwoText = playerTwoScoreText.GetComponent<Text>();
        playerWinText = playerText.GetComponent<Text>();

        playerOneText.text = "0";
        playerTwoText.text = "0";

        updateTime = 0.1f;
        iterator = 0;
        canUpdate = true;

        //needed to know which player won.
        //maxScore = System.Math.Max(gameController.prevScoreOne, gameController.prevScoreTwo);
        if(gameController.prevScoreOne > gameController.prevScoreTwo)
        {
            playerWinText.text = "Player 1 Wins!";
            maxScore = gameController.prevScoreOne;
        } else if(gameController.prevScoreTwo > gameController.prevScoreOne)
        {
            playerWinText.text = "Player 2 Wins!";
            maxScore = gameController.prevScoreTwo;
        } else
        {
            playerWinText.text = "Draw, Everybody Wins!";
            maxScore = gameController.prevScoreTwo;
        }
    }

    public void nextLevel()
    {
        gameController.loadLevel();
    }
	
	// Update is called once per frame
	void Update () {
        if (canUpdate)
        {
            canUpdate = false;
            StartCoroutine(updateText(updateTime));
        }
    }

    public IEnumerator updateText(float waitSeconds)
    {
        iterator++;
        updateTime += 0.01f;

        if (gameController.prevScoreOne >= iterator)
        { 
            playerOneText.text = iterator+ "";
        }

        if(gameController.prevScoreTwo >= iterator)
        {
            playerTwoText.text = iterator + "";
        }

        yield return new WaitForSeconds(waitSeconds);

        if(maxScore == iterator)
        {
            playerWinText.enabled = true;
            canUpdate = false;
            nextLevelButton.GetComponent<Button>().interactable = true;
        } else
        {
            canUpdate = true;
        }
    }
}
