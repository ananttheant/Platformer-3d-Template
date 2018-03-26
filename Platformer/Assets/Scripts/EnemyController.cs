using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed = 3;
    private int _direction = 1;
    public float RangeY = 2; //y

    private Vector3 _initialLocation;
	// Use this for initialization
	void Start ()
	{
	    _initialLocation = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
        //factor by which it should move
	    float factor = _direction == -1 ? 1.2f : 1;

        //speed + direction
	    float movementY = factor * Speed * Time.deltaTime * _direction;

        //new position in that frame after adding movement(i.e speed and direction covered in that frame)
	    float newY = transform.position.y + movementY;

        //abs = mod 
	    if (Mathf.Abs(newY - _initialLocation.y) > RangeY)
	    {
	        _direction *= -1;
	    }
        //if we can move further , move further
	    else
	    {
            transform.position += new Vector3(0,movementY,0);
	    }

	}
}
