using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRotatorAnimated : MonoBehaviour, IBoardRotator
{
    [Tooltip("The animation that should be used to rotate this board.")]
    public AnimationApplier animationApplier;
    [Tooltip("Yaw rotation offset to apply to animation (degrees).")]
    public float yawOffset = 0.0f;

    void Awake()
    {
        Debug.Assert(animationApplier);
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            RotateCounterClockwise();
	}

    public void RotateClockwise()
    {
        //Debug.Log("BoardRotatorAnimated: Attempting animated clockwise rotation on: " + gameObject.name);
        animationApplier.ApplyTo(transform, Quaternion.Euler(0.0f, yawOffset, 0.0f), "RotateCW");
    }

    public void RotateCounterClockwise()
    {
        //Debug.Log("BoardRotatorAnimated: Attempting animated counterclockwise rotation on: " + gameObject.name);
        animationApplier.ApplyTo(transform, Quaternion.Euler(0.0f, yawOffset, 0.0f), "RotateCCW");
    }

    public bool IsBusyRotating()
    {
        return animationApplier.IsAnimationInProgress();
    }
}
