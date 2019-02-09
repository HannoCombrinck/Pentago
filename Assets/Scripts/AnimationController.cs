using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [Tooltip("The transform/gameobject that is animated and drives the rotation of the boards.")]
    public Transform animatedTransform; 
    [Tooltip("This depends on the angle at which the original animation was authored.")]
    public float animationAngleOffset = -135.0f;
    [Tooltip("The transforms/gameobjects of the top left board.")]
    public Transform topLeftBoard;
    [Tooltip("The transforms/gameobjects of the top right board.")]
    public Transform topRightBoard;
    [Tooltip("The transforms/gameobjects of the bottom left board.")]
    public Transform bottomLeftBoard;           
    [Tooltip("The transforms/gameobjects of the bottom right board.")]
    public Transform bottomRightBoard;

    public enum RotateDirection
    {
        LEFT,
        RIGHT
    }

    private Transform boardBeingAnimated;
    private Transform previousParent;
    private Vector3 previousPosition;
    private Vector3 previousScale;

    Animator anim;
    bool rotating = false;

    void Start ()
    {
        anim = gameObject.GetComponent<Animator>();
	}

    void Update()
    {
        // Keyboard shortcuts for testing
        if (Input.GetKeyDown("1") && !rotating)
            Rotate(topLeftBoard, RotateDirection.LEFT);
        if (Input.GetKeyDown("2") && !rotating)
            Rotate(topLeftBoard, RotateDirection.RIGHT);

        if (Input.GetKeyDown("3") && !rotating)
            Rotate(topRightBoard, RotateDirection.LEFT);
        if (Input.GetKeyDown("4") && !rotating)
            Rotate(topRightBoard, RotateDirection.RIGHT);

        if (Input.GetKeyDown("5") && !rotating)
            Rotate(bottomLeftBoard, RotateDirection.LEFT);
        if (Input.GetKeyDown("6") && !rotating)
            Rotate(bottomLeftBoard, RotateDirection.RIGHT);

        if (Input.GetKeyDown("7") && !rotating)
            Rotate(bottomRightBoard, RotateDirection.LEFT);
        if (Input.GetKeyDown("8") && !rotating)
            Rotate(bottomRightBoard, RotateDirection.RIGHT);
    }

    public void Rotate(Transform boardToAnimate, RotateDirection direction)
    {
        var lookTarget = new Vector3(boardToAnimate.position.x, 0, boardToAnimate.position.z);
        gameObject.transform.rotation = Quaternion.LookRotation(lookTarget) * Quaternion.Euler(0.0f, animationAngleOffset, 0.0f);
        gameObject.transform.position = boardToAnimate.transform.position;
        
        AttachTransformToAnimate(boardToAnimate);

        anim.SetTrigger(direction == RotateDirection.LEFT ? "CCW" : "CW");
        rotating = true;

        Debug.Log("Rotating!");
    }

    void OnRotateFinished()
    {
        DetachTransformToAnimate(boardBeingAnimated);

        anim.ResetTrigger("CW");
        anim.ResetTrigger("CCW");
        anim.SetTrigger("Reset");
        rotating = false;

        Debug.Log("Rotate finished!");
    }

    private void AttachTransformToAnimate(Transform t)
    {
        boardBeingAnimated = t;
        previousParent = t.parent;
        previousPosition = t.localPosition;
        previousScale = t.localScale;
        t.SetParent(animatedTransform);
    }

    private void DetachTransformToAnimate(Transform t)
    {
        t.SetParent(previousParent);
        t.localPosition = previousPosition;
        t.localScale = previousScale;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;

        //var directionBone = GameObject.Find("DirectionBone");

        /*var iconPos = animParent.position - animParent.transform.forward + animParent.transform.up;
        Gizmos.DrawIcon(iconPos + Vector3.up, "TopLeft.tiff", true);
        Gizmos.DrawIcon(Quaternion.AngleAxis(90.0f, Vector3.up) * iconPos + Vector3.up, "TopRight.tiff", true);
        Gizmos.DrawIcon(Quaternion.AngleAxis(180.0f, Vector3.up) * iconPos + Vector3.up, "BottomRight.tiff", true);
        Gizmos.DrawIcon(Quaternion.AngleAxis(270.0f, Vector3.up) * iconPos + Vector3.up, "BottomLeft.tiff", true);*/

        /*Gizmos.DrawIcon(animParent.position + Vector3.up, "TopLeft.tiff", true);
        Gizmos.DrawIcon(Quaternion.AngleAxis(90.0f, Vector3.up) * animParent.position + Vector3.up, "TopRight.tiff", true);
        Gizmos.DrawIcon(Quaternion.AngleAxis(180.0f, Vector3.up) * animParent.position + Vector3.up, "BottomRight.tiff", true);
        Gizmos.DrawIcon(Quaternion.AngleAxis(270.0f, Vector3.up) * animParent.position + Vector3.up, "BottomLeft.tiff", true);*/
    }
}
