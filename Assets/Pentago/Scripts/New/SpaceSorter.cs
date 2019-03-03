using UnityEngine;

// Initialize and sort Space components with regards to their world space positions.
public class SpaceSorter : SpatialSorter<Space>
{
    public Pentago game;

    protected override void Awake()
    {
        base.Awake();

        Debug.Assert(game.state != null);
        Debug.Assert(game.settings != null);

        for (int i = 0; i < sortedSpatials.Count; i++)
        {
            sortedSpatials[i].spaceIndex = i;
            sortedSpatials[i].game = game;
        }
    }

    public void OnSpaceIndicesChanged()
    {
        Sort();
    }
}
