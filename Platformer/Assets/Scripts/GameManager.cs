using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //score of the player
    public int Score = 0;
    
    //high score
    public int HighScore = 0;

    //Static instance of the game manger can be accessed from anywhere
    public static GameManager Instance;

    void Awake()
    {
        //check if instace is assigned
        if (Instance == null)
        {
            //assign  it to the current game object
            Instance = this;
        }
        //make sure it is equal to the current game object
        else if (Instance != this)
        {
            //destroy the current game object, need only one 
            Destroy(gameObject);
        }
        //dont destroy gameobject when changing scene
        DontDestroyOnLoad(gameObject);

    }
    
    //increase the score
    public void IncreaseScore(int amount)
    {
        Score += amount;

        //show new score
        print("new score: " + Score);

        if (Score > HighScore)
        {
            HighScore = Score;
            print("new High score: "+ HighScore);
        }
    }
}
