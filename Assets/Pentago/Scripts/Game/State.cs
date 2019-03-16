using UnityEngine;
using static Game;

[CreateAssetMenu]
public class State : ScriptableObject
{
    public void ResetState()
    {
        currentPlayer = PLAYER.PLAYER1;
        winState = WIN_STATE.IN_PROGRESS;
        nextMove = MOVE_TYPE.PLACE_MARBLE;

        for (int i = 0; i < spaces.Length; i++)
            spaces[i] = SPACE_STATE.UNOCCUPIED;
    }

    public WIN_STATE winState = WIN_STATE.IN_PROGRESS;
    public PLAYER currentPlayer = PLAYER.PLAYER1;
    public MOVE_TYPE nextMove = MOVE_TYPE.PLACE_MARBLE;
    public SPACE_STATE[] spaces = new SPACE_STATE[36];
}
