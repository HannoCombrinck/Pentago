using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardState : MonoBehaviour
{
    byte[] boardState = new byte[36];

    void Awake()
    {
        for (int i = 0; i < boardState.Length; i++)
            boardState[i] = 0;
    }


}
