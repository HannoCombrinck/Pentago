using System.Collections;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int boardIndex;
    public Pentago game { get; set; }

    public void RotateBoard(ActionRotateBoard.DIRECTION direction)
    {
        StartCoroutine(AnimateBoardThenExecuteRotateAction(direction));
    }

    IEnumerator AnimateBoardThenExecuteRotateAction(ActionRotateBoard.DIRECTION direction)
    {
        // TODO: Play animation and re-sort spaces
        // CONTINUE HERE
        yield return new WaitForSeconds(2.0f);
        game.ExecuteAction(new ActionRotateBoard(boardIndex, direction));
    }
}
