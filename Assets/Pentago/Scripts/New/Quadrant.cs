using System.Collections;
using UnityEngine;

public class Quadrant : MonoBehaviour
{
    public int quadrantIndex;
    public Pentago game { get; set; }

    private IQuadrantRotator rotator;

    private void Awake()
    {
        rotator = GetComponent<IQuadrantRotator>();
        Debug.Assert(rotator != null);
    }

    public void RotateQuadrant(ActionRotateQuadrant.DIRECTION direction)
    {
        StartCoroutine(AnimateQuadrantThenExecuteAction(direction));
    }

    IEnumerator AnimateQuadrantThenExecuteAction(ActionRotateQuadrant.DIRECTION direction)
    {
        switch (direction)
        {
            case ActionRotateQuadrant.DIRECTION.CLOCKWISE:
                rotator.RotateClockwise();
                break;
            case ActionRotateQuadrant.DIRECTION.COUNTERCLOCKWISE:
                rotator.RotateCounterClockwise();
                break;
        }
        
        while (rotator.IsBusyRotating())
        {
            yield return null; //new WaitForSeconds(0.0f);
        }

        // TODO: Re-sort space indices - how to access SpaceSorter from here?


        game.ExecuteAction(new ActionRotateQuadrant(quadrantIndex, direction));
    }
}
