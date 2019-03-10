
interface IEvaluator
{
    CommonTypes.WIN_STATE Evaluate(State gameState);
    int[] GetWinningRow();
}
