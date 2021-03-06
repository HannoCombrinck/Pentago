﻿using UnityEngine;
using static IGame;

// Represents a visual Space where a Marble can be placed.
// Holds index to map to Game state.
public class Space : MonoBehaviour
{
    public SPACE_STATE state = SPACE_STATE.UNOCCUPIED;
    public int spaceIndex;

    private GameObject currentMarble;

    public GameObject GetMarble()
    {
        return currentMarble;
    }

    public void AddMarble(PLAYER player, GameObject marble)
    {
        currentMarble = marble;
        currentMarble.transform.SetParent(transform);
        state = player == PLAYER.PLAYER1 ? SPACE_STATE.OCCUPIED_PLAYER1 : SPACE_STATE.OCCUPIED_PLAYER2;
    }

    public GameObject RemoveMarble()
    {
        if (currentMarble == null)
            return null;
            
        var temp = currentMarble;
        currentMarble = null;
        state = SPACE_STATE.UNOCCUPIED;
        return temp;
    }
}
