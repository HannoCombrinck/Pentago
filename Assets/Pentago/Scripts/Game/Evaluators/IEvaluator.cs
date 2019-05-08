
// An evaluator's purpose is to determine whether a player has won the game 
// and what the winning line indices are.
public interface IEvaluator
{
    IGame.WIN_STATE Evaluate(State state);
    int[] GetLastEvaluatedLine();
}
