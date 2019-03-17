using UnityEngine;

// Initialize and control interacitons with all descendant Quadrant's.
public class QuadrantController : SpatialSorter<Quadrant>
{
    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < sortedSpatials.Count; i++)
            sortedSpatials[i].quadrantIndex = i;
    }
}
