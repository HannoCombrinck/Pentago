using System;
using System.Collections;
using System.Collections.Generic;
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
    [Tooltip("Prefab to use as preivew placement of Player 1's marble.")]
    public GameObject player1MarblePreviewPrefab;
    [Tooltip("Prefab to use as Player 2's marble.")]
    public GameObject player2MarblePrefab;
    [Tooltip("Prefab to use as preivew placement of Player 2's marble.")]
    public GameObject player2MarblePreviewPrefab;
    [Tooltip("Marble visual offset from space. In other words, the marble's position will be the space's position + marbleVisualOffset.")]
    public Vector3 marbleVisualOffset = Vector3.up * 0.3f;

    // Board events
    // {
    public Action onIllegalMarblePlacement;
    public Action onIllegalQuadrantRotation;
    // }

    private bool actionInProgress = false;
    //private bool gameInProgress = false;
    private bool gameInProgress = true;
    private GameObject player1MarblePreview;
    private GameObject player2MarblePreview;
    private GameObject currentMarblePreview;
    private QuadrantController quadrants;
    private SpaceController spaces;

    private void Awake()
    {
        Debug.Assert(game != null, "Game component required.");
        Debug.Assert(player1MarblePrefab != null, "Prefab required.");
        Debug.Assert(player1MarblePreviewPrefab != null, "Prefab required.");
        Debug.Assert(player2MarblePrefab != null, "Prefab required.");
        Debug.Assert(player2MarblePreviewPrefab != null, "Prefab required.");

        player1MarblePreview = Instantiate(player1MarblePreviewPrefab);
        player1MarblePreview.SetActive(false);
        player2MarblePreview = Instantiate(player2MarblePreviewPrefab);
        player2MarblePreview.SetActive(false);

        quadrants = GetComponent<QuadrantController>();
        spaces = GetComponent<SpaceController>();

        game.onNewGameStarted += OnGameStarted;
        game.onActionExecuted += ApplyGameStateToVisuals;
        game.onGameWon += OnGameEnded;
    }

    private void Start()
    {
        ApplyGameStateToVisuals();
    }

    public void OnGameStarted()
    {
        gameInProgress = true;
        ApplyGameStateToVisuals();
    }

    public void OnGameEnded()
    {
//      Debug.Log("Game ended: " + game.GetState().winState.ToString());
        gameInProgress = false;

        if (game.GetState().winState == WIN_STATE.PLAYER1_WON || game.GetState().winState == WIN_STATE.PLAYER2_WON)
            StartCoroutine(HighlightWinningLine(game.GetState().winningLine));
    }

    // Attempt to visually place a marble in the game world and execute a ActionPlaceMarble action on the Game state.
    public void PlaceMarble(int spaceIndex)
    {
        if (!gameInProgress)
            return;

        if (actionInProgress)
            return;

        StartCoroutine(PlaceMarbleCoroutine(spaceIndex));
    }

    // Show a visual preview of what it would look like if a marble were placed.
    public void PlaceMarbleShowPreview(int spaceIndex)
    {
        if (!gameInProgress)
            return;

        currentMarblePreview?.SetActive(false);
        currentMarblePreview = game.GetState().currentPlayer == PLAYER.PLAYER1 ? player1MarblePreview : player2MarblePreview;
        currentMarblePreview.transform.position = spaces[spaceIndex].transform.position + marbleVisualOffset;
        currentMarblePreview.SetActive(true);
    }

    // Hide the visual preview of a marble placement.
    public void PlaceMarbleHidePreview()
    {
        currentMarblePreview?.SetActive(false);
    }

    // Attempt to visually rotate the quadrant in the game world and execute a ActionRotateQuadrant action on the Game.
    public void RotateQuadrant(int quadrantIndex, ROTATE_DIRECTION direction)
    {
        if (!gameInProgress)
            return;

        if (actionInProgress)
            return;

        StartCoroutine(RotateQuadrantCoroutine(quadrantIndex, direction));
    }

    private struct HighlightedMarble
    {
        public Transform currentTransform;
        public Vector3 originalPosition;
        public Vector3 originalScale;
        public Material material;
    };

    private IEnumerator HighlightWinningLine(int[] winningLine)
    {
        List<HighlightedMarble> winningMarbleTransforms = new List<HighlightedMarble>();
        foreach (var i in winningLine)
        {
            spaces[i].GetMarble().GetComponent<Renderer>().material = Instantiate(spaces[i].GetMarble().GetComponent<Renderer>().material);
            var m = new HighlightedMarble();
            m.currentTransform = spaces[i].GetMarble().transform;
            m.originalPosition = spaces[i].GetMarble().transform.position;
            m.originalScale = spaces[i].GetMarble().transform.localScale;
            m.material = spaces[i].GetMarble().GetComponent<Renderer>().material;
            winningMarbleTransforms.Add(m);
        }

        float time = 0.0f;
        float scaleFactor = 1.0f;
        while (!gameInProgress)
        {
            scaleFactor = Mathf.Sin(time*4.0f) * 0.2f + 1.0f;

            foreach (var m in winningMarbleTransforms)
            {
                m.currentTransform.localScale = m.originalScale * scaleFactor;
                m.material.SetColor("_Color", Color.white);
                m.material.SetVector("_EmissionColor", Color.white * 1.5f);
            }

            time += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator PlaceMarbleCoroutine(int spaceIndex)
    {
        actionInProgress = true;

        var action = new ActionPlaceMarble(spaceIndex);

        if (!action.IsValid(game.GetState()))
        {
            Debug.Log(game.GetState().currentPlayer.ToString() + " attempted to illegaly place a marble.");
            onIllegalMarblePlacement?.Invoke();
            actionInProgress = false;
            yield break;
        }

        yield return StartCoroutine(PlaceMarbleVisual(spaceIndex));

        game.ExecuteAction(action);

        actionInProgress = false;
    }

    private IEnumerator PlaceMarbleVisual(int spaceIndex)
    {
        var space = spaces[spaceIndex];
        var marblePrefab = game.GetState().currentPlayer == PLAYER.PLAYER1 ? player1MarblePrefab : player2MarblePrefab;
        //////////
        // For now just instantiate the marble at its destination position.
        // Marble can be instantiated somewhere else and animated to its destination position on the Space.
        var marble = Instantiate(marblePrefab, space.transform.position + marbleVisualOffset, Quaternion.identity);
        //////////
        space.AddMarble(game.GetState().currentPlayer, marble);
        yield return null;
    }

    private IEnumerator RotateQuadrantCoroutine(int quadrantIndex, ROTATE_DIRECTION direction)
    {
        actionInProgress = true;

        var action = new ActionRotateQuadrant(quadrantIndex, direction);

        if (!action.IsValid(game.GetState()))
        {
            Debug.Log(game.GetState().currentPlayer.ToString() + " attempted to illegaly rotate a quadrant.");
            onIllegalQuadrantRotation?.Invoke();
            actionInProgress = false;
            yield break;
        }

        yield return StartCoroutine(RotateQuadrantVisual(quadrantIndex, direction));

        game.ExecuteAction(action);

        actionInProgress = false;
    }

    // Visually roatate quadrant 
    private IEnumerator RotateQuadrantVisual(int quadrantIndex, ROTATE_DIRECTION direction)
    {
        var quadrantRotator = quadrants[quadrantIndex].GetComponent<IQuadrantRotator>();

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

    // Change the visuals to represent the game state as stored in Game state.
    private void ApplyGameStateToVisuals()
    {
        spaces.UpdateAll(game.GetState());
    }
}
