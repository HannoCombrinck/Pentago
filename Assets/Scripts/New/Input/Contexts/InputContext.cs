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

    protected abstract void OnEnter();
    protected abstract void OnExit();
    protected abstract void OnHandleInput();
}
