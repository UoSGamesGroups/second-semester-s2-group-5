using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public List<string> levels;
    public int iteration;

    public int currentLevel;
    public int playerOneRounds;
    public int playerTwoRounds;

    public int prevScoreOne;
    public int prevScoreTwo;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        iteration = 0;
        levels = new List<string>();

        playerOneRounds = 0;
        playerTwoRounds = 0;

        currentLevel = -1;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void scores(int playerOne, int playerTwo)
    {
        prevScoreOne = playerOne;
        prevScoreTwo = playerTwo;

        if (playerOne > playerTwo)
        {
            playerOneRounds++;
        } else if(playerTwo > playerOne)
        {
            playerTwoRounds++;
        } else
        {
            playerOneRounds++;
            playerTwoRounds++;
        }
    }

    public void endLevel()
    {
        SceneManager.LoadScene("PauseScene");
    }

    public void loadLevel()
    {
        currentLevel++;
        //load next level
        if (currentLevel < levels.Count)
        {
            SceneManager.LoadScene(levels[currentLevel]);
        } else
        {
            SceneManager.LoadScene("MainMenu");
            Destroy(this.gameObject);
        }
    }

    public void pushLevel(string selected)
    {
        if(iteration < 3)
        {
            levels.Add(selected);
            iteration++;
        }
    }

    public void removeLevel(string selected)
    {
        if (iteration < 0)
        {
            levels.Remove(selected);
            iteration--;
        }
    }
}
