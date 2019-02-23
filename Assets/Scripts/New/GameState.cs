using UnityEngine;

[CreateAssetMenu]
public class GameState : ScriptableObject
{
    public enum WIN_STATE
    {
        IN_PROGRESS,
        PLAYER1_WON,
        PLAYER2_WON
    }
    public WIN_STATE winState = WIN_STATE.IN_PROGRESS;

    public enum PLAYER
    {
        PLAYER1,
        PLAYER2
    }
    public PLAYER currentPlayer = PLAYER.PLAYER1;

    public enum MOVE_TYPE
    {
        PLACE_MARBLE,
        ROTATE_BOARD
    }
    public MOVE_TYPE nextMove = MOVE_TYPE.PLACE_MARBLE;

    public enum SPACE_STATE
    {
        UNOCCUPIED,
        OCCUPIED_PLAYER1,
        OCCUPIED_PLAYER2,
    }
    public SPACE_STATE[] boardState = new SPACE_STATE[36];
}
