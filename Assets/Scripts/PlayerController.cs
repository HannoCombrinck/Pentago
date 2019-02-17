using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The PlayerController encapsulates the player's will/intention and actions and communicates them directly to other game objects and systems.
// This means that all input (mouse and keyboard) is handled, interpreted and forwarded from here.
public class PlayerController : MonoBehaviour
{
    public BoardController boardController;
    public CameraController cameraController;
    public LayerMask clickableLayer;

    delegate void UpdateState();
    UpdateState updateState;
    
    private RaycastHit mouseRayHitInfo;

    void Start()
    {
        Debug.Assert(boardController);
        Debug.Assert(cameraController);

        // Set initial update state
        updateState = new UpdateState(StateHoverEmpty);
    }

    void Update()
    {
        // Run update for current input state
        //updateState();
    }

    // Cast a ray to determine of mouse is hovering over a clickable game object
    private bool IsMouseOverClickable()
    {
        Ray camRay = cameraController.GetCamera().ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(camRay, out mouseRayHitInfo, 100f, clickableLayer);
    }

    // Hovering mouse over empty space
    void StateHoverEmpty()
    {
        if (IsMouseOverClickable())
        {
            updateState = StateHoverClickable;
            return;
        }

        if (Input.GetMouseButton(0)) // Holding down left mouse button
            updateState = StateMovingCamera;

        if (Input.GetMouseButton(1)) // Holding down right mouse button
            updateState = StateZoomingCamera;
    }

    // Hovering mouse over a clickable object
    void StateHoverClickable()
    {
        if (!IsMouseOverClickable()) // Mouse moved away from clickable
        {
            updateState = StateHoverEmpty;
            boardController.PlaceMarblePreview(null);
            return;
        }

        var hitGo = mouseRayHitInfo.collider.gameObject;
        if (hitGo.GetComponent<Clickable>())
        {
            if (hitGo.transform.childCount > 0)
                boardController.PlaceMarblePreview(null);
            else
                boardController.PlaceMarblePreview(hitGo);
        }

        if (Input.GetMouseButtonDown(0)) // Left click on clickable
        {
            boardController.PlaceMarblePreview(null);
            var clickableObject = mouseRayHitInfo.collider.gameObject;
            if (clickableObject)
            {
                updateState = StateHoldingClickable;
                OnClick(clickableObject);
            }
        }
    }

    // Left mouse button held down on clickable 
    void StateHoldingClickable()
    {
        if (!Input.GetMouseButton(0)) // Released mouse button after clicking on clickable
            updateState = StateHoverEmpty;

        // TODO: Forward mouse movements to currently clicked clickable???
        //mouseRayHitInfo.collider.gameObject
    }

    // Left mouse button clicked on empty space and moving camera
    void StateMovingCamera()
    {
        if (!Input.GetMouseButton(0)) // Released left mouse button while moving camera
        {
            updateState = StateHoverEmpty;
            //cameraController.IgnoreCameraOrbit(true);
            return;
        }

        //cameraController.IgnoreCameraOrbit(false);
    }

    // Right mouse button clicked on empty space and zooming camera
    void StateZoomingCamera()
    {
        if (!Input.GetMouseButton(1)) // Released right mouse button while zooming camera
            updateState = StateHoverEmpty;
    }

    void OnClick(GameObject go)
    {
        Debug.Log("Player clicked on: " + go.name);

        var clickable = go.GetComponent<Clickable>();
        if (clickable)
            clickable.Clicked();

    }
}
