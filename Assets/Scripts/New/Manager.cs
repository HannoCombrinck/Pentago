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

    
}
