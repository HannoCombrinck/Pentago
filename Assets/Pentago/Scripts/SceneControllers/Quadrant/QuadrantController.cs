using System.Collections.Generic;
using UnityEngine;
using static IGame;

// Initialize and control interacitons with all descendant Quadrant's.
public class QuadrantController : MonoBehaviour
{
    public List<Quadrant> sortedQuadrants;
    public Quadrant this[int key] => sortedQuadrants[key];
    public int Count => sortedQuadrants.Count;

    private SpatialSorter<Quadrant> quadrantSorter;

    private void Awake()
    {
        var quadrantsToSort = GetComponentsInChildren<Quadrant>();
        quadrantSorter = new SpatialSorter<Quadrant>(quadrantsToSort, ref sortedQuadrants);

        for (int i = 0; i < sortedQuadrants.Count; i++)
            sortedQuadrants[i].quadrantIndex = i;
    }
}
