
// An Action is a move that can be performed by a Player that will modify the game state (e.g. place marble, rotate quadrant).
public interface IAction
{
    string GetDescription();
    bool IsValid(State gameState);
    void Execute(State gameState);
}
