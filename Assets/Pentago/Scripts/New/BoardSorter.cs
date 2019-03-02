using UnityEngine;

// Initialize and sort Board components with regards to their world space positions
public class BoardSorter : SpatialSorter<Board>
{
    public Pentago game;

    protected override void Awake()
    {
        base.Awake();

        Debug.Assert(game.state != null);
        Debug.Assert(game.settings != null);

        for (int i = 0; i < sortedSpatials.Count; i++)
        {
            sortedSpatials[i].boardIndex = i;
            sortedSpatials[i].game = game;
        }
    }
}
