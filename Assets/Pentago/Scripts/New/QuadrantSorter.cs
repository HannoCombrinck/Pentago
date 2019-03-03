using UnityEngine;

// Initialize and sort Quadrant components with regards to their world space positions
public class QuadrantSorter : SpatialSorter<Quadrant>
{
    public Pentago game;

    protected override void Awake()
    {
        base.Awake();

        Debug.Assert(game.state != null);
        Debug.Assert(game.settings != null);

        for (int i = 0; i < sortedSpatials.Count; i++)
        {
            sortedSpatials[i].quadrantIndex = i;
            sortedSpatials[i].game = game;
        }
    }
}
