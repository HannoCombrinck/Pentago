using UnityEngine;

public class Space : MonoBehaviour
{
    public GameState gameState;
    public GameState.SPACE_STATE state = GameState.SPACE_STATE.UNOCCUPIED;
    public int boardIndex;

    void Update()
    {
        if (state != gameState.boardState[boardIndex])
        {
            state = gameState.boardState[boardIndex];
            Debug.Log("Space " + gameObject.name + " has changed stated");
            // TODO: Fire event (space state changed to reflect new game state)
        }
    }

    public void SetState()
    {
        // Space state changed (e.g. can happen when state loaded from saved game)
    }

    public void PlaceMarble()
    {
        // Space state changed (can happen when Player (Human or AI) placed a marble)
        
    }
    
}
