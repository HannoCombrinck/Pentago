
interface IEvaluator
{
    Game.WIN_STATE Evaluate(State gameState);
    int[] GetLastEvaluatedLine();
}
