
public interface IGameAction
{
    string GetDescription();
    void Execute(State gameState);
}
