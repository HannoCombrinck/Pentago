﻿
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
    private Board board;

    private void Awake()
    {
        board = GetComponent<Board>();
        Debug.Assert(board != null);

        var spacesToSort = GetComponentsInChildren<Space>();
        spaceSorter = new SpatialSorter<Space>(spacesToSort, ref sortedSpaces);

        for (int i = 0; i < sortedSpaces.Count; i++)
            sortedSpaces[i].spaceIndex = i;
    }

    public void UpdateAll(State gameState)
    {
        spaceSorter.Sort();

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // TODO: Find a better way to implement this - shouldn't be instantiating prefabs and checking Space state here.
        for (int i = 0; i < sortedSpaces.Count; i++)
        {
            sortedSpaces[i].spaceIndex = i;
            var removedMarble = sortedSpaces[i].RemoveMarble();
            // TODO: Possibly animate the "deletion/removal" of the marble instead of just destroying.
            Destroy(removedMarble);

            switch (gameState.spaces[i])
            {
                case SPACE_STATE.OCCUPIED_PLAYER1:
                    sortedSpaces[i].AddMarble(PLAYER.PLAYER1, Instantiate(board.player1MarblePrefab, sortedSpaces[i].transform.position + board.marbleVisualOffset, Quaternion.identity));
                    break;
                case SPACE_STATE.OCCUPIED_PLAYER2:
                    sortedSpaces[i].AddMarble(PLAYER.PLAYER2, Instantiate(board.player2MarblePrefab, sortedSpaces[i].transform.position + board.marbleVisualOffset, Quaternion.identity));
                    break;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    public void OnSpaceIndicesChanged()
    {
        spaceSorter.Sort();
    }
}
