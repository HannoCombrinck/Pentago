using System.Collections.Generic;
using UnityEngine;

public class SpaceHandler : MonoBehaviour
{
    public List<Space> sortedSpaces = new List<Space>();

    void Awake()
    {
        var xMin = float.MaxValue;
        var xMax = float.MinValue;

        var spaces = GetComponentsInChildren<Space>();
        foreach (var s in spaces)
        {
            sortedSpaces.Add(s);

            if (s.gameObject.transform.position.x < xMin)
                xMin = s.gameObject.transform.position.x;
            else if (s.gameObject.transform.position.x > xMax)
                xMax = s.gameObject.transform.position.x;
        }

        var xRange = (xMax - xMin) * 2.0f; // * 2.0f to prevent edge cases albeit excessive
        spaceComparer = new SpaceIndexComparer(xRange);

        FindBoardStateIndices();
    }

    public void FindBoardStateIndices()
    {
        sortedSpaces.Sort(spaceComparer);

        for (int i = 0; i < sortedSpaces.Count; i++)
            sortedSpaces[i].boardIndex = i;
    }

    // Comparer to sorts spaces from top left to bottom right based on world space positions in xz plane.
    private class SpaceIndexComparer : IComparer<Space>
    {
        float xRange;

        public SpaceIndexComparer(float xRange)
        {
            this.xRange = xRange;
        }

        public int Compare(Space x, Space y)
        {
            var xIndexOrder = x.gameObject.transform.position.x - x.gameObject.transform.position.z * xRange;
            var yIndexOrder = y.gameObject.transform.position.x - y.gameObject.transform.position.z * xRange;
            return xIndexOrder < yIndexOrder ? -1 : 1;
        }
    }
    private SpaceIndexComparer spaceComparer;
}
