using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public IInputContext activeContext;
    [HideInInspector]
    public MousePointer mousePointer;
    [HideInInspector]
    public MouseMovement mouseMovement;

    private IInputContext contextToSwitchTo;

    void Awake()
    {
        Debug.Assert(activeContext != null);
        mousePointer = GetComponent<MousePointer>();
        Debug.Assert(mousePointer != null);
        mouseMovement = GetComponent<MouseMovement>();
        Debug.Assert(mouseMovement != null);
    }

    void Start()
    {
        // Handle switch to default input context
        contextToSwitchTo = activeContext;
        activeContext.OnEnter();
        activeContext.OnFirstUpdate();
    }

    void Update()
    {
        HandleContextSwitch();
        activeContext.OnHandleInput();
    }

    public void SwitchContext(IInputContext newContext)
    {
        // TODO: Keep track of previous context so SwitchContextToPrevious();
        activeContext.OnExit();
        newContext.OnEnter();
        contextToSwitchTo = newContext;
    }

    void HandleContextSwitch()
    {
        if (contextToSwitchTo == activeContext)
            return;

        activeContext.OnLastUpdate();
        activeContext = contextToSwitchTo;
        activeContext.OnFirstUpdate();
    }
}
