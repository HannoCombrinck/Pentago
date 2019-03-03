using System.Collections;
using UnityEngine;

// Initialize and manage the board visual.
[RequireComponent(typeof(QuadrantManager), typeof(SpaceManager))]
public class BoardManager : MonoBehaviour
{
    [Tooltip("Reference to the Pentago game manager component.")]
    public Game game;

    private QuadrantManager quadrants;
    private SpaceManager spaces;

    private void Awake()
    {
        Debug.Assert(game != null, "Game component required.");
        quadrants = GetComponent<QuadrantManager>();
        spaces = GetComponent<SpaceManager>();
    }

    private void Update()
    {
        
    }

    // Change the visuals to represent the game state as stored in Game.state.
    public void ApplyGameState()
    {
        // TODO

    }

    // Visually places a marble in the game world and executes a ActionPlaceMarble action on the Game.
    public void PlaceMarble(int spaceIndex)
    {
        // Update visuals to represent action visually
        // TODO
        var space = spaces.Get(spaceIndex);

        // Execute action to update abstract game representation
        game.ExecuteAction(new ActionPlaceMarble(spaceIndex));
    }

    // Visually rotates the quadrant in the game world and executes a ActionRotateQuadrant action on the Game.
    public void RotateQuadrant(int quadrantIndex, CommonTypes.ROTATE_DIRECTION direction)
    {
        // Update visuals to represent action visually
        // TODO run coroutine to do animation first then execute the game action
        var quadrant = quadrants.Get(quadrantIndex);
        //StartCoroutine(AnimateQuadrantThenExecuteAction(quadrant, direction));

        // Execute action to update abstract game representation
        game.ExecuteAction(new ActionRotateQuadrant(quadrantIndex, direction));
    }

    private IEnumerator AnimateQuadrantThenExecuteAction(Quadrant quadrant, CommonTypes.ROTATE_DIRECTION direction)
    {
        yield return null;

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


        game.ExecuteAction(new ActionRotateQuadrant(quadrantIndex, direction));*/
    }
}
