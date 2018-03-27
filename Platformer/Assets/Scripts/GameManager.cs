using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = System.Diagnostics.Debug;

public class GameManager : MonoBehaviour {

    //score of the player
    public int Score = 0;
    
    //high score
    public int HighScore = 0;

    //current level
    public int CurrentLevel = 1;

    //number of levels ( change accordingly)
    public int Highestlevel = 2; // in this case 2

    HudManager _hudManager;

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


            // find an object of type HudManager
            Instance._hudManager = FindObjectOfType<HudManager>();

            /*we have a static instance attribute to make sure the GameManager is always the same object.
            * This allows us to keep track of the high score, since the object does not change when the player resets the game.
            * However, when the player starts a new game, the GameManager instance must be updated to have the reference to the HudManager of this new game,
            * otherwise it can’t update the score correctly.
            * When it is the first time the Awake method is called, the instance is set to the hud GameObject.
            * Otherwise, we only save the HudManager and then delete the new Game Object (in order to keep only one Game Object of the Game Manager).*/
            
        }
        //dont destroy gameobject when changing scene
        DontDestroyOnLoad(gameObject);

        //find Hud Manager
        _hudManager = FindObjectOfType<HudManager>();
    }
    
    //increase the score
    public void IncreaseScore(int amount)
    {
        Score += amount;

        //show new score
        //print("new score: " + Score);
        //Update Hud 
        Debug.Assert(_hudManager != null, "hudManager = null");
        _hudManager.ResetHud();

        if (Score > HighScore)
        {
            HighScore = Score;
            //print("new High score: "+ HighScore);
        }
    }

    public void ResetGame()
    {
        //reset score to 0
        Score = 0;

        //Update Hud 
        Debug.Assert(_hudManager != null, "hudManager = null");
        _hudManager.ResetHud();

        //reset current level to 1
        CurrentLevel = 1;

        //load scene 1
        SceneManager.LoadScene("Level1");
    }

    //send player to next level
   public void IncreseLevel()
    {
        //check if currrent level isn't the last level
        if (CurrentLevel < Highestlevel)
        {
            CurrentLevel++;
        }
        else
        {
            //go back to level one
            CurrentLevel = 1;
        }
        SceneManager.LoadScene("Level"+CurrentLevel);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
