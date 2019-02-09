using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    Pentago.PlayerID nextPlayer = Pentago.PlayerID.PLAYER1;
    public GameObject whiteMarble;
    public GameObject blackMarble;
    public GameObject whiteMarblePreview;
    public GameObject blackMarblePreview;
    public float marbleHeightOffset = 0.3f;

    void Start()
    {
        Debug.Assert(whiteMarble);
        Debug.Assert(blackMarble);
        Debug.Assert(whiteMarblePreview);
        Debug.Assert(blackMarblePreview);

        whiteMarblePreview = Instantiate(whiteMarblePreview);
        whiteMarblePreview.SetActive(false);
        blackMarblePreview = Instantiate(blackMarblePreview);
        blackMarblePreview.SetActive(false);
    }

    public void PlaceMarblePreview(GameObject go)
    {
        if (go == null)
        {
            whiteMarblePreview.SetActive(false);
            blackMarblePreview.SetActive(false);
            return;
        }

        if (nextPlayer == Pentago.PlayerID.PLAYER1)
        {
            whiteMarblePreview.SetActive(true);
            blackMarblePreview.SetActive(false);
            whiteMarblePreview.transform.position = go.transform.position + Vector3.up * marbleHeightOffset;
        }
        else
        {
            whiteMarblePreview.SetActive(false);
            blackMarblePreview.SetActive(true);
            blackMarblePreview.transform.position = go.transform.position + Vector3.up * marbleHeightOffset;
        }
    }

    public void PlaceMarble(GameObject go)
    {
        if (go.transform.childCount > 0)
        {
            Debug.Log("Marble already placed on this position");
            return;
        }

        GameObject marble = null;

        if (nextPlayer == Pentago.PlayerID.PLAYER1)
        {
            Debug.Log("Placing white marble on position: " + go.name);
            marble = Instantiate(whiteMarble);
            nextPlayer = Pentago.PlayerID.PLAYER2;
        }
        else if (nextPlayer == Pentago.PlayerID.PLAYER2)
        {
            Debug.Log("Placing black marble on position: " + go.name);
            marble = Instantiate(blackMarble);
            nextPlayer = Pentago.PlayerID.PLAYER1;
        }

        if (marble != null)
        {
            marble.transform.position = go.transform.position + Vector3.up * marbleHeightOffset;
            marble.transform.SetParent(go.transform);
        }
    }
}
