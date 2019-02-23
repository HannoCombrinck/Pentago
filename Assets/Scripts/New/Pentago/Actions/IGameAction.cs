
public interface IGameAction
{
    string GetDescription();
    void Execute(GameState gameState);
}
