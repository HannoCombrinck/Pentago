using UnityEngine;
using static IGame;

// Initialize and control interacitons with all descendant Space's.
public class SpaceController : SpatialSorter<Space>
{
    private Board boardManager;

    protected override void Awake()
    {
        base.Awake();

        boardManager = GetComponent<Board>();
        Debug.Assert(boardManager != null);

        for (int i = 0; i < sortedSpatials.Count; i++)
            sortedSpatials[i].spaceIndex = i;
    }

    public void UpdateAll(State gameState)
    {
        Sort();

        // TODO: Find a better way to implement this - shouldn't be instantiating prefabs here and have convoluted logic for checkin Space state etc.
        for (int i = 0; i < sortedSpatials.Count; i++)
        {
            sortedSpatials[i].spaceIndex = i;
            sortedSpatials[i].RemoveMarble();

            switch (gameState.spaces[i])
            {
                case SPACE_STATE.OCCUPIED_PLAYER1:
                    sortedSpatials[i].AddMarble(PLAYER.PLAYER1, Instantiate(boardManager.player1MarblePrefab, sortedSpatials[i].transform.position + Vector3.up * boardManager.marbleHeightOffset, Quaternion.identity));
                    break;
                case SPACE_STATE.OCCUPIED_PLAYER2:
                    sortedSpatials[i].AddMarble(PLAYER.PLAYER2, Instantiate(boardManager.player2MarblePrefab, sortedSpatials[i].transform.position + Vector3.up * boardManager.marbleHeightOffset, Quaternion.identity));
                    break;
            }
        }
    }

    public void OnSpaceIndicesChanged()
    {
        Sort();
    }
}
