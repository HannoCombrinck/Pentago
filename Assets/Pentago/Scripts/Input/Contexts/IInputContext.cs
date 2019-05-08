using UnityEngine;

// An InputContext handles the raw user input (mouse, keyboard etc.) in a given context/state of the game.
[RequireComponent(typeof(InputHandler))]
public abstract class IInputContext : MonoBehaviour
{
    protected InputHandler handler;

    protected virtual void Awake()
    {
        handler = GetComponent<InputHandler>();
    }

    // Called every Update() while the context is the active context.
    public abstract void OnHandleInput();
    // Called immediately when context switch is requested (directly after OnExit() of previously active context).
    public virtual void OnEnter() { }
    // Called immediately when context switch is requested (directly before OnEnter() of newly requested context).
    public virtual void OnExit() { }
    // Called at the beginning of the next Update() after context switch is requested.
    public virtual void OnFirstUpdate() { }
    // Called at the beginning of the next Update() after context switch is requested.
    public virtual void OnLastUpdate() { }
}
