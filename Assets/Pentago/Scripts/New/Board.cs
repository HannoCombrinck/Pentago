using UnityEngine;

public class Board : MonoBehaviour
{
    public int boardIndex;
    public Pentago game { get; set; }

    public void RotateBoard(ActionRotateBoard.DIRECTION direction)
    {
        // TODO: Update visual by playing animation or instantiating appropriate visual
        game.ExecuteAction(new ActionRotateBoard(boardIndex, direction));
    }
}
