using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRotatorAnimated : MonoBehaviour, IBoardRotator
{
    [Tooltip("The animator object used to animate this object")]
    public Animator animation;

    void Awake()
    {
        Debug.Assert(animation);
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            RotateCounterClockwise();
	}

    public void RotateClockwise()
    {
        Debug.Log("Attempting animated clockwise rotation on: " + gameObject.name);
    }

    public void RotateCounterClockwise()
    {
        Debug.Log("Attempting animated counterclockwise rotation on: " + gameObject.name);
    }
}
