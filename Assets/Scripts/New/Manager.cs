using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameState gameState;

    void Awake()
    {
        Debug.Assert(gameState != null);
    }

    void Update()
    {
        
    }

    public bool ExecuteAction(IGameAction action)
    {
        /*if (!IsValidAction(action))
            return false;*/

        action.Execute(gameState);
        return true;
    }
    
}
