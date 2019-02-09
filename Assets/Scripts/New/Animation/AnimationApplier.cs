using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This AnimationApplier provides and interface to apply an animation of the specified 
 * GameObject/Bone/Transform (controlled by the animator it is attached to) to another 
 * GameObject/Transform
 */
public class AnimationApplier : MonoBehaviour
{
    [Tooltip("The animated GameObject/Node/Transform/Bone that will be used to animate another object")]
    public Transform animatedBone;

    private Animator animator;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        Debug.Assert(animator);
        Debug.Assert(animatedBone);
    }

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Apply animationName animation to objectToAnimate
    public void ApplyTo(Transform objectToAnimate, string animationName)
    {
        Debug.Log("AnimationApplier: " + gameObject.name + " is applying animation " + animationName + " of bone " + animatedBone.name + " to object " + objectToAnimate.gameObject.name);

    }
}
