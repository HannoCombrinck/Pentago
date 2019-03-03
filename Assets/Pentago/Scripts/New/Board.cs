using System.Collections;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int boardIndex;
    public Pentago game { get; set; }

    private IBoardRotator rotator;

    private void Awake()
    {
        rotator = GetComponent<IBoardRotator>();
        Debug.Assert(rotator != null);
    }

    public void RotateBoard(ActionRotateBoard.DIRECTION direction)
    {
        StartCoroutine(AnimateBoardThenExecuteRotateAction(direction));
    }

    IEnumerator AnimateBoardThenExecuteRotateAction(ActionRotateBoard.DIRECTION direction)
    {
        rotator.RotateClockwise();

        while (rotator.IsBusyRotating())
        {
            yield return null; //new WaitForSeconds(0.0f);
        }

        // TODO: Re-sort space indices - how to access SpaceSorter from here?


        game.ExecuteAction(new ActionRotateBoard(boardIndex, direction));
    }
}
