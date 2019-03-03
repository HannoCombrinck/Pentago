using UnityEngine;

// Initialize and manage all space visuals (i.e. GameObjects with Space component attached) that 
// are descendants of this GameObject.
public class SpaceManager : SpatialSorter<Space>
{
    [Tooltip("TEMPORARY TO BE REMOVED")]
    public Game game;

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
