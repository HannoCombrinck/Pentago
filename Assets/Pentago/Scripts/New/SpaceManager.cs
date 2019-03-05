using UnityEngine;

// Initialize and manage all space visuals (i.e. GameObjects with Space component attached) that 
// are descendants of this GameObject.
public class SpaceManager : SpatialSorter<Space>
{
    [Tooltip("TEMPORARY TO BE REMOVED")]
    public Game game;

    private BoardManager boardManager;

    protected override void Awake()
    {
        base.Awake();

        boardManager = GetComponent<BoardManager>();
        Debug.Assert(boardManager != null);

        Debug.Assert(game.state != null); // TEMP TO BE REMOVED

        for (int i = 0; i < sortedSpatials.Count; i++)
        {
            sortedSpatials[i].spaceIndex = i;
            sortedSpatials[i].game = game;
        }
    }

    public void UpdateAll()
    {
        Sort();

        // TODO: Find a better way to implement this - shouldn't be instantiating prefabs here and have convoluted logic for checkin Space state etc.
        for (int i = 0; i < sortedSpatials.Count; i++)
        {
            sortedSpatials[i].RemoveMarble();

            switch (game.state.spaces[i])
            {
                case CommonTypes.SPACE_STATE.OCCUPIED_PLAYER1:
                    sortedSpatials[i].AddMarble(CommonTypes.PLAYER.PLAYER1, Instantiate(boardManager.player1MarblePrefab, sortedSpatials[i].transform.position + Vector3.up * boardManager.marbleHeightOffset, Quaternion.identity));
                    break;
                case CommonTypes.SPACE_STATE.OCCUPIED_PLAYER2:
                    sortedSpatials[i].AddMarble(CommonTypes.PLAYER.PLAYER2, Instantiate(boardManager.player2MarblePrefab, sortedSpatials[i].transform.position + Vector3.up * boardManager.marbleHeightOffset, Quaternion.identity));
                    break;
            }
        }
    }

    public void OnSpaceIndicesChanged()
    {
        Sort();
    }
}
