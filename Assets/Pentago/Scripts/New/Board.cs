using System;
using System.Collections;
using UnityEngine;

// Initialize and manage the board visual.
[RequireComponent(typeof(QuadrantManager), typeof(SpaceManager))]
public class Board : MonoBehaviour
{
    [Tooltip("Reference to the Pentago game manager component.")]
    public Game game;
    [Tooltip("Prefab to use as Player 1's marble.")]
    public GameObject player1MarblePrefab;
    [Tooltip("Prefab to use as Player 2's marble.")]
    public GameObject player2MarblePrefab;
    [Tooltip("Visually how high above the space to place marble prefabs.")]
    public float marbleHeightOffset;

    private bool actionInProgress = false;
    private QuadrantManager quadrants;
    private SpaceManager spaces;

    private void Awake()
    {
        Debug.Assert(game != null, "Game component required.");
        quadrants = GetComponent<QuadrantManager>();
        spaces = GetComponent<SpaceManager>();

        game.onNewGameStarted += ApplyGameStateToVisuals;
        game.onActionExecuted += ApplyGameStateToVisuals;
    }

    private void Start()
    {
        ApplyGameStateToVisuals();
    }

    // Change the visuals to represent the game state as stored in Game state.
    public void ApplyGameStateToVisuals()
    {
        spaces.UpdateAll(game.state);
    }

    // Attempt to visually place a marble in the game world and execute a ActionPlaceMarble action on the Game state.
    public void PlaceMarble(int spaceIndex)
    {
        if (actionInProgress)
            return;

        StartCoroutine(DoPlaceMarble(spaceIndex));
    }

    private IEnumerator DoPlaceMarble(int spaceIndex)
    {
        actionInProgress = true;

        var action = new ActionPlaceMarble(spaceIndex);

        if (!game.IsValidAction(action))
        {
            actionInProgress = false;
            yield break;
        }

        yield return StartCoroutine(AnimatePlaceMarble(spaceIndex));

        game.ExecuteAction(action);

        actionInProgress = false;
    }

    private IEnumerator AnimatePlaceMarble(int spaceIndex)
    {
        // TODO: Animate marble placement instead of just instantly placing it
        var marblePrefab = game.state.currentPlayer == CommonTypes.PLAYER.PLAYER1 ? player1MarblePrefab : player2MarblePrefab;
        var space = spaces.Get(spaceIndex);
        var marble = Instantiate(marblePrefab, space.transform.position + Vector3.up * marbleHeightOffset, Quaternion.identity);
        space.AddMarble(game.state.currentPlayer, marble);
        yield return null;
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
        if (actionInProgress)
            return;

        StartCoroutine(DoRotateQuadrant(quadrantIndex, direction));
    }

    private IEnumerator DoRotateQuadrant(int quadrantIndex, CommonTypes.ROTATE_DIRECTION direction)
    {
        actionInProgress = true;

        var action = new ActionRotateQuadrant(quadrantIndex, direction);

        if (!game.IsValidAction(action))
        {
            actionInProgress = false;
            yield break;
        }

        yield return StartCoroutine(AnimateRotateQuadrant(quadrantIndex, direction));

        game.ExecuteAction(action);

        actionInProgress = false;
    }

    // Visually roatate quadrant 
    private IEnumerator AnimateRotateQuadrant(int quadrantIndex, CommonTypes.ROTATE_DIRECTION direction)
    {
        var quadrantRotator = quadrants.Get(quadrantIndex).GetComponent<IQuadrantRotator>();

        switch (direction)
        {
            case CommonTypes.ROTATE_DIRECTION.CLOCKWISE:
                quadrantRotator.RotateClockwise();
                break;
            case CommonTypes.ROTATE_DIRECTION.COUNTERCLOCKWISE:
                quadrantRotator.RotateCounterClockwise();
                break;
        }

        while (quadrantRotator.IsBusyRotating())
            yield return null;
    }
}
