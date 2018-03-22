using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HudManager : MonoBehaviour
{

    public Text ScoreLable;

	// Use this for initialization
    void Start()
    {
        ResetHud();
    }

    public void ResetHud()
    {
        ScoreLable.text = "Score: " + GameManager.Instance.Score;
    }
    
	
}
