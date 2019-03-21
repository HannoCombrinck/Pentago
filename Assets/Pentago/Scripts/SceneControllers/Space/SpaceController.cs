
using System.Collections.Generic;
using UnityEngine;
using static IGame;

// Initialize and control interacitons with all descendant Space's.
public class SpaceController : MonoBehaviour
{
    public List<Space> sortedSpaces;
    public Space this[int key] => sortedSpaces[key];
    public int Count => sortedSpaces.Count;

    private SpatialSorter<Space> spaceSorter;
    private Board boardManager;

    private void Awake()
    {
        boardManager = GetComponent<Board>();
        Debug.Assert(boardManager != null);

        var spacesToSort = GetComponentsInChildren<Space>();
        spaceSorter = new SpatialSorter<Space>(spacesToSort, ref sortedSpaces);

        for (int i = 0; i < sortedSpaces.Count; i++)
            sortedSpaces[i].spaceIndex = i;
    }

    public void UpdateAll(State gameState)
    {
        spaceSorter.Sort();

        // TODO: Find a better way to implement this - shouldn't be instantiating prefabs here and have convoluted logic for checkin Space state etc.
        for (int i = 0; i < sortedSpaces.Count; i++)
        {
            sortedSpaces[i].spaceIndex = i;
            sortedSpaces[i].RemoveMarble();

            switch (gameState.spaces[i])
            {
                case SPACE_STATE.OCCUPIED_PLAYER1:
                    sortedSpaces[i].AddMarble(PLAYER.PLAYER1, Instantiate(boardManager.player1MarblePrefab, sortedSpaces[i].transform.position + Vector3.up * boardManager.marbleHeightOffset, Quaternion.identity));
                    break;
                case SPACE_STATE.OCCUPIED_PLAYER2:
                    sortedSpaces[i].AddMarble(PLAYER.PLAYER2, Instantiate(boardManager.player2MarblePrefab, sortedSpaces[i].transform.position + Vector3.up * boardManager.marbleHeightOffset, Quaternion.identity));
                    break;
            }
        }
    }

    public void OnSpaceIndicesChanged()
    {
        spaceSorter.Sort();
    }
}
