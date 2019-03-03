using UnityEngine;

public class QuadrantRotatorAnimated : MonoBehaviour, IQuadrantRotator
{
    [Tooltip("The animation that should be used to rotate this quadrant.")]
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
        //Debug.Log("QuadrantRotatorAnimated: Attempting animated clockwise rotation on: " + gameObject.name);
        animationApplier.ApplyTo(transform, Quaternion.Euler(0.0f, yawOffset, 0.0f), "RotateCW");
    }

    public void RotateCounterClockwise()
    {
        //Debug.Log("QuadrantRotatorAnimated: Attempting animated counterclockwise rotation on: " + gameObject.name);
        animationApplier.ApplyTo(transform, Quaternion.Euler(0.0f, yawOffset, 0.0f), "RotateCCW");
    }

    public bool IsBusyRotating()
    {
        return animationApplier.IsAnimationInProgress();
    }
}
