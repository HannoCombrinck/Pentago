using UnityEngine;

public class Space : MonoBehaviour
{
    public GameState.SPACE_STATE state = GameState.SPACE_STATE.UNOCCUPIED;
    public int boardIndex;

    public GameState gameState { get; set; }
    public GameSettings gameSettings { get; set; }

    private GameObject currentMarble;

    void Start()
    {
        Debug.Assert(gameState != null);
        Debug.Assert(gameSettings != null);
    }

    void Update()
    {
        if (state != gameState.boardState[boardIndex])
        {
            state = gameState.boardState[boardIndex];
            Debug.Log("Space " + gameObject.name + " has changed stated");

            switch (state)
            {
                case GameState.SPACE_STATE.UNOCCUPIED:
                    if (currentMarble != null)
                        Destroy(currentMarble);

                    currentMarble = null;
                    break;
                case GameState.SPACE_STATE.OCCUPIED_PLAYER1:
                    if (currentMarble != null)
                        Destroy(currentMarble);

                    currentMarble = Instantiate(gameSettings.player1MarblePrefab, transform.position + Vector3.up * gameSettings.marbleHeightOffset, Quaternion.identity);
                    currentMarble.transform.SetParent(transform);
                    break;
                case GameState.SPACE_STATE.OCCUPIED_PLAYER2:
                    if (currentMarble != null)
                        Destroy(currentMarble);

                    currentMarble = Instantiate(gameSettings.player2MarblePrefab, transform.position + Vector3.up * gameSettings.marbleHeightOffset, Quaternion.identity);
                    currentMarble.transform.SetParent(transform);
                    break;
            }

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
