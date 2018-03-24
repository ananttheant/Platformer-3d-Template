using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;


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
        if (ScoreLable != null) ScoreLable.text = "Score: " + GameManager.Instance.Score;
    }
    
	
}
