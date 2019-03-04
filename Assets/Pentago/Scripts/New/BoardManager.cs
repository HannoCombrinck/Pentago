using System;
using System.Collections;
using UnityEngine;

// Initialize and manage the board visual.
[RequireComponent(typeof(QuadrantManager), typeof(SpaceManager))]
public class BoardManager : MonoBehaviour
{
    [Tooltip("Reference to the Pentago game manager component.")]
    public Game game;
    [Tooltip("Prefab to use as Player 1's marble.")]
    public GameObject player1MarblePrefab;
    [Tooltip("Prefab to use as Player 2's marble.")]
    public GameObject player2MarblePrefab;
    [Tooltip("Visually how high above the space to place marble prefabs.")]
    public float marbleHeightOffset;

    private QuadrantManager quadrants;
    private SpaceManager spaces;

    private void Awake()
    {
        Debug.Assert(game != null, "Game component required.");
        quadrants = GetComponent<QuadrantManager>();
        spaces = GetComponent<SpaceManager>();

        /////
        // TEMP 
        game.onNewGameStarted += ApplyGameStateToVisuals;
        game.onActionExecuted += ApplyGameStateToVisuals;
        /////
    }

    private void Start()
    {
        ApplyGameStateToVisuals();
    }

    // Change the visuals to represent the game state as stored in Game.state.
    public void ApplyGameStateToVisuals()
    {
        // TODO
        spaces.UpdateAll();
    }

    // Visually place a marble in the game world and execute a ActionPlaceMarble action on the Game.
    public void PlaceMarble(int spaceIndex)
    {
        var action = new ActionPlaceMarble(spaceIndex);
        if (!game.IsValidAction(action))
        {
            Debug.Log(game.state.currentPlayer.ToString() + " attempted an invalid move. Cannot place a marble on " + spaceIndex + " at this time.");
            return;
        }

        ////////
        //TODO: VISUAL ANIMATION GOES HERE
        var marblePrefab = game.state.currentPlayer == CommonTypes.PLAYER.PLAYER1 ? player1MarblePrefab : player2MarblePrefab;
        var space = spaces.Get(spaceIndex);
        var marble = Instantiate(marblePrefab, space.transform.position + Vector3.up * marbleHeightOffset, Quaternion.identity);
        space.AddMarble(game.state.currentPlayer, marble);
        ////////

        // Execute action to update abstract game representation
        game.ExecuteAction(action);
    }

    // Show a visual preview of what it would look like if a marble were placed.
    public void PlaceMarbleShowPreview(int spaceIndex)
    {
        //TODO:
    }

    // Hide the visual preview of a marble placement.
    public void PlaceMarbleHidePreview()
    {
        //TODO:
    }

    // Visually rotate the quadrant in the game world and execute a ActionRotateQuadrant action on the Game.
    public void RotateQuadrant(int quadrantIndex, CommonTypes.ROTATE_DIRECTION direction)
    {
        var action = new ActionRotateQuadrant(quadrantIndex, direction);
        if (!game.IsValidAction(action))
        {
            Debug.Log(game.state.currentPlayer.ToString() + " attempted invalid move. Cannot rotate quadrant " + quadrantIndex + " " + direction.ToString() + " at this time.");
            return;
        }

        var quadrant = quadrants.Get(quadrantIndex);
        StartCoroutine(AnimateQuadrant(quadrant, direction, () => game.ExecuteAction(action)));
    }

    // TODO: Add comment
    private IEnumerator AnimateQuadrant(Quadrant quadrant, CommonTypes.ROTATE_DIRECTION direction, Action onFinished)
    {
        ////////
        //TODO: VISUAL ANIMATION GOES HERE
        /*switch (direction)
        {
            case CommonTypes.ROTATE_DIRECTION.CLOCKWISE:
                rotator.RotateClockwise();
                break;
            case CommonTypes.ROTATE_DIRECTION.COUNTERCLOCKWISE:
                rotator.RotateCounterClockwise();
                break;
        }

        while (rotator.IsBusyRotating())
        {
            yield return null; //new WaitForSeconds(0.0f);
        }

        // TODO: Re-sort space indices - how to access SpaceSorter from here?
        */
        ////////
        yield return null;
        onFinished();
    }
}
