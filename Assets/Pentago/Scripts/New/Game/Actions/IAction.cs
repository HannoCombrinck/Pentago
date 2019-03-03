
public interface IAction
{
    string GetDescription();
    void Execute(State gameState);
}
