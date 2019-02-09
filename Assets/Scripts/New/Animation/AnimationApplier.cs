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
    private Transform objectBeingAnimated;
    private bool inProgress = false;
    private string currentAnimation;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        Debug.Assert(animator);
        Debug.Assert(animatedBone);
    }

    void Update()
    {
        if (!inProgress)
            return;

        // TODO: This check could run in a coroutine so it doesn't have to happen every frame
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(currentAnimation))
        {
            Debug.Log("AnimationApplier: Animation time: " + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            return;
        }

        Debug.Log("AnimationApplier: Finished animation");
        objectBeingAnimated = null;
        inProgress = false;

        // TODO
        // This point is reached if the current animation has finished playing
        // Detach objectToAnimate
    }

    // Apply animationName animation to objectToAnimate
    public void ApplyTo(Transform objectToAnimate, string animationName)
    {
        if (inProgress)
            return;

        Debug.Log("AnimationApplier: " + gameObject.name + " is applying animation " + animationName + " of bone " + animatedBone.name + " to object " + objectToAnimate.gameObject.name);

        objectBeingAnimated = objectToAnimate;
        currentAnimation = animationName;
        inProgress = true;

        // TODO
        // Attach objectToAnimate to animatedBone
        // Play animation
    }
    
}
