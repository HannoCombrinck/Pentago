using UnityEngine;

public class MatchHandler : MonoBehaviour
{
    public IMatchContext activeContext;

    private IMatchContext contextToSwitchTo;

    void Awake()
    {

    }
}
