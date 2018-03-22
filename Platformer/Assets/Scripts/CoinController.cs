using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    public float RotationSpeed = 100;
    
	// Update is called once per frame
	void Update () {
		//angle of rotation v = d/t i.e d = v * t
	    float angleRot = RotationSpeed * Time.deltaTime;

        //rotate coin
        transform.Rotate(Vector3.up * angleRot , Space.World);
	}
}
