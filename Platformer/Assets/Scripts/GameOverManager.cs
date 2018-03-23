using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

    //access the score value
    public Text ScoreValue;

    //access the high score value
    public Text HighScoreValue;

	// Use this for initialization
	void Start () {
        //set "text" of score value
	    ScoreValue.text = GameManager.Instance.Score.ToString();

	    //set "text" of high score value
	    HighScoreValue.text = GameManager.Instance.HighScore.ToString();
	}

   //it will send the player to level 1
    public void RestartGame()
    {
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene("Level1");
    }
}
