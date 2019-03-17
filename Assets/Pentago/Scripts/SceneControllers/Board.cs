using System;
using System.Collections;
using UnityEngine;
using static IGame;

// Initialize and manage the board GameObject.
[RequireComponent(typeof(QuadrantController), typeof(SpaceController))]
public class Board : MonoBehaviour
{
    [Tooltip("Reference to the game interface.")]
    public IGame game;
    [Tooltip("Prefab to use as Player 1's marble.")]
    public GameObject player1MarblePrefab;
    [Tooltip("Prefab to use as Player 2's marble.")]
    public GameObject player2MarblePrefab;
    [Tooltip("Visually how high above the space to place marble prefabs.")]
    public float marbleHeightOffset;

    // Board events
    // {
    public Action onIllegalMarblePlacement;
    public Action onIllegalQuadrantRotation;
    // }

    private bool actionInProgress = false;
    private QuadrantController quadrants;
    private SpaceController spaces;

    private void Awake()
    {
        Debug.Assert(game != null, "Game component required.");
        quadrants = GetComponent<QuadrantController>();
        spaces = GetComponent<SpaceController>();

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
        spaces.UpdateAll(game.GetState());
    }

    // Attempt to visually place a marble in the game world and execute a ActionPlaceMarble action on the Game state.
    public void PlaceMarble(int spaceIndex)
    {
        if (actionInProgress)
            return;

        StartCoroutine(PlaceMarbleCoroutine(spaceIndex));
    }

    private IEnumerator PlaceMarbleCoroutine(int spaceIndex)
    {
        actionInProgress = true;

        var action = new ActionPlaceMarble(spaceIndex);

        if (!action.IsValid(game.GetState()))
        {
            Debug.Log(game.GetState().currentPlayer.ToString() + " attempted to illegaly place a marble.");
            onIllegalMarblePlacement?.Invoke();
        }
        else
        {
            yield return StartCoroutine(PlaceMarbleVisual(spaceIndex));
        }

        game.ExecuteAction(action);

        actionInProgress = false;
    }

    private IEnumerator PlaceMarbleVisual(int spaceIndex)
    {
        // TODO: Animate marble placement instead of just instantly placing it
        //       Move logic to Space component - just pass reference to the marble to be placed from here.
        //       Placement/Animation should be initiated from Space
        var marblePrefab = game.GetState().currentPlayer == PLAYER.PLAYER1 ? player1MarblePrefab : player2MarblePrefab;
        var space = spaces.Get(spaceIndex);
        var marble = Instantiate(marblePrefab, space.transform.position + Vector3.up * marbleHeightOffset, Quaternion.identity);
        space.AddMarble(game.GetState().currentPlayer, marble);
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
    public void RotateQuadrant(int quadrantIndex, ROTATE_DIRECTION direction)
    {
        if (actionInProgress)
            return;

        StartCoroutine(RotateQuadrantCoroutine(quadrantIndex, direction));
    }

    private IEnumerator RotateQuadrantCoroutine(int quadrantIndex, ROTATE_DIRECTION direction)
    {
        actionInProgress = true;

        var action = new ActionRotateQuadrant(quadrantIndex, direction);

        if (!action.IsValid(game.GetState()))
        {
            Debug.Log(game.GetState().currentPlayer.ToString() + " attempted to illegaly rotate a quadrant.");
            onIllegalQuadrantRotation?.Invoke();
        }
        else
        {
            yield return StartCoroutine(RotateQuadrantVisual(quadrantIndex, direction));
        }

        game.ExecuteAction(action);

        actionInProgress = false;
    }

    // Visually roatate quadrant 
    private IEnumerator RotateQuadrantVisual(int quadrantIndex, ROTATE_DIRECTION direction)
    {
        var quadrantRotator = quadrants.Get(quadrantIndex).GetComponent<IQuadrantRotator>();

        switch (direction)
        {
            case ROTATE_DIRECTION.CLOCKWISE:
                quadrantRotator.RotateClockwise();
                break;
            case ROTATE_DIRECTION.COUNTERCLOCKWISE:
                quadrantRotator.RotateCounterClockwise();
                break;
        }

        while (quadrantRotator.IsBusyRotating())
            yield return null;
    }
}
