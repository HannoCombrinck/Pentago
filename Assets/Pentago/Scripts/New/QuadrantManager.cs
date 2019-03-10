using UnityEngine;

// Initialize and manage all quadrant visuals (i.e. GameObjects with Quadrant component attached) that 
// are descendants of this GameObject.
public class QuadrantManager : SpatialSorter<Quadrant>
{
    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < sortedSpatials.Count; i++)
            sortedSpatials[i].quadrantIndex = i;
    }
}
