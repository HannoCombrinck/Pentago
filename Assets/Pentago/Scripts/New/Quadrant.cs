using System.Collections;
using UnityEngine;

public class Quadrant : MonoBehaviour
{
    public int quadrantIndex;
    public Game game { get; set; }

    private IQuadrantRotator rotator;

    private void Awake()
    {
        rotator = GetComponent<IQuadrantRotator>();
        Debug.Assert(rotator != null);
    }
}
