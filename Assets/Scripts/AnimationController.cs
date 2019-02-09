using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    public Transform animatedTransform; // The transform/gameobject that is animated and drives the rotation of the boards
    public float animationAngleOffset = -135.0f; // This depends on the angle at which the original animation was authored 
    public Transform[] boardsToAnimate = new Transform[4]; // The transforms/gameobjects that represent the sub boards that can be rotated


    //private Vector3 previousAnimatorPosition;
    //private Quaternion previousAnimatorRotation;

    private Transform boardToAnimate;

    private Transform previousParent;
    private Vector3 previousPosition;
    private Vector3 previousScale;

    Animator anim;
    bool rotating = false;

    void Start ()
    {
        anim = gameObject.GetComponent<Animator>();
        Debug.Assert(boardsToAnimate.Length == 4);
	}

    void Update()
    {
        if (Input.GetKeyDown("1") && !rotating)
            Rotate(0, Pentago.RotateDirection.LEFT);
        if (Input.GetKeyDown("2") && !rotating)
            Rotate(0, Pentago.RotateDirection.RIGHT);

        if (Input.GetKeyDown("3") && !rotating)
            Rotate(1, Pentago.RotateDirection.LEFT);
        if (Input.GetKeyDown("4") && !rotating)
            Rotate(1, Pentago.RotateDirection.RIGHT);

        if (Input.GetKeyDown("5") && !rotating)
            Rotate(2, Pentago.RotateDirection.LEFT);
        if (Input.GetKeyDown("6") && !rotating)
            Rotate(2, Pentago.RotateDirection.RIGHT);

        if (Input.GetKeyDown("7") && !rotating)
            Rotate(3, Pentago.RotateDirection.LEFT);
        if (Input.GetKeyDown("8") && !rotating)
            Rotate(3, Pentago.RotateDirection.RIGHT);
    }

    public void Rotate(int index, Pentago.RotateDirection direction)
    {
        boardToAnimate = boardsToAnimate[index];

        //previousAnimatorPosition = gameObject.transform.position;
        var lookTarget = new Vector3(boardToAnimate.position.x, 0, boardToAnimate.position.z);
        gameObject.transform.rotation = Quaternion.LookRotation(lookTarget) * Quaternion.Euler(0.0f, animationAngleOffset, 0.0f);
        gameObject.transform.position = boardToAnimate.transform.position;
        
        AttachTransformToAnimate(boardToAnimate);

        anim.SetTrigger(direction == Pentago.RotateDirection.LEFT ? "CCW" : "CW");
        rotating = true;

        Debug.Log("Rotating!");
    }

    void OnRotateFinished()
    {
        DetachTransformToAnimate(boardToAnimate);

        //gameObject.transform.position = previousAnimatorPosition;
        //gameObject.transform.rotation = previousAnimatorRotation;

        anim.ResetTrigger("CW");
        anim.ResetTrigger("CCW");
        anim.SetTrigger("Reset");
        rotating = false;

        Debug.Log("Rotate finished!");
    }

    private void AttachTransformToAnimate(Transform t)
    {
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
