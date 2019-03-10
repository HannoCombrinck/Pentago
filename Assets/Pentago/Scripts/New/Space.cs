using UnityEngine;

// Represents a visual Space where a Marble can be placed.
// Holds index to map to Game state.
public class Space : MonoBehaviour
{
    public CommonTypes.SPACE_STATE state = CommonTypes.SPACE_STATE.UNOCCUPIED;
    public int spaceIndex;

    private GameObject currentMarble;

    public void AddMarble(CommonTypes.PLAYER player, GameObject marble)
    {
        currentMarble = marble;
        currentMarble.transform.SetParent(transform);
        state = player == CommonTypes.PLAYER.PLAYER1 ? CommonTypes.SPACE_STATE.OCCUPIED_PLAYER1 : CommonTypes.SPACE_STATE.OCCUPIED_PLAYER2;
    }

    public void RemoveMarble()
    {
        if (currentMarble == null)
            return;
            
        Destroy(currentMarble);
        currentMarble = null;
        state = CommonTypes.SPACE_STATE.UNOCCUPIED;
    }
}
