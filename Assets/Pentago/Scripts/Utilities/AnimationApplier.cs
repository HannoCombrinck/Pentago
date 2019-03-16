using UnityEngine;

// This AnimationApplier provides and interface to apply an animation of the attached animator to another GameObject
public class AnimationApplier : MonoBehaviour
{
    [Tooltip("The animated GameObject/Node/Transform/Bone that will be used to animate another object.")]
    public Transform animatedBone;
    [Tooltip("Ignore the object being animated's parent while the animation is being applied.")]
    public bool ignoreParent = false;
    [Tooltip("Reset the position of the object being animated when the animation is finished.")]
    public bool resetPosition = false;
    [Tooltip("Reset the scale of the object being animated when the animation is finished.")]
    public bool resetScale = false;

    private Animator animator;
    private Transform objectBeingAnimated;
    private Transform previousParentOfObjectBeingAnimated;
    private Vector3 previousPosition;
    private Vector3 previousScale;
    private bool isAboutToStart = false;
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
        // Check animator state to determine when the animation has actually started playing
        if (isAboutToStart && animator.GetCurrentAnimatorStateInfo(0).IsName(currentAnimation))
        {
            inProgress = true;
            isAboutToStart = false;
        }

        // Early return if no animation is in progress
        if (!inProgress)
            return;

        // Early return while the animation is in progress
        // TODO: This check could run in a coroutine so it doesn't have to happen every frame
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(currentAnimation) &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
        {
            return;
        }

        // If this point is reached then an animation has played and finished
        DetachObjectBeingAnimated();
        objectBeingAnimated = null;
        currentAnimation = "None";
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        animator.SetTrigger("Reset");
        inProgress = false;

        //Debug.Log("AnimationApplier: Finished animation");
    }

    public bool IsAnimationInProgress()
    {
        return inProgress || isAboutToStart;
    }

    // Convenience override for ApplyTo with no rotationOffset 
    public void ApplyTo(Transform objectToAnimate, string animationName)
    {
        ApplyTo(objectToAnimate, Quaternion.identity, animationName);
    }

    // Apply animationName animation to objectToAnimate (there must exist a trigger property with the same name as the provided animationName)
    public void ApplyTo(Transform objectToAnimate, Quaternion rotationOffset, string animationName)
    {
        if (inProgress || isAboutToStart)
            return;

        //Debug.Log("AnimationApplier: " + gameObject.name + " is applying animation " + animationName + " of bone " + animatedBone.name + " to object " + objectToAnimate.gameObject.name);

        objectBeingAnimated = objectToAnimate;
        currentAnimation = animationName;

        transform.position = objectBeingAnimated.position;
        transform.rotation = rotationOffset; //TODO take objectBeingAnimated.rotation into account
        AttachObjectToAnimate();
        animator.SetTrigger(animationName);

        // Indicate that everything is ready for animation to start playing
        isAboutToStart = true;
    }

    void AttachObjectToAnimate()
    {
        if (resetPosition)
            previousPosition = objectBeingAnimated.localPosition;
        if (resetScale)
            previousScale = objectBeingAnimated.localScale;

        if (!ignoreParent)
            transform.SetParent(objectBeingAnimated.parent);

        previousParentOfObjectBeingAnimated = objectBeingAnimated.parent;
        objectBeingAnimated.SetParent(animatedBone);
    }

    void DetachObjectBeingAnimated()
    {
        objectBeingAnimated.SetParent(previousParentOfObjectBeingAnimated);

        if (!ignoreParent)
            transform.SetParent(null);

        if (resetPosition)
            objectBeingAnimated.localPosition = previousPosition;
        if (resetScale)
            objectBeingAnimated.localScale = previousScale;
    }
}
