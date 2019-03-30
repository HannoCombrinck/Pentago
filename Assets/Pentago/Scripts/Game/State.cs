using UnityEngine;
using static IGame;

public class State : MonoBehaviour
{
    public WIN_STATE winState = WIN_STATE.IN_PROGRESS;
    public int[] winningLine = new int[5];
    public PLAYER currentPlayer = PLAYER.PLAYER1;
    public MOVE_TYPE nextMove = MOVE_TYPE.PLACE_MARBLE;
    public SPACE_STATE[] spaces = new SPACE_STATE[36];

    public void ResetState()
    {
        winState = WIN_STATE.IN_PROGRESS;
        currentPlayer = PLAYER.PLAYER1;
        nextMove = MOVE_TYPE.PLACE_MARBLE;

        for (int i = 0; i < winningLine.Length; i++)
            winningLine[i] = 0;

        for (int i = 0; i < spaces.Length; i++)
            spaces[i] = SPACE_STATE.UNOCCUPIED;


        //////////////////////////////////////////////////
        // TEMP
        /*
        for (int i = 0; i < 4; i++)
            spaces[i] = SPACE_STATE.OCCUPIED_PLAYER1;
        */
    }
}
