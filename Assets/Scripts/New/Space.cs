using UnityEngine;

public class Space : MonoBehaviour
{
    public GameState gameState;
    public GameState.SPACE_STATE state = GameState.SPACE_STATE.UNOCCUPIED;

    void Start()
    {
        
    }

    void Update()
    {
        if (state != gameState.boardState[GetBoardStateIndex()])
        {
            state = gameState.boardState[GetBoardStateIndex()];
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

    public int GetBoardStateIndex()
    {
        // TODO: Calculate Space index (range from 0 to 35) in boardState array
        return 0;
    }
}
