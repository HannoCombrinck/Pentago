using UnityEngine;

// Initialize and manage all quadrant visuals (i.e. GameObjects with Quadrant component attached) that 
// are descendants of this GameObject.
public class QuadrantManager : SpatialSorter<Quadrant>
{
    [Tooltip("TEMPORARY TO BE REMOVED")]
    public Game game;

    protected override void Awake()
    {
        base.Awake();

        Debug.Assert(game.state != null);

        for (int i = 0; i < sortedSpatials.Count; i++)
        {
            sortedSpatials[i].quadrantIndex = i;
            sortedSpatials[i].game = game;
        }
    }
}
