using UnityEngine;

public abstract class InputContext : MonoBehaviour
{
    protected InputHandler handler;

    public void Enter(InputHandler h)
    {
        handler = h;
        OnEnter();
    }

    public void Exit()
    {
        OnExit();
    }

    public void HandleInput()
    {
        OnHandleInput();
    }

    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void OnHandleInput();
}
