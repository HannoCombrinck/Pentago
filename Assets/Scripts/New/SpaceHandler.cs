using System.Collections.Generic;
using UnityEngine;

public class SpaceHandler : MonoBehaviour
{
    Space[] spaces;

    void Awake()
    {
        spaces = GetComponentsInChildren<Space>();

        /*foreach (var s in spaces)
            Debug.Log("Space: " + s.gameObject.name);*/
    }

    void Update()
    {
        
    }
}
