using UnityEngine;

[CreateAssetMenu]
public class GameState : ScriptableObject
{
    // 0 = game in progress 
    // 1 = player 1 won
    // 2 = player 2 won
    public int winState = 0;
    public int currentPlayer = 1;
    public byte[] boardState = new byte[36];
}
