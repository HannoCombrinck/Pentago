using System.Collections.Generic;
using UnityEngine;

public class MatchLocal : MonoBehaviour
{
    public Match match;

    public void Begin()
    {
        match.Begin();
    }

    public void End()
    {
        Debug.Log("Local match ended");
        match.End();
        gameObject.SetActive(false);
    }

    
}
