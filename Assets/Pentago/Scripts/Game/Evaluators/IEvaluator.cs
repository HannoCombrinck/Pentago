
interface IEvaluator
{
    IGame.WIN_STATE Evaluate(State state);
    int[] GetLastEvaluatedLine();
}
