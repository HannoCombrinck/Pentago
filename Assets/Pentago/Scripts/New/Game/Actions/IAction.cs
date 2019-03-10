
public interface IAction
{
    string GetDescription();
    bool IsValid(State gameState);
    void Execute(State gameState);
}
