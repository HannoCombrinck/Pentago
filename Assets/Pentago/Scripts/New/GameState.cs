using UnityEngine;

[CreateAssetMenu]
public class GameState : ScriptableObject
{
    public void ResetState()
    {

        currentPlayer = CommonTypes.PLAYER.PLAYER1;
        winState = CommonTypes.WIN_STATE.IN_PROGRESS;
        nextMove = CommonTypes.MOVE_TYPE.PLACE_MARBLE;

        for (int i = 0; i < spaceState.Length; i++)
            spaceState[i] = CommonTypes.SPACE_STATE.UNOCCUPIED;
    }

    public CommonTypes.WIN_STATE winState = CommonTypes.WIN_STATE.IN_PROGRESS;
    public CommonTypes.PLAYER currentPlayer = CommonTypes.PLAYER.PLAYER1;
    public CommonTypes.MOVE_TYPE nextMove = CommonTypes.MOVE_TYPE.PLACE_MARBLE;
    public CommonTypes.SPACE_STATE[] spaceState = new CommonTypes.SPACE_STATE[36];
}
